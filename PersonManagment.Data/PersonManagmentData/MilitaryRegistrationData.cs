using PersonManagment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;

namespace PersonManagment.Data.PersonManagmentData
{
    public class MilitaryRegistrationData
    {
        public MilitaryRegistrationData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public IEnumerable<MilitaryRegistrationDataModel> GetMilitaryRegistration()
        {
            var res = _context.MilitaryRegistration.Where(x => x.deleted == false).Select(x => new MilitaryRegistrationDataModel()
            {
                id = x.id,
                stockCategoryId = x.stockCategoryId,
                typeMilitaryRegistrationId = x.typeMilitaryRegistrationId,
                militaryRankId = x.militaryRankId,
                militaryFitnessCategoryId = x.militaryFitnessCategoryId,
                militaryProfileId = x.militaryProfileId,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public MilitaryRegistrationDataModel GetMilitaryRegistrationById(int id)
        {
            var res = _context.MilitaryRegistration.Select(x => new MilitaryRegistrationDataModel()
            {
                id = x.id,
                stockCategoryId = x.stockCategoryId,
                typeMilitaryRegistrationId = x.typeMilitaryRegistrationId,
                militaryRankId = x.militaryRankId,
                militaryFitnessCategoryId = x.militaryFitnessCategoryId,
                militaryProfileId = x.militaryProfileId,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }

        public bool DeleteMilitaryRegistrationById(int id)
        {
            var res = _context.MilitaryRegistration.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddMilitaryRegistration(MilitaryRegistration data)
        {
            var checkNumber = _context.MilitaryRegistration.Any(x => x.employeeId == data.employeeId);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.MilitaryRegistration.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateMilitaryRegistration(int id, MilitaryRegistration data)
        {
            var res = _context.MilitaryRegistration.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.MilitaryRegistration.Where(x => x.id != id).Any(x => x.employeeId == data.employeeId);
            if (checkNumber)
            {
                return false;
            }
            res.stockCategoryId = data.stockCategoryId;
            res.typeMilitaryRegistrationId = data.typeMilitaryRegistrationId;
            res.militaryRankId = data.militaryRankId;
            res.militaryFitnessCategoryId = data.militaryFitnessCategoryId;
            res.militaryProfileId = data.militaryProfileId;
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

            // Освобождаем неуправляемые ресурсы
        }
        #endregion

        public class MilitaryRegistrationDataModel
        {
            public int id { get; set; }
            public int? employeeId { get; set; }
            public int stockCategoryId { get; set; }
            public int militaryRankId { get; set; }
            public int militaryProfileId { get; set; }
            public string? VUS { get; set; }
            public int militaryFitnessCategoryId { get; set; }
            public string? nameOfCommissariat { get; set; }
            public int? typeMilitaryRegistrationId { get; set; }
            public bool deleted { get; set; }
        }
    }
}
