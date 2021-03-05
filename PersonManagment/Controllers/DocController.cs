using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonManagment.Data.Models;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.PersonManagmentData;

namespace PersonManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocController : ControllerBase
    {
        private readonly DocData _repository;
        public DocController(ApplicationDbContext context)
        {
            _repository = new DocData(context);
        }

        /// <summary>
        /// Метод получения личной карточки сотрудника Word
        /// </summary>   
        [HttpGet]
        [Route("ExportEmployeeCard")]
        public IActionResult ExportEmployeeCard(int employeeId)
        {
            try
            {
                fileToSend res = _repository.GeneratePersonalEmployeeCard(employeeId);
                return File(res.data, res.mime, res.name);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Метод получения табеля за месяц в виде Excel
        /// </summary>   
        [HttpGet]
        [Route("SaveTimeSheet")]
        public IActionResult SaveTimeSheet(int date)
        {
            try
            {
                fileToSend res = _repository.GenerateTimeSheet(date);
                return File(res.data, res.mime, res.name);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Сохранить документ о расписании отпусков
        /// </summary>
        [HttpGet]
        [Route("SaveVacationShedule")]
        public IActionResult SaveVacationShedule()
        {
            try
            {
                fileToSend res = _repository.GenerateVacationShedule();
                return File(res.data, res.mime, res.name);
                //return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Экспорт графика отпусков word
        /// </summary>
        [HttpGet]
        [Route("ExportVacationSheduleDocs")]
        public IActionResult ExportVacationSheduleDocs()
        {
            try
            {
                fileToSend res = _repository.GenerateVacationSheduleDoc();
                return File(res.data, res.mime, res.name);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Экспорт графика отпусков word c выбранным id (filedata) старой версии документа
        /// </summary>
        [HttpGet]
        [Route("ExportVacationSheduleOldDocs")]
        public IActionResult ExportVacationSheduleOldDocs(int idDocument)
        {
            try
            {
                fileToSend res = _repository.GenerateVacationSheduleDoc(idDocument);
                return File(res.data, res.mime, res.name);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// загрузка шаблона документа 
        /// </summary>

        [Route("UploadTemplate")]
        [HttpPost]
        public IActionResult UploadTemplate(IFormFile file, string comment)
        {
            try
            {
                var res = _repository.UploadTemplate(file, comment);

                if (res == -1)
                {
                    return Conflict("Не удалось загрузить файл");
                }

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// загрузка новой версии шаблона документа 
        /// </summary>
        [Route("UpdateTemplate")]
        [HttpPost]
        public IActionResult UpdateTemplate(int id, IFormFile file)
        {
            try
            {
                var res = _repository.UpdateTemplate(id, file);

                if (res == false)
                {
                    return Conflict("Не удалось обновить файл");
                }

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// обновление выбранной версии шаблона документа 
        /// </summary>
        [Route("UpdateOldTemplate")]
        [HttpPost]
        public IActionResult UpdateOldTemplate(int id, IFormFile file, int version)
        {
            try
            {
                var res = _repository.UpdateOldTemplate(id, file, version);

                if (res == false)
                {
                    return Conflict("Не удалось обновить файл");
                }

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Экспорт трудового договора word
        /// </summary>
        [HttpGet]
        [Route("ExportContractDoc/{employeeId}")]
        public IActionResult ExportContractDoc(int employeeId)
        {
            try
            {
                var res = _repository.GenerateContractDoc(employeeId);
                return File(res.data, res.mime, res.name);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        // заполнение и экспорт трудового договора с совместителем  c выбранным id(filedata) старой версии документа
        /// </summary>
        [Route("PartTimeTemplateOld/{id}")]
        [HttpGet]
        public IActionResult GetPartTimeTemplateOld(int id, int idDocument)
        {
            try
            {
                fileToSend res = _repository.GetPartTimeTemplate(id, idDocument);
                return File(res.data, res.mime, res.name);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        // заполнение и экспорт трудового договора с совместителем
        /// </summary>
        [Route("PartTimeTemplate/{id}")]
        [HttpGet]
        public IActionResult GetPartTimeTemplate(int id)
        {
            try
            {
                fileToSend res = _repository.GetPartTimeTemplate(id);
                return File(res.data, res.mime, res.name);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }



        /// <summary>
        // загрузка календарного графика
        /// </summary>
        [Route("ProductionCalendarDay")]
        [HttpPost]
        public IActionResult LoadProductionCalendarDay(IFormFile file)
        {
            try
            {
                var res = _repository.LoadCalendar(file);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
