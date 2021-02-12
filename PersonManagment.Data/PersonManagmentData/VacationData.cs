using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonManagment.Data;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;

namespace PersonManagment.Data.PersonManagmentData
{
    public class VacationData
    {
        public VacationData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public IEnumerable<BusinessTripsDataModel> GetBusinessTrips()
        {
            var res = _context.BusinessTrips.Where(x => x.deleted == false).Select(x => new BusinessTripsDataModel()
            {
                id = x.id,
                dateOfCreation = x.Accruals.dateOfCreation,
                employees = string.Join(",", x.Accruals.AccrualsEmployee.Select(x => x.Employee.FullName).ToArray()),
                number = x.Accruals.number,
                documentAccruals = x.Accruals.DocumentAccruals.title,
                accrued = x.Accruals.accrued,
                comment = x.Accruals.comment,
                withheld = x.Accruals.withheld,
                accrualId = x.accrualId,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                deleted = x.deleted,
                destination = x.destination,
                organization = x.organization,
                reason = x.reason,
                mission = x.mission
            }).ToArray();

            return res;
        }

        public BusinessTripsDataModel GetBusinessTripsById(int id)
        {
            var res = _context.BusinessTrips.Select(x => new BusinessTripsDataModel()
            {
                id = x.id,
                accrualId = x.accrualId,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                deleted = x.deleted,
                destination = x.destination,
                organization = x.organization,
                reason = x.reason,
                mission = x.mission
            }).FirstOrDefault(x => x.id == id);

            return res;
        }


        public int AddBusinessTrips(BusinessTrips data)
        {
            var res = _context.BusinessTrips.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateBusinessTrips(int id, BusinessTrips data)
        {
            var res = _context.BusinessTrips.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.accrualId = data.accrualId;
            res.dateStart = data.dateStart;
            res.dateEnd = data.dateEnd;
            res.destination = data.destination;
            res.organization = data.organization;
            res.reason = data.reason;
            res.mission = data.mission;
            _context.SaveChanges();
            return true;
        }


        public bool DeleteBusinessTrips(int id)
        {
            var res = _context.BusinessTrips.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
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
        }
        #endregion

        public IEnumerable<SickLeavesDataModel> GetSickLeaves()
        {
            var res = _context.SickLeaves.Where(x => x.deleted == false).Select(x => new SickLeavesDataModel()
            {
                id = x.id,
                dateOfCreation = x.Accruals.dateOfCreation,
                employees = string.Join(",", x.Accruals.AccrualsEmployee.Select(x => x.Employee.FullName).ToArray()),
                number = x.Accruals.number,
                documentAccruals = x.Accruals.DocumentAccruals.title,
                accrued = x.Accruals.accrued,
                comment = x.Accruals.comment,
                withheld = x.Accruals.withheld,
                disablementIncapacityReasonId = x.disablementIncapacityReasonId,
                accrualId = x.accrualId,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public SickLeavesDataModel GetSickLeavesById(int id)
        {
            var res = _context.SickLeaves.Select(x => new SickLeavesDataModel()
            {
                id = x.id,
                disablementIncapacityReasonId = x.disablementIncapacityReasonId,
                accrualId = x.accrualId,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                deleted = x.deleted
            }).FirstOrDefault(x => x.id == id);

            return res;
        }


        public int AddSickLeaves(SickLeaves data)
        {
            var res = _context.SickLeaves.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateSickLeaves(int id, SickLeaves data)
        {
            var res = _context.SickLeaves.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.disablementIncapacityReasonId = data.disablementIncapacityReasonId;
            res.accrualId = data.accrualId;
            res.dateStart = data.dateStart;
            res.dateEnd = data.dateEnd;
            _context.SaveChanges();
            return true;
        }


        public bool DeleteSickLeaves(int id)
        {
            var res = _context.SickLeaves.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<VacationsDataModel> GetVacations()
        {
            var res = _context.Vacations.Where(x => x.deleted == false).Select(x => new VacationsDataModel()
            {
                id = x.id,
                dateOfCreation = x.Accruals.dateOfCreation,
                employees = string.Join(",", x.Accruals.AccrualsEmployee.Select(x => x.Employee.FullName).ToArray()),
                number = x.Accruals.number,
                documentAccruals = x.Accruals.DocumentAccruals.title,
                accrued = x.Accruals.accrued,
                comment = x.Accruals.comment,
                withheld = x.Accruals.withheld,
                vacationEntitlementId = x.vacationEntitlementId,
                accrualId = x.accrualId,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public VacationsDataModel GetVacationsById(int id)
        {
            var res = _context.Vacations.Select(x => new VacationsDataModel()
            {
                id = x.id,
                vacationEntitlementId = x.vacationEntitlementId,
                accrualId = x.accrualId,
                dateStart = x.dateStart,
                dateEnd = x.dateEnd,
                deleted = x.deleted
            }).FirstOrDefault(x => x.id == id);

            return res;
        }


        public int AddVacations(Vacations data)
        {
            var res = _context.Vacations.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateVacations(int id, Vacations data)
        {
            var res = _context.Vacations.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.vacationEntitlementId = data.vacationEntitlementId;
            res.accrualId = data.accrualId;
            res.dateStart = data.dateStart;
            res.dateEnd = data.dateEnd;
            _context.SaveChanges();
            return true;
        }


        public bool DeleteVacations(int id)
        {
            var res = _context.Vacations.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public ICollection<AccrualsDataModel> GetAllAccurals()
        {
            var res = _context.Accruals.Where(x => x.deleted == false).Select(x => new AccrualsDataModel()
            {
                id = x.id,
                dateOfCreation = x.dateOfCreation,
                employees = string.Join(",", x.AccrualsEmployee.Select(x => x.Employee.FullName).ToArray()),
                number = x.number,
                documentAccruals = x.DocumentAccruals.title,
                accrued = x.accrued,
                comment = x.comment,
                withheld = x.withheld
            }).ToArray();


            return res;
        }

    }

    public class BusinessTripsDataModel: AccrualsDataModel
    {
        public int id { get; set; }
        public int accrualId { get; set; }
        public int dateStart { get; set; }
        public int dateEnd { get; set; }
        public bool deleted { get; set; }
        public string destination { get; set; }
        public string organization { get; set; }
        public string reason { get; set; }
        public string mission { get; set; }
    }
    public class SickLeavesDataModel: AccrualsDataModel
    {
        public int id { get; set; }
        public int accrualId { get; set; }
        public int disablementIncapacityReasonId { get; set; }
        public int dateStart { get; set; }
        public int dateEnd { get; set; }
        public bool deleted { get; set; }
    }

    public class VacationsDataModel: AccrualsDataModel
    {
        public int id { get; set; }
        public int accrualId { get; set; }
        public int dateStart { get; set; }
        public int dateEnd { get; set; }
        public int vacationEntitlementId { get; set; }
        public bool deleted { get; set; }
    }

    public class AccrualsDataModel
    {
        public int id { get; set; }
        public int dateOfCreation { get; set; }
        //сотрудник?
        public string employees { get; set; }
        public string number { get; set; }
        public string documentAccruals { get; set; }
        public decimal? accrued { get; set; }
        public decimal? withheld { get; set; }
        public string comment { get; set; }
    }

}
