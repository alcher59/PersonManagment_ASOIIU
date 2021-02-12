using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using PersonManagment.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static PersonManagment.Data.PersonManagmentData.RecruitmentData;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;

namespace PersonManagment.Data.PersonManagmentData
{
    public class DocData
    {
        public DocData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public fileToSend GeneratePersonalEmployeeCard(int employeeId)
        {
            fileToSend file = new fileToSend();
            EmployeeData empData = new EmployeeData(_context);
            var data = empData.GetDataEmployeeCardDoc(employeeId);
            byte[] result = WordShedule.CreateEmployeeCard(data);
            file.data = result;
            file.name = "Personal Employee Card";
            file.mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            return file;
        }

        public fileToSend GenerateTimeSheet(int date)
        {
            fileToSend file = new fileToSend();
            file.name = "tabel za mesac";
            file.mime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            using (var stream = new MemoryStream())
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                FileVersion fv = new FileVersion();
                fv.ApplicationName = "Microsoft Office Excel";
                worksheetPart.Worksheet = new Worksheet(new SheetData());
                WorkbookStylesPart wbsp = workbookPart.AddNewPart<WorkbookStylesPart>();
                wbsp.Stylesheet = GenerateStyleSheet();
                wbsp.Stylesheet.Save();

                Columns lstColumns = worksheetPart.Worksheet.GetFirstChild<Columns>();
                Boolean needToInsertColumns = false;
                if (lstColumns == null)
                {
                    lstColumns = new Columns();
                    needToInsertColumns = true;
                }

                var dateTime = Utils.ConvertFromUnixTimestamp(date);
                string month_year = $"{dateTime.ToString("MMMMMMMM_yyyy")}";
                DateTime last = new DateTime(dateTime.Year, dateTime.Month + 1, 1).AddDays(-1);
                uint lastDay = (uint)last.Day;

                lstColumns.Append(new Column() { Min = 1, Max = 1, Width = 20, CustomWidth = true });
                for (uint i = 1; i <= lastDay; i++)
                {
                    lstColumns.Append(new Column() { Min = i + 1, Max = i + 1, Width = 20, CustomWidth = true });
                }
                if (needToInsertColumns)
                    worksheetPart.Worksheet.InsertAt(lstColumns, 0);



                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Лист" };
                sheets.Append(sheet);

                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                Row row = new Row() { RowIndex = 1 };
                sheetData.Append(row);

                uint styleNum = 1;
                InsertCell(row, 1, "", CellValues.String, styleNum);
                for (int i = 1; i <= lastDay; i++)
                {
                    InsertCell(row, i + 1, ReplaceHexadecimalSymbols(i.ToString()), CellValues.String, styleNum);
                }

                TimeSheetData tsData = new TimeSheetData(_context);



                UInt32 rowIndex = 2;
                styleNum = 2;


                List<employeeShortData> users = getEmployee(); //все юзеры
                var timeSheet = tsData.GetTimeSheetByDate(date); //данные из timesheet
                var defaultCalendar = tsData.GetProductionCalendarDayByDate(date); // стандартный календарь

                //idUser, (день, значение)
                Dictionary<int, Dictionary<int, string>> dictDay = new Dictionary<int, Dictionary<int, string>>();
                foreach (var user in users)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    DateTime start = new DateTime(dateTime.Year, dateTime.Month, 1);
                    for (int i = 1; i <= lastDay; i++)
                    {
                        if (user.rate != null)
                        {
                            var defaultCalendarDay = defaultCalendar.Where(x => x.date == start).FirstOrDefault();
                            if (defaultCalendarDay == null) break;
                            var hourse = (float)user.rate.Value * defaultCalendarDay.countWorkHours;
                            string resultTimeSheet = defaultCalendarDay.isHoliday ? "В(0)" : $"{ReplaceHexadecimalSymbols($"Ф({hourse})")}";
                            dict.Add(i, resultTimeSheet);
                        } else
                        {
                            dict.Add(i, "");
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
                rowIndex = 2;
                foreach (var user in dictDay)
                {
                    row = new Row() { RowIndex = rowIndex };
                    var nameUser = users.Where(x => x.id == user.Key).FirstOrDefault().name;
                    InsertCell(row, 1, ReplaceHexadecimalSymbols(nameUser), CellValues.String, styleNum);


                    for (int i = 1; i <= lastDay; i++)
                    {
                        var codeWork = user.Value[i];
                        InsertCell(row, i + 1, codeWork, CellValues.String, styleNum);
                    }
                    sheetData.Append(row);
                    rowIndex++;
                }

                workbookPart.Workbook.Save();
                document.Close();

                file.data = stream.ToArray();
            }

            return file;
        }


        public string getStringWorkCodes(int userId, IEnumerable<TimeSheetData.TimeSheetDocumentDataModel> timeSheet, int day)
        {

            var obj = timeSheet.Where(x => x.userId == userId && Utils.ConvertFromUnixTimestamp(x.date).Day == day).ToList();
            string result = "";
            foreach (var days in obj)
            {
                result += $" {ReplaceHexadecimalSymbols($"{days.code}({days.hours})")}";
            }
            return result;
        }
        public List<employeeShortData> getEmployee()
        {
            var result = _context.Employees.Where(x => x.Deleted == false).Select(y => new employeeShortData()
            {
                id = y.Id,
                name = y.FullName,
                rate = _context.Recruitment.Where(z => z.employeeId == y.Id).FirstOrDefault().Contract.rate
            }).ToList();
            return result;
        }



        public PartTimeDataModel GetPartTimeData(int idEmployee)
        {
            var result = _context.Employees.Where(y => y.Id == idEmployee).Select(x => new PartTimeDataModel()
            {
                numContract = _context.Recruitment.FirstOrDefault(x => x.employeeId == idEmployee).Contract.number,
                contractSize = _context.Recruitment.FirstOrDefault(x => x.employeeId == idEmployee).Contract.rate,
                fullName = x.FullName,
                position = x.Recruitment.Any() ? x.Recruitment.First().Position.Name : string.Empty,
                salary = _context.Recruitment.FirstOrDefault(x => x.employeeId == idEmployee).Salary.salary,
                INN = _context.PersonData.FirstOrDefault(x => x.EmployeeId == idEmployee).INN,
                address = _context.PersonData.FirstOrDefault(x => x.EmployeeId == idEmployee).PersonAddress.RegistrationAddress,
                passportDate = _context.DocumentPassportData.FirstOrDefault(x => x.personDataId == _context.PersonData.FirstOrDefault(x => x.EmployeeId == idEmployee).Id).IssuedDate,
                passportIssued = _context.DocumentPassportData.FirstOrDefault(x => x.personDataId == _context.PersonData.FirstOrDefault(x => x.EmployeeId == idEmployee).Id).DocumentIssued,
                passportNumber = _context.DocumentPassportData.FirstOrDefault(x => x.personDataId == _context.PersonData.FirstOrDefault(x => x.EmployeeId == idEmployee).Id).Number,
                passportSerial = _context.DocumentPassportData.FirstOrDefault(x => x.personDataId == _context.PersonData.FirstOrDefault(x => x.EmployeeId == idEmployee).Id).Series,
                dateOfReceipt = _context.Recruitment.FirstOrDefault(x => x.employeeId == idEmployee).dateOfReceipt,
                SNILS = _context.PersonData.FirstOrDefault(x => x.EmployeeId == idEmployee).SNILS
            }).FirstOrDefault();

            return result;
        }

        private static string Translit(string str)
        {
            string[] lat_up = { "A", "B", "V", "G", "D", "E", "Yo", "Zh", "Z", "I", "Y", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "Kh", "Ts", "Ch", "Sh", "Shch", "\"", "Y", "'", "E", "Yu", "Ya", "#" };
            string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", "\"", "y", "'", "e", "yu", "ya", "#" };
            string[] rus_up = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я", "№" };
            string[] rus_low = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я", "№" };
            for (int i = 0; i < lat_up.Length; i++)
            {
                str = str.Replace(rus_up[i], lat_up[i]);
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return str;
        }




        public byte[] FileToByte(IFormFile file)
        {
            byte[] mas = null;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                mas = ms.ToArray();
            }
            return mas;
        }

