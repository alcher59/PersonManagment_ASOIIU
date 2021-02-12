using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Начисления
    /// </summary>
    public class Accruals
    {
        public int id { get; set; }
        [Required]
        public int dateOfCreation { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        [Required]
        public string number { get; set; }

        /// <summary>
        /// Тип документа (Начисления)
        /// </summary>
        [Required]
        public int documentAccrualsId { get; set; }
        [ForeignKey("documentAccrualsId")]
        public DocumentAccruals DocumentAccruals { get; set; }

        /// <summary>
        /// Начислено
        /// </summary>
        public decimal? accrued { get; set; }

        /// <summary>
        /// Удержано
        /// </summary>
        public decimal? withheld { get; set; }

        /// <summary>
        /// Сотрудники
        /// </summary>
        public ICollection<AccrualsEmployee> AccrualsEmployee { get; set; }

        /// <summary>
        /// Ответственный
        /// </summary>
        public int? responsibleId { get; set; }
        [InverseProperty("AccrualsResponsible")]
        [ForeignKey("responsibleId")]
        public Employee Responsible { get; set; }
        
        /// <summary>
        /// Комментарий
        /// </summary>
        public string? comment { get; set; }
        public bool deleted { get; set; }


        public ICollection<Awards> Awards { get; set; }
        public ICollection<Payroll> Payroll { get; set; }

        public ICollection<SickLeaves> SickLeaves { get; set; }
        public ICollection<BusinessTrips> BusinessTrips { get; set; }
        public ICollection<Vacations> Vacations { get; set; }

        


    }
}
