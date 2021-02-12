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
    public class AccrualsController : ControllerBase
    {
        private readonly AccrualsData _repository;
        public AccrualsController(ApplicationDbContext context)
        {
            _repository = new AccrualsData(context);
        }


        [HttpGet]
        [Route("DisablementIncapacityReason")]
        public IActionResult GetDisablementIncapacityReason()
        {
            try
            {
                var res = _repository.GetDisablementIncapacityReason();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        
        [Route("DisablementIncapacityReason/{id}")]
        [HttpGet]
        public IActionResult GetDisablementIncapacityReasonById(int id)
        {
            try
            {
                var res = _repository.GetDisablementIncapacityReasonById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpPost]
        [Route("DisablementIncapacityReason")]
        public IActionResult PostDisablementIncapacityReason(DisablementIncapacityReason data)
        {
            try
            {
                var res = _repository.AddDisablementIncapacityReason(data);

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
        [Route("DisablementIncapacityReason/{id}")]
        public IActionResult PutDisablementIncapacityReason(int id, [FromBody] DisablementIncapacityReason data)
        {
            try
            {
                var res = _repository.UpdateDisablementIncapacityReason(id, data);

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
        [Route("DisablementIncapacityReason/{id}")]
        public IActionResult DeleteDisablementIncapacityReason(int id)
        {
            try
            {
                var res = _repository.DeleteDisablementIncapacityReason(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        ///

        [HttpGet]
        [Route("DocumentAccruals")]

        public IActionResult GetDocumentAccruals()
        {
            try
            {
                var res = _repository.GetDocumentAccruals();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet]
        [Route("DocumentAccruals/{id}")]
        public IActionResult GetDocumentAccrualsById(int id)
        {
            try
            {
                var res = _repository.GetDocumentAccrualsById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpPost]
        [Route("DocumentAccruals")]
        public IActionResult PostDocumentAccruals(DocumentAccruals data)
        {
            try
            {
                var res = _repository.AddDocumentAccruals(data);

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
        [Route("DocumentAccruals/{id}")]
        public IActionResult PutDocumentAccruals(int id, [FromBody] DocumentAccruals data)
        {
            try
            {
                var res = _repository.UpdateDocumentAccruals(id, data);

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
        [Route("DocumentAccruals/{id}")]
        public IActionResult DeleteDocumentAccruals(int id)
        {
            try
            {
                var res = _repository.DeleteDocumentAccruals(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        ///
        
        [HttpGet]
        [Route("TypeAccrual")]

        public IActionResult GetTypeAccrual()
        {
            try
            {
                var res = _repository.GeTypeAccrual();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet]
        [Route("TypeAccrual/{id}")]
        public IActionResult GetTypeAccrualById(int id)
        {
            try
            {
                var res = _repository.GetTypeAccrualById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpPost]
        [Route("TypeAccrual")]
        public IActionResult PostTypeAccrual(TypeAccrual data)
        {
            try
            {
                var res = _repository.AddTypeAccrual(data);

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
        [Route("TypeAccrual/{id}")]
        public IActionResult PutTypeAccrual(int id, [FromBody] TypeAccrual data)
        {
            try
            {
                var res = _repository.UpdateTypeAccrual(id, data);

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
        [Route("TypeAccrual/{id}")]
        public IActionResult DeleteTypeAccrual(int id)
        {
            try
            {
                var res = _repository.DeleteTypeAccrual(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// TypeAward
        [HttpGet]
        [Route("TypeAward")]

        public IActionResult GetTypeAward()
        {
            try
            {
                var res = _repository.GetTypeAward();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet]
        [Route("TypeAward/{id}")]
        public IActionResult GetTypeAwardById(int id)
        {
            try
            {
                var res = _repository.GetTypeAwardById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpPost]
        [Route("TypeAward")]
        public IActionResult PostTypeAward(TypeAward data)
        {
            try
            {
                var res = _repository.AddTypeAward(data);

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
        [Route("TypeAward/{id}")]
        public IActionResult PutTypeAward(int id, [FromBody] TypeAward data)
        {
            try
            {
                var res = _repository.UpdateTypeAward(id, data);

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
        [Route("TypeAward/{id}")]
        public IActionResult DeleteTypeAward(int id)
        {
            try
            {
                var res = _repository.DeleteTypeAward(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        //Payroll
        [HttpGet]
        [Route("Payroll")]

        public IActionResult GetPayroll()
        {
            try
            {
                var res = _repository.GetPayroll();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet]
        [Route("Payroll/{id}")]
        public IActionResult GetPayrollById(int id)
        {
            try
            {
                var res = _repository.GetPayrollById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [HttpPost]
        [Route("Payroll")]
        public IActionResult PostPayroll(Payroll data)
        {
            try
            {
                var res = _repository.AddPayroll(data);

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
        [Route("Payroll/{id}")]
        public IActionResult PutPayroll(int id, [FromBody] Payroll data)
        {
            try
            {
                var res = _repository.UpdatePayroll(id, data);

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
        [Route("Payroll/{id}")]
        public IActionResult DeletePayroll(int id)
        {
            try
            {
                var res = _repository.DeletePayroll(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
