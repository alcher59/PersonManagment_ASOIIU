using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Приём на работу
    /// </summary>
    public class Recruitment
    {
        public int Id { get; set; }
        public int employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }

        /// <summary>
        /// Дата приёма
        /// </summary>
        [Required]
        public int dateOfReceipt { get; set; }

        public int positionId { get; set; }
        [ForeignKey("positionId")]
        public Position Position { get; set; }

        public int sheduleId { get; set; }
        [ForeignKey("sheduleId")]
        public Shedule Shedule { get; set; }

        /// <summary>
        /// основание
        /// </summary>
        public int vacationEntitlementId { get; set; }
        [ForeignKey("vacationEntitlementId")]
        public VacationEntitlement VacationEntitlement { get; set; }

        /// <summary>
        /// Вид отпуска (ежегодный, учебный, без сохранения заработной платы и др.)
        /// </summary>
        public int? vacationTypeId { get; set; }
        [ForeignKey("vacationTypeId")]
        public VacationType VacationType { get; set; }

        public int unitId { get; set; }
        [ForeignKey("unitId")]
        public Unit Unit { get; set; }

        public int typeOfEmploymentId { get; set; }
        [ForeignKey("typeOfEmploymentId")]
        public TypeOfEmployment TypeOfEmployment { get; set; }

        /// <summary>
        /// Испытательный срок (мес.)
        /// </summary>
        [Required]
        public int probation { get; set; }

        /// <summary>
        /// Кол-во дней отпуска
        /// </summary>
        [Required]
        public int vacationDays { get; set; }
        
        /// <summary>
        /// Оплата труда
        /// </summary> 
        public int salaryId { get; set; }
        [ForeignKey("salaryId")]
        public Salary Salary { get; set; }

        public int? contractId { get; set; }
        [ForeignKey("contractId")]
        public Contract Contract { get; set; }

        public bool Deleted { get; set; }

        /// <summary>
        /// перевод сотрудника (true - сотрудник был переведен, запись является архивной)
        /// </summary> 
        public bool isTransfer { get; set; }

        /// <summary>
        /// причина перевода
        /// </summary> 
        public string causeTransferComment { get; set; }
    }
}
