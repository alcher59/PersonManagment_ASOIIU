using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Табель учёта рабочего времени
    /// </summary>
    public class TimeSheet
    {
        public int id { get; set; }
        public int? employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [Required]
        public int date { get; set; }

        /// <summary>
        /// Кол-во часов
        /// </summary>
        [Required]
        public int hours { get; set; }

        /// <summary>
        /// Показатель
        /// </summary>
        [Required]
        public int IndicatorsId { get; set; }
        [ForeignKey("IndicatorsId")]
        public Indicators Indicators { get; set; }
        public bool deleted { get; set; }
    }
}
