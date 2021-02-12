using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;

namespace PersonManagment.Data.PersonManagmentData
{
    public class RecruitmentData : IDisposable
    {
        public RecruitmentData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public IEnumerable<RecruitmentDataModel> GetRecruitment()
        {
            var res = _context.Recruitment.Where(x => x.Deleted == false).Select(x => new RecruitmentDataModel()
            {
                Id = x.Id,
                contractId = x.contractId,
                dateOfReceipt = x.dateOfReceipt,
                employeeId = x.employeeId,
                positionId = x.positionId,
                probation = x.probation,
                salarytId = x.salaryId,
                sheduleId = x.sheduleId,
                vacationDays = x.vacationDays,
                unitId = x.unitId,
                typeOfEmploymentId = x.typeOfEmploymentId,
                vacationEntitlementId = x.vacationEntitlementId,
                causeTransferComment = x.causeTransferComment,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public RecruitmentInfoDataModel GetRecruitmentById(int id)
        {
            var res = _context.Recruitment.Select(x => new RecruitmentInfoDataModel()
            {
                EmployeeId = x.employeeId,
                DateOfReceipt = x.dateOfReceipt,
                PositionId = x.positionId,
                UnitId = x.unitId,
                SheduleId = x.sheduleId,
                Probation = x.probation,
                Vacation = new Vacation() { 
                    VacationEntitlementId = x.vacationEntitlementId,
                    VacationDays = x.vacationDays,
                },
                Salary = new Models.Salary() { 
                    Rates = x.Contract.rate,
                    Value = x.Salary.salary
                },
                TypeOfEmploymentId = x.typeOfEmploymentId,
                Contract = new Models.Contract()
                {
                    ContractNumber = x.Contract.number,
                    StartDate = x.Contract.dateStart,
                    FinishDate = x.Contract.dateEnd
                }
            }).FirstOrDefault(y => y.EmployeeId == id);

            return res;
        }

        public bool DeleteRecruitmentById(int id)
        {
            var res = _context.Recruitment.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddRecruitment(RecruitmentInfoDataModel data)
        {
            var existEmployee = _context.Employees.FirstOrDefault(x => x.Id == data.EmployeeId);
            if (existEmployee == null)
            {
                return -1;
            }

            var checkRecruitment = _context.Recruitment.Any(x => x.employeeId == data.EmployeeId && !x.Deleted);

            var checkDismissal = _context.Dismissal.Any(x => x.employeeId == data.EmployeeId && !x.deleted);
            if (!checkDismissal && checkRecruitment)
            {
                return -3;
            }

            var res = _context.Recruitment.Add(new Recruitment() {
                employeeId = data.EmployeeId,
                dateOfReceipt = data.DateOfReceipt,
                positionId = data.PositionId,
                unitId = data.UnitId,
                sheduleId = data.SheduleId,
                typeOfEmploymentId = data.TypeOfEmploymentId,
                probation = data.Probation,
                vacationEntitlementId = data.Vacation.VacationEntitlementId,
                vacationDays = data.Vacation.VacationDays,
                Salary = new DataModel.Salary()
                {
                    salary = data.Salary.Value
                },
                Contract = new DataModel.Contract() { 
                    dateStart = data.Contract.StartDate,
                    dateEnd = data.Contract.FinishDate,
                    number = data.Contract.ContractNumber,
                    rate = data.Salary.Rates
                }
            });

            existEmployee.StatusId = 2;

            _context.SaveChanges();

            return res.Entity.Id;
        }

        public bool UpdateRecruitment(int id, RecruitmentInfoDataModel data)
        {
            var existEmployee = _context.Employees.FirstOrDefault(x => x.Id == id);
            if (existEmployee == null)
            {
                return false;
            }

            var existRecruitment = _context.Recruitment.Include(x => x.Salary).Include(x => x.Contract).FirstOrDefault(x => x.employeeId == id && !x.Deleted);

            var checkDismissal = _context.Dismissal.Any(x => x.employeeId == data.EmployeeId && !x.deleted);
            if (!checkDismissal && existRecruitment == null)
            {
                return false;
            }

            existRecruitment.dateOfReceipt = data.DateOfReceipt;
            existRecruitment.positionId = data.PositionId;
            existRecruitment.unitId = data.UnitId;
            existRecruitment.sheduleId = data.SheduleId;
            existRecruitment.typeOfEmploymentId = data.TypeOfEmploymentId;
            existRecruitment.probation = data.Probation;
            existRecruitment.vacationEntitlementId = data.Vacation.VacationEntitlementId;
            existRecruitment.vacationDays = data.Vacation.VacationDays;
            existRecruitment.Salary.salary = data.Salary.Value;
            existRecruitment.Contract.dateStart = data.Contract.StartDate;
            existRecruitment.Contract.dateEnd = data.Contract.FinishDate;
            existRecruitment.Contract.number = data.Contract.ContractNumber;
            existRecruitment.Contract.rate = data.Salary.Rates;

            _context.SaveChanges();

            return true;
        }

        public IEnumerable<DismissalDataModel> GetDismissal()
        {
            var res = _context.Dismissal.Where(x => x.deleted == false).Select(x => new DismissalDataModel()
            {
                id = x.id,
                employeeId = x.employeeId,
                dateOfDismissal = x.dateOfDismissal,
                cause = x.cause,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public DismissalDataModel GetDismissalById(int id)
        {
            var res = _context.Dismissal.Select(x => new DismissalDataModel()
            {
                id = x.id,
                employeeId = x.employeeId,
                dateOfDismissal = x.dateOfDismissal,
                cause = x.cause,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }

        public bool DeleteDismissalById(int id)
        {
            var res = _context.Dismissal.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddDismissal(Dismissal data)
        {
            //проверяем был ли сотрудник принят на работу (имеется запись в таблице Recruitment)
            if (!_context.Recruitment.Any(x => x.Deleted == false && x.employeeId == data.employeeId))
            {
                //нет записи - значит еще не был принят, уволить нельзя
                return -2;
            }

            //проверяем был ли сотрудник уволен ранее (имеется запись в таблице Dismissal)
            if (_context.Dismissal.Any(x => x.deleted == false && x.employeeId == data.employeeId))
            {
                //уже уволен - уволить повторно нельзя
                return -1;
            }

            //добавлям заипсь об уволнении сотрудника (в таблицу Dismissal)
            var res = _context.Dismissal.Add(data);

            //сохряняем изменения
            _context.SaveChanges();

            //вернуть id новой записи в таблице Dismissal
            return res.Entity.id;
        }

        public bool UpdateDismissal(int id, Dismissal data)
        {
            var res = _context.Dismissal.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.dateOfDismissal = data.dateOfDismissal;
            res.cause = data.cause;
            _context.SaveChanges();
            return true;
        }


        public class DismissalDataModel
        {
            public int id { get; set; }
            public int employeeId { get; set; }
            public int dateOfDismissal { get; set; }
            public string? cause { get; set; }
            public bool deleted { get; set; }
        }

        public IEnumerable<TransferDataModel> GetTransferData(int employeeId)
        {
            var res = _context.Recruitment.Where(x => x.employeeId == employeeId).Select(x => new TransferDataModel()
            {
                Id = x.Id,
                fullName = _context.Employees.FirstOrDefault(y => y.Id == employeeId).FullName,
                dateOfReceipt = x.dateOfReceipt,
                positionId = x.positionId,
                unitId = x.unitId,
                causeTransferComment = x.causeTransferComment,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }
        public int EmployeeTransfer(Recruitment data)
        {
            var checkEmployee = _context.Employees.Any(x => x.Id == data.employeeId);
            if (!checkEmployee)
            {
                return -1;
            }
            var recruitment = _context.Recruitment.FirstOrDefault(x => x.employeeId == data.employeeId && x.isTransfer == false && x.Deleted == false);
            if (recruitment == null || recruitment.dateOfReceipt == data.dateOfReceipt || recruitment.contractId == data.contractId)
            {
                return -1;
            }
            var res = _context.Recruitment.Add(data);
            recruitment.isTransfer = true;
            _context.SaveChanges();
            return res.Entity.Id;
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null) _context.Dispose();
            }

            // Освобождаем неуправляемые ресурсы
        }
        #endregion

        public class RecruitmentDataModel
        {
            public int Id { get; set; }
            public int? employeeId { get; set; }
            public int dateOfReceipt { get; set; }
            public int? positionId { get; set; }
            public int probation { get; set; }
            public int? sheduleId { get; set; }
            public int? vacationEntitlementId { get; set; }
            public int? unitId { get; set; }
            public int? typeOfEmploymentId { get; set; }
            public int vacationDays { get; set; }
            public int? salarytId { get; set; }
            public int? contractId { get; set; }
            public bool Deleted { get; set; }
            public bool isTransfer { get; set; }
            public string? causeTransferComment { get; set; }
        }

        public class TransferDataModel
        {
            public int Id { get; set; }
            public string fullName { get; set; }
            public int dateOfReceipt { get; set; }
            public int? positionId { get; set; }
            public int? unitId { get; set; }
            public string? causeTransferComment { get; set; }
            public bool Deleted { get; set; }
        }

        public IEnumerable<ContractDataModel> GetContract()
        {
            var res = _context.Contract.Where(x => x.Deleted == false).Select(x => new ContractDataModel()
            {
                Id = x.Id,
                number = x.number,
                OtherConditions = x.OtherConditions,
                RCId = x.RCId,
                dateEnd = x.dateEnd,
                dateStart = x.dateStart,
                Deleted = x.Deleted,
                workСontract = x.workСontract
            }).ToArray();

            return res;
        }

        public ContractDataModel GetContractById(int id)
        {
            var res = _context.Contract.Select(x => new ContractDataModel()
            {
                Id = x.Id,
                number = x.number,
                OtherConditions = x.OtherConditions,
                RCId = x.RCId,
                dateEnd = x.dateEnd,
                dateStart = x.dateStart,
                Deleted = x.Deleted,
                workСontract = x.workСontract
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteContractById(int id)
        {
            var res = _context.Contract.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddContract(DataModel.Contract data)
        {
            var checkNumber = _context.Contract.Any(x => x.number == data.number);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.Contract.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateContract(int id, DataModel.Contract data)
        {
            var res = _context.Contract.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.Contract.Where(x => x.Id != id).Any(x => x.number == data.number);
            if (checkNumber)
            {
                return false;
            }
            res.number = data.number;
            res.OtherConditions = data.OtherConditions;
            res.workСontract = data.workСontract;
            res.RCId = data.RCId;
            res.Recruitment = data.Recruitment;
            res.dateEnd = data.dateEnd;
            res.dateStart = data.dateStart;
            _context.SaveChanges();
            return true;
        }


        public class ContractDataModel
        {
            public int Id { get; set; }
            public string number { get; set; }
            public int dateStart { get; set; }
            public int dateEnd { get; set; }
            public int? RCId { get; set; }
            public string OtherConditions { get; set; }
            public bool Deleted { get; set; }
            public string? workСontract { get; set; }
        }

        public IEnumerable<FOTDataModel> GetFOT()
        {
            var res = _context.FOT.Where(x => x.Deleted == false).Select(x => new FOTDataModel()
            {
                Id = x.Id,
                salary = x.salary,
                incentivePayments = x.incentivePayments,
                compensationPayments = x.compensationPayments,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public FOTDataModel GetFOTById(int id)
        {
            var res = _context.FOT.Select(x => new FOTDataModel()
            {
                Id = x.Id,
                salary = x.salary,
                incentivePayments = x.incentivePayments,
                compensationPayments = x.compensationPayments,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteFOTById(int id)
        {
            var res = _context.FOT.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;

        }

        public int AddFOT(FOT data)
        {
            var res = _context.FOT.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateFOT(int id, FOT data)
        {
            var res = _context.FOT.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.salary = data.salary;
            res.incentivePayments = data.incentivePayments;
            res.compensationPayments = data.compensationPayments;
            _context.SaveChanges();
            return true;
        }
        public class FOTDataModel
        {
            public int Id { get; set; }
            public decimal? salary { get; set; }
            public decimal? incentivePayments { get; set; }
            public decimal? compensationPayments { get; set; }
            public bool Deleted { get; set; }
        }
        public IEnumerable<PositionDataModel> GetPosition()
        {
            var res = _context.Positions.Where(x => x.Deleted == false).OrderBy(x => x.Name).Select(x => new PositionDataModel()
            {
                Id = x.Id,
                Name = x.Name,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public PositionDataModel GetPositionById(int id)
        {
            var res = _context.Positions.Select(x => new PositionDataModel()
            {
                Id = x.Id,
                Name = x.Name,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeletePositionById(int id)
        {
            var res = _context.Positions.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddPosition(Position data)
        {
            var checkNumber = _context.Positions.Any(x => x.Name == data.Name);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.Positions.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdatePosition(int id, Position data)
        {
            var res = _context.Positions.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.Positions.Any(x => x.Name == data.Name);
            if (checkNumber)
            {
                return false;
            }
            res.Name = data.Name;
            _context.SaveChanges();
            return true;
        }

        public class PositionDataModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Deleted { get; set; }
        }

        public IEnumerable<ReceptionConditionsDataModel> GetReceptionConditions()
        {
            var res = _context.ReceptionConditions.Where(x => x.Deleted == false).Select(x => new ReceptionConditionsDataModel()
            {
                Id = x.Id,
                placeWork = x.placeWork,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public ReceptionConditionsDataModel GetReceptionConditionsById(int id)
        {
            var res = _context.ReceptionConditions.Select(x => new ReceptionConditionsDataModel()
            {
                Id = x.Id,
                placeWork = x.placeWork,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteReceptionConditionsById(int id)
        {
            var res = _context.ReceptionConditions.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddReceptionConditions(ReceptionConditions data)
        {
            var res = _context.ReceptionConditions.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateReceptionConditions(int id, ReceptionConditions data)
        {
            var res = _context.ReceptionConditions.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.placeWork = data.placeWork;
            _context.SaveChanges();
            return true;
        }

        
        public class ReceptionConditionsDataModel
        {
            public int Id { get; set; }
            public string placeWork { get; set; }
            public bool Deleted { get; set; }
        }
        public IEnumerable<SalaryDataModel> GetSalary()
        {
            var res = _context.Salary.Where(x => x.Deleted == false).Select(x => new SalaryDataModel()
            {
                Id = x.Id,
                salary = x.salary,
                title = x.title,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public SalaryDataModel GetSalaryById(int id)
        {
            var res = _context.Salary.Select(x => new SalaryDataModel()
            {
                Id = x.Id,
                salary = x.salary,
                title = x.title,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteSalaryById(int id)
        {
            var res = _context.Salary.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddSalary(DataModel.Salary data)
        {
            var res = _context.Salary.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateSalary(int id, DataModel.Salary data)
        {
            var res = _context.Salary.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.salary = data.salary;
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }


        public class SalaryDataModel
        {
            public int Id { get; set; }
            public decimal salary { get; set; }
            public string title { get; set; }
            public bool Deleted { get; set; }
        }
        
        public IEnumerable<VacationEntitlementDataModel> GetVacationEntitlement()
        {
            var res = _context.VacationEntitlement.Where(x => x.Deleted == false).Select(x => new VacationEntitlementDataModel()
            {
                Id = x.Id,
                title = x.title,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public VacationEntitlementDataModel GetVacationEntitlementById(int id)
        {
            var res = _context.VacationEntitlement.Select(x => new VacationEntitlementDataModel()
            {
                Id = x.Id,
                title = x.title,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteVacationEntitlementById(int id)
        {
            var res = _context.VacationEntitlement.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;

        }

        public int AddVacationEntitlement(VacationEntitlement data)
        {
            var res = _context.VacationEntitlement.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateVacationEntitlement(int id, VacationEntitlement data)
        {
            var res = _context.VacationEntitlement.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }
        /// <summary>
        /// ///
        /// </summary>
        public IEnumerable<AwardsDataModel> GetAwards()
        {
            var res = _context.Awards.Where(x => x.deleted == false).Select(x => new AwardsDataModel()
            {
                id = x.id ,
                dateOfCreation = x.Accruals.dateOfCreation,
                employees = string.Join(",", x.Accruals.AccrualsEmployee.Select(x => x.Employee.FullName).ToArray()),
                number = x.Accruals.number,
                documentAccruals = x.Accruals.DocumentAccruals.title,
                accrued = x.Accruals.accrued,
                comment = x.Accruals.comment,
                withheld = x.Accruals.withheld,
                accrualId = x.accrualId ,
                employeeId = x.employeeId ,
                typeAwardId = x.typeAwardId ,
                amount = x.amount ,
                deleted = x.deleted
            }).ToArray();

            return res;
        }


        public AwardsDataModel GetAwardsById(int id)
        {
            var res = _context.Awards.Select(x => new AwardsDataModel()
            {
                id = x.id,
                accrualId = x.accrualId,
                employeeId = x.employeeId,
                typeAwardId = x.typeAwardId,
                amount = x.amount,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }

        public bool DeleteAwards(int id)
        {
            var res = _context.Awards.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddAwards(Awards data)
        {
            var res = _context.Awards.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateAwards(int id, Awards data)
        {
            var res = _context.Awards.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.accrualId = data.accrualId;
            res.employeeId = data.employeeId;
            res.typeAwardId = data.typeAwardId;
            res.amount = data.amount;
            res.deleted = data.deleted;
            _context.SaveChanges();
            return true;
        }

        public ContractWordDataModel GetContractDataDoc(int employeeId)
        {
            var recruitment = _context.Recruitment.FirstOrDefault(x => x.employeeId == employeeId && x.Deleted == false);

            if (recruitment == null)
                return null;

            var salary = _context.Salary.FirstOrDefault(x => x.Id == recruitment.salaryId).salary;

            //var mainVacation = _context.VacationShedule.FirstOrDefault(x => x.employeeId == employeeId && x.vacationEntitlementId == 2); //2 - основной

            //var additionalVacation = _context.VacationShedule.FirstOrDefault(x => x.employeeId == employeeId && x.vacationEntitlementId == 3); //3 - дополнительный

            var personData = _context.PersonData.FirstOrDefault(x => x.EmployeeId == employeeId);

            var personAddress = _context.PersonAddress.FirstOrDefault(x => x.Id == personData.PersonAddressId);

            var passport = _context.DocumentPassportData.FirstOrDefault(x => x.personDataId == personData.Id);

            var position = _context.Positions.FirstOrDefault(x => x.Id == recruitment.positionId);

            var employee = _context.Employees.FirstOrDefault(x => x.Id == employeeId);

            var data = _context.Contract.Where(x => x.Id == recruitment.contractId).Select(x => new ContractWordDataModel()
            {
                dateOfReceipt = recruitment.dateOfReceipt,
                fullName = employee.FullName,
                number = x.number,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                position = position.Name,
                rate = x.rate,
                salary = salary * x.rate,
                mainVacationDays = recruitment.vacationDays,
                additionalVacationDays = 0,//Utils.ConvertFromUnixTimestamp(additionalVacation.dateEnd).Subtract(Utils.ConvertFromUnixTimestamp(additionalVacation.dateStart)).Days + 1,
                address = personAddress.RegistrationAddress,
                INN = personData.INN,
                passportNumber = passport.Number,
                passportSerial = passport.Series,
                passportDate = passport.IssuedDate,
                passportIssued = passport.DocumentIssued,
                workСontract = x.workСontract,
                SNILS = personData.SNILS
            }).FirstOrDefault();
            
            return data;
        }

        public class VacationEntitlementDataModel
        {
            public int Id { get; set; }
            public string title { get; set; }
            public bool Deleted { get; set; }
        }

        public class AwardsDataModel: AccrualsDataModel
        {
            public int id { get; set; }
            public int accrualId { get; set; }
            public int employeeId { get; set; }
            public int typeAwardId { get; set; }
            public decimal amount { get; set; }
            public bool deleted { get; set; }
        }

        public class ContractWordDataModel
        {
            /// <summary>
            /// рабочий договор
            /// </summary>
            public string workСontract { get; set; }
            /// <summary>
            /// Договор №
            /// </summary>
            public string number { get; set; }
            /// <summary>
            /// Дата приёма
            /// </summary>
            public int dateOfReceipt { get; set; }
            /// <summary>
            /// ФИО работника
            /// </summary>
            public string fullName { get; set; }
            /// <summary>
            /// Должность
            /// </summary>
            public string position { get; set; }
            /// <summary>
            /// Ставка
            /// </summary>
            public decimal rate { get; set; }
            /// <summary>
            ///Дата начала действия договора
            /// </summary>
            public int dateStart { get; set; }
            /// <summary>
            ///Дата окончания действия договора
            /// </summary>
            public int dateEnd { get; set; }

            //испыт.срок

            /// <summary>
            /// должностной оклад
            /// </summary>
            public decimal salary { get; set; }

            //режим работы

            /// <summary>
            /// кол-во дней основного отпуска
            /// </summary>
            public int mainVacationDays { get; set; }

            /// <summary>
            /// кол-во дней дополнительного отпуска
            /// </summary>
            public int additionalVacationDays { get; set; }

            /// <summary>
            /// адрес
            /// </summary>
            public string address { get; set; }

            /// <summary>
            /// Паспорт: серия
            /// </summary>
            public int passportSerial { get; set; }
            /// <summary>
            /// Паспорт: номер
            /// </summary>
            public int passportNumber { get; set; }
            /// <summary>
            /// Паспорт: дата выдачи
            /// </summary>
            public int passportDate { get; set; }
            /// <summary>
            /// Паспорт: кем выдан
            /// </summary>
            public string passportIssued { get; set; }

            /// <summary>
            /// ИНН
            /// </summary>
            public long INN { get; set; }

            public string SNILS { get; set; }
        }
    }

}

