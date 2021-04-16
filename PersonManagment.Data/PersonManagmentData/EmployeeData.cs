using DocumentFormat.OpenXml.Bibliography;
using PersonManagment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;

namespace PersonManagment.Data.PersonManagmentData
{
    public class EmployeeData : IDisposable
    {
        public EmployeeData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public IEnumerable<EmployeeDataModel> GetEmployee(int? dateStart, int? dateEnd)
        {
            var recruitment = _context.Recruitment.Where(x => (dateStart == null || x.dateOfReceipt >= dateStart) && (dateEnd == null || x.dateOfReceipt <= dateEnd)).ToArray();

            EmployeeDataModel[] res;

            if (dateStart != null || dateEnd != null)
            {
                res = _context.Employees.Where(x => !x.Deleted && recruitment.Select(y => y.employeeId).Contains(x.Id)).Select(x => new EmployeeDataModel()
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    PersonnelNumber = x.PersonnelNumber,
                    Position = x.Recruitment.Any() ? x.Recruitment.First().Position.Name : string.Empty,
                    Status = x.Status.Name,
                    TypeOfEmployment = x.Recruitment.Any() ? x.Recruitment.FirstOrDefault().TypeOfEmployment.Title : string.Empty,
                    Unit = x.Recruitment.Any() ? x.Recruitment.FirstOrDefault().Unit.Title : string.Empty,
                    dateOfReceipt = x.Recruitment.Any() ? x.Recruitment.First().dateOfReceipt : default(int?),
                    //Deleted = x.Deleted
                }).OrderBy(x => x.FullName).ToArray();
            }
            else
            {
                res = _context.Employees.Where(x => !x.Deleted).Select(x => new EmployeeDataModel()
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    PersonnelNumber = x.PersonnelNumber,
                    Position = x.Recruitment.Any() ? x.Recruitment.First().Position.Name : string.Empty,
                    Status = x.Status.Name,
                    TypeOfEmployment = x.Recruitment.Any() ? x.Recruitment.FirstOrDefault().TypeOfEmployment.Title : string.Empty,
                    Unit = x.Recruitment.Any() ? x.Recruitment.FirstOrDefault().Unit.Title : string.Empty,
                    dateOfReceipt = x.Recruitment.Any() ? x.Recruitment.First().dateOfReceipt : default(int?),
                    //Deleted = x.Deleted
                }).OrderBy(x => x.FullName).ToArray();
            }

