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
        public CamundaController(ApplicationDbContext context)
        {
            _repository = new CamundaData(context);
        }

        [HttpPost]
        [Route("StartProcess")]
        public IActionResult StartProcess(ProcessModel processModel)
        {
            try
            {
                var res = _repository.StartProcess(processModel);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        [Route("RegisterWorker")]
        public IActionResult RegisterWorker(ProcessModel data)
        {
            try
            {
                _repository.RegisterWorker(data);

                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("GetProcessInstances")]
        [HttpGet]
        public IActionResult GetProcessInstances()
        {
            try
            {
                var res = _repository.GetProcessInstances();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("GetProcessInstance")]
        [HttpGet]
        public IActionResult GetProcessInstance(string processInstanceId)
        {
            try
            {
                var res = _repository.GetProcessInstance(processInstanceId);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("GetProcessVariables")]
        [HttpGet]
        public IActionResult GetProcessVariables(string processInstanceId)
        {
            try
            {
                var res = _repository.GetProcessVariables(processInstanceId);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("GetProcessInstanceXML")]
        [HttpGet]
        public IActionResult GetProcessInstanceXML(string processDefinitionId)
        {
            try
            {
                var res = _repository.GetProcessInstanceXML(processDefinitionId);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("GetUserTasks")]
        [HttpGet]
        public IActionResult GetUserTasks(string processInstanceId)
        {
            try
            {
                var res = _repository.GetUserTasks(processInstanceId);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        [Route("CompleteUserTask")]
        public IActionResult CompleteUserTask(string taskId)
        {
            try
            {
                _repository.CompleteUserTask(taskId);

                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpDelete]
        [Route("StopProcess")]
        public IActionResult StopProcess(string processInstanceId)
        {
            try
            {
                _repository.StopProcess(processInstanceId);

                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

       


    }
}
