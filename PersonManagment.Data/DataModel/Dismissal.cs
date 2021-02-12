using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Увольнение
    /// </summary>
    public class Dismissal
    {
        public int id { get; set; }
        [Required]
        public int employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }
        [Required]
        /// <summary>
        /// Дата увольнения
        /// </summary>
        public int dateOfDismissal { get; set; }
        /// <summary>
        /// Основание
        /// </summary>
        public string cause { get; set; }
        public bool deleted { get; set; }
    }
}
