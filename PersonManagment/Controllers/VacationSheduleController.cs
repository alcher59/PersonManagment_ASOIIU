using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;
using PersonManagment.Data.PersonManagmentData;

namespace PersonManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationSheduleController : ControllerBase
    {
        private readonly VacationSheduleData _repository;
        public VacationSheduleController(ApplicationDbContext context)
        {
            _repository = new VacationSheduleData(context);
        }

        [HttpGet]
        public IActionResult GetVacationShedule()
        {
            try
            {
                var res = _repository.GetVacationShedule();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetVacationSheduleById(int id)
        {
            try
            {
                var res = _repository.GetVacationSheduleById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpDelete]
        public IActionResult DeleteVacationShedule(int id)
        {
            try
            {
                var res = _repository.DeleteVacationShedule(id);
                return Ok(res);
               
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutVacationShedule(int id, VacationShedule model)
        {
            try
            {
                var res = _repository.UpdateVacationShedule(id, model);
                return Ok(res);

            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        public IActionResult PostVacationShedule(VacationShedule model)
        {
            try
            {
                var res = _repository.AddVacationShedule(model);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Перенос отпуска
        /// </summary>
        [Route("TransferVacation/{id}")]
        [HttpPost]
        public IActionResult TransferVacation(int id, TransferVacationShedule model)
        {
            try
            {
                var res = _repository.TransferVacation(id, model);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
