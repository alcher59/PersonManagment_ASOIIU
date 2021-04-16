namespace PersonManagment.Data.Models
{
    public class EmployeeInfoDataModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PersonnelNumber { get; set; }
        public PersonData PersonData { get; set; }
        public int Status { get; set; }
    }

    public class PersonData
    {
        public int Id { get; set; }
        public int DateBirth { get; set; }
        public int Gender { get; set; }
        public long INN { get; set; }
        public string SNILS { get; set; }
        public int Citizenship { get; set; }
        public string Birthplace { get; set; }
        public int DocumentTypeId { get; set; }
        public PassportData PassportData { get; set; }
        public Contacts Contacts { get; set; }
    }
    public class PassportData
    {
        public int Id { get; set; }
        public int Series { get; set; }
        public int Number { get; set; }
        public string Division { get; set; }
        public int ValidityDocumentDateStart { get; set; }
        public int Code { get; set; }
        public int InformationСitizenshipDateStart { get; set; }
        public int DocumentPassportId { get; set; }
    }

    public class Contacts
    {
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
