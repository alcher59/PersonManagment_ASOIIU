using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonManagment.Data.Models;

namespace PersonManagment.Data.DataModel
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }


        public DbSet<ProductionCalendarDay> ProductionCalendarDay { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Status> Statuses { get; set; }
       
        public DbSet<PersonData> PersonData { get; set; }
        public DbSet<DocumentPassportData> DocumentPassportData { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }

        public DbSet<PersonAddress> PersonAddress { get; set; }
        public DbSet<PersonContacts> PersonContacts { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<Contract> Contract { get; set; }
        public DbSet<FOT> FOT { get; set; }
        public DbSet<ReceptionConditions> ReceptionConditions { get; set; }
        public DbSet<Recruitment> Recruitment { get; set; }
        public DbSet<Dismissal> Dismissal { get; set; }
        public DbSet<Salary> Salary { get; set; }
        public DbSet<Shedule> Shedule { get; set; }
        public DbSet<StaffingTable> StaffingTable { get; set; }
        public DbSet<VacationEntitlement> VacationEntitlement { get; set; }
        public DbSet<VacationType> VacationType { get; set; }
        public DbSet<Experience> Experience { get; set; }


        public DbSet<AcademicDegrees> AcademicDegrees { get; set; }
        public DbSet<AcademicTitles> AcademicTitles { get; set; }
        public DbSet<DiplomaDocument> DiplomaDocument { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<EducationalInstitution> EducationalInstitution { get; set; }
        public DbSet<EducationDegrees> EducationDegrees { get; set; }
        public DbSet<EducationTitles> EducationTitles { get; set; }
        public DbSet<Qualification> Qualification { get; set; }
        public DbSet<Specialty> Specialty { get; set; }
        public DbSet<TypeOfEducation> TypeOfEducation { get; set; }

        public DbSet<WorkPlace> WorkPlace { get; set; }
        public DbSet<ExperienceWork> ExperienceWork { get; set; }
        public DbSet<VacationShedule> VacationShedule { get; set; }

        public DbSet<DisablementIncapacityReason> DisablementIncapacityReason { get; set; }
        public DbSet<DocumentAccruals> DocumentAccruals { get; set; }
        public DbSet<TypeAccrual> TypeAccrual { get; set; }
        public DbSet<TypeAward> TypeAward { get; set; }

        public DbSet<Accruals> Accruals { get; set; }
        public DbSet<Awards> Awards { get; set; }
        public DbSet<Payroll> Payroll { get; set; }
        public DbSet<SickLeaves> SickLeaves { get; set; }
        public DbSet<BusinessTrips> BusinessTrips { get; set; }
        public DbSet<Vacations> Vacations { get; set; }

        public DbSet<TimeSheet> TimeSheet { get; set; }
        public DbSet<Indicators> Indicators { get; set; }

        public DbSet<AccrualsEmployee> AccrualsEmployee { get; set; }

        public DbSet<MilitaryRegistration> MilitaryRegistration { get; set; }
        public DbSet<MilitaryFitnessCategory> MilitaryFitnessCategory { get; set; }
        public DbSet<MilitaryProfile> MilitaryProfile { get; set; }
        public DbSet<MilitaryRank> MilitaryRank { get; set; }
        public DbSet<StockCategory> StockCategory { get; set; }
        public DbSet<TypeMilitaryRegistration> TypeMilitaryRegistration { get; set; }


        public DbSet<EnhancingCertification> EnhancingCertification { get; set; }

        public DbSet<FilesInfo> FilesInfo { get; set; }
        public DbSet<FilesData> FilesData { get; set; }
        public DbSet<FilesInfoFilesData> FilesInfoFilesData { get; set; }

        public DbSet<Unit> Unit { get; set; }
        public DbSet<TypeOfEmployment> TypeOfEmployment { get; set; }

        public DbSet<Request> Request { get; set; }
        public DbSet<RequestCategory> RequestCategory { get; set; }
    }
}
