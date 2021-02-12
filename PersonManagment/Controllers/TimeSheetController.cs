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
    public class TimeSheetController : ControllerBase
    {
        private readonly TimeSheetData _repository;
        public TimeSheetController(ApplicationDbContext context)
        {
            _repository = new TimeSheetData(context);
        }

        [HttpGet]
        public IActionResult GetTimeSheet()
        {
            try
            {
                var res = _repository.GetTimeSheet();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetTimeSheetById(int id)
        {
            try
            {
                var res = _repository.GetTimeSheetById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpPost]
        public IActionResult PostTimeSheet(TimeSheet data)
        {
            try
            {
                var res = _repository.AddTimeSheet(data);

                if (res == -1)
                {
                    return Conflict(409);
                }

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpPut("{id}")]
        public IActionResult PutTimeSheet(int id, [FromBody] TimeSheet data)
        {
            try
            {
                var res = _repository.UpdateTimeSheet(id, data);

                if (!res)
                {
                    return Conflict(409);
                }

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteTimeSheet(int id)
        {
            try
            {
                var res = _repository.DeleteTimeSheetById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        

        /// <summary>
        /// Метод получения табеля за месяц в виде матрицы
        /// </summary>   
        [HttpGet]
        [Route("TimeSheetMonth/{date}")]
        public IActionResult GetTimeSheetMonth(int date)
        {
            try
            {
                var res = _repository.GetTimeSheetMonth(date);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
