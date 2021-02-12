using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
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
    public class EmployeeSheduleData
    {
        public EmployeeSheduleData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

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
        public Dictionary<int, EmployeeTimeSheeDatesInfo> GetTotal(IEnumerable<TimeSheetDocDocument> data)
        {
            Dictionary<int, EmployeeTimeSheeDatesInfo> total = new Dictionary<int, EmployeeTimeSheeDatesInfo>();
            foreach (var obj in data)
            {
                if (total.ContainsKey(obj.employee.Id))
                {
                    DateTime date = Utils.ConvertFromUnixTimestamp(obj.date);
                    int day = date.Day;
                    if (total[obj.employee.Id].dates.ContainsKey(day))
                    {
                        total[obj.employee.Id].dates[day] += " [" + obj.indicators.code + " " + obj.hours + "]";
                    }
                    else
                    {

                        total[obj.employee.Id].dates.Add(day, " [" + obj.indicators.code + " " + obj.hours + "]");
                    }


                    if (total[obj.employee.Id].total.ContainsKey(obj.indicators.code))
                    {
                        total[obj.employee.Id].total[obj.indicators.code] += obj.hours;
                    }
                    else
                    {
                        total[obj.employee.Id].total.Add(obj.indicators.code, obj.hours);
                    }
                }
                else
                {
                    string position = string.Empty;
                    try
                    {
                        position = obj.employee.Recruitment.Any() ? obj.employee.Recruitment.First().Position.Name : "-";
                    }
                    catch(Exception ex)
                    { 
                        position = "-"; 
                    }

                    Dictionary<string, int> newCodes = new Dictionary<string, int>();
                    newCodes.Add(obj.indicators.code, obj.hours);

                    DateTime date = Utils.ConvertFromUnixTimestamp(obj.date);
                    int day = date.Day;

                    Dictionary<int, string> newMouth = new Dictionary<int, string>();
                    newMouth.Add(day, " [" + obj.indicators.code + " " + obj.hours + "]");

                    EmployeeTimeSheeDatesInfo newEmployee = new EmployeeTimeSheeDatesInfo()
                    {
                        fullname = obj.employee.FullName,
                        positions = position,
                        total = newCodes,
                        dates = newMouth
                    };
                    total.Add(obj.employee.Id, newEmployee);
                }
            }

            return total;
        }

        public byte[] GenerateTimeSheetDoc(int date)
        {
            var dateTime = Utils.ConvertFromUnixTimestamp(date);
            DateTime startMouth = new DateTime(dateTime.Year, dateTime.Month, 1);
            DateTime endMouth = new DateTime(dateTime.Year, dateTime.Month + 1, 1).AddDays(-1);
            int last = (int)((DateTimeOffset)endMouth).ToUnixTimeSeconds();
            int first = (int)((DateTimeOffset)startMouth).ToUnixTimeSeconds();


            //месяц и год
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
                InsertCell(row, 1, ReplaceHexadecimalSymbols("Сотрудник"), CellValues.String, styleNum);
                InsertCell(row, 2, ReplaceHexadecimalSymbols("Должность"), CellValues.String, styleNum);
                for (int i = 1; i < 32; i++)
                {
                    InsertCell(row, i + 2, ReplaceHexadecimalSymbols(i.ToString()), CellValues.String, styleNum);
                }
                InsertCell(row, 34, ReplaceHexadecimalSymbols("Итого"), CellValues.String, styleNum);


                var shedule = GetTimeSheetDocDocument(first, last);

                Dictionary<int, EmployeeTimeSheeDatesInfo> total = GetTotal(shedule);

                UInt32 rowIndex = 2;
                foreach (var sheduleRow in total)
                {
                    row = new Row() { RowIndex = rowIndex };
                    sheetData.Append(row);
                    InsertCell(row, 1, ReplaceHexadecimalSymbols(sheduleRow.Value.fullname), CellValues.String, styleNum);
                    InsertCell(row, 2, ReplaceHexadecimalSymbols(sheduleRow.Value.positions), CellValues.String, styleNum);
                    for (int i = 1; i < 32; i++)
                    {
                        if (!sheduleRow.Value.dates.ContainsKey(i)) {
                            InsertCell(row, i, ReplaceHexadecimalSymbols(""), CellValues.String, styleNum);
                        } else
                        {
                            InsertCell(row, i, ReplaceHexadecimalSymbols(sheduleRow.Value.dates[i]), CellValues.String, styleNum);
                        }
                    }
                    InsertCell(row, 34, ReplaceHexadecimalSymbols(string.Join(" ", sheduleRow.Value.total)), CellValues.String, styleNum);
                    rowIndex++;
                }
                workbookPart.Workbook.Save();
                document.Close();

                //сохранить файл
               // File.WriteAllBytes("D:\\график.xlsx", stream.ToArray());

                return stream.ToArray();
            }
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



        public class EmployeeTimeSheeDatesInfo
        {
            public string fullname { get; set; }
            public string positions { get; set; }
            public Dictionary<string, int> total { get; set; } = new Dictionary<string, int>();
            public Dictionary<int, string> dates { get; set; } = new Dictionary<int, string>();
        }
    }
    public class TimeSheetDocDocument
    {
        public int id { get; set; }
        public Employee employee { get; set; }
        public int date { get; set; }
        public int hours { get; set; }
        public Indicators indicators { get; set; }
        public bool deleted { get; set; }

    }
}
