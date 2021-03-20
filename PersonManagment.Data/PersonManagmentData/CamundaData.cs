using CamundaClient;
using CamundaClient.Dto;
using PersonManagment.Data.DataModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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

        public void DeployModel()
        {
            camunda.RepositoryService.Deploy("Recruitment", new List<object> {
                FileParameter.FromManifestResource(Assembly.GetExecutingAssembly(), "PersonManagment.Data.BPMN.Recruitment.bpmn") });
        }

        public void RegisterWorker()
        {
            registerWorker("GetEmployees", externalTask => {
                //Console.WriteLine("Get employees now..."); // e.g. by calling a REST endpoint
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
                (externalTask) => {
                    workers[externalTask.TopicName](externalTask);
                });

            // schedule next run
            pollingTimer.Change(pollingIntervalInMilliseconds, Timeout.Infinite);
        }

        public void registerWorker(string topicName, Action<ExternalTask> workerFunction)
        {
            workers.Add(topicName, workerFunction);
        }
        public void Shutdown()
        {
            camunda.Shutdown();
        }
    }
}
