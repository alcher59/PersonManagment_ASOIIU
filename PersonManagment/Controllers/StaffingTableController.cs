using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonManagment.Data.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PersonManagment.Data.Models;
using PersonManagment.Data.PersonManagmentData;

namespace PersonManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffingTableController : ControllerBase
    {
        private readonly StaffingTableData _repository;

        public StaffingTableController(ApplicationDbContext context)
        {
            _repository = new StaffingTableData(context);
        }

        [HttpGet]
        public IActionResult GetStaffingTable()
        {
            try
            {
                var res = _repository.GetStaffingTable();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStaffingTableById(int id)
        {
            try
            {
                var res = _repository.GetStaffingTableById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        public IActionResult PostStaffingTable(StaffingTable data)
        {
            try
            {
                var res = _repository.AddStaffingTable(data);

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
        public IActionResult PutStaffingTable(int id, [FromBody] StaffingTable data)
        {
            try
            {
                var res = _repository.UpdateStaffingTable(id, data);

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
        public IActionResult DeleteStaffingTable(int id)
        {
            try
            {
                var res = _repository.DeleteStaffingTableById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Shedule")]
        [HttpGet]
        public IActionResult GetShedule()
        {
            try
            {
                var res = _repository.GetShedule();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Shedule/{id}")]
        [HttpGet]
        public IActionResult GetSheduleById(int id)
        {
            try
            {
                var res = _repository.GetSheduleById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Shedule")]
        [HttpPost]
        public IActionResult PostShedule(Shedule data)
        {
            try
            {
                var res = _repository.AddShedule(data);

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

        [Route("Shedule/{id}")]
        [HttpPut]
        public IActionResult PutShedule(int id, [FromBody] Shedule data)
        {
            try
            {
                var res = _repository.UpdateShedule(id, data);

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

        [Route("Shedule/{id}")]
        [HttpDelete]
        public IActionResult DeleteShedule(int id)
        {
            try
            {
                var res = _repository.DeleteSheduleById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("StaffingApproval/{acceptEmployeeId}")]
        [HttpPost]
        public IActionResult StaffingApproval(int acceptEmployeeId)
        {
            try
            {
                var res = _repository.ApproveStaffingTable(acceptEmployeeId);

                if (res == 0)
                {
                    return Conflict("Нет позиций для утверждения");
                }

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Сохранить документ о штатном расписании
        /// </summary>   
        [HttpPost]
        [Route("SaveStaffingTable")]
        public IActionResult SaveStaffingTable()
        {
            try
            {
                var res = _repository.GenerateStaffingTable();
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Authorize(Roles = "Leader")]
        [Route("AcceptShedule/{employeeId}")]
        [HttpGet]
        public IActionResult AcceptShedule(int employeeId)
        {
            try
            {
                var result = _repository.ApproveStaffingTable(employeeId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
