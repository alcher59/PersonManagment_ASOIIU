using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonManagment.Data;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;

namespace PersonManagment.Data.PersonManagmentData
{
    public class AccrualsData
    {
        public AccrualsData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public IEnumerable<DisablementIncapacityReasonDataModel> GetDisablementIncapacityReason()
        {
            var res = _context.DisablementIncapacityReason.Where(x => x.deleted == false).Select(x => new DisablementIncapacityReasonDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public DisablementIncapacityReasonDataModel GetDisablementIncapacityReasonById(int id)
        {
            var res = _context.DisablementIncapacityReason.Select(x => new DisablementIncapacityReasonDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).FirstOrDefault(x => x.id == id);

            return res;
        }

       
        public int AddDisablementIncapacityReason(DisablementIncapacityReason data)
        {
            var checkNumber = _context.DisablementIncapacityReason.Any(x => x.title == data.title );
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.DisablementIncapacityReason.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateDisablementIncapacityReason(int id, DisablementIncapacityReason data)
        {
            var res = _context.DisablementIncapacityReason.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.DisablementIncapacityReason.Where(x => x.id != id).Any(x => x.title == data.title);
            if (checkNumber)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }


        public bool DeleteDisablementIncapacityReason(int id)
        {
            var res = _context.DisablementIncapacityReason.FirstOrDefault(x => x.id == id);
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


        public IEnumerable<DocumentAccrualsDataModel> GetDocumentAccruals()
        {
            var res = _context.DocumentAccruals.Where(x => x.deleted == false).Select(x => new DocumentAccrualsDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public DocumentAccrualsDataModel GetDocumentAccrualsById(int id)
        {
            var res = _context.DocumentAccruals.Select(x => new DocumentAccrualsDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).FirstOrDefault(x => x.id == id);

            return res;
        }


        public int AddDocumentAccruals(DocumentAccruals data)
        {
            var checkNumber = _context.DocumentAccruals.Any(x => x.title == data.title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.DocumentAccruals.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateDocumentAccruals(int id, DocumentAccruals data)
        {
            var res = _context.DocumentAccruals.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.DocumentAccruals.Where(x => x.id != id).Any(x => x.title == data.title);
            if (checkNumber)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }


        public bool DeleteDocumentAccruals(int id)
        {
            var res = _context.DocumentAccruals.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        ///

        public IEnumerable<TypeAccrualDataModel> GeTypeAccrual()
        {
            var res = _context.TypeAccrual.Where(x => x.deleted == false).Select(x => new TypeAccrualDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public TypeAccrualDataModel GetTypeAccrualById(int id)
        {
            var res = _context.DocumentAccruals.Select(x => new TypeAccrualDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).FirstOrDefault(x => x.id == id);

            return res;
        }


        public int AddTypeAccrual(TypeAccrual data)
        {
            var checkNumber = _context.DocumentAccruals.Any(x => x.title == data.title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.TypeAccrual.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateTypeAccrual(int id, TypeAccrual data)
        {
            var res = _context.TypeAccrual.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.TypeAccrual.Where(x => x.id != id).Any(x => x.title == data.title);
            if (checkNumber)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }


        public bool DeleteTypeAccrual(int id)
        {
            var res = _context.TypeAccrual.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }


        ///

        public IEnumerable<TypeAwardDataModel> GetTypeAward()
        {
            var res = _context.TypeAward.Where(x => x.deleted == false).Select(x => new TypeAwardDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public TypeAwardDataModel GetTypeAwardById(int id)
        {
            var res = _context.TypeAward.Select(x => new TypeAwardDataModel()
            {
                id = x.id,
                title = x.title,
                deleted = x.deleted
            }).FirstOrDefault(x => x.id == id);

            return res;
        }


        public int AddTypeAward(TypeAward data)
        {
            var checkNumber = _context.TypeAward.Any(x => x.title == data.title);
            if (checkNumber)
            {
                return -1;
            }
            var res = _context.TypeAward.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateTypeAward(int id, TypeAward data)
        {
            var res = _context.TypeAward.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            var checkNumber = _context.TypeAward.Where(x => x.id != id).Any(x => x.title == data.title);
            if (checkNumber)
            {
                return false;
            }
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }


        public bool DeleteTypeAward(int id)
        {
            var res = _context.TypeAward.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }
        ///

        public IEnumerable<PayrollDataModel> GetPayroll()
        {
            var res = _context.Payroll.Where(x => x.deleted == false).Select(x => new PayrollDataModel()
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
                employeeId = x.employeeId, 
                typeAccrualId= x.typeAccrualId ,
                amount = x.amount ,
                periodDateStart = x.periodDateStart ,
                periodDateEnd = x.periodDateEnd ,
                cause = x.cause ,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public PayrollDataModel GetPayrollById(int id)
        {
            var res = _context.Payroll.Select(x => new PayrollDataModel()
            {
                id = x.id,
                accrualId = x.accrualId,
                employeeId = x.employeeId,
                typeAccrualId = x.typeAccrualId,
                amount = x.amount,
                periodDateStart = x.periodDateStart,
                periodDateEnd = x.periodDateEnd,
                cause = x.cause,
                deleted = x.deleted
            }).FirstOrDefault(x => x.id == id);

            return res;
        }


        public int AddPayroll(Payroll data)
        {
            var res = _context.Payroll.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdatePayroll(int id, Payroll data)
        {
            var res = _context.Payroll.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }

            res.accrualId = data.accrualId;
            res.employeeId = data.employeeId;
            res.typeAccrualId = data.typeAccrualId;
            res.amount = data.amount;
            res.periodDateStart = data.periodDateStart;
            res.periodDateEnd = data.periodDateEnd;
            res.cause = data.cause;
            res.deleted = data.deleted;
            _context.SaveChanges();
            return true;
        }


        public bool DeletePayroll(int id)
        {
            var res = _context.Payroll.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }
    }



    public class DisablementIncapacityReasonDataModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool deleted { get; set; }
    }

    public class DocumentAccrualsDataModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool deleted { get; set; }
    }
    public class TypeAccrualDataModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool deleted { get; set; }
    }
    public class TypeAwardDataModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool deleted { get; set; }
    }

    public class PayrollDataModel: AccrualsDataModel
    {
        public int id { get; set; }
        public int accrualId { get; set; }
        public int employeeId { get; set; }
        public int typeAccrualId { get; set; }
        public decimal amount { get; set; }
        public int periodDateStart { get; set; }
        public int periodDateEnd { get; set; }
        public string? cause { get; set; }
        public bool deleted { get; set; }
    }
}
