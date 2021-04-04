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
        public IActionResult StartProcess(string processName)
        {
            try
            {
                var res = _repository.StartProcess(processName);

                return Ok(res);
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
        public IActionResult CompleteUserTask(TaskModel taskModel)
        {
            try
            {
                _repository.CompleteUserTask(taskModel);

                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        [Route("StopProcess")]
        public IActionResult StopProcess()
        {
            try
            {
                _repository.StopProcess(); // Stop Task Workers

                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

       


    }
}