        public void AddHolidays(IEnumerable<DateTime> list)
        {
            foreach (var date in list)
            {
                var data = _context.ProductionCalendarDay.FirstOrDefault(x => x.date == date);
                data.isHoliday = true;
                data.countWorkHours = 0;
            }
            _context.SaveChanges();
        }

        public void AddShortDay(IEnumerable<DateTime> list, float countHoursShortDay = 7)
        {
            foreach (var date in list)
            {
                var data = _context.ProductionCalendarDay.FirstOrDefault(x => x.date == date);
                data.isShortDay = true;
                data.countWorkHours = countHoursShortDay;
            }
            _context.SaveChanges();
        }


        /// <summary>
        /// првоерка наличия дней года в бд
        /// </summary>
        /// <param name="date"></param>
        public void СheckYear(DateTime date)
        {
            var checkYear = _context.ProductionCalendarDay.FirstOrDefault(x => x.date.Year == date.Year);
            if (checkYear == null)
            {
                AddDateYear(date);
            }
            else
            {
                DefaultYear(date);
            }
        }


        /// <summary>
        /// создание дней года
        /// </summary>
        /// <param name="date"></param>
        public void AddDateYear(DateTime date)
        {
            DateTime newDate = new DateTime(date.Year, 1, 1);
            while (newDate.Year != (date.Year + 1))
            {
                _context.ProductionCalendarDay.Add(new ProductionCalendarDay()
                {
                    date = newDate,
                    countWorkHours = 8
                });
                newDate = newDate.AddDays(1);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// сброс праздников и сокр дней
        /// </summary>
        /// <param name="date"></param>
        public void DefaultYear(DateTime date)
        {
            var data = _context.ProductionCalendarDay.Where(x => x.date.Year == date.Year).ToArray();
            foreach (var obj in data)
            {
                obj.isHoliday = false;
                obj.isShortDay = false;
                obj.countWorkHours = 8;
            }
            _context.SaveChanges();
        }



        public bool LoadCalendar(IFormFile file)
        {
            if (file == null)
            {
                return false;
            }
            var excel = FileToByte(file);
            List<DateTime> holidays = new List<DateTime>();
            List<DateTime> shortDays = new List<DateTime>();

            using (var stream = new MemoryStream())
            {
                stream.Write(excel, 0, excel.Length);

                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(stream, false))
                {
                    var worksheetPart = spreadsheetDocument.WorkbookPart.WorksheetParts.FirstOrDefault();
                    var sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();
                    var rows = sheetData.Elements<Row>().ToArray();
                    var rowHoliday = rows[0];
                    var rowShortDay = rows[1];
                    var cellHoliday = rowHoliday.Elements<Cell>().ToArray();
                    var daysCellsHoliday = cellHoliday.Skip(1).Where(x => !string.IsNullOrEmpty(x.CellReference)).ToArray();
                    var cellShortDay = rowShortDay.Elements<Cell>().ToArray();
                    var daysCellsShortDay = cellShortDay.Skip(1).Where(x => !string.IsNullOrEmpty(x.CellReference)).ToArray();

                    foreach (var cell in daysCellsShortDay)
                    {
                        if (cell.CellValue != null) shortDays.Add(DateTime.FromOADate(double.Parse(cell.CellValue.InnerText)));
                    }

                    foreach (var cell in daysCellsHoliday)
                    {
                        if (cell.CellValue != null) holidays.Add(DateTime.FromOADate(double.Parse(cell.CellValue.InnerText)));
                    }

                }
            }
            if (shortDays.Count == 0 && holidays.Count == 0)
            {
                return false;
            }

            if (shortDays.Count > 0)
            {
                СheckYear(shortDays[0]);
            }
            else
            {
                СheckYear(holidays[0]);
            }

            AddShortDay(shortDays);
            AddHolidays(holidays);
            return true;
        }




        public fileToSend GetPartTimeTemplate(int id) //актуальная версия документа 
        {
            fileToSend file = new fileToSend();
            byte[] docForm = GetDocumentFormById(3); //id графика "трудовой договор для совместителей.docx" в бд 3
            if (docForm == null) return null;

            PartTimeDataModel data = GetPartTimeData(id);
            if (data == null)
            {
                file.data = docForm;
                file.name = "trudovoi dogovor dla sovmestiteley";
                file.mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return file;
            }
            else
            {
                byte[] result = WordShedule.CreatePartTimeDoc(data, docForm);
                file.data = result;
                file.name = Translit(data.fullName) + "  trudovoi dogovor dla sovmestiteley";
                file.mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return file;
            }
        }

        public fileToSend GetPartTimeTemplate(int idEmployee, int idDocument) //старая версия  документа 
        {
            fileToSend file = new fileToSend();
            byte[] docForm = GetOldDocumentFormById(idDocument); //id графика "трудовой договор для совместителей.docx" в бд 3
            if (docForm == null) return null;

            PartTimeDataModel data = GetPartTimeData(idEmployee);
            if (data == null)
            {
                file.data = docForm;
                file.name = "trudovoi dogovor dla sovmestiteley";
                file.mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return file;
            }
            else
            {
                byte[] result = WordShedule.CreatePartTimeDoc(data, docForm);
                file.data = result;
                file.name = Translit(data.fullName) + "  trudovoi dogovor dla sovmestiteley";
                file.mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return file;
            }
        }


        public byte[] GenerateVacationShedule()
        {
            using (var stream = new MemoryStream())
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                FileVersion fv = new FileVersion();
                fv.ApplicationName = "Microsoft Office Excel";
                worksheetPart.Worksheet = new Worksheet(new SheetData());
                WorkbookStylesPart wbsp = workbookPart.AddNewPart<WorkbookStylesPart>();
                wbsp.Stylesheet = GenerateStyleSheet();
                wbsp.Stylesheet.Save();

                Columns lstColumns = worksheetPart.Worksheet.GetFirstChild<Columns>();
                Boolean needToInsertColumns = false;
                if (lstColumns == null)
                {
                    lstColumns = new Columns();
                    needToInsertColumns = true;
                }
                lstColumns.Append(new Column() { Min = 1, Max = 1, Width = 25, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 2, Max = 2, Width = 25, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 3, Max = 3, Width = 25, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 4, Max = 4, Width = 25, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 5, Max = 5, Width = 25, CustomWidth = true });
                if (needToInsertColumns)
                    worksheetPart.Worksheet.InsertAt(lstColumns, 0);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Лист" };
                sheets.Append(sheet);

                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                Row row = new Row() { RowIndex = 1 };
                sheetData.Append(row);

                uint styleNum = 1;
                InsertCell(row, 1, ReplaceHexadecimalSymbols("ФИО"), CellValues.String, styleNum);
                InsertCell(row, 2, ReplaceHexadecimalSymbols("Должность"), CellValues.String, styleNum);
                InsertCell(row, 3, ReplaceHexadecimalSymbols("Дата начала"), CellValues.String, styleNum);
                InsertCell(row, 4, ReplaceHexadecimalSymbols("Дата окончания"), CellValues.String, styleNum);
                InsertCell(row, 5, ReplaceHexadecimalSymbols("Заместитель"), CellValues.String, styleNum);

                VacationSheduleData vacData = new VacationSheduleData(_context);
                var shedule = vacData.GetVacationSheduleForDocument();
                UInt32 rowIndex = 2;
                styleNum = 2;
                foreach (var x in shedule)
                {
                    DateTime start = Utils.ConvertFromUnixTimestamp(x.dateStart);
                    DateTime end = Utils.ConvertFromUnixTimestamp(x.dateEnd);
                    row = new Row() { RowIndex = rowIndex };
                    sheetData.Append(row);

                    InsertCell(row, 1, ReplaceHexadecimalSymbols(x.fullName), CellValues.String, styleNum);
                    InsertCell(row, 2, ReplaceHexadecimalSymbols(x.position), CellValues.String, styleNum);
                    InsertCell(row, 3, ReplaceHexadecimalSymbols(start.ToShortDateString()), CellValues.String, styleNum);
                    InsertCell(row, 4, ReplaceHexadecimalSymbols(end.ToShortDateString()), CellValues.String, styleNum);
                    InsertCell(row, 5, ReplaceHexadecimalSymbols(x.replacementEmployeeName), CellValues.String, styleNum);
                    rowIndex++;
                }

                workbookPart.Workbook.Save();
                document.Close();

                //сохранить файл
                //File.WriteAllBytes("path\\график отпусков.xlsx", stream.ToArray());

                return stream.ToArray();
            }
        }

