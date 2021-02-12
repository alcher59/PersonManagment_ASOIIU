using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Таблица-связка для Accruals/Employee
    /// </summary>
    public class AccrualsEmployee
    {
        public int Id { get; set; }

        [Required]
        public int employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }

        [Required]
        public int accrualsId { get; set; }
        [ForeignKey("accrualsId")]
        public Accruals Accruals { get; set; }

        public bool deleted { get; set; }
    }
}
