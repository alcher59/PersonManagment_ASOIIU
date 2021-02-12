using PersonManagment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Text.RegularExpressions;
using AutoMapper.Configuration.Conventions;
using DocumentFormat.OpenXml.Bibliography;
using static PersonManagment.Data.PersonManagmentData.EmployeeSheduleData;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;

namespace PersonManagment.Data.PersonManagmentData
{
    public class TimeSheetData: IDisposable
    {
        public TimeSheetData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public IEnumerable<TimeSheetDataModel> GetTimeSheet()
        {
            var res = _context.TimeSheet.Where(x => x.deleted == false).Select(x => new TimeSheetDataModel()
            {
                id = x.id,
                employeeId = x.employeeId,
                date = x.date,
                hours = x.hours,
                indicatorsId = x.IndicatorsId,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public TimeSheetDataModel GetTimeSheetById(int id)
        {
            var res = _context.TimeSheet.Select(x => new TimeSheetDataModel()
            {
                id = x.id,
                employeeId = x.employeeId,
                date = x.date,
                hours = x.hours,
                indicatorsId = x.IndicatorsId,
                deleted = x.deleted
            }).FirstOrDefault(y => y.id == id);

            return res;
        }


        public IEnumerable<ProductionCalendarDay> GetProductionCalendarDayByDate(int date)
        {
            var dateTime = Utils.ConvertFromUnixTimestamp(date);
            DateTime first = new DateTime(dateTime.Year, dateTime.Month, 1);
            DateTime last = new DateTime(dateTime.Year, dateTime.Month + 1, 1).AddDays(-1);
            var res = _context.ProductionCalendarDay.Where(x => x.date >= first && x.date <= last).ToArray();
            return res;
        }
        public IEnumerable<TimeSheetDocumentDataModel> GetTimeSheetByDate(int date)
        {
            var dateTime = Utils.ConvertFromUnixTimestamp(date);
            DateTime first = new DateTime(dateTime.Year, dateTime.Month, 1);
            DateTime last = new DateTime(dateTime.Year, dateTime.Month + 1, 1).AddDays(-1);
            uint start = (uint)Utils.ConvertToUnixTimestamp(first);
            uint end = (uint)Utils.ConvertToUnixTimestamp(last);

            var res = _context.TimeSheet.Where(x => x.deleted == false && x.date >= start && x.date <= end).Select(x => new TimeSheetDocumentDataModel()
            {
                id = x.id,
                userId = x.employeeId.Value,
                date = x.date,
                hours = x.hours,
                code = _context.Indicators.FirstOrDefault(y => y.id == x.IndicatorsId).code,
                deleted = x.deleted
            }).ToArray();

            return res;
        }

        public bool DeleteTimeSheetById(int id)
        {
            var res = _context.TimeSheet.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddTimeSheet(TimeSheet data)
        {
            var checkEmployee = _context.Employees.Any(x => x.Id == data.employeeId);
            if (!checkEmployee)
            {
                return -1;
            }
            var res = _context.TimeSheet.Add(data);
            _context.SaveChanges();
            return res.Entity.id;
        }

        public bool UpdateTimeSheet(int id, TimeSheet data)
        {
            var res = _context.TimeSheet.FirstOrDefault(x => x.id == id);
            if (res == null)
            {
                return false;
            }
            res.employeeId = data.employeeId;
            res.date = data.date;
            res.hours = data.hours;
            res.IndicatorsId = data.IndicatorsId;
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
        public IEnumerable<TimeSheetDocDocument> GetTimeSheetDocDocument(int first, int last)
        {
            var res = _context.TimeSheet.Where(x => x.deleted == false && x.date <= last && x.date >= first).Select(x => new TimeSheetDocDocument()
            {
                id = x.id,
                employee = x.Employee,
                date = x.date,
                hours = x.hours,
                indicators = x.Indicators,
            }).ToList();
            return res;
        }

        public StructuredInfo[] GetTimeSheetMonth(int date)
        {
            var dateTime = Utils.ConvertFromUnixTimestamp(date);
            string month_year = $"{dateTime.ToString("MMMMMMMM_yyyy")}";
            DateTime last = new DateTime(dateTime.Year, dateTime.Month + 1, 1).AddDays(-1);
            uint lastDay = (uint)last.Day;


            var dc = new DocData(_context);

            List<employeeShortData> users = dc.getEmployee(); //все юзеры
            var timeSheet = GetTimeSheetByDate(date); //данные из timesheet
            var defaultCalendar = GetProductionCalendarDayByDate(date); // стандартный календарь

            //idUser, (день, (значение, кол))
            Dictionary<int, Dictionary<int, Dictionary<string, float>>> dictDay = new Dictionary<int, Dictionary<int, Dictionary<string, float>>>();
            foreach (var user in users)
            {
                Dictionary<int, Dictionary<string, float>> dict = new Dictionary<int, Dictionary<string, float>>();
                DateTime start = new DateTime(dateTime.Year, dateTime.Month, 1);
                for (int i = 1; i <= lastDay; i++)
                {
                    if (user.rate != null)
                    {
                        var defaultCalendarDay = defaultCalendar.Where(x => x.date == start).FirstOrDefault();
                        if (defaultCalendarDay == null) break;
                        var hourse = (float)user.rate.Value * defaultCalendarDay.countWorkHours;
                        string resultTimeSheet = defaultCalendarDay.isHoliday ? "В" : "Ф";
                        var news = new Dictionary<string, float>();
                        news.Add(resultTimeSheet, hourse);
                        dict.Add(i, news);
                    }
                    else
                    {
                        dict.Add(i, new Dictionary<string, float>());
                    }
                    start = start.AddDays(1);
                }

                dictDay.Add(user.id, dict);
            }

            foreach (var time in timeSheet)
            {
                if (dictDay.ContainsKey(time.userId))
                {
                    var day = Utils.ConvertFromUnixTimestamp(time.date).Day;
                    dictDay[time.userId][day] = getStringWorkCodes(time.userId, timeSheet, day);
                }
            }

            StructuredInfo[] formatedArray = new StructuredInfo[dictDay.Count];
            int iteration = 0;
            foreach (var obj in dictDay)
            {

                formatedArray[iteration] = new StructuredInfo();
                string name = users.Where(x => x.id == obj.Key).FirstOrDefault().name;
                formatedArray[iteration].fullname = name;


                for (int i = 0; i < 31; i++)
                {
                    formatedArray[iteration].indicators[i] = new Dictionary<string, float>();

                    if (obj.Value.ContainsKey(i))
                    {
                        formatedArray[iteration].indicators[i] = obj.Value[i];
                    }
                }
                iteration++;

            }



            return formatedArray;
        }


        public Dictionary<string, float> getStringWorkCodes(int userId, IEnumerable<TimeSheetDocumentDataModel> timeSheet, int day)
        {

            var obj = timeSheet.Where(x => x.userId == userId && Utils.ConvertFromUnixTimestamp(x.date).Day == day).ToList();
            Dictionary<string, float> result = new Dictionary<string, float>();
            foreach (var days in obj)
            {
                if (result.ContainsKey(days.code))
                {
                    result[days.code] += days.hours;
                } else
                {
                    result.Add(days.code, days.hours);
                }
            }
            return result;
        }


        public class StructuredInfo
        {
            public string fullname { get; set; }
            public Dictionary<string, float>[] indicators { get; set; } = new Dictionary<string, float>[31];
        }

        public class TimeSheeDatesInfo
        {
            public string fullname { get; set; }
            public Dictionary<int, Dictionary<string, int>> indicators { get; set; } = new Dictionary<int, Dictionary<string, int>>();
        }

        //public class IndicatorInfo
        //{
        //    public string indicator { get; set; }
        //    public int hours { get; set; }
        //}

        public class TimeSheetDataModel
        {
            public int id { get; set; }
            public int? employeeId { get; set; }
            public int date { get; set; }
            public int hours { get; set; }
            public int indicatorsId { get; set; }
            public bool deleted { get; set; }
        }

        public class TimeSheetDocumentDataModel
        {
            public int id { get; set; }
            public int userId { get; set; }
            public int date { get; set; }
            public int hours { get; set; }
            public string code { get; set; }
            public bool deleted { get; set; }
        }
    }
}
