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
    public class MilitaryRegistrationController : ControllerBase
    {
        private readonly MilitaryRegistrationData _repository;
        public MilitaryRegistrationController(ApplicationDbContext context)
        {
            _repository = new MilitaryRegistrationData(context);
        }

        [HttpGet]
        public IActionResult GetMilitaryRegistration()
        {
            try
            {
                var res = _repository.GetMilitaryRegistration();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMilitaryRegistrationById(int id)
        {
            try
            {
                var res = _repository.GetMilitaryRegistrationById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        public IActionResult PostMilitaryRegistration(MilitaryRegistration data)
        {
            try
            {
                var res = _repository.AddMilitaryRegistration(data);

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
        public IActionResult PutMilitaryRegistration(int id, [FromBody] MilitaryRegistration data)
        {
            try
            {
                var res = _repository.UpdateMilitaryRegistration(id, data);

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
        public IActionResult DeleteMilitaryRegistration(int id)
        {
            try
            {
                var res = _repository.DeleteMilitaryRegistrationById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