        public byte[] GetDocumentFormById(int id) //id названия документа (актуальная версия)
        {
            byte[] res = _context.FilesData.FirstOrDefault(y => y.id == (_context.FilesInfoFilesData.FirstOrDefault(x => x.filesInfoId == id && x.isActual == true).filesDataId)).data;
            return res;
        }
        public byte[] GetOldDocumentFormById(int id) //id файла в базе данных  (выбор конкретной старой версит)
        {
            byte[] res = _context.FilesInfoFilesData.FirstOrDefault(x => x.filesDataId == id && x.isActual == false).FilesData.data;
            return res;
        }

        public fileToSend GenerateVacationSheduleDoc()
        {
            VacationSheduleData vacData = new VacationSheduleData(_context);
            fileToSend file = new fileToSend();
            byte[] docForm = GetDocumentFormById(4); //id графика отпусков в бд 4
            if (docForm == null) return null;
            ICollection<VacationSheduleWordModel> data = vacData.GetDataGenerateVacationSheduleDoc();
            if (data == null)
            {
                file.data = docForm;
                file.name = "grafic otpuskov";
                file.mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return file;
            }
            else
            {
                byte[] result = WordShedule.CreateDoc(data, docForm);
                file.data = result;
                file.name = "grafic otpuskov";
                file.mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return file;
            }
        }
        public fileToSend GenerateVacationSheduleDoc(int idOldVersionDocument)
        {
            VacationSheduleData vacData = new VacationSheduleData(_context);
            fileToSend file = new fileToSend();
            byte[] docForm = GetOldDocumentFormById(idOldVersionDocument); //id графика "трудовой договор для совместителей.docx" в бд 3
            ICollection<VacationSheduleWordModel> data = vacData.GetDataGenerateVacationSheduleDoc();
            if (data == null)
            {
                file.data = docForm;
                file.name = "grafic otpuskov";
                file.mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return file;
            }
            else
            {
                byte[] result = WordShedule.CreateDoc(data, docForm);
                file.data = result;
                file.name = "grafic otpuskov";
                file.mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return file;
            }
        }


