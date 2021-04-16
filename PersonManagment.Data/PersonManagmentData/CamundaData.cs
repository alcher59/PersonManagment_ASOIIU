using CamundaClient;
using CamundaClient.Dto;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PersonManagment.Data.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using static PersonManagment.Data.PersonManagmentData.EmployeeData;
using static System.Net.WebRequestMethods;

namespace PersonManagment.Data.PersonManagmentData
{
    public class CamundaData
    {
        private static int pollingIntervalInMilliseconds = 5;
        private static int pollingNumberOfTasks = 100;
        private static int pollingLockTimeInMs = 5 * 60 * 1000;
        private static int pollingMaxDegreeOfParallelism = 1;
        private static Timer pollingTimer;

        private static string workerId = "worker1";
        private static IDictionary<string, Action<ExternalTask>> workers = new Dictionary<string, Action<ExternalTask>>();

        public CamundaEngineClient camunda;
        public CamundaData(ApplicationDbContext context)
        {
            camunda = new CamundaEngineClient(new Uri("http://localhost:8080/engine-rest/engine/default/"), null, null);
        }

        public Dictionary<string, string> StartProcess(ProcessModel data)
        {
            string deploymentId = DeployModel(data.processName); //определяем выполняемую модель

            var instances = (List<ProcessInstance>)camunda.BpmnWorkflowService.LoadProcessInstances(new Dictionary<string, string>() {
                        { "processDefinitionKey", data.processName }
                });

            if(instances.Count == 0) RegisterWorker(data);

            // start some instances:
            string processInstanceId = camunda.BpmnWorkflowService.StartProcessInstance(data.processName, data.variables);

            return new Dictionary<string, string>()
                    {
                        {"deploymentId", deploymentId },
                        {"processInstanceId", processInstanceId }
                    };
        }

        public string DeployModel(string processName)
        {
            if (processName != null)
            {
                return camunda.RepositoryService.Deploy(processName, new List<object> {
                FileParameter.FromManifestResource(Assembly.GetExecutingAssembly(), $"PersonManagment.Data.BPMN.{processName}.bpmn") });
            }
            else return string.Empty;
        }

        public void RegisterWorker(ProcessModel data)
        {
            if (data.topicNames != null)
            {
                foreach (var name in data.topicNames)
                {
                    registerWorkers(name, externalTask =>
                    {
                        camunda.ExternalTaskService.Complete(workerId, externalTask.Id);
                    });
                }
                StartPolling();
            }
        }

        public void StartPolling()
        {
            pollingTimer = new Timer(_ => PollTasks(), null, pollingIntervalInMilliseconds, Timeout.Infinite);
        }

        public void PollTasks()
        {
            var tasks = camunda.ExternalTaskService.FetchAndLockTasks(workerId, pollingNumberOfTasks, workers.Keys, pollingLockTimeInMs, null);
            Parallel.ForEach(
                tasks,
                new ParallelOptions { MaxDegreeOfParallelism = pollingMaxDegreeOfParallelism },
                (externalTask) =>
                {
                    workers[externalTask.TopicName](externalTask);
                });

            pollingTimer.Change(pollingIntervalInMilliseconds, Timeout.Infinite);
        }



        public void registerWorkers(string topicName, Action<ExternalTask> workerFunction)
        {
            workers.Add(topicName, workerFunction);
        }
        public void StopProcess(string processInstanceId)
        {
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:8080/engine-rest/process-instance/{processInstanceId}");
            getRequest.Method = "DELETE";
            var getResponse = (HttpWebResponse)getRequest.GetResponse();
        }

