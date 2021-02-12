using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Командировки
    /// </summary>
    public class BusinessTrips
    {
        public int id { get; set; }
        public int accrualId { get; set; }
        [ForeignKey("accrualId")]
        public Accruals Accruals { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public int dateStart { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public int dateEnd { get; set; }


        /// <summary>
        /// Место назначения
        /// </summary>
        public string destination { get; set; }

        /// <summary>
        /// Организация
        /// </summary>
        public string organization { get; set; }

        /// <summary>
        /// Основание
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// Цель
        /// </summary>
        public string mission { get; set; }


        public bool deleted { get; set; }
    }
}