        public fileToSend GenerateContractDoc(int employeeId)
        {
            fileToSend file = new fileToSend();
            byte[] docForm = GetDocumentFormById(2);
            if (docForm == null) return null;
            RecruitmentData data = new RecruitmentData(_context);
            ContractWordDataModel dataModel = data.GetContractDataDoc(employeeId);
            if (dataModel == null)
            {
                file.data = docForm;
                file.name = "trudovoi dogovor";
                file.mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return file;
            }
            else
            {
                byte[] result = WordShedule.CreateContractDoc(dataModel, docForm);
                file.data = result;
                file.name = Translit(dataModel.fullName) + "  trudovoi dogovor";
                file.mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return file;
            }
        }

        public int UploadTemplate(IFormFile file, string comment)
        {
            byte[] data = null;

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                data = binaryReader.ReadBytes((int)file.Length);
            }

            if (data == null)
            {
                return -1;
            }

            var filesInfo = _context.FilesInfo.Add(new FilesInfo()
            {
                title = file.FileName,
                comment = comment,
            });
            _context.SaveChanges();

            var filesData = _context.FilesData.Add(new FilesData()
            {
                data = data,
                size = data.Length
            });
            _context.SaveChanges();

            _context.FilesInfoFilesData.Add(new FilesInfoFilesData()
            {
                filesInfoId = filesInfo.Entity.id,
                filesDataId = filesData.Entity.id,
                date = (int)Utils.ConvertToUnixTimestamp(DateTime.Now),
                isActual = true,
                version = 1
            });
            _context.SaveChanges();

