using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Штатное расписание
    /// </summary>
    public class StaffingTable
    {
        public int Id { get; set; }
        [Required]
        public int? FOTId { get; set; }
        [ForeignKey("FOTId")]
        public FOT FOT { get; set; }
        public bool Deleted { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        public string title { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public int? positionId { get; set; }
        [ForeignKey("positionId")]
        public Position Position { get; set; }


        /// <summary>
        /// График работы
        /// </summary>
        public Shedule Shedule { get; set; }



        /// <summary>
        /// Количество ставок
        /// </summary>
        [Required]
        public int countRates { get; set; }




        public int? employeeId { get; set; }
        [ForeignKey("employeeId")]
        [InverseProperty("StaffingTableEmployee")]
        public Employee EmployeeST { get; set; }


        /// <summary>
        /// Кем утверждена(id сотрудника, null не если утверждена)
        /// </summary>
        public int? acceptEmployeeId { get; set; }
        [InverseProperty("StaffingTableAccept")]
        [ForeignKey("acceptEmployeeId")]
        public Employee EmployeeAccept { get; set; }

        /// <summary>
        /// Дата утверждения(null если не утверждена)
        /// </summary>
        public int? dateAccept { get; set; }

        /// <summary>
        ///Кол-во дней отпуска
        /// </summary>
        [Required]
        public int daysVacation { get; set; }

        /// <summary>
        /// Основание
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
        /// подразделение
        /// </summary>
        public int? unitId { get; set; }
        [ForeignKey("unitId")]
        public Unit Unit { get; set; }

    }
}
