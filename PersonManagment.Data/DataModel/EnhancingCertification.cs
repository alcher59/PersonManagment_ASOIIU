using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    ///  информации о повышении аттестации
    /// </summary>
    public class EnhancingCertification
    {

        public int id { get; set; }

        public int? employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }

        /// <summary>
        /// Дата аттестации
        /// </summary>
        public int date { get; set; }

        /// <summary>
        /// Решение комиссии
        /// </summary>
        public string solve { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public int number { get; set; }
        /// <summary>
        /// Дата документа
        /// </summary>
        public int dateDocument { get; set; }

        /// <summary>
        /// Основание
        /// </summary>
        public string reason { get; set; }


        public bool deleted { get; set; }

    }
}
