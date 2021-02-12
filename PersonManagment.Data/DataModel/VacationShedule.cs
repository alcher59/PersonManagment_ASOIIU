using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{


    /// <summary>
    /// График отпусков:
    /// </summary>
    public class VacationShedule
    {
        public int Id { get; set; }

        /// <summary>
        /// сотрудник
        /// </summary>
        public int? employeeId { get; set; }

        [ForeignKey("employeeId")]
        [InverseProperty("VacationSheduleeEmployee")]
        public Employee Employee { get; set; }


        /// <summary>
        /// кто замещает работника
        /// </summary>
        public int? replacementEmployeeId { get; set; }
        [ForeignKey("replacementEmployeeId")]
        [InverseProperty("VacationSheduleeReplacement")]
        public Employee EmployeeReplacement { get; set; }


        /// <summary>
        /// дата начала
        /// </summary>
        [Required]
        public int dateStart { get; set; }

        /// <summary>
        /// дата конца
        /// </summary>
        [Required]
        public int dateEnd { get; set; }

        /// <summary>
        /// Основание (Основной, Доп отпуск по КД)
        /// </summary>
        public int? vacationEntitlementId { get; set; }
        [ForeignKey("vacationEntitlementId")]
        public VacationEntitlement VacationEntitlement { get; set; }

        /// <summary>
        /// Вид отпуска (ежегодный, учебный, без сохранения заработной платы и др.)
        /// </summary>
        public int? vacationTypeId { get; set; }
        [ForeignKey("vacationTypeId")]
        public VacationType VacationType { get; set; }

        /// <summary>
        /// перенос отпуска
        /// </summary>
        public bool vacationTransfer { get; set; }

        /// <summary>
        /// причина переноса
        /// </summary>
        public string? causeTransferComment { get; set; }


        public bool deleted { get; set; }

    }
}
