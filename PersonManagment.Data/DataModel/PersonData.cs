using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Персональные данные
    /// </summary>
    public class PersonData
    {
        public int Id { get; set; }
        
        //гражданство 
        /// <summary>
        /// место рождения
        /// </summary>
        [Required]
        public string Birthplace { get; set; }
        /// <summary>
        /// ИНН
        /// </summary>
        public long INN { get; set; }
        /// <summary>
        /// сведения о гражданстве действуют с 
        /// </summary>
        public int InformationСitizenshipDateStart { get; set; }

        //Документ
        /// <summary>
        /// Вид документа
        /// </summary>
        [Required]
        public int DocumentTypeId { get; set; }
        [ForeignKey("DocumentTypeId")]
        public DocumentType DocumentType { get; set; }
        /// <summary>
        /// паспорт
        /// </summary>
        // public int? DocumentPassportId { get; set; }
        //[ForeignKey("DocumentPassportId")]
        //  public DocumentPassportData DocumentPassportData { get; set; }

        public ICollection<DocumentPassportData> DocumentPassportData { get; set; }

        /// <summary>
        /// Количество документов
        /// </summary>
        public int? countDocument { get; set; }

        /// <summary>
        ///Сведенеия о документе действуют с 
        /// </summary>
        public int ValidityDocumentDateStart { get; set; }

        //Адреса, телефоны
        /// <summary>
        /// Контакты
        /// </summary>
        [Required]
        public int PersonContactsId { get; set; }
        [ForeignKey("PersonContactsId")]
        public PersonContacts PersonContacts { get; set; }
        /// <summary>
        /// Адреса
        /// </summary>
        [Required]
        public int PersonAddressId { get; set; }
        [ForeignKey("PersonAddressId")]
        public PersonAddress PersonAddress { get; set; }
        public bool Deleted { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        /// <summary>
        ///  Дата рождения
        /// </summary>
        public int DateBirth { get; set; }
        /// <summary>
        ///  Пол
        /// </summary>
        public int Gender { get; set; }
        /// /// <summary>
        /// СНИЛС
        /// </summary>
        public string SNILS { get; set; }
    }
}
