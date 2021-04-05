using CamundaClient;
using CamundaClient.Dto;

using Newtonsoft.Json;
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
            _context = context;

            camunda = new CamundaEngineClient(new Uri("http://localhost:8080/engine-rest/engine/default/"), null, null);
        }
        private readonly ApplicationDbContext _context;

        public Dictionary<string, string> StartProcess(string processName)
        {
            string deploymentId = DeployModel(processName); //определяем выполняемую модель
            RegisterWorker();

            // start some instances:
            string processInstanceId = camunda.BpmnWorkflowService.StartProcessInstance(processName, new Dictionary<string, object>()
                    {
                        {"militaryDoc", "true" },
                        {"man", "true" },
                        {"emp", "false" }
                        //{"test", "false" }
                    });
            var definitions = camunda.RepositoryService.LoadProcessDefinitions(true);
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

        public void RegisterWorker()
        {
            registerWorkers("createContract", externalTask =>
            {
                camunda.ExternalTaskService.Complete(workerId, externalTask.Id);
            });
            registerWorkers("printContract", externalTask =>
            {
                camunda.ExternalTaskService.Complete(workerId, externalTask.Id);
            });
            StartPolling();
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
        public void StopProcess()
        {
            //camunda.Shutdown();
            //camunda.RepositoryService.DeleteDeployment(deploymentId);
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
                    res.Add(new InstancesModel()
                    {
                        processInstanceId = i.Id,
                        processName = def.Name
                    });
                }
            }
           
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
            var def = camunda.RepositoryService.LoadProcessDefinitionXml(processDefinitionId);

            return def;
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
            camunda.HumanTaskService.Complete(taskId, new Dictionary<string, object>() { });
        }
    }


    public class InstancesModel
    {
        public string processInstanceId { get; set; } 

        public string processName { get; set; } 
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
