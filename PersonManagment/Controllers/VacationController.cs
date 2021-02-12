using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonManagment.Data.Models;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.PersonManagmentData;

namespace PersonManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly VacationData _repository;
        public VacationController(ApplicationDbContext context)
        {
            _repository = new VacationData(context);
        }

        [HttpGet]
        [Route("BusinessTrips")]

        public IActionResult GetBusinessTrips()
        {
            try
            {
                var res = _repository.GetBusinessTrips();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [Route("BusinessTrips/{id}")]
        [HttpGet]
        public IActionResult GetBusinessTripsById(int id)
        {
            try
            {
                var res = _repository.GetBusinessTripsById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpPost]
        [Route("BusinessTrips")]
        public IActionResult PostBusinessTrips(BusinessTrips data)
        {
            try
            {
                var res = _repository.AddBusinessTrips(data);

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


        [HttpPut]
        [Route("BusinessTrips/{id}")]
        public IActionResult PutBusinessTrips(int id, [FromBody] BusinessTrips data)
        {
            try
            {
                var res = _repository.UpdateBusinessTrips(id, data);

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


        [HttpDelete]
        [Route("BusinessTrips/{id}")]
        public IActionResult DeleteBusinessTrips(int id)
        {
            try
            {
                var res = _repository.DeleteBusinessTrips(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        //SickLeaves

        [HttpGet]
        [Route("SickLeaves")]

        public IActionResult GetSickLeaves()
        {
            try
            {
                var res = _repository.GetSickLeaves();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [Route("SickLeaves/{id}")]
        [HttpGet]
        public IActionResult GetSickLeavesById(int id)
        {
            try
            {
                var res = _repository.GetSickLeavesById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpPost]
        [Route("SickLeaves")]
        public IActionResult PostSickLeaves(SickLeaves data)
        {
            try
            {
                var res = _repository.AddSickLeaves(data);

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


        [HttpPut]
        [Route("SickLeaves/{id}")]
        public IActionResult PutSickLeaves(int id, [FromBody] SickLeaves data)
        {
            try
            {
                var res = _repository.UpdateSickLeaves(id, data);

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


        [HttpDelete]
        [Route("SickLeaves/{id}")]
        public IActionResult DeleteSickLeaves(int id)
        {
            try
            {
                var res = _repository.DeleteSickLeaves(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet]
        public IActionResult GetVacations()
        {
            try
            {
                var res = _repository.GetVacations();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetVacationsById(int id)
        {
            try
            {
                var res = _repository.GetVacationsById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpPost]
        public IActionResult PostVacations(Vacations data)
        {
            try
            {
                var res = _repository.AddVacations(data);

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
        public IActionResult PutVacations(int id, [FromBody] Vacations data)
        {
            try
            {
                var res = _repository.UpdateVacations(id, data);

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
        public IActionResult DeleteVacations(int id)
        {
            try
            {
                var res = _repository.DeleteVacations(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet]
        [Route("AllAccurals")]
        public IActionResult GetAllAccurals()
        {
            try
            {
                var res = _repository.GetAllAccurals();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
