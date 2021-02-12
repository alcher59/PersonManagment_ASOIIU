using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using PersonManagment.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using PersonManagment.Data.DataModel;
using PersonManagment.Data.Models;

namespace PersonManagment.Data.PersonManagmentData
{
    public class StaffingTableData
    {
        public StaffingTableData(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public IEnumerable<SheduleDataModel> GetShedule()
        {
            var res = _context.Shedule.Where(x => x.Deleted == false).Select(x => new SheduleDataModel()
            {
                Id = x.Id,
                StaffingTableId = x.StaffingTableId,
                title = x.title,
                Deleted = x.Deleted
            }).ToArray();

            return res;
        }

        public SheduleDataModel GetSheduleById(int id)
        {
            var res = _context.Shedule.Select(x => new SheduleDataModel()
            {
                Id = x.Id,
                StaffingTableId = x.StaffingTableId,
                title = x.title,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteSheduleById(int id)
        {
            var res = _context.Shedule.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddShedule(Shedule data)
        {
            var res = _context.Shedule.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateShedule(int id, Shedule data)
        {
            var res = _context.Shedule.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.StaffingTableId = data.StaffingTableId;
            res.title = data.title;
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<StaffingTableDataModel> GetStaffingTable()
        {
            var res = _context.StaffingTable.Where(x => x.Deleted == false).Select(x => new StaffingTableDataModel()
            {
                Id = x.Id,
                FOTId = x.FOTId,
                title = x.title,
                unitId = x.unitId,
                positionId = x.positionId,
                countRates = x.countRates,
                acceptEmployeeId = x.acceptEmployeeId,
                dateAccept = x.dateAccept,
                daysVacation = x.daysVacation,
                salaryId = _context.Recruitment.FirstOrDefault(x => x.employeeId == x.employeeId).salaryId,
                vacationEntitlementId = x.vacationEntitlementId
            }).ToArray();

            return res;
        }

        public StaffingTableDataModel GetStaffingTableById(int id)
        {
            var res = _context.StaffingTable.Select(x => new StaffingTableDataModel()
            {
                Id = x.Id,
                FOTId = x.FOTId,
                title = x.title,
                unitId = x.unitId,
                positionId = x.positionId,
                countRates = x.countRates,
                acceptEmployeeId = x.acceptEmployeeId,
                dateAccept = x.dateAccept,
                daysVacation = x.daysVacation,
                employeeId = x.employeeId,
               // salaryId = x.salaryId,
                vacationEntitlementId = x.vacationEntitlementId,
                Deleted = x.Deleted
            }).FirstOrDefault(y => y.Id == id);

            return res;
        }

        public bool DeleteStaffingTableById(int id)
        {
            var res = _context.StaffingTable.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public int AddStaffingTable(StaffingTable data)
        {
            var res = _context.StaffingTable.Add(data);
            _context.SaveChanges();
            return res.Entity.Id;
        }

        public bool UpdateStaffingTable(int id, StaffingTable data)
        {
            var res = _context.StaffingTable.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return false;
            }
            res.FOTId = data.FOTId;
            res.title = data.title;
            res.unitId = data.unitId;
            res.positionId = data.positionId;
            res.countRates = data.countRates;
            res.acceptEmployeeId = data.acceptEmployeeId;
            res.dateAccept = data.dateAccept;
            res.daysVacation = data.daysVacation;
            res.employeeId = data.employeeId;
            //res.salaryId = data.salaryId;
            res.vacationEntitlementId = data.vacationEntitlementId;
            _context.SaveChanges();
            return true;
        }

        public int ApproveStaffingTable(int acceptEmployeeId)
        {
            var res = _context.StaffingTable.Where(x => x.acceptEmployeeId == null).ToArray();

            foreach (var x in res)
            {
                x.acceptEmployeeId = acceptEmployeeId;
                x.dateAccept = (int?)Utils.ConvertToUnixTimestamp(DateTime.Now);
            }

            _context.SaveChanges();
            return res.Length;
        }

        public byte[] GenerateStaffingTable()
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
                lstColumns.Append(new Column() { Min = 6, Max = 6, Width = 25, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 7, Max = 7, Width = 25, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 8, Max = 8, Width = 25, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 9, Max = 9, Width = 25, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 10, Max = 10, Width = 25, CustomWidth = true });
                if (needToInsertColumns)
                    worksheetPart.Worksheet.InsertAt(lstColumns, 0);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Лист" };
                sheets.Append(sheet);

                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                Row row = new Row() { RowIndex = 1 };
                sheetData.Append(row);

                uint styleNum = 1;
                InsertCell(row, 1, ReplaceHexadecimalSymbols("FOT"), CellValues.String, styleNum);
                InsertCell(row, 2, ReplaceHexadecimalSymbols("Наименование"), CellValues.String, styleNum);
                InsertCell(row, 3, ReplaceHexadecimalSymbols("Подразделение"), CellValues.String, styleNum);
                InsertCell(row, 4, ReplaceHexadecimalSymbols("Должность"), CellValues.String, styleNum);
                InsertCell(row, 5, ReplaceHexadecimalSymbols("Кол-во ставок"), CellValues.String, styleNum);
                InsertCell(row, 6, ReplaceHexadecimalSymbols("Кем утверждено"), CellValues.String, styleNum);
                InsertCell(row, 7, ReplaceHexadecimalSymbols("Дата утверждения"), CellValues.String, styleNum);
                InsertCell(row, 8, ReplaceHexadecimalSymbols("Дней отпуска"), CellValues.String, styleNum);
                InsertCell(row, 9, ReplaceHexadecimalSymbols("Зарплата"), CellValues.String, styleNum);
                InsertCell(row, 10, ReplaceHexadecimalSymbols("Вид отпуска"), CellValues.String, styleNum);

                var staffingTable = GetStaffingTable();
                UInt32 rowIndex = 2;
                styleNum = 2;
                foreach (var x in staffingTable)
                {
                    DateTime date = Utils.ConvertFromUnixTimestamp((double)x.dateAccept);

                    row = new Row() { RowIndex = rowIndex };
                    sheetData.Append(row);
                    InsertCell(row, 1, ReplaceHexadecimalSymbols(x.FOTId.ToString()), CellValues.String, styleNum);
                    InsertCell(row, 2, ReplaceHexadecimalSymbols(x.title), CellValues.String, styleNum);
                    InsertCell(row, 3, ReplaceHexadecimalSymbols(_context.Unit.FirstOrDefault(y => y.Id == x.unitId).Title), CellValues.String, styleNum);
                    InsertCell(row, 4, ReplaceHexadecimalSymbols(_context.Positions.FirstOrDefault(y => y.Id == x.positionId).Name), CellValues.String, styleNum);
                    InsertCell(row, 5, ReplaceHexadecimalSymbols(x.countRates.ToString()), CellValues.String, styleNum);
                    InsertCell(row, 6, ReplaceHexadecimalSymbols(_context.Employees.FirstOrDefault(y => y.Id == x.acceptEmployeeId).FullName), CellValues.String, styleNum);
                    InsertCell(row, 7, ReplaceHexadecimalSymbols(date.ToShortDateString()), CellValues.String, styleNum);
                    InsertCell(row, 8, ReplaceHexadecimalSymbols(x.daysVacation.ToString()), CellValues.String, styleNum);
                    InsertCell(row, 9, ReplaceHexadecimalSymbols((_context.Salary.FirstOrDefault(y => y.Id == x.salaryId).salary).ToString()), CellValues.String, styleNum);
                    InsertCell(row, 10, ReplaceHexadecimalSymbols(_context.VacationEntitlement.FirstOrDefault(y => y.Id == x.vacationEntitlementId).title), CellValues.String, styleNum);
                    rowIndex++;
                }

                workbookPart.Workbook.Save();
                document.Close();

                //сохранить файл
                //File.WriteAllBytes(@"path\штатное расписание.xlsx", stream.ToArray());

                return stream.ToArray();
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
        public class StaffingTableDataModel
        {
            public int Id { get; set; }
            public int? FOTId { get; set; }
            public bool Deleted { get; set; }
            public int? employeeId { get; set; }
            public string title { get; set; }
            public int? unitId { get; set; }
            public int? positionId { get; set; }
            public int countRates { get; set; }
            public int? acceptEmployeeId { get; set; }
            public int? dateAccept { get; set; }
            public int daysVacation { get; set; }
            public int? salaryId { get; set; }
            public int? vacationEntitlementId { get; set; }
        }

        public class SheduleDataModel
        {
            public int Id { get; set; }
            public string title { get; set; }
            public bool Deleted { get; set; }
            public int? StaffingTableId { get; set; }
        }
    }
}
