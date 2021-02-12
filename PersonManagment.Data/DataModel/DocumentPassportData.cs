using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Данные документа
    /// </summary>
    public class DocumentPassportData
    {
        public int Id { get; set; }
        /// <summary>
        /// гражданство
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// Серия
        /// </summary>
        public int Series { get; set; }
        /// <summary>
        /// Номер
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Кем выдан
        /// </summary>
        public string DocumentIssued { get; set; }
        /// <summary>
        /// Дата выдачи
        /// </summary>
        public int IssuedDate { get; set; }
        /// <summary>
        /// Код подразд.
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Срок действия
        /// </summary>
        public int Validity { get; set; }

        public bool Deleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? personDataId { get; set; }
        [ForeignKey("personDataId")]
        public PersonData PersonData { get; set; }

        public Country Country { get; set; }
    }
}
