using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonManagment.Data;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;

namespace PersonManagment.Data.PersonManagmentData
{
    public class EducationData : IDisposable
    {
        public EducationData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public IEnumerable<EducationDataModel> GetEducation()
        {
            var res = _context.Education.Where(x => x.deleted == false).Select(x => new EducationDataModel()
            {
                Id = x.Id,
                employeeId = x.employeeId,
                typeOfEducationId = x.typeOfEducationId,
                educationalInstitutionId = x.educationalInstitutionId,
                dateStartEducation = x.dateStartEducation,
                dateEndEducation = x.dateEndEducation,
                specialtyId = x.specialtyId,
                qualificationId = x.qualificationId,
                documentTypeId = x.documentTypeId,
                diplomaDocumentId = x.diplomaDocumentId,
                scientificWorks = x.scientificWorks,
                inventions = x.inventions,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public EducationDataModel GetEducationById(int id)
        {
            var res = _context.Education.Select(x => new EducationDataModel()
            {
                Id = x.Id,
                employeeId = x.employeeId,
                typeOfEducationId = x.typeOfEducationId,
                educationalInstitutionId = x.educationalInstitutionId,
                dateStartEducation = x.dateStartEducation,
                dateEndEducation = x.dateEndEducation,
                specialtyId = x.specialtyId,
                qualificationId = x.qualificationId,
                documentTypeId = x.documentTypeId,
                diplomaDocumentId = x.diplomaDocumentId,
                scientificWorks = x.scientificWorks,
                inventions = x.inventions,
                deleted = x.deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteEducationById(int id)
        {
            var res = _context.Education.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddEducation(Education data)
        {
            var checkNumber = _context.Education.Any(x => x.employeeId == data.employeeId && x.diplomaDocumentId == data.diplomaDocumentId);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.Education.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateEducation(int id, Education data)
        {
            var res = _context.Education.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.Education.Where(x => x.Id != id).Any(x => x.employeeId == data.employeeId && x.diplomaDocumentId == data.diplomaDocumentId);
            if (checkNumber)
            {
                return false;
            }
            res.employeeId = data.employeeId;
            res.typeOfEducationId = data.typeOfEducationId;
            res.educationalInstitutionId = data.educationalInstitutionId;
            res.dateStartEducation = data.dateStartEducation;
            res.dateEndEducation = data.dateEndEducation;
            res.specialtyId = data.specialtyId;
            res.qualificationId = data.qualificationId;
            res.documentTypeId = data.documentTypeId;
            res.diplomaDocumentId = data.diplomaDocumentId;
            res.scientificWorks = data.scientificWorks;
            res.inventions = data.inventions;
            _context.SaveChanges();
            return true;
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null) _context.Dispose();
            }

            // Освобождаем неуправляемые ресурсы
        }
        #endregion

        public class EducationDataModel
        {
            public int Id { get; set; }
            public int? employeeId { get; set; }
            public int? typeOfEducationId { get; set; }
            public int? educationalInstitutionId { get; set; }
            public int? dateStartEducation { get; set; }
            public int? dateEndEducation { get; set; }
            public int? specialtyId { get; set; }
            public int? qualificationId { get; set; }
            public int? documentTypeId { get; set; }
            public int? diplomaDocumentId { get; set; }
            public bool scientificWorks { get; set; }
            public bool inventions { get; set; }
            public bool deleted { get; set; }
        }

        public IEnumerable<AcademicDegreesDataModel> GetAcademicDegrees()
        {
            var res = _context.AcademicDegrees.Where(x => x.deleted == false).Select(x => new AcademicDegreesDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public AcademicDegreesDataModel GetAcademicDegreesById(int id)
        {
            var res = _context.AcademicDegrees.Select(x => new AcademicDegreesDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }

        public bool DeleteAcademicDegreesById(int id)
        {
            var res = _context.AcademicDegrees.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;

        }

        public int AddAcademicDegrees(AcademicDegrees data)
        {
            var checkNumber = _context.AcademicDegrees.Any(x => x.title == data.title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.AcademicDegrees.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateAcademicDegrees(int id, AcademicDegrees data)
        {
            var res = _context.AcademicDegrees.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.AcademicDegrees.Where(x => x.id != id).Any(x => x.title == data.title);
            if (checkNumber)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }


        public class AcademicDegreesDataModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public bool deleted { get; set; }
        }
        public IEnumerable<AcademicTitlesDataModel> GetAcademicTitles()
        {
            var res = _context.AcademicTitles.Where(x => x.deleted == false).Select(x => new AcademicTitlesDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public AcademicTitlesDataModel GetAcademicTitlesById(int id)
        {
            var res = _context.AcademicTitles.Select(x => new AcademicTitlesDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }

        public bool DeleteAcademicTitlesById(int id)
        {
            var res = _context.AcademicTitles.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddAcademicTitles(AcademicTitles data)
        {
            var checkNumber = _context.AcademicTitles.Any(x => x.title == data.title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.AcademicTitles.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateAcademicTitles(int id, AcademicTitles data)
        {
            var res = _context.AcademicTitles.Where(x => x.id == id).FirstOrDefault();
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.AcademicTitles.Where(x => x.id != id).Any(x => x.title == data.title);
            if (checkNumber)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }
        public class AcademicTitlesDataModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public bool deleted { get; set; }
        }
        public IEnumerable<DiplomaDocumentDataModel> GetDiplomaDocument()
        {
            var res = _context.DiplomaDocument.Where(x => x.deleted == false).Select(x => new DiplomaDocumentDataModel()
            {
                id = x.id,
                number = x.number,
                serial = x.serial,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public DiplomaDocumentDataModel GetDiplomaDocumentById(int id)
        {
            var res = _context.DiplomaDocument.Select(x => new DiplomaDocumentDataModel()
            {
                id = x.id,
                number = x.number,
                serial = x.serial,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }

        public bool DeleteDiplomaDocumentById(int id)
        {
            var res = _context.DiplomaDocument.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;

        }

        public int AddDiplomaDocument(DiplomaDocument data)
        {
            var checkNumber = _context.DiplomaDocument.Any(x => x.number == data.number);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.DiplomaDocument.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateDiplomaDocument(int id, DiplomaDocument data)
        {
            var res = _context.DiplomaDocument.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            else
            {
                var checkNumber = _context.DiplomaDocument.Where(x => x.id != id).Any(x => x.number == data.number && x.serial == data.serial);
                if (checkNumber)
                {
                    return false;
                }
                res.number = data.number;
                res.serial = data.serial;
                _context.SaveChanges();
                return true;
            }
        }


        public class DiplomaDocumentDataModel
        {
            public int id { get; set; }
            public int serial { get; set; }
            public int number { get; set; }
            public bool deleted { get; set; }
        }

        public IEnumerable<EducationalInstitutionDataModel> GetEducationalInstitution()
        {
            var res = _context.EducationalInstitution.Where(x => x.deleted == false).Select(x => new EducationalInstitutionDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public EducationalInstitutionDataModel GetEducationalInstitutionById(int id)
        {
            var res = _context.EducationalInstitution.Select(x => new EducationalInstitutionDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }

        public bool DeleteEducationalInstitutionById(int id)
        {
            var res = _context.EducationalInstitution.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddEducationalInstitution(EducationalInstitution data)
        {
            var checkNumber = _context.EducationalInstitution.Any(x => x.title == data.title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.EducationalInstitution.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateEducationalInstitution(int id, EducationalInstitution data)
        {
            var res = _context.EducationalInstitution.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.EducationalInstitution.Where(x => x.id != id).Any(x => x.title == data.title);
            if (checkNumber)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }


        public class EducationalInstitutionDataModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public bool deleted { get; set; }
        }
        public IEnumerable<EducationDegreesDataModel> GetEducationDegrees()
        {
            var res = _context.EducationDegrees.Where(x => x.deleted == false).Select(x => new EducationDegreesDataModel()
            {
                Id = x.Id,
                educationId = x.educationId,
                academicDegreesId = x.academicDegreesId,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public EducationDegreesDataModel GetEducationDegreesById(int id)
        {
            var res = _context.EducationDegrees.Select(x => new EducationDegreesDataModel()
            {
                Id = x.Id,
                educationId = x.educationId,
                academicDegreesId = x.academicDegreesId,
                deleted = x.deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteEducationDegreesById(int id)
        {
            var res = _context.EducationDegrees.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddEducationDegrees(EducationDegrees data)
        {
            var checkNumber = _context.EducationDegrees.Any(x => x.educationId == data.educationId && x.academicDegreesId == data.academicDegreesId);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.EducationDegrees.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateEducationDegrees(int id, EducationDegrees data)
        {
            var res = _context.EducationDegrees.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.EducationDegrees.Where(x => x.Id != id).Any(x => x.educationId == data.educationId && x.academicDegreesId == data.academicDegreesId);
            if (checkNumber)
            {
                return false;
            }
            res.educationId = data.educationId;
            res.academicDegreesId = data.academicDegreesId;
            _context.SaveChanges();
            return true;
        }


        public class EducationDegreesDataModel
        {
            public int Id { get; set; }
            public int educationId { get; set; }
            public int academicDegreesId { get; set; }
            public bool deleted { get; set; }

        }


        public IEnumerable<EducationTitlesDataModel> GetEducationTitles()
        {
            var res = _context.EducationTitles.Where(x => x.deleted == false).Select(x => new EducationTitlesDataModel()
            {
                Id = x.Id,
                educationId = x.educationId,
                academicTitlesId = x.academicTitlesId,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public EducationTitlesDataModel GetEducationTitlesById(int id)
        {
            var res = _context.EducationTitles.Select(x => new EducationTitlesDataModel()
            {
                Id = x.Id,
                educationId = x.educationId,
                academicTitlesId = x.academicTitlesId,
                deleted = x.deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteEducationTitlesById(int id)
        {
            var res = _context.EducationTitles.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddEducationTitles(EducationTitles data)
        {
            var checkNumber = _context.EducationTitles.Any(x => x.educationId == data.educationId && x.academicTitlesId == data.academicTitlesId);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.EducationTitles.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateEducationTitles(int id, EducationTitles data)
        {
            var res = _context.EducationTitles.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.EducationTitles.Where(x => x.Id != id).Any(x => x.educationId == data.educationId && x.academicTitlesId == data.academicTitlesId);
            if (checkNumber)
            {
                return false;
            }
            res.educationId = data.educationId;
            res.academicTitlesId = data.academicTitlesId;
            _context.SaveChanges();
            return true;
        }
        public class EducationTitlesDataModel
        {
            public int Id { get; set; }
            public int educationId { get; set; }
            public int academicTitlesId { get; set; }
            public bool deleted { get; set; }
        }

        public IEnumerable<TypeOfEducationDataModel> GetTypeOfEducation()
        {
            var res = _context.TypeOfEducation.Where(x => x.deleted == false).Select(x => new TypeOfEducationDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public TypeOfEducationDataModel GetTypeOfEducationById(int id)
        {
            var res = _context.TypeOfEducation.Select(x => new TypeOfEducationDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }

        public bool DeleteTypeOfEducationById(int id)
        {
            var res = _context.TypeOfEducation.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddTypeOfEducation(TypeOfEducation data)
        {
            var checkNumber = _context.TypeOfEducation.Any(x => x.title == data.title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.TypeOfEducation.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateTypeOfEducation(int id, TypeOfEducation data)
        {
            var res = _context.TypeOfEducation.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }

        public class TypeOfEducationDataModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public bool deleted { get; set; }
        }

        public IEnumerable<QualificationDataModel> GetQualification()
        {
            var res = _context.Qualification.Where(x => x.deleted == false).Select(x => new QualificationDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public QualificationDataModel GetQualificationById(int id)
        {
            var res = _context.Qualification.Select(x => new QualificationDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }

        public bool DeleteQualificationById(int id)
        {
            var res = _context.Qualification.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddQualification(Qualification data)
        {
            var checkNumber = _context.Qualification.Any(x => x.title == data.title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.Qualification.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public EnhancingCertificationDataModel GetEnhancingCertificationById(int id)
        {
            var res = _context.EnhancingCertification.Where(x => x.deleted == false && x.id == id).Select(x => new EnhancingCertificationDataModel()
            {
                id = x.id,
                employeeId = x.employeeId,
                date = x.date,
                solve = x.solve,
                number = x.number,
                dateDocument = x.dateDocument,
                reason = x.reason,
                deleted = x.deleted,
            }).FirstOrDefault();

            return res;
        }

        public int AddEnhancingCertification(EnhancingCertification data)
        {
            var res = _context.EnhancingCertification.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateEnhancingCertification(int id, EnhancingCertification data)
        {
            var res = _context.EnhancingCertification.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.employeeId = data.employeeId;
            res.date = data.date;
            res.dateDocument = data.dateDocument;
            res.solve  = data.solve;
            res.number = data.number;
            res.reason = data.reason;
            _context.SaveChanges();
            return true;

        }

        public bool DeleteEnhancingCertification(int id)
        {
            var res = _context.EnhancingCertification.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public ICollection<EnhancingCertificationDataModel>  GetEnhancingCertification()
        {
            var res = _context.EnhancingCertification.Where(x => x.deleted == false).Select(x => new EnhancingCertificationDataModel()
            {
                id = x.id,
                employeeId = x.employeeId,
                date = x.date,
                solve = x.solve,
                number = x.number,
                dateDocument = x.dateDocument,
                reason = x.reason,
                deleted = x.deleted,
            }).ToArray();

            return res;
        }
        public bool UpdateQualification(int id, Qualification data)
        {
            var res = _context.Qualification.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.Qualification.Where(x => x.id != id).Any(x => x.title == data.title);
            if (checkNumber)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }
        public IEnumerable<SpecialtyDataModel> GetSpecialty()
        {
            var res = _context.Specialty.Where(x => x.deleted == false).Select(x => new SpecialtyDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public SpecialtyDataModel GetSpecialtyById(int id)
        {
            var res = _context.Specialty.Select(x => new SpecialtyDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }

        public bool DeleteSpecialtyById(int id)
        {
            var res = _context.Specialty.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }
        public int AddSpecialty(Specialty data)
        {
            var checkNumber = _context.Specialty.Any(x => x.title == data.title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.Specialty.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateSpecialty(int id, Specialty data)
        {
            var res = _context.Specialty.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.Specialty.Where(x => x.id != id).Any(x => x.title == data.title);
            if (checkNumber)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }

        public class SpecialtyDataModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public bool deleted { get; set; }
        }
        public class QualificationDataModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public bool deleted { get; set; }
        }

        public class EnhancingCertificationDataModel
        {
            public int id { get; set; }
            public int? employeeId { get; set; }
            public int date { get; set; }
            public string solve { get; set; }
            public int number { get; set; }
            public int dateDocument { get; set; }
            public string reason { get; set; }
            public bool deleted { get; set; }
        }
    }
}