        public IEnumerable<InstancesModel> GetProcessInstances()
        {
            var definitions = camunda.RepositoryService.LoadProcessDefinitions(true);

            var instances = new List<ProcessInstance>();
            var res = new List<InstancesModel>();

            foreach (var def in definitions)
            {
                instances = (List<ProcessInstance>)camunda.BpmnWorkflowService.LoadProcessInstances(new Dictionary<string, string>() {
                        { "processDefinitionId", def.Id }
                });

                foreach (var i in instances)
                {
                    //var vars = JObject.Parse(GetProcessVariables(i.Id))[0];

                    res.Add(new InstancesModel()
                    {
                        processInstanceId = i.Id,
                        processName = def.Name,
                        //description = vars != null ? "employeeId: " + Convert.ToString(JObject.Parse(vars.ToString())["value"]) : string.Empty
                        description = GetProcessVariables(i.Id)
                    });
                }
            }
           
            return res;
        }

        public object GetProcessVariables(string processInstanceId)
        {
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:8080/engine-rest/process-instance/{processInstanceId}/variables");
            getRequest.Method = Http.Get;
            var getResponse = (HttpWebResponse)getRequest.GetResponse();
            StreamReader sr = new StreamReader(getResponse.GetResponseStream());
            var res = JsonConvert.DeserializeObject(sr.ReadToEnd());
            return res;

        }

        public object GetProcessInstance(string processInstanceId)
        {
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:8080/engine-rest/history/process-instance/{processInstanceId}");
            getRequest.Method = Http.Get;
            var getResponse = (HttpWebResponse)getRequest.GetResponse();
            StreamReader sr = new StreamReader(getResponse.GetResponseStream());
            return JsonConvert.DeserializeObject(sr.ReadToEnd());
        }

        public string GetProcessInstanceXML(string processDefinitionId)
        {
            return camunda.RepositoryService.LoadProcessDefinitionXml(processDefinitionId);
        }

        public IEnumerable<TaskModel> GetUserTasks(string processInstanceId)
        {
            var res = new List<TaskModel>();

            var tasks = camunda.HumanTaskService.LoadTasks(new Dictionary<string, string>() {
                        { "processInstanceId", processInstanceId }
                    });
            
            foreach(var task in tasks)
            {
                res.Add(new TaskModel()
                {
                    taskId = task.Id,
                    taskName = task.Name,
                    processInstanceId = task.ProcessInstanceId
                });
            }

            return res;
        }

        public void CompleteUserTask(string taskId)
        {
            //var tasks = camunda.HumanTaskService.LoadTasks();
            //var taskId = string.Empty;
            //for (int i = 0; i < tasks.Count; i++)
            //{
            //    if (tasks[i].Name == taskInfo.taskName && tasks[i].ProcessInstanceId == taskInfo.processInstanceId)
            //    {
            //        taskId = tasks[i].Id;
            //    }
            //}
            //var variables = camunda.HumanTaskService.LoadVariables(taskId);
            //camunda.HumanTaskService.Complete(taskId, new Dictionary<string, object>() { });
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://localhost:8080/engine-rest/task/{taskId}/complete");
            request.Method = Http.Post;
            request.ContentType = "application/json";
            request.GetResponse();
            //StreamReader sr = new StreamReader(getResponse.GetResponseStream());
            //JsonConvert.DeserializeObject(sr.ReadToEnd());
        }
    }

    public class ProcessModel
    {
        public string processName { get; set; }
        public string[] topicNames { get; set; }
        public Dictionary<string, object> variables { get; set; }
    }
    public class InstancesModel
    {
        public string processInstanceId { get; set; } 
        public string processName { get; set; }
        public object description { get; set; } //ид сотрудника
    }

    public class Variables
    {
        public string varName { get; set; }
        public VariablesInfo info { get; set; }
    }

    public class VariablesInfo
    {
        public string value { get; set; }
        public string type { get; set; }
        public object valueInfo { get; set; }
    }
    public class TaskModel
    {
        public string processInstanceId { get; set; }
        public string taskName { get; set; }
        public string taskId { get; set; }
        public VariablesModel[] variables { get; set; }

    }

    public class VariablesModel
    {
        public string key { get; set; }

        public object value { get; set; }
    }
}
