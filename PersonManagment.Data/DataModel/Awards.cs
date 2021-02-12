using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Премии
    /// </summary>
    public class Awards
    {

        public int id { get; set; }
        public int accrualId { get; set; }
        [ForeignKey("accrualId")]
        public Accruals Accruals { get; set; }

        public int employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }

        /// <summary>
        /// Вид премии
        /// </summary>
        public int typeAwardId { get; set; }
        [ForeignKey("typeAwardId")]
        public TypeAward TypeAward { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal amount { get; set; }
        public bool deleted { get; set; }
    }
}
