using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;
using PersonManagment.Data.PersonManagmentData;
using System;


namespace PersonManagment.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RecruitmentController : ControllerBase
    {
        private readonly RecruitmentData _repository;
       

        public RecruitmentController(ApplicationDbContext context)
        {
            _repository = new RecruitmentData(context);
            
        }

        [HttpGet]
        public IActionResult GetRecruitment()
        {
            try
            {
                var res = _repository.GetRecruitment();


                
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetRecruitmentById(int id)
        {
            try
            {
                var res = _repository.GetRecruitmentById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        public IActionResult PostRecruitment(RecruitmentInfoDataModel data)
        {
            try
            {
                var res = _repository.AddRecruitment(data);

                if (res == -1)
                {
                    return Conflict("Такого сотрудника не существует!");
                }

                if (res == -3)
                {
                    return Conflict("Сотрудник еще не уволен!");
                }

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutRecruitment(int id, [FromBody] RecruitmentInfoDataModel data)
        {
            try
            {
                var res = _repository.UpdateRecruitment(id, data);

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
        public IActionResult DeleteRecruitment(int id)
        {
            try
            {
                var res = _repository.DeleteRecruitmentById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Dismissal")]
        [HttpGet]
        public IActionResult GetDismissal()
        {
            try
            {
                var res = _repository.GetDismissal();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Dismissal/{id}")]
        [HttpGet]
        public IActionResult GetDismissal(int id)
        {
            try
            {
                var res = _repository.GetDismissalById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Dismissal")]
        [HttpPost]
        public IActionResult PostDismissal(Dismissal data)
        {
            try
            {
                var res = _repository.AddDismissal(data);

                if (res == -1)
                {
                    return Conflict("Сотрудник уже был уволен!");
                }

                if (res == -2)
                {
                    return Conflict("Сотрудник еще не был принят!");
                }

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Dismissal/{id}")]
        [HttpPut]
        public IActionResult PutDismissal(int id, [FromBody] Dismissal data)
        {
            try
            {
                var res = _repository.UpdateDismissal(id, data);

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

        [Route("Dismissal/{id}")]
        [HttpDelete]
        public IActionResult DeleteDismissal(int id)
        {
            try
            {
                var res = _repository.DeleteDismissalById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        [Route("EmployeeTransfer")]
        public IActionResult EmployeeTransfer(Recruitment data)
        {
            try
            {
                var res = _repository.EmployeeTransfer(data);

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

        [HttpGet]
        [Route("GetTransferData/{employeeId}")]
        public IActionResult GetTransferData(int employeeId)
        {
            try
            {
                var res = _repository.GetTransferData(employeeId);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Contract")]
        [HttpGet]
        public IActionResult GetContract()
        {
            try
            {
                var res = _repository.GetContract();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Contract/{id}")]
        [HttpGet]
        public IActionResult GetContract(int id)
        {
            try
            {
                var res = _repository.GetContractById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Contract")]
        [HttpPost]
        public IActionResult PostContract(Data.DataModel.Contract data)
        {
            try
            {
                var res = _repository.AddContract(data);

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

        [Route("Contract/{id}")]
        [HttpPut]
        public IActionResult PutContract(int id, [FromBody] Data.DataModel.Contract data)
        {
            try
            {
                var res = _repository.UpdateContract(id, data);

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

        [Route("Contract/{id}")]
        [HttpDelete]
        public IActionResult DeleteContract(int id)
        {
            try
            {
                var res = _repository.DeleteContractById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("FOT")]
        [HttpGet]
        public IActionResult GetFOT()
        {
            try
            {
                var res = _repository.GetFOT();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("FOT/{id}")]
        [HttpGet]
        public IActionResult GetFOTById(int id)
        {
            try
            {
                var res = _repository.GetFOTById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("FOT")]
        [HttpPost]
        public IActionResult PostFOT(FOT data)
        {
            try
            {
                var res = _repository.AddFOT(data);

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
        [Route("FOT/{id}")]
        [HttpPut]
        public IActionResult PutFOT(int id, [FromBody] FOT data)
        {
            try
            {
                var res = _repository.UpdateFOT(id, data);

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

        [Route("FOT/{id}")]
        [HttpDelete]
        public IActionResult DeleteFOT(int id)
        {
            try
            {
                var res = _repository.DeleteFOTById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Position")]
        [HttpGet]
        public IActionResult GetPosition()
        {
            try
            {
                var res = _repository.GetPosition();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Position/{id}")]
        [HttpGet]
        public IActionResult GetPositionById(int id)
        {
            try
            {
                var res = _repository.GetPositionById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Position")]
        [HttpPost]
        public IActionResult PostPosition(Position data)
        {
            try
            {
                var res = _repository.AddPosition(data);

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

        [Route("Position/{id}")]
        [HttpPut]
        public IActionResult PutPosition(int id, [FromBody] Position data)
        {
            try
            {
                var res = _repository.UpdatePosition(id, data);

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

        [Route("Position/{id}")]
        [HttpDelete]
        public IActionResult DeletePosition(int id)
        {
            try
            {
                var res = _repository.DeletePositionById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("ReceptionConditions")]
        [HttpGet]
        public IActionResult GetReceptionConditions()
        {
            try
            {
                var res = _repository.GetReceptionConditions();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("ReceptionConditions/{id}")]
        [HttpGet]
        public IActionResult GetReceptionConditionsById(int id)
        {
            try
            {
                var res = _repository.GetReceptionConditionsById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("ReceptionConditions")]
        [HttpPost]
        public IActionResult PostReceptionConditions(ReceptionConditions data)
        {
            try
            {
                var res = _repository.AddReceptionConditions(data);

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

        [Route("ReceptionConditions/{id}")]
        [HttpPut]
        public IActionResult PutReceptionConditions(int id, [FromBody] ReceptionConditions data)
        {
            try
            {
                var res = _repository.UpdateReceptionConditions(id, data);

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

        [Route("ReceptionConditions/{id}")]
        [HttpDelete]
        public IActionResult DeleteReceptionConditions(int id)
        {
            try
            {
                var res = _repository.DeleteReceptionConditionsById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Salary")]
        [HttpGet]
        public IActionResult GetSalary()
        {
            try
            {
                var res = _repository.GetSalary();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Salary/{id}")]
        [HttpGet]
        public IActionResult GetSalaryById(int id)
        {
            try
            {
                var res = _repository.GetSalaryById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Salary")]
        [HttpPost]
        public IActionResult PostSalary(Data.DataModel.Salary data)
        {
            try
            {
                var res = _repository.AddSalary(data);

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

        [Route("Salary/{id}")]
        [HttpPut]
        public IActionResult PutSalary(int id, [FromBody] Data.DataModel.Salary data)
        {
            try
            {
                var res = _repository.UpdateSalary(id, data);

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
        [Route("Salary/{id}")]
        [HttpDelete]
        public IActionResult DeleteSalary(int id)
        {
            try
            {
                var res = _repository.DeleteSalaryById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

       

        [Route("VacationEntitlement")]
        [HttpGet]
        public IActionResult GetVacationEntitlement()
        {
            try
            {
                var res = _repository.GetVacationEntitlement();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("VacationEntitlement/{id}")]
        [HttpGet]
        public IActionResult GetVacationEntitlementById(int id)
        {
            try
            {
                var res = _repository.GetVacationEntitlementById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("VacationEntitlement")]
        [HttpPost]
        public IActionResult PostVacationEntitlement(VacationEntitlement data)
        {
            try
            {
                var res = _repository.AddVacationEntitlement(data);

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

        [Route("VacationEntitlement/{id}")]
        [HttpPut]
        public IActionResult PutVacationEntitlement(int id, [FromBody] VacationEntitlement data)
        {
            try
            {
                var res = _repository.UpdateVacationEntitlement(id, data);

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

        [Route("VacationEntitlement/{id}")]
        [HttpDelete]
        public IActionResult DeleteVacationEntitlement(int id)
        {
            try
            {
                var res = _repository.DeleteVacationEntitlementById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        ////
        [Route("Awards")]
        [HttpGet]
        public IActionResult GetAwards()
        {
            try
            {
                var res = _repository.GetAwards();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Awards/{id}")]
        [HttpGet]
        public IActionResult GetAwardsById(int id)
        {
            try
            {
                var res = _repository.GetAwardsById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Awards")]
        [HttpPost]
        public IActionResult PostAwards(Awards data)
        {
            try
            {
                var res = _repository.AddAwards(data);

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

        [Route("Awards/{id}")]
        [HttpPut]
        public IActionResult PutAwards(int id, [FromBody] Awards data)
        {
            try
            {
                var res = _repository.UpdateAwards(id, data);

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

        [Route("Awards/{id}")]
        [HttpDelete]
        public IActionResult DeleteAwards(int id)
        {
            try
            {
                var res = _repository.DeleteAwards(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
