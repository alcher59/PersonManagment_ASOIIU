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
    public class PersonDataController : ControllerBase
    {
        private readonly PersonDataData _repository;
        public PersonDataController(ApplicationDbContext context)
        {
            _repository = new PersonDataData(context);
        }

        [HttpGet]
        public IActionResult GetPersonData()
        {
            try
            {
                var res = _repository.GetPersonData();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPersonDataById(int id)
        {
            try
            {
                var res = _repository.GetPersonDataById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        public IActionResult PostPersonData(PersonInfoDataModel data)
        {
            try
            {
                var res = _repository.AddPersonData(data);

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
        public IActionResult PutPersonData(int id, [FromBody] PersonInfoDataModel data)
        {
            try
            {
                var res = _repository.UpdatePersonData(id, data);

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
        public IActionResult DeletePersonData(int id)
        {
            try
            {
                var res = _repository.DeletePersonDataById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Country")]
        [HttpGet]
        public IActionResult GetCountry()
        {
            try
            {
                var res = _repository.GetCountry();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Country/{id}")]
        [HttpGet]
        public IActionResult GetCountryById(int id)
        {
            try
            {
                var res = _repository.GetCountryById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Country")]
        [HttpPost]
        public IActionResult PostCountry(Country data)
        {
            try
            {
                var res = _repository.AddCountry(data);

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

        [Route("Country/{id}")]
        [HttpPut]
        public IActionResult PutCountry(int id, [FromBody] Country data)
        {
            try
            {
                var res = _repository.UpdateCountry(id, data);

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

        [Route("Country/{id}")]
        [HttpDelete]
        public IActionResult DeleteCountry(int id)
        {
            try
            {
                var res = _repository.DeleteCountryById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("PassportData")]
        [HttpGet]
        public IActionResult GetPassportData()
        {
            try
            {
                var res = _repository.GetPassportData();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("PassportData/{id}")]
        [HttpGet]
        public IActionResult GetPassportData(int id)
        {
            try
            {
                var res = _repository.GetPassportDataById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("PassportData")]
        [HttpPost]
        public IActionResult PostPassportData(DocumentPassportData data)
        {
            try
            {
                var res = _repository.AddPassportData(data);

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

        [Route("PassportData/{id}")]
        [HttpPut]
        public IActionResult PutPassportData(int id, [FromBody] DocumentPassportData data)
        {
            try
            {
                var res = _repository.UpdatePassportData(id, data);

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

        [Route("PassportData/{id}")]
        [HttpDelete]
        public IActionResult DeletePassportData(int id)
        {
            try
            {
                var res = _repository.DeletePassportDataById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("DocumentType")]
        [HttpGet]
        public IActionResult GetDocumentType()
        {
            try
            {
                var res = _repository.GetDocumentType();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("DocumentType/{id}")]
        [HttpGet]
        public IActionResult GetDocumentType(int id)
        {
            try
            {
                var res = _repository.GetDocumentTypeById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("DocumentType")]
        [HttpPost]
        public IActionResult PostDocumentType(DocumentType data)
        {
            try
            {
                var res = _repository.AddDocumentType(data);

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

        [Route("DocumentType/{id}")]
        [HttpPut]
        public IActionResult PutDocumentType(int id, [FromBody] DocumentType data)
        {
            try
            {
                var res = _repository.UpdateDocumentType(id, data);

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

        [Route("DocumentType/{id}")]
        [HttpDelete]
        public IActionResult DeleteDocumentType(int id)
        {
            try
            {
                var res = _repository.DeleteDocumentTypeById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
       
        [Route("PersonAddress")]
        [HttpGet]
        public IActionResult GetPersonAddress()
        {
            try
            {
                var res = _repository.GetPersonAddress();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("PersonAddress/{id}")]
        [HttpGet]
        public IActionResult GetPersonAddressById(int id)
        {
            try
            {
                var res = _repository.GetPersonAddressById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("PersonAddress")]
        [HttpPost]
        public IActionResult PostPersonAddress(PersonAddress data)
        {
            try
            {
                var res = _repository.AddPersonAddress(data);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("PersonAddress/{id}")]
        [HttpPut]
        public IActionResult PutPersonAddress(int id, [FromBody] PersonAddress data)
        {
            try
            {
                var res = _repository.UpdatePersonAddress(id, data);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("PersonAddress/{id}")]
        [HttpDelete]
        public IActionResult DeletePersonAddress(int id)
        {
            try
            {
                var res = _repository.DeletePersonAddressById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("PersonContacts")]
        [HttpGet]
        public IActionResult GetPersonContacts()
        {
            try
            {
                var res = _repository.GetPersonContacts();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("PersonContacts/{id}")]
        [HttpGet]
        public IActionResult GetPersonContacts(int id)
        {
            try
            {
                var res = _repository.GetPersonContactsById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        // POST: api/personcontacts
        [Route("PersonContacts")]
        [HttpPost]
        public IActionResult PostPersonContacts(PersonContacts data)
        {
            try
            {
                var res = _repository.AddPersonContacts(data);

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

        [Route("PersonContacts/{id}")]
        [HttpPut]
        public IActionResult PutPersonContacts(int id, [FromBody] PersonContacts data)
        {
            try
            {
                var res = _repository.UpdatePersonContacts(id, data);

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

        [Route("PersonContacts/{id}")]
        [HttpDelete]
        public IActionResult DeletePersonContacts(int id)
        {
            try
            {
                var res = _repository.DeletePersonContactsById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

    }
}
