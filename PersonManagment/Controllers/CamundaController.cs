using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamundaClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.PersonManagmentData;

namespace PersonManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamundaController : ControllerBase
    {
        private readonly CamundaData _repository;
        //private CamundaEngineClient camunda;
        public CamundaController(ApplicationDbContext context)
        {
            _repository = new CamundaData(context);

            //camunda = new CamundaEngineClient(new Uri("http://localhost:8080/engine-rest/engine/default/"), null, null);
        }

        [HttpPost]
        [Route("DeployAndStartWorkers")]
        public IActionResult DeployAndStartWorkers(string processDefinitionKey)
        {
            try
            {
                _repository.DeployModel();
                _repository.RegisterWorker();

                // start some instances:
                string processInstanceId = _repository.camunda.BpmnWorkflowService.StartProcessInstance(processDefinitionKey, new Dictionary<string, object>()
                    {
                        {"someData", "..." }
                    });
                //Console.WriteLine($"Started {processDefinitionKey} {processInstanceId}");

                return Ok(processInstanceId);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        [Route("StopTaskWorkers")]
        public IActionResult StopTaskWorkers()
        {
            try
            {
                _repository.Shutdown(); // Stop Task Workers

                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> SubcribeTopic(string topic)
        //{
        //    try
        //    {

        //        return Ok();
        //    }
        //    catch (Exception error)
        //    {
        //        return BadRequest(error);
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> CompleteTask(string processId, string taskId)
        //{
        //    try
        //    {
               

        //        return Ok();
        //    }
        //    catch (Exception error)
        //    {
        //        return BadRequest(error);
        //    }
        //}

       
    }
}
