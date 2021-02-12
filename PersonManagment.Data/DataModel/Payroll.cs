using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Начисление зарплаты
    /// </summary>
    public class Payroll
    {
        public int id { get; set; }
        public int accrualId { get; set; }
        [ForeignKey("accrualId")]
        public Accruals Accruals { get; set; }

        public int employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }

        /// <summary>
        /// Тип начисления
        /// </summary>
        public int typeAccrualId { get; set; }
        [ForeignKey("typeAccrualId")]
        public TypeAccrual TypeAccrual { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal amount { get; set; }

        /// <summary>
        /// Дата начала периода
        /// </summary>
        public int periodDateStart { get; set; }

        /// <summary>
        /// Дата окончания периода
        /// </summary>
        public int periodDateEnd { get; set; }

        /// <summary>
        /// Основание
        /// </summary>
        public string? cause { get; set; }
        public bool deleted { get; set; }
    }
}
