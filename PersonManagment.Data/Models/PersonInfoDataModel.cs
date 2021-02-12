using PersonManagment.Data.DataModel;

namespace PersonManagment.Data.Models
{
    public class PersonInfoDataModel
    {
        public string Birthplace { get; set; }
        public long INN { get; set; }
        public int InformationСitizenshipDateStart { get; set; }
        public int DocumentTypeId { get; set; }
        public int ValidityDocumentDateStart { get; set; }
        public int EmployeeId { get; set; }
        public int DategBirth { get; set; }
        public int Gender { get; set; }
        public string SNILS { get; set; }
        public int DateBirth { get; set; }
        public DocumentPassportData DocumentPassportData { get; set; }
        public PersonContacts PersonContacts { get; set; }
        public PersonAddress PersonAddress { get; set; }
    }
}
