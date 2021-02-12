using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;
using PersonManagment.Data.PersonManagmentData;
using System;

namespace PersonManagment.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeData _repository;
        public EmployeesController(ApplicationDbContext context)
        {
            _repository = new EmployeeData(context);
        }

        [HttpGet]
        public IActionResult GetEmployee(int? dateStart, int? dateEnd)
        {
            try
            {
                var res = _repository.GetEmployee(dateStart, dateEnd);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            try
            {
                var res = _repository.GetEmployeeById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        public IActionResult PostEmployee(EmployeeInfoDataModel data)
        {
            try
            {
                var res = _repository.AddEmployee(data);

                if (res == -1)
                {
                    return Conflict("Сотрудник уже был создан");
                }

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, [FromBody] EmployeeInfoDataModel data)
        {
            try
            {
                var res = _repository.UpdateEmployee(id, data);

                if (!res)
                {
                    return Conflict("Сотрудник не найден");
                }

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var res = _repository.DeleteEmployeeById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Experience")]
        [HttpGet]
        public IActionResult GetExperience()
        {
            try
            {
                var res = _repository.GetExperience();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Experience/{id}")]
        [HttpGet]
        public IActionResult GetExperienceById(int id)
        {
            try
            {
                var res = _repository.GetExperienceById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Experience")]
        [HttpPost]
        public IActionResult PostExperience(Experience data)
        {
            try
            {
                var res = _repository.AddExperience(data);

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

        [Route("Experience/{id}")]
        [HttpPut]
        public IActionResult PutExperience(int id, [FromBody] Experience data)
        {
            try
            {
                var res = _repository.UpdateExperience(id, data);

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

        [Route("Experience/{id}")]
        [HttpDelete]
        public IActionResult DeleteExperience(int id)
        {
            try
            {
                var res = _repository.DeleteExperienceById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        
        [Route("Status")]
        [HttpGet]
        public IActionResult GetStatus()
        {
            try
            {
                var res = _repository.GetStatus();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Status/{id}")]
        [HttpGet]
        public IActionResult GetStatusById(int id)
        {
            try
            {
                var res = _repository.GetStatusById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Status")]
        [HttpPost]
        public IActionResult PostStatus(Status data)
        {
            try
            {
                var res = _repository.AddStatus(data);

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

        [Route("Status/{id}")]
        [HttpPut]
        public IActionResult PutStatus(int id, [FromBody] Status data)
        {
            try
            {
                var res = _repository.UpdateStatus(id, data);

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

        [Route("Status/{id}")]
        [HttpDelete]
        public IActionResult DeleteStatus(int id)
        {
            try
            {
                var res = _repository.DeleteStatusById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("ExperienceWork")]
        [HttpGet]
        public IActionResult GetExperienceWork()
        {
            try
            {
                var res = _repository.GetExperienceWork();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("ExperienceWork/{id}")]
        [HttpGet]
        public IActionResult GetExperienceWorkById(int id)
        {
            try
            {
                var res = _repository.GetExperienceWorkById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("ExperienceWork")]
        [HttpPost]
        public IActionResult PostExperienceWork(ExperienceWork data)
        {
            try
            {
                var res = _repository.AddExperienceWork(data);

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

        [Route("ExperienceWork/{id}")]
        [HttpPut]
        public IActionResult PutExperienceWork(int id, [FromBody] ExperienceWork data)
        {
            try
            {
                var res = _repository.UpdateExperienceWork(id, data);

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

        [Route("ExperienceWork/{id}")]
        [HttpDelete]
        public IActionResult DeleteExperienceWork(int id)
        {
            try
            {
                var res = _repository.DeleteExperienceWorkById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("WorkPlace")]
        [HttpGet]
        public IActionResult GetWorkPlace()
        {
            try
            {
                var res = _repository.GetWorkPlace();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("WorkPlace/{id}")]
        [HttpGet]
        public IActionResult GetWorkPlaceById(int id)
        {
            try
            {
                var res = _repository.GetWorkPlaceById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("WorkPlace")]
        [HttpPost]
        public IActionResult PostWorkPlace(WorkPlace data)
        {
            try
            {
                var res = _repository.AddWorkPlace(data);

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

        [Route("WorkPlace/{id}")]
        [HttpPut]
        public IActionResult PutWorkPlace(int id, [FromBody] WorkPlace data)
        {
            try
            {
                var res = _repository.UpdateWorkPlace(id, data);

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

        [Route("WorkPlace/{id}")]
        [HttpDelete]
        public IActionResult DeleteWorkPlace(int id)
        {
            try
            {
                var res = _repository.DeleteWorkPlaceById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Unit")]
        [HttpGet]
        public IActionResult GetUnit()
        {
            try
            {
                var res = _repository.GetUnit();
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        [Route("Unit/{id}")]
        [HttpGet]
        public IActionResult GetUnitById(int id)
        {
            try
            {
                var res = _repository.GetUnitById(id);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        [Route("Unit")]
        [HttpPost]
        public IActionResult PostUnit(Unit data)
        {
            try
            {
                var res = _repository.AddUnit(data);
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
        // PUT: api/units/5
        [Route("Unit/{id}")]
        [HttpPut]
        public IActionResult PutUnit(int id, [FromBody] Unit data)
        {
            try
            {
                var res = _repository.UpdateUnit(id, data);
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
        [Route("Unit/{id}")]
        [HttpDelete]
        public IActionResult DeleteUnit(int id)
        {
            try
            {
                var res = _repository.DeleteUnitById(id);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [Route("TypeOfEmployment")]
        [HttpGet]
        public IActionResult GetTypeOfEmployment()
        {
            try
            {
                var res = _repository.GetTypeOfEmployment();
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        [Route("TypeOfEmployment/{id}")]
        [HttpGet]
        public IActionResult GetTypeOfEmploymentById(int id)
        {
            try
            {
                var res = _repository.GetTypeOfEmploymentById(id);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        [Route("TypeOfEmployment")]
        [HttpPost]
        public IActionResult PostTypeOfEmployment(TypeOfEmployment data)
        {
            try
            {
                var res = _repository.AddTypeOfEmployment(data);
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
        [Route("TypeOfEmployment/{id}")]
        [HttpPut]
        public IActionResult PutTypeOfEmployment(int id, [FromBody] TypeOfEmployment data)
        {
            try
            {
                var res = _repository.UpdateTypeOfEmployment(id, data);
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
        [Route("TypeOfEmployment/{id}")]
        [HttpDelete]
        public IActionResult DeleteTypeOfEmployment(int id)
        {
            try
            {
                var res = _repository.DeleteTypeOfEmploymentById(id);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        [Route("Request")]
        [HttpGet]
        public IActionResult GetRequest()
        {
            try
            {
                var res = _repository.GetRequest();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Request/{id}")]
        [HttpGet]
        public IActionResult GetRequestById(int id)
        {
            try
            {
                var res = _repository.GetRequestById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Request")]
        [HttpPost]
        public IActionResult PostRequest(Request data)
        {
            try
            {
                var res = _repository.AddRequest(data);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Request/{id}")]
        [HttpPut]
        public IActionResult PutRequest(int id, [FromBody] Request data)
        {
            try
            {
                var res = _repository.UpdateRequest(id, data);

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

        [Route("Request/{id}")]
        [HttpDelete]
        public IActionResult DeleteRequest(int id)
        {
            try
            {
                var res = _repository.DeleteRequestById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("RequestComplete/{id}")]
        [HttpPost]
        public IActionResult CompleteRequest(int id)
        {
            try
            {
                var res = _repository.CompleteRequestById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [Route("RequestCategory")]
        [HttpGet]
        public IActionResult GetRequestCategory()
        {
            try
            {
                var res = _repository.GetRequestCategory();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("RequestArchive")]
        [HttpGet]
        public IActionResult GetRequestArchive()
        {
            try
            {
                var res = _repository.GetRequest(true);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


    }
}