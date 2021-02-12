namespace PersonManagment.Data.Models
{
    public class RecruitmentInfoDataModel
    {
        public int EmployeeId { get; set; }
        public int PositionId { get; set; }
        public int DateOfReceipt { get; set; }
        public int UnitId { get; set; }
        public int TypeOfEmploymentId { get; set; }
        public int SheduleId { get; set; }
        public int Probation { get; set; }
        public Contract Contract { get; set; }
        public Salary Salary { get; set; }
        public Vacation Vacation { get; set; }
    }
    public class Contract
    {
        public string ContractNumber { get; set; }
        public int StartDate { get; set; }
        public int FinishDate { get; set; }
    }

    public class Salary
    {
        public decimal Value { get; set; }
        public decimal Rates { get; set; }
    }

    public class Vacation
    {
        public int VacationEntitlementId { get; set; }
        public int VacationDays { get; set; }
    }
}