            return filesData.Entity.id;
        }

        public bool UpdateTemplate(int id, IFormFile newFile)
        {
            byte[] newData = null;

            using (var binaryReader = new BinaryReader(newFile.OpenReadStream()))
            {
                newData = binaryReader.ReadBytes((int)newFile.Length);
            }
            if (newData == null)
            {
                return false;
            }

            var oldVersion = _context.FilesInfoFilesData.FirstOrDefault(x => x.isActual && x.filesInfoId == id).version;

            var actuals = _context.FilesInfoFilesData.Where(x => x.filesInfoId == id);
            foreach (var x in actuals)
                x.isActual = false;

            var filesData = _context.FilesData.Add(new FilesData()
            {
                data = newData,
                size = newData.Length
            });
            _context.SaveChanges();

            var filesInfoFilesData = _context.FilesInfoFilesData.Add(new FilesInfoFilesData()
            {
                filesInfoId = id,
                filesDataId = filesData.Entity.id,
                date = (int)Utils.ConvertToUnixTimestamp(DateTime.Now),
                isActual = true,
                version = oldVersion + 1
            });
            _context.SaveChanges();

            return true;
        }

        public bool UpdateOldTemplate(int id, IFormFile newFile, int version)
        {
            byte[] newData = null;

            using (var binaryReader = new BinaryReader(newFile.OpenReadStream()))
            {
                newData = binaryReader.ReadBytes((int)newFile.Length);
            }
            if (newData == null)
            {
                return false;
            }

            var filesInfoFilesData = _context.FilesInfoFilesData.FirstOrDefault(x => x.filesInfoId == id && x.version == version);


            var filesData = _context.FilesData.FirstOrDefault(x => x.id == filesInfoFilesData.filesDataId);
            filesData.data = newData;
            filesData.size = newData.Length;
            _context.SaveChanges();

            var filesInfo = _context.FilesInfo.FirstOrDefault(x => x.id == id);
            filesInfo.title = newFile.FileName;
            _context.SaveChanges();

            return true;
        }

