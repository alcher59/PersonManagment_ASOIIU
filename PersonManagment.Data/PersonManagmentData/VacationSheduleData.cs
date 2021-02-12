using ClosedXML.Excel;
//using Aspose.Words;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Wordprocessing;
using PersonManagment.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;

namespace PersonManagment.Data.PersonManagmentData
{
    public class VacationSheduleData : IDisposable
    {
        public VacationSheduleData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;
        public IEnumerable<VacationSheduleDataModel> GetVacationShedule()
        {
            var res = _context.VacationShedule.Where(x => x.deleted == false).Select(x => new VacationSheduleDataModel()
            {
                id = x.Id,
                employeeId = x.employeeId,
                replacementEmployeeId = x.replacementEmployeeId,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                vacationEntitlementId = x.vacationEntitlementId,
                vacationTransfer = x.vacationTransfer,
                deleted = x.deleted,
                causeTransferComment = x.causeTransferComment
            }).ToArray();

            return res;
        }
        public IEnumerable<VacationShedule_Document> GetVacationSheduleForDocument()
        {
            var res = _context.VacationShedule.Where(x => x.deleted == false && x.vacationTransfer == false).Select(x => new VacationShedule_Document()
            {
                fullName = _context.Employees.FirstOrDefault(y => y.Id == x.employeeId).FullName,
                replacementEmployeeName = _context.Employees.FirstOrDefault(y => y.Id == x.replacementEmployeeId).FullName,
                position = x.Employee.Recruitment.Any() ? x.Employee.Recruitment.First().Position.Name : string.Empty,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd
            }).ToArray();

            return res;
        }
        public VacationSheduleDataModel GetVacationSheduleById(int id)
        {
            var res = _context.VacationShedule.Where(x => x.deleted == false && x.Id == id).Select(x => new VacationSheduleDataModel()
            {
                id = x.Id,
                employeeId = x.employeeId,
                replacementEmployeeId = x.replacementEmployeeId,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                vacationEntitlementId = x.vacationEntitlementId,
                vacationTransfer = x.vacationTransfer,
                deleted = x.deleted,
                causeTransferComment = x.causeTransferComment
            }).FirstOrDefault();

            return res;
        }

        public bool DeleteVacationShedule(int id)
        {
            var res = _context.VacationShedule.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public bool UpdateVacationShedule(int id, VacationShedule model)
        {
            var res = _context.VacationShedule.FirstOrDefault(x => x.Id == id && x.deleted == false);
            if (res == null)
            {
                return false;
            }
            res.dateStart = model.dateStart;
            res.dateEnd = model.dateEnd;
            res.replacementEmployeeId = model.replacementEmployeeId;
            res.vacationEntitlementId = model.vacationEntitlementId;
            res.vacationTransfer = model.vacationTransfer;
            res.causeTransferComment = model.causeTransferComment;
            _context.SaveChanges();
            return true;
        }
        public int AddVacationShedule(VacationShedule model)
        {
            if (!model.vacationTransfer)
                model.causeTransferComment = null;
            var res = _context.VacationShedule.Add(model);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool TransferVacation(int id, TransferVacationShedule model)
        {
            var res = _context.VacationShedule.FirstOrDefault(x => x.Id == id && x.deleted == false);
            if (res == null)
            {
                return false;
            }
            res.dateStart = model.dateStart;
            res.dateEnd = model.dateEnd;
            res.replacementEmployeeId = model.replacementEmployeeId;
            res.causeTransferComment = model.causeTransferComment;
            res.vacationTransfer = true;
            _context.SaveChanges();
            return true;
        }

        



        public ICollection<VacationSheduleWordModel> GetDataGenerateVacationSheduleDoc()
        {
            var data = _context.Employees.Where(x => x.Deleted == false).Select(x => new VacationSheduleWordModel()
            {

                name = x.FullName,
                personnelNumber = x.PersonnelNumber,
                //structure = x.Unit.Title,
                position = x.Recruitment.Any() ? x.Recruitment.First().Position.Name : string.Empty,
                daysVacation = _context.Recruitment.Where(z => z.employeeId == x.Id).Select(z => z.vacationDays).FirstOrDefault(),
                typeVacation = _context.VacationEntitlement.Where( 
                    y => y.Id == _context.Recruitment.Where(z => z.employeeId == x.Id).Select(z => z.vacationEntitlementId).FirstOrDefault()
                    ).FirstOrDefault().title,
                //period = ,
                startDate = _context.VacationShedule.Where(z => z.employeeId == x.Id).Select(z => z.dateStart).FirstOrDefault(),
                endDate = _context.VacationShedule.Where(z => z.employeeId == x.Id).Select(z => z.dateEnd).FirstOrDefault(),
            }).ToList();
            foreach (var obj in data)
            {
                DateTime dataStart = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(obj.startDate);
                DateTime dataEnd= new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(obj.endDate);
                obj.allDays = (dataEnd - dataStart).Days;
            }

            return data;
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
    }


    public class VacationShedule_Document
    {
        public string fullName { get; set; }
        public string replacementEmployeeName { get; set; }
        public string position { get; set; }
        public int dateStart { get; set; }
        public int dateEnd { get; set; }
    }

    public class TransferVacationShedule
    {
        public int? replacementEmployeeId { get; set; }
        public int dateStart { get; set; }
        public int dateEnd { get; set; }
        public string? causeTransferComment { get; set; }
    }

    public class VacationSheduleDataModel
    {
        public int id { get; set; }
        public int? employeeId { get; set; }
        public int? replacementEmployeeId { get; set; }
        public int dateStart { get; set; }
        public int dateEnd { get; set; }
        public int? vacationEntitlementId { get; set; }

        public bool vacationTransfer { get; set; }
        public bool deleted { get; set; }
        public string? causeTransferComment { get; set; }
    }

    public class VacationSheduleWordModel
    {
        /// <summary>
        /// Фамилия, имя, отчество
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Табельный номер
        /// </summary>
        public string personnelNumber { get; set; }
        /// <summary>
        /// Структурное подразделение
        /// </summary>
        //public string structure { get; set; }
        /// <summary>
        /// Должность (специальность, профессия по штатному расписанию)
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// ежегодный основной оплачиваемый отпуск, календарных дней
        /// </summary>
        public int daysVacation { get; set; }
        /// <summary>
        /// ежегодный дополнительный оплачиваемый отпуск, учебный, без сохранения заработной платы и другие (указать) 
        /// </summary>
        public string typeVacation { get; set; }
        /// <summary>
        /// за период работы
        /// </summary>
        public string period { get; set; }
        /// <summary>
        /// всего календарных дней
        /// </summary>
        public int allDays { get; set; }
        /// <summary>
        /// дата начала
        /// </summary>
        public int startDate { get; set; }
        /// <summary>
        /// дата конца
        /// </summary>
        public int endDate { get; set; }
    }
}