            return res;
        }

        public EmployeeInfoDataModel GetEmployeeById(int id)
        {
            var personData = _context.PersonData.FirstOrDefault(x => x.EmployeeId == id);

            var person = new Models.PersonData() { PassportData = new PassportData(), Contacts = new Contacts() };
            if (personData != null)
            {
                var PersonContacts = _context.PersonContacts.FirstOrDefault(x => x.PersonData.Id == personData.Id);
                var PersonAddress = _context.PersonAddress.FirstOrDefault(x => x.PersonData.Id == personData.Id);

                var DocumentPassportData = _context.DocumentPassportData.Where(x => x.personDataId == personData.Id).ToList();
                var passport = DocumentPassportData.FirstOrDefault();

                person.Id = personData.Id;
                person.DateBirth = personData.DateBirth;
                person.Gender = personData.Gender;
                person.INN = personData.INN;
                person.SNILS = personData.SNILS;
                person.Birthplace = personData.Birthplace;
                person.DocumentTypeId = personData.DocumentTypeId;
                person.Citizenship = 1;

                if (passport != null)
                {
                    person.Citizenship = passport.CountryId;

                    person.PassportData.Id = passport.Id;
                    person.PassportData.Series = passport.Series;
                    person.PassportData.Number = passport.Number;
                    person.PassportData.Division = passport.DocumentIssued;
                    person.PassportData.ValidityDocumentDateStart = passport.IssuedDate;
                    person.PassportData.Code = passport.Code;
                    person.PassportData.InformationСitizenshipDateStart = personData.InformationСitizenshipDateStart;
                    person.PassportData.DocumentPassportId = passport.Id;
                }

                if (PersonContacts != null)
                {
                    person.Contacts.Phone = PersonContacts.PhoneNumber;
                }

                if (PersonAddress != null)
                {
                    person.Contacts.Address = PersonAddress.ResidenceAddress;
                }
            }

            var res = _context.Employees.Select(x => new EmployeeInfoDataModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                PersonnelNumber = x.PersonnelNumber,
                PersonData = person,
                Status = x.Status.Id
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteEmployeeById(int id)
        {
            var person = _context.PersonData.FirstOrDefault(x => x.EmployeeId == id);

            var res = _context.Employees.FirstOrDefault(x => x.Id == id);

            if (res == null)
            {
                return false;
            }

            //person.Deleted = true;
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddEmployee(EmployeeInfoDataModel data)
        {
            var checkPerson = _context.PersonData.Any(x => x.INN == data.PersonData.INN || x.SNILS == data.PersonData.SNILS);

            if (checkPerson)
            {
                return -1;
            }

            var employee = new Employee()
            {
                FullName = data.FullName,
                PersonnelNumber = !string.IsNullOrEmpty(data.PersonnelNumber) ? data.PersonnelNumber : string.Empty
            };

            var res = _context.Employees.Add(employee);
            _context.SaveChanges();

            var person = new DataModel.PersonData()
            {
                EmployeeId = res.Entity.Id,
                DateBirth = data.PersonData.DateBirth,
                Birthplace = data.PersonData.Birthplace,
                Gender = data.PersonData.Gender,
                INN = data.PersonData.INN,
                SNILS = data.PersonData.SNILS,
                DocumentTypeId = data.PersonData.DocumentTypeId,
                DocumentPassportData = new List<DocumentPassportData>()
                {
                    new DocumentPassportData()
                    {
                        CountryId = data.PersonData.Citizenship,
                        Series = data.PersonData.PassportData.Series,
                        Number = data.PersonData.PassportData.Number,
                        DocumentIssued = data.PersonData.PassportData.Division,
                        IssuedDate = data.PersonData.PassportData.ValidityDocumentDateStart,
                        Code = data.PersonData.PassportData.Code
                    }
                },
                PersonAddress = new PersonAddress()
                {
                    ResidenceAddress = data.PersonData.Contacts.Address
                },
                PersonContacts = new PersonContacts()
                {
                    PhoneNumber = data.PersonData.Contacts.Phone
                }
            };

            _context.PersonData.Add(person);

            _context.SaveChanges();

            return res.Entity.Id;
        }

        public bool UpdateEmployee(int id, EmployeeInfoDataModel data)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == id);

            if (employee == null)
            {
                return false;
            }

            employee.FullName = data.FullName;
            employee.PersonnelNumber = data.PersonnelNumber;

            var person = _context.PersonData.FirstOrDefault(x => x.EmployeeId == id);
            if (person == null)
            {
                person = _context.PersonData.Add(new DataModel.PersonData()).Entity;
            }

            var PersonContacts = _context.PersonContacts.FirstOrDefault(x => x.PersonData.Id == person.Id);
            if (PersonContacts == null)
            {
                PersonContacts = _context.PersonContacts.Add(new PersonContacts()).Entity;
                person.PersonContacts = PersonContacts;
            }

            var PersonAddress = _context.PersonAddress.FirstOrDefault(x => x.PersonData.Id == person.Id);
            if (PersonAddress == null)
            {
                PersonAddress = _context.PersonAddress.Add(new PersonAddress()).Entity;
                person.PersonAddress = PersonAddress;
            }

            var DocumentPassportData = _context.DocumentPassportData.Where(x => x.personDataId == person.Id).ToList();
            if (!DocumentPassportData.Any())
            {
                DocumentPassportData = new List<DocumentPassportData>() { new DocumentPassportData() };
                person.DocumentPassportData = DocumentPassportData;
            }

            person.EmployeeId = employee.Id;
            person.Birthplace = data.PersonData.Birthplace;
            person.DateBirth = data.PersonData.DateBirth;
            person.Gender = data.PersonData.Gender;
            person.INN = data.PersonData.INN;
            person.SNILS = data.PersonData.SNILS;
            person.DocumentTypeId = data.PersonData.DocumentTypeId;

            if (DocumentPassportData.Any())
            {
                var passport = DocumentPassportData.First();
                passport.Series = data.PersonData.PassportData.Series;
                passport.Number = data.PersonData.PassportData.Number;
                passport.DocumentIssued = data.PersonData.PassportData.Division;
                passport.IssuedDate = data.PersonData.PassportData.ValidityDocumentDateStart;
                passport.Code = data.PersonData.PassportData.Code;
                passport.CountryId = data.PersonData.Citizenship;
            }

            person.InformationСitizenshipDateStart = data.PersonData.PassportData.InformationСitizenshipDateStart;

            person.ValidityDocumentDateStart = data.PersonData.PassportData.ValidityDocumentDateStart;

            PersonContacts.PhoneNumber = data.PersonData.Contacts.Phone;
            PersonAddress.ResidenceAddress = data.PersonData.Contacts.Address;

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

        public class EmployeeDataModel
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string PersonnelNumber { get; set; }
            public string Position { get; set; }
            public int? dateOfReceipt { get; set; }
            public string Unit { get; set; }
            public string TypeOfEmployment { get; set; }
            public string Status { get; set; }
            public bool Deleted { get; set; }
            public int? DateOfDismissal { get; set; }
            //public decimal Salary { get; set; }
        }
        public IEnumerable<ExperienceDataModel> GetExperience()
        {
            var res = _context.Experience.Where(x => x.Deleted == false).Select(x => new ExperienceDataModel()
            {
                Id = x.Id,
                employeeId = x.employeeId,
                positionId = x.positionId,
                countRates = x.countRates,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public ExperienceDataModel GetExperienceById(int id)
        {
            var res = _context.Experience.Select(x => new ExperienceDataModel()
            {
                Id = x.Id,
                employeeId = x.employeeId,
                positionId = x.positionId,
                countRates = x.countRates,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteExperienceById(int id)
        {
            var res = _context.Experience.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddExperience(Experience data)
        {
            var checkNumber = _context.Experience.Any(x => x.employeeId == data.employeeId);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.Experience.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateExperience(int id, Experience data)
        {
            var res = _context.Experience.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.employeeId = data.employeeId;
            res.positionId = data.positionId;
            res.countRates = data.countRates;
            _context.SaveChanges();
            return true;
        }


        public class ExperienceDataModel
        {
            public int Id { get; set; }
            public string placeWork { get; set; }
            public int? positionId { get; set; }
            public int? employeeId { get; set; }
            public int? countRates { get; set; }
            public bool Deleted { get; set; }
        }

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

        public IEnumerable<StatusDataModel> GetStatus()
        {
            var res = _context.Statuses.Where(x => x.Deleted == false).Select(x => new StatusDataModel()
            {
                Id = x.Id,
                Name = x.Name,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public StatusDataModel GetStatusById(int id)
        {
            var res = _context.Statuses.Select(x => new StatusDataModel()
            {
                Id = x.Id,
                Name = x.Name,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteStatusById(int id)
        {
            var res = _context.Statuses.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddStatus(Status data)
        {
            var checkNumber = _context.Statuses.Any(x => x.Name == data.Name);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.Statuses.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateStatus(int id, Status data)
        {
            var res = _context.Statuses.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.Statuses.Any(x => x.Name == data.Name);
            if (checkNumber)
            {
                return false;
            }
            res.Name = data.Name;
            _context.SaveChanges();
            return true;
        }


        public class StatusDataModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Deleted { get; set; }

        }

        public IEnumerable<ExperienceWorkDataModel> GetExperienceWork()
        {
            var res = _context.ExperienceWork.Where(x => x.deleted == false).Select(x => new ExperienceWorkDataModel()
            {
                Id = x.Id,
                experienceId = x.experienceId,
                workPlaceId = x.workPlaceId,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public ExperienceWorkDataModel GetExperienceWorkById(int id)
        {
            var res = _context.ExperienceWork.Select(x => new ExperienceWorkDataModel()
            {
                Id = x.Id,
                experienceId = x.experienceId,
                workPlaceId = x.workPlaceId,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                deleted = x.deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteExperienceWorkById(int id)
        {
            var res = _context.ExperienceWork.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddExperienceWork(ExperienceWork data)
        {
            var checkNumber = _context.ExperienceWork.Any(x => x.experienceId == data.experienceId);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.ExperienceWork.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateExperienceWork(int id, ExperienceWork data)
        {
            var res = _context.ExperienceWork.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.experienceId = data.experienceId;
            res.workPlaceId = data.workPlaceId;
            res.dateStart = data.dateStart;
            res.dateEnd = data.dateEnd;
            _context.SaveChanges();
            return true;
        }
        public class ExperienceWorkDataModel
        {
            public int Id { get; set; }
            public int experienceId { get; set; }
            public int workPlaceId { get; set; }
            public int? dateStart { get; set; }
            public int? dateEnd { get; set; }
            public bool deleted { get; set; }
        }

        public IEnumerable<WorkPlaceDataModel> GetWorkPlace()
        {
            var res = _context.WorkPlace.Where(x => x.deleted == false).Select(x => new WorkPlaceDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public WorkPlaceDataModel GetWorkPlaceById(int id)
        {
            var res = _context.WorkPlace.Select(x => new WorkPlaceDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }

        public bool DeleteWorkPlaceById(int id)
        {
            var res = _context.WorkPlace.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddWorkPlace(WorkPlace data)
        {
            var checkNumber = _context.WorkPlace.Any(x => x.title == data.title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.WorkPlace.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateWorkPlace(int id, WorkPlace data)
        {
            var res = _context.WorkPlace.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }

        public class WorkPlaceDataModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public bool deleted { get; set; }

        }

        public IEnumerable<UnitDataModel> GetUnit()
        {
            var res = _context.Unit.Where(x => x.Deleted == false).Select(x => new UnitDataModel()
            {
                Id = x.Id,
                Title = x.Title,
                Deleted = x.Deleted
            }).ToArray();
            return res;
        }
        public UnitDataModel GetUnitById(int id)
        {
            var res = _context.Unit.Select(x => new UnitDataModel()
            {
                Id = x.Id,
                Title = x.Title,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);
            return res;
        }
        public bool DeleteUnitById(int id)
        {
            var res = _context.Unit.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }
        public int AddUnit(Unit data)
        {
            var checkNumber = _context.Unit.Any(x => x.Title == data.Title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.Unit.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }
        public bool UpdateUnit(int id, Unit data)
        {
            var res = _context.Unit.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Title = data.Title;
            _context.SaveChanges();
            return true;
        }

        public class UnitDataModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public bool Deleted { get; set; }
        }

        public IEnumerable<TypeOfEmploymentDataModel> GetTypeOfEmployment()
        {
            var res = _context.TypeOfEmployment.Where(x => x.Deleted == false).Select(x => new TypeOfEmploymentDataModel()
            {
                Id = x.Id,
                Title = x.Title,
                Deleted = x.Deleted
            }).ToArray();
            return res;
        }
        public TypeOfEmploymentDataModel GetTypeOfEmploymentById(int id)
        {
            var res = _context.TypeOfEmployment.Select(x => new TypeOfEmploymentDataModel()
            {
                Id = x.Id,
                Title = x.Title,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);
            return res;
        }

        public bool DeleteTypeOfEmploymentById(int id)
        {
            var res = _context.TypeOfEmployment.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }
        public int AddTypeOfEmployment(TypeOfEmployment data)
        {
            var checkNumber = _context.TypeOfEmployment.Any(x => x.Title == data.Title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.TypeOfEmployment.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }
        public bool UpdateTypeOfEmployment(int id, TypeOfEmployment data)
        {
            var res = _context.TypeOfEmployment.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Title = data.Title;
            _context.SaveChanges();
            return true;
        }

        public class TypeOfEmploymentDataModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public bool Deleted { get; set; }
        }

        public IEnumerable<RequestCategoryDataModel> GetRequestCategory()
        {
            var res = _context.RequestCategory.Where(x => x.deleted == false).Select(x => new RequestCategoryDataModel()
            {
                Id = x.Id,
                title = x.title,
                deleted = x.deleted
            }
            ).ToArray();
            return res;
        }

        public IEnumerable<RequestDataModel> GetRequest(bool completed = false)
        {
            var res = _context.Request.Where(x => x.deleted == false && x.completed == completed).Select(x => new RequestDataModel()
            {
                Id = x.Id,
                title = x.title,
                employeeId = x.employeeId,
                employee = x.Employee.FullName,
                requestCategoryId = x.requestCategoryId,
                category = x.RequestCategory.title,
                amount = x.amount,
                periodOfExecution = x.periodOfExecution,
                description = x.description,
                completed = x.completed,
                createdDate = x.createdDate,
                deleted = x.deleted
            }).ToArray();
            return res;
        }
        public RequestDataModel GetRequestById(int id)
        {
            var res = _context.Request.Select(x => new RequestDataModel()
            {
                Id = x.Id,
                title = x.title,
                employeeId = x.employeeId,
                requestCategoryId = x.requestCategoryId,
                amount = x.amount,
                periodOfExecution = x.periodOfExecution,
                description = x.description,
                completed = x.completed,
                createdDate = x.createdDate,
                deleted = x.deleted
            }).FirstOrDefault(y => y.Id == id && y.deleted == false && y.completed == false);
            return res;
        }

        public bool DeleteRequestById(int id)
        {
            var res = _context.Request.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public bool CompleteRequestById(int id)
        {
            var res = _context.Request.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.completed = true;
            _context.SaveChanges();
            return true;
        }


        public int AddRequest(Request data)
        {
            data.createdDate = (int)Utils.ConvertToUnixTimestamp(DateTime.Now);
            var res = _context.Request.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }
        public bool UpdateRequest(int id, Request data)
        {
            var res = _context.Request.FirstOrDefault(x => x.Id == id && x.deleted == false && x.completed == false);
            if (res == null)
            {
                return false;
            }
            res.title = data.title;
            res.employeeId = data.employeeId;
            res.description = data.description;
            res.amount = data.amount;
            res.periodOfExecution = data.periodOfExecution;
            res.requestCategoryId = data.requestCategoryId;
            res.createdDate = data.createdDate;
            res.completed = data.completed;
            _context.SaveChanges();
            return true;
        }

        public class RequestDataModel
        {
            public int Id { get; set; }
            public string title { get; set; }
            public int employeeId { get; set; }
            public int requestCategoryId { get; set; }
            public int? amount { get; set; }
            public int? periodOfExecution { get; set; }
            public string? description { get; set; }
            public int? createdDate { get; set; }
            public bool completed { get; set; }
            public bool deleted { get; set; }
            public string category { get; set; }
            public string employee { get; set; }
        }
        public class RequestCategoryDataModel
        {
            public int Id { get; set; }
            public string title { get; set; }
            public bool deleted { get; set; }
        }

        public EmployeeCardDataModel GetDataEmployeeCardDoc(int employeeId)
        {
            var employee = _context.Employees.Where(x => x.Deleted == false && x.Id == employeeId);

            if (employee == null)
            {
                return null;
            }
            var person = _context.PersonData.FirstOrDefault(x => x.EmployeeId == employeeId);

            var personAdress = person != null ? _context.PersonAddress.FirstOrDefault(x => x.Id == person.PersonAddressId) : null;

            var personContacts = person != null ? _context.PersonContacts.FirstOrDefault(x => x.Id == person.PersonContactsId) : null;

            var passport = _context.DocumentPassportData.FirstOrDefault(x => x.personDataId == person.Id && x.Deleted != false);

            var personData = new PersonModel()
            {
                Birthplace = person != null ? person.Birthplace : string.Empty,
                DateBirth = person != null ? person.DateBirth : 0,
                INN = person != null ? person.INN : 0,
                SNILS = person != null ? person.SNILS : string.Empty,
                Gender = person != null ? person.Gender : -1,
                DocumentPassport = new PassportModel() { serial = passport != null ? passport.Series : 0, number = passport != null ? passport.Number : 0, DocumentIssued = passport != null ? passport.DocumentIssued : string.Empty, IssuedDate = passport != null ? passport.IssuedDate : 0 },
                PersonAdress = new PersonAdressModel() { RegistrationAddress = personAdress != null ? personAdress.RegistrationAddress : string.Empty, ResidenceAddress = personAdress != null ? personAdress.ResidenceAddress : string.Empty, RegistrationDate = personAdress != null ? personAdress.RegistrationDate : 0 },
                PhoneNumber = personContacts != null ? personContacts.PhoneNumber : string.Empty,
            };


            var educationData = _context.Education.Where(x => x.employeeId == employeeId).Select(x => new EducationModel()
            {
                educationalInstitution = x.EducationalInstitution.title,
                dateEndEducation = x.dateEndEducation,
                qualification = x.Qualification.title,
                speciality = x.Specialty.title,
                typeOfEducation = x.TypeOfEducation.title,
                diplomaDocument = _context.DiplomaDocument.Where(y => y.id == x.Id).Select(y => new DiplomaDocumentModel()
                {
                    number = y.number,
                    serial = y.serial
                }).FirstOrDefault()
            }).ToArray();

            var recruitmentData = _context.Recruitment.Where(x => x.employeeId == employeeId).Select(x => new RecruitmentModel()
            {
                dateOfReceipt = x.dateOfReceipt,
                unit = x.Unit.Title,
                position = x.Position.Name,
                salary = x.Salary.salary,
                causeTransferComment = x.causeTransferComment,
                typeOfEmployment = x.TypeOfEmployment.Title,
            }).ToArray();

            var dismissalData = _context.Dismissal.Where(x => x.employeeId == employeeId).Select(x => new DismissalModel()
            {
                dateOfDismissal = x.dateOfDismissal,
                cause = x.cause,
            }).FirstOrDefault();

            var recruitment = _context.Recruitment.FirstOrDefault(x => x.employeeId == employeeId && x.isTransfer == false);

            var contractData = _context.Contract.Where(x => x.Id == recruitment.contractId).Select(x => new ContractModel()
            {
                number = x.number,
                dateStart = x.dateStart
            }).FirstOrDefault();

            var militaryRegistrationData = _context.MilitaryRegistration.Where(x => x.employeeId == employeeId).Select(x => new MilitaryRegistrationModel()
            {
                stockCategory = x.StockCategory.title,
                militaryRank = x.MilitaryRank.title,
                militaryProfile = x.MilitaryProfile.title,
                VUS = x.VUS,
                militaryFitnessCategory = x.MilitaryFitnessCategory.title,
                nameOfCommissariat = x.nameOfCommissariat,
                typeMilitaryRegistration = x.TypeMilitaryRegistration.title
            }).FirstOrDefault();

            var vacationsData = _context.VacationShedule.Where(x => x.employeeId == employeeId).Select(x => new VacationModel()
            {
                vacationType = x.VacationType.Title,
                //periodDateStart = ,
                //periodDateEnd = ,
                vacationDays = recruitment.vacationDays,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                vacationEntitlement = x.VacationEntitlement.title
            }).ToArray();

            var certificationData = _context.EnhancingCertification.Where(x => x.employeeId == employeeId).Select(x => new CertificationModel()
            {
                dateCertification = x.date,
                solve = x.solve,
                numberDoc = x.number,
                dateDoc = x.dateDocument,
                reason = x.reason
            }).ToArray();

            var data = employee.Select(x => new EmployeeCardDataModel()
            {
                employeeData = new EmployeeModel()
                {
                    PersonnelNumber = x.PersonnelNumber,
                    FullName = x.FullName,
                },
                personData = personData,
                militaryRegistrationData = militaryRegistrationData,
                contractData = contractData,
                educationData = educationData,
                certificationData = certificationData,
                recruitmentData = recruitmentData,
                vacationData = vacationsData,
                dismissalData = dismissalData
            }).FirstOrDefault();

            return data;
        }

        public class EmployeeCardDataModel
        {
            public EmployeeModel employeeData { get; set; } //сотрудник
            public PersonModel? personData { get; set; } //личные ланные
            public ContractModel? contractData { get; set; } //трудовой договор
            public MilitaryRegistrationModel? militaryRegistrationData { get; set; } //военный учёт
            public EducationModel[]? educationData { get; set; } //образование
            public RecruitmentModel[]? recruitmentData { get; set; } //приём на работу
            public VacationModel[]? vacationData { get; set; }  //отпуск
            public CertificationModel[]? certificationData { get; set; } //аттестация
            public DismissalModel? dismissalData { get; set; } //увольнение
        }

        public class EmployeeModel
        {
            public string? FullName { get; set; }
            public string? PersonnelNumber { get; set; }
        }
        public class EducationModel
        {
            public string? typeOfEducation { get; set; } //образование
            public string? educationalInstitution { get; set; } //Наименование образовательного учреждения
            public int? dateEndEducation { get; set; }
            public string? qualification { get; set; } //Квалификация по документу об образовании
            public string? speciality { get; set; } //специальность по документу об образовании
            public DiplomaDocumentModel? diplomaDocument { get; set; }

        }

        public class CertificationModel
        {
            public int? dateCertification { get; set; } //дата аттестации
            public string? solve { get; set; } //решение комиссии
            public int? numberDoc { get; set; } //номер документа
            public int? dateDoc { get; set; } //дата документа
            public string? reason { get; set; } //основание аттестации
        }
        public class PersonModel
        {
            public string? Birthplace { get; set; }
            public int? DateBirth { get; set; }
            public int? Gender { get; set; }
            public long? INN { get; set; }
            public string? SNILS { get; set; }
            public string? PhoneNumber { get; set; }
            public PersonAdressModel? PersonAdress { get; set; }
            public PassportModel? DocumentPassport { get; set; }
        }
        public class RecruitmentModel
        {
            public int dateOfReceipt { get; set; }
            public string? position { get; set; } //Должность 
            public string? unit { get; set; } //Структурное подразделение
            public string? typeOfEmployment { get; set; } //вид работы
            public decimal? salary { get; set; }
            public string? causeTransferComment { get; set; }

        }
        public class DismissalModel
        {
            public int? dateOfDismissal { get; set; } //дата увольнения
            public string? cause { get; set; } //причина увольнения
        }
        public class MilitaryRegistrationModel
        {
            public string? stockCategory { get; set; } // Категория запаса
            public string? militaryRank { get; set; } //Воинское звание
            public string? militaryProfile { get; set; } //Состав (профиль)
            public string? VUS { get; set; } //ВУС
            public string militaryFitnessCategory { get; set; } //Категория годности к военной службе
            public string? nameOfCommissariat { get; set; }
            public string? typeMilitaryRegistration { get; set; } //Состоит на воинском учете (общем, специальном)
        }
        public class ContractModel
        {
            public string? number { get; set; }
            public int? dateStart { get; set; }
            public int? dateEnd { get; set; }
            public int? RCId { get; set; }
            public string? OtherConditions { get; set; }
        }

        public class DiplomaDocumentModel
        {
            public string? title { get; set; }
            public int? serial { get; set; }
            public int? number { get; set; }
        }
        public class PassportModel
        {
            public int? serial { get; set; }
            public int? number { get; set; }
            public string? DocumentIssued { get; set; } //выдан
            public int? IssuedDate { get; set; } //дата выдачи
        }

        public class PersonAdressModel
        {
            /// <summary>
            /// адрес по прописке 
            /// </summary>
            public string? RegistrationAddress { get; set; }
            /// <summary>
            /// дата регистрации 
            /// </summary>
            public int? RegistrationDate { get; set; }
            /// <summary>
            /// адрес места проживания (фактический)
            /// </summary>
            public string? ResidenceAddress { get; set; }
        }

        public class VacationModel
        {
            public string? vacationType { get; set; } //вид отпуска
            public string? vacationEntitlement { get; set; } //основание
            public int? vacationDays { get; set; } //кол-во дней отпуска (из recruitment)
            public int? dateStart { get; set; }
            public int? dateEnd { get; set; }
            public int? periodDateStart { get; set; } //период работы с 
            public int? periodDateEnd { get; set; } //период работы по

        }


    }
}
