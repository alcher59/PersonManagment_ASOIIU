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
    public class EducationController : ControllerBase
    {
        private readonly EducationData _repository;
        public EducationController(ApplicationDbContext context)
        {
            _repository = new EducationData(context);
        }

        [HttpGet]
        public IActionResult GetEducation()
        {
            try
            {
                var res = _repository.GetEducation();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEducationById(int id)
        {
            try
            {
                var res = _repository.GetEducationById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        public IActionResult PostEducation(Education data)
        {
            try
            {
                var res = _repository.AddEducation(data);

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
        public IActionResult PutEducation(int id, [FromBody] Education data)
        {
            try
            {
                var res = _repository.UpdateEducation(id, data);

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
        public IActionResult DeleteEducation(int id)
        {
            try
            {
                var res = _repository.DeleteEducationById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("AcademicDegrees")]
        [HttpGet]
        public IActionResult GetAcademicDegrees()
        {
            try
            {
                var res = _repository.GetAcademicDegrees();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("AcademicDegrees/{id}")]
        [HttpGet]
        public IActionResult GetAcademicDegreesById(int id)
        {
            try
            {
                var res = _repository.GetAcademicDegreesById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("AcademicDegrees")]
        [HttpPost]
        public IActionResult PostAcademicDegrees(AcademicDegrees data)
        {
            try
            {
                var res = _repository.AddAcademicDegrees(data);

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

        [Route("AcademicDegrees/{id}")]
        [HttpPut]
        public IActionResult PutAcademicDegrees(int id, [FromBody] AcademicDegrees data)
        {
            try
            {
                var res = _repository.UpdateAcademicDegrees(id, data);

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

        [Route("AcademicDegrees/{id}")]
        [HttpDelete]
        public IActionResult DeleteAcademicDegrees(int id)
        {
            try
            {
                var res = _repository.DeleteAcademicDegreesById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("AcademicTitles")]
        [HttpGet]
        public IActionResult GetAcademicTitles()
        {
            try
            {
                var res = _repository.GetAcademicTitles();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("AcademicTitles/{id}")]
        [HttpGet]
        public IActionResult GetAcademicTitlesById(int id)
        {
            try
            {
                var res = _repository.GetAcademicTitlesById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("AcademicTitles")]
        [HttpPost]
        public IActionResult PostAcademicTitle(AcademicTitles data)
        {
            try
            {
                var res = _repository.AddAcademicTitles(data);

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

        [Route("AcademicTitles/{id}")]
        [HttpPut]
        public IActionResult PutAcademicTitles(int id, [FromBody] AcademicTitles data)
        {
            try
            {
                var res = _repository.UpdateAcademicTitles(id, data);

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

        [Route("AcademicTitles/{id}")]
        [HttpDelete]
        public IActionResult DeleteAcademicTitles(int id)
        {
            try
            {
                var res = _repository.DeleteAcademicTitlesById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("DiplomaDocument")]
        [HttpGet]
        public IActionResult GetDiplomaDocument()
        {
            try
            {
                var res = _repository.GetDiplomaDocument();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("DiplomaDocument/{id}")]
        [HttpGet]
        public IActionResult GetDiplomaDocumentById(int id)
        {
            try
            {
                var res = _repository.GetDiplomaDocumentById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("DiplomaDocument")]
        [HttpPost]
        public IActionResult PostDiplomaDocument(DiplomaDocument data)
        {
            try
            {
                var res = _repository.AddDiplomaDocument(data);

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

        [Route("DiplomaDocument/{id}")]
        [HttpPut]
        public IActionResult PutDiplomaDocument(int id, [FromBody] DiplomaDocument data)
        {
            try
            {
                var res = _repository.UpdateDiplomaDocument(id, data);

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

        [Route("DiplomaDocument/{id}")]
        [HttpDelete]
        public IActionResult DeleteDiplomaDocument(int id)
        {
            try
            {
                var res = _repository.DeleteDiplomaDocumentById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EducationalInstitution")]
        [HttpGet]
        public IActionResult GetEducationalInstitution()
        {
            try
            {
                var res = _repository.GetEducationalInstitution();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EducationalInstitution/{id}")]
        [HttpGet]
        public IActionResult GetEducationalInstitutionById(int id)
        {
            try
            {
                var res = _repository.GetEducationalInstitutionById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EducationalInstitution")]
        [HttpPost]
        public IActionResult PostEducationalInstitution(EducationalInstitution data)
        {
            try
            {
                var res = _repository.AddEducationalInstitution(data);

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

        [Route("EducationalInstitution/{id}")]
        [HttpPut]
        public IActionResult PutEducationalInstitution(int id, [FromBody] EducationalInstitution data)
        {
            try
            {
                var res = _repository.UpdateEducationalInstitution(id, data);

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

        [Route("EducationalInstitution/{id}")]
        [HttpDelete]
        public IActionResult DeleteEducationalInstitution(int id)
        {
            try
            {
                var res = _repository.DeleteEducationalInstitutionById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EducationDegrees")]
        [HttpGet]
        public IActionResult GetEducationDegrees()
        {
            try
            {
                var res = _repository.GetEducationDegrees();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EducationDegrees/{id}")]
        [HttpGet]
        public IActionResult GetEducationDegreesById(int id)
        {
            try
            {
                var res = _repository.GetEducationDegreesById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EducationDegrees")]
        [HttpPost]
        public IActionResult PostEducationDegrees(EducationDegrees data)
        {
            try
            {
                var res = _repository.AddEducationDegrees(data);

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

        [Route("EducationDegrees/{id}")]
        [HttpPut]
        public IActionResult PutEducationDegrees(int id, [FromBody] EducationDegrees data)
        {
            try
            {
                var res = _repository.UpdateEducationDegrees(id, data);

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

        [Route("EducationDegrees/{id}")]
        [HttpDelete]
        public IActionResult DeleteEducationDegrees(int id)
        {
            try
            {
                var res = _repository.DeleteEducationDegreesById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EducationTitles")]
        [HttpGet]
        public IActionResult GetEducationTitles()
        {
            try
            {
                var res = _repository.GetEducationTitles();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EducationTitles/{id}")]
        [HttpGet]
        public IActionResult GetEducationTitlesById(int id)
        {
            try
            {
                var res = _repository.GetEducationTitlesById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EducationTitles")]
        [HttpPost]
        public IActionResult PostEducationTitles(EducationTitles data)
        {
            try
            {
                var res = _repository.AddEducationTitles(data);

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

        [Route("EducationTitles/{id}")]
        [HttpPut]
        public IActionResult PutEducationTitles(int id, [FromBody] EducationTitles data)
        {
            try
            {
                var res = _repository.UpdateEducationTitles(id, data);

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

        [Route("EducationTitles/{id}")]
        [HttpDelete]
        public IActionResult DeleteEducationTitles(int id)
        {
            try
            {
                var res = _repository.DeleteEducationTitlesById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("TypeOfEducation")]
        [HttpGet]
        public IActionResult GetTypeofEducation()
        {
            try
            {
                var res = _repository.GetTypeOfEducation();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("TypeOfEducation/{id}")]
        [HttpGet]
        public IActionResult GetTypeOfEducationById(int id)
        {
            try
            {
                var res = _repository.GetTypeOfEducationById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("TypeOfEducation")]
        [HttpPost]
        public IActionResult PostTypeofEducation(TypeOfEducation data)
        {
            try
            {
                var res = _repository.AddTypeOfEducation(data);

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

        [Route("TypeOfEducation/{id}")]
        [HttpPut("{id}")]
        public IActionResult PutTypeofEducation(int id, [FromBody] TypeOfEducation data)
        {
            try
            {
                var res = _repository.UpdateTypeOfEducation(id, data);

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

        [Route("TypeOfEducation/{id}")]
        [HttpDelete]
        public IActionResult DeleteTypeofEducation(int id)
        {
            try
            {
                var res = _repository.DeleteTypeOfEducationById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }


        [Route("Qualification")]
        [HttpGet]
        public IActionResult GetQualification()
        {
            try
            {
                var res = _repository.GetQualification();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Qualification/{id}")]
        [HttpGet]
        public IActionResult GetQualificationById(int id)
        {
            try
            {
                var res = _repository.GetQualificationById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Qualification")]
        [HttpPost]
        public IActionResult PostQualification(Qualification data)
        {
            try
            {
                var res = _repository.AddQualification(data);

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

        [Route("Qualification/{id}")]
        [HttpPut]
        public IActionResult PutQualification(int id, [FromBody] Qualification data)
        {
            try
            {
                var res = _repository.UpdateQualification(id, data);

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

        [Route("Qualification/{id}")]
        [HttpDelete]
        public IActionResult DeleteQualification(int id)
        {
            try
            {
                var res = _repository.DeleteQualificationById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        ///
        [Route("Specialty")]
        [HttpGet]
        public IActionResult GetSpecialty()
        {
            try
            {
                var res = _repository.GetSpecialty();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Specialty/{id}")]
        [HttpGet]
        public IActionResult GetSpecialtyById(int id)
        {
            try
            {
                var res = _repository.GetSpecialtyById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("Specialty")]
        [HttpPost]
        public IActionResult PostSpecialty(Specialty data)
        {
            try
            {
                var res = _repository.AddSpecialty(data);

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

        [Route("Specialty/{id}")]
        [HttpPut]
        public IActionResult PutSpecialty(int id, [FromBody] Specialty data)
        {
            try
            {
                var res = _repository.UpdateSpecialty(id, data);

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

        [Route("Specialty/{id}")]
        [HttpDelete]
        public IActionResult DeleteSpecialty(int id)
        {
            try
            {
                var res = _repository.DeleteSpecialtyById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        ///
        [Route("EnhancingCertification")]
        [HttpGet]
        public IActionResult GetEnhancingCertification()
        {
            try
            {
                var res = _repository.GetEnhancingCertification();

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EnhancingCertification/{id}")]
        [HttpGet]
        public IActionResult GetEnhancingCertificationById(int id)
        {
            try
            {
                var res = _repository.GetEnhancingCertificationById(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EnhancingCertification")]
        [HttpPost]
        public IActionResult PostEnhancingCertification(EnhancingCertification data)
        {
            try
            {
                var res = _repository.AddEnhancingCertification(data);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Route("EnhancingCertification/{id}")]
        [HttpPut]
        public IActionResult PutEnhancingCertification(int id, [FromBody] EnhancingCertification data)
        {
            try
            {
                var res = _repository.UpdateEnhancingCertification(id, data);

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

        [Route("EnhancingCertification/{id}")]
        [HttpDelete]
        public IActionResult DeleteEnhancingCertification(int id)
        {
            try
            {
                var res = _repository.DeleteEnhancingCertification(id);

                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }



}