        static Stylesheet GenerateStyleSheet()
        {
            return new Stylesheet(
                new Fonts(
                    new Font(                                                               // Стиль под номером 0 - Шрифт по умолчанию.
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" }),
                    new Font(                                                               // Стиль под номером 1 - Жирный шрифт Times New Roman.
                        new Bold(),
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),
                    new Font(                                                               // Стиль под номером 2 - Обычный шрифт Times New Roman.
                        new FontSize() { Val = 12 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" })
                ),
                new Fills(
                    new Fill(                                                           // Стиль под номером 0 - Заполнение ячейки по умолчанию.
                        new PatternFill() { PatternType = PatternValues.None }),
                    new Fill(                                                           // Стиль под номером 1 - Заполнение ячейки серым цветом
                        new PatternFill(
                            new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFFFFFFF" } }
                            )
                        { PatternType = PatternValues.Solid }),
                    new Fill(                                                           // Стиль под номером 2 - Заполнение ячейки красным.
                        new PatternFill(
                            new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFFFFFFF" } }
                        )
                        { PatternType = PatternValues.Solid })
                ),
                new Borders(
                    new Border(                                                         // Стиль под номером 0 - Грани.
                        new LeftBorder(),
                        new RightBorder(),
                        new TopBorder(),
                        new BottomBorder(),
                        new DiagonalBorder()),
                    new Border(                                                         // Стиль под номером 1 - Грани
                        new LeftBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Medium },
                        new RightBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Medium },
                        new TopBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Medium },
                        new BottomBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Medium },
                        new DiagonalBorder()),
                    new Border(                                                         // Стиль под номером 2 - Грани.
                        new LeftBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new RightBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Thin },
                        new TopBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new BottomBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                ),
                new CellFormats(
                    new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 },                          // Стиль под номером 0 - The default cell style.  (по умолчанию)
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true }) { FontId = 1, FillId = 2, BorderId = 1, ApplyFont = true },       // Стиль под номером 1 - Bold 
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true }) { FontId = 2, FillId = 0, BorderId = 2, ApplyFont = true }       // Стиль под номером 2 - REgular
                )
            );
        }

        //Добавление Ячейки в строку (На вход подаем: строку, номер колонки, тип значения, стиль)
        static Cell InsertCell(Row row, int cell_num, string val, CellValues type, uint styleIndex = 0)
        {
            Cell refCell = null;
            Cell newCell = new Cell() { CellReference = cell_num.ToString() + ":" + row.RowIndex.ToString(), StyleIndex = styleIndex };
            row.InsertBefore(newCell, refCell);
            // Устанавливает тип значения.
            newCell.CellValue = new CellValue(val);
            newCell.DataType = new EnumValue<CellValues>(type);

            return newCell;
        }

        //Важный метод, при вставки текстовых значений надо использовать.
        //Метод убирает из строки запрещенные спец символы.
        //Если не использовать, то при наличии в строке таких символов, вылетит ошибка.
        static string ReplaceHexadecimalSymbols(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return !string.IsNullOrEmpty(txt) ? Regex.Replace(txt, r, "", RegexOptions.Compiled) : string.Empty;
        }
    }


    public class PartTimeDataModel
    {
        /// <summary>
        /// номер трудового договора 
        /// </summary>
        public string numContract { get; set; }
        /// <summary>
        /// ФИО работника
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public string position { get; set; }


        /// <summary>
        /// вел ставки 
        /// </summary>
        public decimal contractSize { get; set; }

        /// <summary>
        /// должностной оклад
        /// </summary>
        public decimal salary { get; set; }

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

        public int dateOfReceipt { get; set; }
        public string SNILS { get; set; }
    }


    public class employeeShortData
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal? rate { get; set; }
    }

    public class fileToSend
    {
        public string name { get; set; }
        public byte[] data { get; set; }
        public string mime { get; set; }
    }
}
