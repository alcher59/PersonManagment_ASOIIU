using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonManagment.Data;
using PersonManagment.Data.Models;
using PersonManagment.Data.DataModel; 

namespace PersonManagment.Data.PersonManagmentData
{
    public class PersonDataData : IDisposable
    {
        public PersonDataData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public IEnumerable<PersonDataDataModel> GetPersonData()
        {

            var res = _context.PersonData.Where(x => x.Deleted == false).Select(x => new PersonDataDataModel()
            {
                Id = x.Id,
                Birthplace = x.Birthplace,
                INN = x.INN,
                InformationСitizenshipDateStart = x.InformationСitizenshipDateStart,
                DocumentTypeId = x.DocumentTypeId,
                DocumentPassport = _context.DocumentPassportData.Where(y => y.personDataId == x.Id && y.Deleted == false).ToArray(),
                ValidityDocumentDateStart = x.ValidityDocumentDateStart,
                PersonContactsId = x.PersonContactsId,
                PersonAddressId = x.PersonAddressId,
                EmployeeId = x.EmployeeId,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public PersonDataDataModel GetPersonDataById(int id)
        {
            //var doc = _context.DocumentPassportData.Select(x => new DocumentPassportDataDataModel()
            //{
            //	Id = x.Id,
            //	CountryId = x.CountryId,
            //	Series = x.Series,
            //	Number = x.Number,
            //	DocumentIssued = x.DocumentIssued,
            //	IssuedDate = x.IssuedDate,
            //	Code = x.Code,
            //	Validity = x.Validity,
            //	personDataId = x.personDataId,
            //	Deleted = x.Deleted
            //}).Where(y => y.personDataId == id).ToArray();

            var res = _context.PersonData.Select(x => new PersonDataDataModel()
            {
                Id = x.Id,
                Birthplace = x.Birthplace,
                INN = x.INN,
                InformationСitizenshipDateStart = x.InformationСitizenshipDateStart,
                DocumentTypeId = x.DocumentTypeId,
                DocumentPassport = _context.DocumentPassportData.Where(y => y.personDataId == x.Id).ToArray(),
                ValidityDocumentDateStart = x.ValidityDocumentDateStart,
                PersonContactsId = x.PersonContactsId,
                PersonAddressId = x.PersonAddressId,
                EmployeeId = x.EmployeeId,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeletePersonDataById(int id)
        {
            var res = _context.PersonData.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddPersonData(PersonInfoDataModel data)
        {
            var checkNumber = _context.PersonData.Any(x =>
                x.INN == data.INN ||
                x.SNILS == data.SNILS ||
                x.EmployeeId == data.EmployeeId);
            if (checkNumber)
            {
                return -1;
            }

            PersonContacts personContacts = new PersonContacts() {
                PhoneNumber = data.PersonContacts.PhoneNumber,
                HomePhoneNumber = data.PersonContacts.HomePhoneNumber,
                WorkPhoneNumber = data.PersonContacts.WorkPhoneNumber,
                Email = data.PersonContacts.Email
            };

            PersonAddress personAddress = new PersonAddress() {
                RegistrationAddress = data.PersonAddress.RegistrationAddress,
                RegistrationDate = data.PersonAddress.RegistrationDate,
                ResidenceAddress = data.PersonAddress.ResidenceAddress,
                OutsideAddress = data.PersonAddress.OutsideAddress,
                InformationAddress = data.PersonAddress.InformationAddress
            };

            var personData = new DataModel.PersonData() {
                Birthplace = data.Birthplace,
                INN = data.INN,
                InformationСitizenshipDateStart = data.InformationСitizenshipDateStart,
                DocumentTypeId = data.DocumentTypeId,
                ValidityDocumentDateStart = data.ValidityDocumentDateStart,
                PersonContacts = _context.PersonContacts.Add(personContacts).Entity,
                PersonAddress = _context.PersonAddress.Add(personAddress).Entity,
                EmployeeId = data.EmployeeId,
                SNILS = data.SNILS,
                DateBirth = data.DateBirth,
                Gender = data.Gender
            };

            var res = _context.PersonData.Add(personData);
            _context.SaveChanges();

            DocumentPassportData documentPassportData = new DocumentPassportData() {
                Code = data.DocumentPassportData.Code,
                CountryId = data.DocumentPassportData.CountryId,
                Series = data.DocumentPassportData.Series,
                Number = data.DocumentPassportData.Number,
                IssuedDate = data.DocumentPassportData.IssuedDate,
                Validity = data.DocumentPassportData.Validity,
                DocumentIssued = data.DocumentPassportData.DocumentIssued,
                personDataId = res.Entity.Id
            };
            
            _context.DocumentPassportData.Add(documentPassportData);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdatePersonData(int id, PersonInfoDataModel data)
        {
            var person = _context.PersonData.FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return false;
            }
            person.Birthplace = data.Birthplace;
            person.INN = data.INN;
            person.InformationСitizenshipDateStart = data.InformationСitizenshipDateStart;
            person.DocumentTypeId = data.DocumentTypeId;
            person.ValidityDocumentDateStart = data.ValidityDocumentDateStart;
            person.EmployeeId = data.EmployeeId;
            person.Gender = data.Gender;
            person.DateBirth = data.DateBirth;
            person.SNILS = data.SNILS;

            var contacts = _context.PersonContacts.FirstOrDefault(x => x.PersonData.Id == id);
            if (contacts == null)
            {
                return false;
            }
            contacts.PhoneNumber = data.PersonContacts.PhoneNumber;
            contacts.Email = data.PersonContacts.Email;
            contacts.WorkPhoneNumber = data.PersonContacts.WorkPhoneNumber;
            contacts.HomePhoneNumber = data.PersonContacts.HomePhoneNumber;
            
            var adress = _context.PersonAddress.FirstOrDefault(x => x.PersonData.Id == id);
            if (adress == null)
            {
                return false;
            }
            adress.InformationAddress = data.PersonAddress.InformationAddress;
            adress.OutsideAddress = data.PersonAddress.OutsideAddress;
            adress.RegistrationAddress = data.PersonAddress.RegistrationAddress;
            adress.RegistrationDate = data.PersonAddress.RegistrationDate;
            adress.ResidenceAddress = data.PersonAddress.ResidenceAddress;

            var document = _context.DocumentPassportData.FirstOrDefault(x => x.PersonData.Id == id);
            if (document == null)
            {
                return false;
            }
            document.Code = data.DocumentPassportData.Code;
            document.CountryId = data.DocumentPassportData.CountryId;
            document.Series = data.DocumentPassportData.Series;
            document.Number = data.DocumentPassportData.Number;
            document.IssuedDate = data.DocumentPassportData.IssuedDate;
            document.Validity = data.DocumentPassportData.Validity;
            document.DocumentIssued = data.DocumentPassportData.DocumentIssued;
            
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

        public class PersonDataDataModel
        {
            public int Id { get; set; }
            public string Birthplace { get; set; }
            public long INN { get; set; }
            public int InformationСitizenshipDateStart { get; set; }
            public int DocumentTypeId { get; set; }
            public int? DocumentPassportId { get; set; }
            public int ValidityDocumentDateStart { get; set; }
            public int PersonContactsId { get; set; }
            public int PersonAddressId { get; set; }
            public DocumentPassportData[] DocumentPassport { get; set; }
            public bool Deleted { get; set; }
            public int EmployeeId { get; set; }
            public int DategBirth { get; set; }
            public int Gender { get; set; }
            public long SNILS { get; set; }
        }

        //      public class DocumentPassportDataDataModel
        //{
        //	public int Id { get; set; }
        //	public int CountryId { get; set; }
        //	public int Series { get; set; }
        //	public int Number { get; set; }
        //	public string DocumentIssued { get; set; }
        //	public int IssuedDate { get; set; }
        //	public int Code { get; set; }
        //	public int Validity { get; set; }
        //	public int? personDataId { get; set; }
        //	public bool Deleted { get; set; }
        //}

        public IEnumerable<ContryDataModel> GetCountry()
        {
            var res = _context.Country.Where(x => x.Deleted == false).Select(x => new ContryDataModel()
            {
                Id = x.Id,
                Name = x.Name,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public ContryDataModel GetCountryById(int id)
        {
            var res = _context.Country.Select(x => new ContryDataModel()
            {
                Id = x.Id,
                Name = x.Name,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteCountryById(int id)
        {
            var res = _context.Country.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddCountry(Country data)
        {
            var checkNumber = _context.Country.Any(x => x.Name == data.Name);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.Country.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateCountry(int id, Country data)
        {
            var res = _context.Country.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Name = data.Name;
            _context.SaveChanges();
            return true;
        }


        public class ContryDataModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Deleted { get; set; }

        }
        public IEnumerable<DocumentPassportDataDataModel> GetPassportData()
        {
            var res = _context.DocumentPassportData.Where(x => x.Deleted == false).Select(x => new DocumentPassportDataDataModel()
            {
                Id = x.Id,
                CountryId = x.CountryId,
                Series = x.Series,
                Number = x.Number,
                DocumentIssued = x.DocumentIssued,
                IssuedDate = x.IssuedDate,
                Code = x.Code,
                Validity = x.Validity,
                personDataId = x.personDataId,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public DocumentPassportDataDataModel GetPassportDataById(int id)
        {
            var res = _context.DocumentPassportData.Select(x => new DocumentPassportDataDataModel()
            {
                Id = x.Id,
                CountryId = x.CountryId,
                Series = x.Series,
                Number = x.Number,
                DocumentIssued = x.DocumentIssued,
                IssuedDate = x.IssuedDate,
                Code = x.Code,
                Validity = x.Validity,
                personDataId = x.personDataId,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeletePassportDataById(int id)
        {
            var res = _context.DocumentPassportData.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddPassportData(DocumentPassportData data)
        {
            var checkNumber = _context.DocumentPassportData.Any(x => x.Series == data.Series && x.Number == data.Number);
            if (checkNumber)
            {
                return -1;
            }
            else
            {
                var res = _context.DocumentPassportData.Add(data);
                _context.SaveChanges();
                return res.Entity.Id;
            }
        }

        public bool UpdatePassportData(int id, DocumentPassportData data)
        {
            var res = _context.DocumentPassportData.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.CountryId = data.CountryId;
            res.Series = data.Series;
            res.Number = data.Number;
            res.DocumentIssued = data.DocumentIssued;
            res.IssuedDate = data.IssuedDate;
            res.Code = data.Code;
            res.Validity = data.Validity;
            res.personDataId = data.personDataId;
            _context.SaveChanges();
            return true;
        }

        public class DocumentPassportDataDataModel
        {
            public int Id { get; set; }
            public int CountryId { get; set; }
            public int Series { get; set; }
            public int Number { get; set; }
            public string DocumentIssued { get; set; }
            public int IssuedDate { get; set; }
            public int Code { get; set; }
            public int Validity { get; set; }
            public int? personDataId { get; set; }
            public bool Deleted { get; set; }
        }

        public IEnumerable<DocumentTypeDataModel> GetDocumentType()
        {
            var res = _context.DocumentType.Where(x => x.Deleted == false).Select(x => new DocumentTypeDataModel()
            {
                Id = x.Id,
                Title = x.Title,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public DocumentTypeDataModel GetDocumentTypeById(int id)
        {
            var res = _context.DocumentType.Select(x => new DocumentTypeDataModel()
            {
                Id = x.Id,
                Title = x.Title,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteDocumentTypeById(int id)
        {
            var res = _context.DocumentType.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddDocumentType(DocumentType data)
        {
            var checkNumber = _context.DocumentType.Any(x => x.Title == data.Title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.DocumentType.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateDocumentType(int id, DocumentType data)
        {
            var res = _context.DocumentType.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Title = data.Title;
            _context.SaveChanges();
            return true;
        }

        public class DocumentTypeDataModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public bool Deleted { get; set; }
        }
        public IEnumerable<PersonAddressDataModel> GetPersonAddress()
        {
            var res = _context.PersonAddress.Where(x => x.Deleted == false).Select(x => new PersonAddressDataModel()
            {
                Id = x.Id,
                RegistrationAddress = x.RegistrationAddress,
                RegistrationDate = x.RegistrationDate,
                ResidenceAddress = x.ResidenceAddress,
                OutsideAddress = x.OutsideAddress,
                InformationAddress = x.InformationAddress,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public PersonAddressDataModel GetPersonAddressById(int id)
        {
            var res = _context.PersonAddress.Select(x => new PersonAddressDataModel()
            {
                Id = x.Id,
                RegistrationAddress = x.RegistrationAddress,
                RegistrationDate = x.RegistrationDate,
                ResidenceAddress = x.ResidenceAddress,
                OutsideAddress = x.OutsideAddress,
                InformationAddress = x.InformationAddress,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeletePersonAddressById(int id)
        {
            var res = _context.PersonAddress.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddPersonAddress(PersonAddress data)
        {
            var res = _context.PersonAddress.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdatePersonAddress(int id, PersonAddress data)
        {
            var res = _context.PersonAddress.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.RegistrationAddress = data.RegistrationAddress;
            res.RegistrationDate = data.RegistrationDate;
            res.ResidenceAddress = data.ResidenceAddress;
            res.OutsideAddress = data.OutsideAddress;
            res.InformationAddress = data.InformationAddress;
            _context.SaveChanges();
            return true;
        }

        public class PersonAddressDataModel
        {
            public int Id { get; set; }
            public string RegistrationAddress { get; set; }
            public int RegistrationDate { get; set; }
            public string ResidenceAddress { get; set; }
            public string OutsideAddress { get; set; }
            public string InformationAddress { get; set; }
            public bool Deleted { get; set; }
        }
        public IEnumerable<PersonContactsDataModel> GetPersonContacts()
        {
            var res = _context.PersonContacts.Where(x => x.Deleted == false).Select(x => new PersonContactsDataModel()
            {
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                HomePhoneNumber = x.HomePhoneNumber,
                WorkPhoneNumber = x.WorkPhoneNumber,
                Email = x.Email,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public PersonContactsDataModel GetPersonContactsById(int id)
        {
            var res = _context.PersonContacts.Select(x => new PersonContactsDataModel()
            {
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                HomePhoneNumber = x.HomePhoneNumber,
                WorkPhoneNumber = x.WorkPhoneNumber,
                Email = x.Email,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeletePersonContactsById(int id)
        {
            var res = _context.PersonContacts.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddPersonContacts(PersonContacts data)
        {
            var checkNumber = _context.PersonContacts.Any(x => x.Email == data.Email || x.PhoneNumber == data.PhoneNumber);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.PersonContacts.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdatePersonContacts(int id, PersonContacts data)
        {
            var res = _context.PersonContacts.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.PhoneNumber = data.PhoneNumber;
            res.HomePhoneNumber = data.HomePhoneNumber;
            res.WorkPhoneNumber = data.WorkPhoneNumber;
            res.Email = data.Email;
            _context.SaveChanges();
            return true;
        }

        public class PersonContactsDataModel
        {
            public int Id { get; set; }
            public string PhoneNumber { get; set; }
            public string HomePhoneNumber { get; set; }
            public string WorkPhoneNumber { get; set; }
            public string Email { get; set; }
            public bool Deleted { get; set; }
        }

    }
}
