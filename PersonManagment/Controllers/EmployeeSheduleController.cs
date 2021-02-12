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
    public class EmployeeSheduleController : ControllerBase
    {
        private readonly EmployeeSheduleData _repository;
        public EmployeeSheduleController(ApplicationDbContext context)
        {
            _repository = new EmployeeSheduleData(context);
        }

        /// <summary>
        /// экспорт табеля в Excel
        /// </summary>   
        [HttpGet]
        [Route("TimeSheetDoc/{date}")]
        public IActionResult GetTimeSheetDoc(int date)
        {
            try
            {
               var res = _repository.GenerateTimeSheetDoc(date);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
