using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Работник
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// Таб. Номер
        /// </summary>
        public string PersonnelNumber { get; set; }

        public MilitaryRegistration MilitaryRegistration { get; set; }

        /// <summary>
        /// Оклад
        /// </summary>
        //  public decimal Salary { get; set; }

        /// <summary>
        /// Состояние
        /// </summary>
        [Required]
        public int StatusId { get; set; } = 1;
        [ForeignKey("StatusId")]
        public Status Status { get; set; }


        public bool Deleted { get; set; }


        public ICollection<Recruitment> Recruitment { get; set; }
        public ICollection<Dismissal> Dismissal { get; set; }

        [InverseProperty("EmployeeReplacement")]
        public ICollection<VacationShedule> VacationSheduleeReplacement { get; set; }

        [InverseProperty("Employee")]
        public ICollection<VacationShedule> VacationSheduleeEmployee { get; set; }

        public Experience Experience { get; set; }

       

        public ICollection<Awards> Awards { get; set; }
        public ICollection<Payroll> Payroll { get; set; }

        /// <summary>
        /// Сотрудники
        /// </summary>
        public ICollection<AccrualsEmployee> AccrualsEmployee { get; set; }


        ///Ответственный
        [InverseProperty("Responsible")]
        public ICollection<Accruals> AccrualsResponsible { get; set; }


        public ICollection<EnhancingCertification> EnhancingCertification { get; set; }



        [InverseProperty("EmployeeAccept")]
        public ICollection<StaffingTable> StaffingTableAccept { get; set; }

        [InverseProperty("EmployeeST")]
        public ICollection<StaffingTable> StaffingTableEmployee { get; set; }

        public Request Request { get; set; }
    }
}
