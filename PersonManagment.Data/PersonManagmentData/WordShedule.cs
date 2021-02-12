using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using PersonManagment.Data.Models;
using static PersonManagment.Data.PersonManagmentData.RecruitmentData;
using static PersonManagment.Data.PersonManagmentData.EmployeeData;
using PersonManagment.Data.DataModel;


namespace PersonManagment.Data.PersonManagmentData
{
    public static class WordShedule
    {
        public static byte[] CreateEmployeeCard(EmployeeCardDataModel data)
        {
            string docDirectory = new FileInfo(@"doc").Directory.FullName + @"\doc\Personal Employee Card.docx";
            
            byte[] bytes = File.ReadAllBytes(docDirectory);

            if(data == null)
            {
                return bytes;
            }

            using (var stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, true))
                {
                    Body body = doc.MainDocumentPart.Document.Body;
                    
                    var header = body.ChildElements[1].ChildElements[4].ChildElements[1];
                    header.ReplaceChild(GetStringCentered($"\"НОЦ ГиРНГМ\"", 9), header.ChildElements[1]);

                    var fullname = data.employeeData.FullName;

                    //вторая таблица - инн,снилс ...
                    var dateOfPreparation = body.ChildElements[3].ChildElements[3].ChildElements[1];
                    dateOfPreparation.ReplaceChild(GetStringCentered($"{DateTime.Now.ToShortDateString()}", 9), dateOfPreparation.ChildElements[1]);

                    var personalNum = body.ChildElements[3].ChildElements[3].ChildElements[2];
                    personalNum.ReplaceChild(GetStringCentered($"{data.employeeData.PersonnelNumber}", 9), personalNum.ChildElements[1]);

                    var alphabet = body.ChildElements[3].ChildElements[3].ChildElements[5];
                    alphabet.ReplaceChild(GetStringCentered($"{fullname.Substring(0,1).ToUpper()}", 9), alphabet.ChildElements[1]);

                    var natureofwork = body.ChildElements[3].ChildElements[3].ChildElements[6];
                    natureofwork.ReplaceChild(GetStringCentered($"", 9), natureofwork.ChildElements[1]);
                    if (data.recruitmentData != null)
                    {
                        var typeOfEmployment = body.ChildElements[3].ChildElements[3].ChildElements[7]; //не перевод !
                        typeOfEmployment.ReplaceChild(GetStringCentered($"{data.recruitmentData.First(x => x.causeTransferComment == null).typeOfEmployment}", 9), typeOfEmployment.ChildElements[1]);
                    }

                    //трудовой договор
                    if (data.contractData != null)
                    {
                        if (data.contractData.dateStart != 0)
                        {
                            var contractNum = body.ChildElements[6].ChildElements[2].ChildElements[3];
                            contractNum.ReplaceChild(GetStringCentered($"{data.contractData.number}", 9), contractNum.ChildElements[1]);

                            var contractDate = body.ChildElements[6].ChildElements[3].ChildElements[3];
                            contractDate.ReplaceChild(GetStringCentered($"{Utils.ConvertFromUnixTimestamp((double)data.contractData.dateStart).ToShortDateString()}", 9), contractDate.ChildElements[1]);
                        }
                    }

                    //третья таблица - фИО, дата рождения ...
                    var fio = fullname.Split(' ');

                    if(fio.Length == 3)
                    {
                        var surname = body.ChildElements[8].ChildElements[2].ChildElements[2];
                        surname.ReplaceChild(GetStringCentered($"{fio[0]}", 9), surname.ChildElements[1]);

                        var name = body.ChildElements[8].ChildElements[2].ChildElements[4];
                        name.ReplaceChild(GetStringCentered($"{fio[1]}", 9), name.ChildElements[1]);

                        var patroname = body.ChildElements[8].ChildElements[2].ChildElements[6];
                        patroname.ReplaceChild(GetStringCentered($"{fio[2]}", 9), patroname.ChildElements[1]);
                    }
                   

                    if (data.educationData.Length != 0)
                    {
                        var typeofeducation = body.ChildElements[10].ChildElements[10].ChildElements[2];
                        typeofeducation.ReplaceChild(GetStringCentered($"", 9), typeofeducation.ChildElements[1]);  //уточнить
                    }
                   
                    if (data.personData != null)
                    {
                        if (data.personData.DateBirth != 0)
                        {
                            var datebirth = body.ChildElements[10].ChildElements[3].ChildElements[2];
                            datebirth.ReplaceChild(GetStringCentered($"{Utils.ConvertFromUnixTimestamp((double)data.personData.DateBirth).ToShortDateString()}", 9), datebirth.ChildElements[1]);
                        }

                        var birthplace = body.ChildElements[10].ChildElements[5].ChildElements[2];
                        birthplace.ReplaceChild(GetStringCentered($"{data.personData.Birthplace}", 9), birthplace.ChildElements[1]);

                        var citizenship = body.ChildElements[10].ChildElements[6].ChildElements[2];
                        citizenship.ReplaceChild(GetStringCentered($"", 9), citizenship.ChildElements[1]);

                        var engName = body.ChildElements[10].ChildElements[7].ChildElements[2];
                        engName.ReplaceChild(GetStringCentered($"", 9), engName.ChildElements[1]);

                        var engDegree = body.ChildElements[10].ChildElements[7].ChildElements[3];
                        engDegree.ReplaceChild(GetStringCentered($"", 9), engDegree.ChildElements[1]);

                        var inn = body.ChildElements[3].ChildElements[3].ChildElements[3];
                        inn.ReplaceChild(GetStringCentered($"{data.personData.INN}", 9), inn.ChildElements[1]);

                        var snils = body.ChildElements[3].ChildElements[3].ChildElements[4];
                        snils.ReplaceChild(GetStringCentered($"{data.personData.SNILS}", 9), snils.ChildElements[1]);

                        var genderString = data.personData.Gender == 1 ? "М" : data.personData.Gender == 0 ? "Ж" : string.Empty;
                        var gender = body.ChildElements[3].ChildElements[3].ChildElements[8];
                        gender.ReplaceChild(GetStringCentered($"{genderString}", 9), gender.ChildElements[1]);

                        //пасспорт
                        var ser = data.personData.DocumentPassport.serial != 0 ? data.personData.DocumentPassport.serial.ToString() : string.Empty;
                        var num = data.personData.DocumentPassport.number != 0 ? data.personData.DocumentPassport.number.ToString() : string.Empty;
                        var number = body.ChildElements[27].ChildElements[2].ChildElements[3];
                        number.ReplaceChild(GetStringCentered($"{ser} {num}", 9), number.ChildElements[1]);

                        var issuedby = body.ChildElements[27].ChildElements[3].ChildElements[2];
                        issuedby.ReplaceChild(GetStringCentered($"{data.personData.DocumentPassport.DocumentIssued}", 9), issuedby.ChildElements[1]);

                        if (data.personData.DocumentPassport.IssuedDate != 0)
                        {
                            var issuedDate = Utils.ConvertFromUnixTimestamp((double)data.personData.DocumentPassport.IssuedDate);

                            var issuedDay = body.ChildElements[27].ChildElements[2].ChildElements[6];
                            issuedDay.ReplaceChild(GetStringCentered($"{issuedDate.Day}", 9), issuedDay.ChildElements[1]);

                            var issuedMonth = body.ChildElements[27].ChildElements[2].ChildElements[8];
                            issuedMonth.ReplaceChild(GetStringCentered($"{GetMonthString(issuedDate.Month)}", 9), issuedMonth.ChildElements[1]);

                            var issuedYear = body.ChildElements[27].ChildElements[2].ChildElements[10];
                            issuedYear.ReplaceChild(GetStringCentered($"{issuedDate.Year}", 9), issuedYear.ChildElements[1]);
                        }

                        //адреса, дата, номер
                        var regAddress = body.ChildElements[35].ChildElements[3].ChildElements[3];
                        regAddress.ReplaceChild(GetStringCentered($"{data.personData.PersonAdress.RegistrationAddress}", 9), regAddress.ChildElements[1]);

                        var resAddress = body.ChildElements[38].ChildElements[3].ChildElements[3];
                        resAddress.ReplaceChild(GetStringCentered($"{data.personData.PersonAdress.ResidenceAddress}", 9), resAddress.ChildElements[1]);

                        if (data.personData.PersonAdress.RegistrationDate != 0)
                        {
                            var regDate = Utils.ConvertFromUnixTimestamp((double)data.personData.PersonAdress.RegistrationDate);

                            var regDay = body.ChildElements[41].ChildElements[2].ChildElements[3];
                            regDay.ReplaceChild(GetStringCentered($"{regDate.Day}", 9), regDay.ChildElements[1]);

                            var regMonth = body.ChildElements[41].ChildElements[2].ChildElements[5];
                            regMonth.ReplaceChild(GetStringCentered($"{GetMonthString(regDate.Month)}", 9), regMonth.ChildElements[1]);

                            var regYear = body.ChildElements[41].ChildElements[2].ChildElements[7];
                            regYear.ReplaceChild(GetStringCentered($"{regDate.Year}", 9), regYear.ChildElements[1]);
                        }

                        body.ReplaceChild(GetString($"Номер телефона     {data.personData.PhoneNumber}", 9), body.ChildElements[42]); //номер телефона
                    }

                    if (data.militaryRegistrationData != null)
                    {
                        //воинский учёт
                        var stock = body.ChildElements[45].ChildElements[3].ChildElements[2];
                        stock.ReplaceChild(GetStringCentered($"{data.militaryRegistrationData.stockCategory}", 9), stock.ChildElements[1]);

                        var rank = body.ChildElements[45].ChildElements[4].ChildElements[2];
                        rank.ReplaceChild(GetStringCentered($"{data.militaryRegistrationData.militaryRank}", 9), rank.ChildElements[1]);

                        var profile = body.ChildElements[45].ChildElements[5].ChildElements[2];
                        profile.ReplaceChild(GetStringCentered($"{data.militaryRegistrationData.militaryProfile}", 9), profile.ChildElements[1]);

                        var vus = body.ChildElements[45].ChildElements[6].ChildElements[2];
                        vus.ReplaceChild(GetStringCentered($"{data.militaryRegistrationData.VUS}", 9), vus.ChildElements[1]);

                        var category = body.ChildElements[45].ChildElements[7].ChildElements[2];
                        category.ReplaceChild(GetStringCentered($"{data.militaryRegistrationData.militaryFitnessCategory}", 9), category.ChildElements[1]);

                        var commissariat = body.ChildElements[45].ChildElements[4].ChildElements[5];
                        commissariat.ReplaceChild(GetStringCentered($"{data.militaryRegistrationData.nameOfCommissariat}", 9), commissariat.ChildElements[1]);

                        //var typeReg = body.ChildElements[45].ChildElements[6].ChildElements[5];
                        //typeReg.ReplaceChild(GetStringCentered($"{data.militaryRegistrationData.typeMilitaryRegistration}", 9), typeReg.ChildElements[1]);

                        //var removeReg = body.ChildElements[4].ChildElements[8].ChildElements[5];  
                        //removeReg.ReplaceChild(GetStringCentered($"", 9), removeReg.ChildElements[1]);

                    }
                    //приём на работу
                    for (int i = 0; i < data.recruitmentData.Length; i++)
                    {
                        var dateRec = body.ChildElements[52].ChildElements[4].ChildElements[1];
                        dateRec.ReplaceChild(GetStringCentered($"{Utils.ConvertFromUnixTimestamp((double)data.recruitmentData[i].dateOfReceipt).ToShortDateString()}", 9), dateRec.ChildElements[1]);

                        var unit = body.ChildElements[52].ChildElements[4].ChildElements[2];
                        unit.ReplaceChild(GetStringCentered($"{data.recruitmentData[i].unit}", 9), unit.ChildElements[1]);

                        var position = body.ChildElements[52].ChildElements[4].ChildElements[3];
                        position.ReplaceChild(GetStringCentered($"{data.recruitmentData[i].position}", 9), position.ChildElements[1]);

                        var salary = body.ChildElements[52].ChildElements[4].ChildElements[4];
                        salary.ReplaceChild(GetStringCentered($"{data.recruitmentData[i].salary}", 9), salary.ChildElements[1]);

                        if (data.recruitmentData[i].causeTransferComment != null)
                        {
                            var reason = body.ChildElements[52].ChildElements[4].ChildElements[5];
                            reason.ReplaceChild(GetStringCentered($"{data.recruitmentData[i].causeTransferComment}", 9), reason.ChildElements[1]); //если перевод
                        }
                        else
                        {
                            var reason = body.ChildElements[52].ChildElements[4].ChildElements[5];
                            reason.ReplaceChild(GetStringCentered($"", 9), reason.ChildElements[1]);   //если приём
                        }
                    }

                    //образование
                    if (data.educationData.Length != 0)
                    {
                        for (int i = 0; i <= 2; i += 2)
                        {
                            string name = data.educationData[i].educationalInstitution;
                            if (name.Length <= 31) //кол-во символов в ячейке
                            {
                                var institutionName = body.ChildElements[12 + i].ChildElements[3].ChildElements[1];
                                institutionName.ReplaceChild(GetStringCentered($"{name}", 9), institutionName.ChildElements[1]);
                            }
                            else
                            {
                                var split = name.Split(' ');
                                string row1 = "", row2 = "";
                                for (int s = 0; s < split.Length; s++) //перенос слов в следующую ячейку
                                {
                                    if (row1.Length + split[s].Length <= 31 && row2.Length == 0) row1 += split[s] + " "; else row2 += split[s] + " ";
                                }
                                var institutionName = body.ChildElements[12 + i].ChildElements[3].ChildElements[1];
                                institutionName.ReplaceChild(GetStringCentered($"{row1}", 9), institutionName.ChildElements[1]);

                                institutionName = body.ChildElements[12 + i].ChildElements[4].ChildElements[1];
                                institutionName.ReplaceChild(GetStringCentered($"{row2}", 9), institutionName.ChildElements[1]);
                            }

                            var docName = body.ChildElements[12 + i].ChildElements[4].ChildElements[2];
                            docName.ReplaceChild(GetStringCentered($"{data.educationData[i].diplomaDocument.title}", 9), docName.ChildElements[1]);

                            var serDoc = body.ChildElements[12 + i].ChildElements[4].ChildElements[3];
                            serDoc.ReplaceChild(GetStringCentered($"{data.educationData[i].diplomaDocument.serial}", 9), serDoc.ChildElements[1]);

                            var numDoc = body.ChildElements[12 + i].ChildElements[4].ChildElements[4];
                            numDoc.ReplaceChild(GetStringCentered($"{data.educationData[i].diplomaDocument.number}", 9), numDoc.ChildElements[1]);

                            var dateDoc = body.ChildElements[12 + i].ChildElements[3].ChildElements[5];
                            dateDoc.ReplaceChild(GetStringCentered($"{Utils.ConvertFromUnixTimestamp((double)data.educationData[i].dateEndEducation).Year}", 9), dateDoc.ChildElements[1]);

                            var qual = body.ChildElements[12 + i].ChildElements[6].ChildElements[1];
                            qual.ReplaceChild(GetStringCentered($"{data.educationData[i].qualification}", 9), qual.ChildElements[1]);

                            var spec = body.ChildElements[12 + i].ChildElements[6].ChildElements[2];
                            spec.ReplaceChild(GetStringCentered($"{data.educationData[i].speciality}        Код по ОКСО", 9), spec.ChildElements[1]);
                        }
                    }
                    //аттестация

                    for (int i = 0; i < data.certificationData.Length; i++)
                    {
                        var dateCertification = body.ChildElements[54].ChildElements[5 + i].ChildElements[1];
                        dateCertification.ReplaceChild(GetStringCentered($"{Utils.ConvertFromUnixTimestamp((double)data.certificationData[i].dateCertification).ToShortDateString()}", 9), dateCertification.ChildElements[1]);

                        var solve = body.ChildElements[52].ChildElements[5 + i].ChildElements[2];
                        solve.ReplaceChild(GetStringCentered($"{data.certificationData[i].solve}", 9), solve.ChildElements[1]);

                        var numDoc = body.ChildElements[52].ChildElements[5 + i].ChildElements[3];
                        numDoc.ReplaceChild(GetStringCentered($"{data.certificationData[i].numberDoc}", 9), numDoc.ChildElements[1]);

                        var dateDoc = body.ChildElements[52].ChildElements[5 + i].ChildElements[4];
                        dateDoc.ReplaceChild(GetStringCentered($"{Utils.ConvertFromUnixTimestamp((double)data.certificationData[i].dateDoc).ToShortDateString()}", 9), dateDoc.ChildElements[1]);

                        var reason = body.ChildElements[52].ChildElements[5 + i].ChildElements[5];
                        reason.ReplaceChild(GetStringCentered($"{data.certificationData[i].reason}", 9), reason.ChildElements[1]);
                    }

                    //отпуск
                    for (int i = 0; i < data.vacationData.Length; i++)
                    {
                        if (data.vacationData[i].dateStart != 0 && data.vacationData[i].dateEnd != 0)
                        {
                            var vacType = body.ChildElements[63].ChildElements[5 + i].ChildElements[1];
                            vacType.ReplaceChild(GetStringCentered($"{data.vacationData[i].vacationType}", 9), vacType.ChildElements[1]);

                            //var work_from = body.ChildElements[63].ChildElements[5].ChildElements[2];
                            //work_from.ReplaceChild(GetStringCentered($"", 9), work_from.ChildElements[1]);

                            //var work_to = body.ChildElements[63].ChildElements[5].ChildElements[3];
                            //work_to.ReplaceChild(GetStringCentered($"", 9), work_to.ChildElements[1]);

                            var start = body.ChildElements[63].ChildElements[5 + i].ChildElements[5];
                            start.ReplaceChild(GetStringCentered($"{Utils.ConvertFromUnixTimestamp((double)data.vacationData[i].dateStart).ToShortDateString()}", 9), start.ChildElements[1]);

                            var end = body.ChildElements[63].ChildElements[5 + i].ChildElements[6];
                            end.ReplaceChild(GetStringCentered($"{Utils.ConvertFromUnixTimestamp((double)data.vacationData[i].dateEnd).ToShortDateString()}", 9), end.ChildElements[1]);

                            var vacationDays = body.ChildElements[63].ChildElements[5 + i].ChildElements[4];
                            vacationDays.ReplaceChild(GetStringCentered($"{data.vacationData[i].vacationDays}", 9), vacationDays.ChildElements[1]);

                            var vacationEntitlement = body.ChildElements[63].ChildElements[5 + i].ChildElements[7];
                            vacationEntitlement.ReplaceChild(GetStringCentered($"{data.vacationData[i].vacationEntitlement}", 9), vacationEntitlement.ChildElements[1]);
                        }
                    }

                    ParagraphProperties paraProperties = new ParagraphProperties();
                    paraProperties.Append(new Justification() { Val = JustificationValues.Center }, new BottomBorder() { Val = BorderValues.Single });


                    //увольнение

                    if (data.dismissalData != null)
                    {
                        if (data.dismissalData.dateOfDismissal != 0)
                        {
                            body.ReplaceChild(GetString($"{data.dismissalData.cause}", paraProperties, 9), body.ChildElements[69]); //причина увольнения

                            var dateOfDismissal = Utils.ConvertFromUnixTimestamp((double)data.dismissalData.dateOfDismissal);

                            var day = body.ChildElements[70].ChildElements[2].ChildElements[3];
                            day.ReplaceChild(GetStringCentered($"{dateOfDismissal.Day}", 9), day.ChildElements[1]);

                            var month = body.ChildElements[70].ChildElements[2].ChildElements[5];
                            month.ReplaceChild(GetStringCentered($"{GetMonthString(dateOfDismissal.Month)}", 9), month.ChildElements[1]);

                            var year = body.ChildElements[70].ChildElements[2].ChildElements[7];
                            year.ReplaceChild(GetStringCentered($"{dateOfDismissal.Year.ToString().Substring(2, 2)}", 9), year.ChildElements[1]);
                        }
                    }
                    //var order = body.ChildElements[70].ChildElements[3].ChildElements[2]; //номер приказа
                    //order.ReplaceChild(GetStringCentered($"{data}", 9), order.ChildElements[1]);


                    //if (data. != 0)
                    //{
                    //    var dateOrder = Utils.ConvertFromUnixTimestamp((double)data);  //приказ от  

                    //    var day = body.ChildElements[70].ChildElements[3].ChildElements[5];
                    //    day.ReplaceChild(GetStringCentered($"{data}", 9), day.ChildElements[1]);

                    //    var month = body.ChildElements[70].ChildElements[3].ChildElements[7];
                    //    month.ReplaceChild(GetStringCentered($"{data}", 9), month.ChildElements[1]);

                    //    var year = body.ChildElements[70].ChildElements[3].ChildElements[9];
                    //    year.ReplaceChild(GetStringCentered($"{data}", 9), year.ChildElements[1]);
                    //}


                    doc.Save();

                }

                bytes = stream.ToArray();
            }



            return bytes;

        }

        public static byte[] CreateDoc(ICollection<VacationSheduleWordModel> result, byte[] docForm)
        {

            //string docDirectory = new FileInfo(@"doc").Directory.FullName + @"\doc\docV2 — копия.docx";
            byte[] bytes;//= File.ReadAllBytes(docDirectory);

            using (var stream = new MemoryStream())
            {
                stream.Write(docForm, 0, docForm.Length);
                using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, true))
                {
                    Body bod = doc.MainDocumentPart.Document.Body;

                    var table = bod.ChildElements[0];
                    TableProperties tblProperties = GetTableProperties();
                    table.AppendChild(tblProperties);

                    var namin = CreateMain();
                    var num = CreateNumbers();
                    var fr = CreateFirstRowSheduleDoc();
                    var sr = CreateSecondRowSheduleDoc();
                    table.Append(fr);
                    table.Append(sr);
                    table.Append(namin);
                    table.Append(num);
                    foreach (var obj in result)
                    {
                        TableRow row = new TableRow();
                        TableCell cell = GetCell(obj.name);
                        row.AppendChild(cell);

                        TableCell cell1 = GetCell(obj.personnelNumber);
                        row.AppendChild(cell1);
                        TableCell cell2 = GetCell($"\"НОЦ ГиРНГМ\"");;
                        row.AppendChild(cell2);
                        TableCell cell3 = GetCell(obj.position);
                        row.AppendChild(cell3);
                        TableCell cell4 = GetCell(obj.daysVacation.ToString());
                        row.AppendChild(cell4);
                        TableCell cell5 = GetCell(obj.typeVacation);
                        row.AppendChild(cell5);
                        TableCell cell6 = GetCell("      -        "); //период работы
                        row.AppendChild(cell6);
                        TableCell cell7 = GetCell(obj.allDays.ToString());
                        row.AppendChild(cell7);

                        var dateStart = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(obj.startDate);
                        var dateEnd = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(obj.endDate);

                        TableCell cell8 = GetCell(dateStart.ToString("dd-MM-yyyy"));
                        row.AppendChild(cell8);

                        TableCell cell9 = GetCell(dateEnd.ToString("dd-MM-yyyy"));
                        row.AppendChild(cell9);
                        TableCell cell10 = GetCell("       ");
                        row.AppendChild(cell10);
                        table.Append(row);
                    }
                    doc.Save();
                   
                  
                }

                bytes = stream.ToArray();
            }



            return bytes;

        }


        /// <summary>
        /// получить настройку объединения
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TableCellProperties createTableCellProperties(string text)
        {
            TableCellProperties tableCellProperties = new TableCellProperties();
            switch (text)
            {
                case "VC":
                    VerticalMerge verticalMerge = new VerticalMerge()
                    {
                        Val = MergedCellValues.Continue
                    };
                    tableCellProperties.Append(verticalMerge);
                    break;
                case "VR":
                    VerticalMerge verticalMergeR = new VerticalMerge()
                    {
                        Val = MergedCellValues.Restart
                    };
                    tableCellProperties.Append(verticalMergeR);
                    break;
                case "HR":
                    HorizontalMerge horizontalMerge = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Restart
                    };
                    tableCellProperties.Append(horizontalMerge);
                    break;
                case "HC":
                    HorizontalMerge horizontalMergeC = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Continue
                    };
                    tableCellProperties.Append(horizontalMergeC);
                    break;
                default:
                    break;
            }

            return tableCellProperties;
        }

        public static TableRow CreateFirstRowSheduleDoc()
        {
            TableRow row = new TableRow();
            for (int i = 1; i < 5; i++)
            {
                TableCell tc = GetCell(" ");
                switch (i)
                {
                    case 1:
                        tc = GetCell("Фамилия, имя, отчество");
                        break;
                    case 2:
                        tc = GetCell("Табельный номер");
                        break;
                    case 3:
                        tc = GetCell("Структурное подразделение");
                        break;
                    case 4:
                        tc = GetCell("Должность (специальность, профессия по штатному расписанию)");
                        break;
                    default:
                        break;
                }
                tc.AppendChild(createTableCellProperties("VC"));
                row.Append(tc);
            }

            for (int i = 1; i < 7; i++)
            {
                TableCellProperties tableCellProperties = createTableCellProperties("HR");
                VerticalMerge verticalMerge = new VerticalMerge()
                {
                    Val = MergedCellValues.Restart
                };
                if (i < 5)
                {
                    tableCellProperties.Append(verticalMerge);
                }

                TableCell cell = CreateCell("Отпуск");
                if (i == 1 || i == 7)
                {
                    cell.Append(tableCellProperties);
                }
                else
                {
                    cell.Append(createTableCellProperties("HC"));
                }

                row.AppendChild(cell);
            }
            TableCell cellEnd = CreateCell("С приказом (распоряжением) работник ознакомлен. Личная подпись. Дата");
            cellEnd.Append(createTableCellProperties("VR"));
            row.AppendChild(cellEnd);
            return row;
        }


        /// <summary>
        /// создает ячейку без стиля
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public static TableCell CreateCell(string test)
        {
            Run run = new Run();
            run.AppendChild(new Text(test));
            RunProperties runProp = new RunProperties();
            runProp.Append(
             new FontSize { Val = new StringValue("20") },
             new RunFonts()
             {
                 Ascii = "Times New Roman",
                 HighAnsi = "Times New Roman",
                 EastAsia = "Times New Roman",
                 ComplexScript = "Times New Roman"
             }
             );
            run.PrependChild(runProp);
            ParagraphProperties ParaProperties = new ParagraphProperties();
            ParaProperties.Append(new Justification() { Val = JustificationValues.Center });
            Paragraph p = new Paragraph(ParaProperties);
            p.AppendChild(run);
            TableCell cell = new TableCell(p);
            return cell;
        }

        public static TableRow CreateSecondRowSheduleDoc()
        {
            TableRow row = new TableRow();
            //пустые 
            for (int i = 1; i < 5; i++)
            {
                TableCell tc = GetCell(" ");
                tc.AppendChild(createTableCellProperties("VC"));
                row.Append(tc);
            }
            //соед
            for (int i = 1; i < 3; i++)
            {
                TableCell cell = GetCell("Вид");
                if (i == 1)
                {
                    cell.Append(createTableCellProperties("HR"));
                }
                else
                {
                    cell.Append(createTableCellProperties("HC"));
                }
                row.AppendChild(cell);
            }
            ///
            TableCell cellZero = CreateCell("За период работы");
            TableCell cellZero1 = CreateCell("Всего календарных дней");
            cellZero.Append(createTableCellProperties("VR"));
            cellZero1.Append(createTableCellProperties("VR"));
            row.AppendChild(cellZero);
            row.AppendChild(cellZero1);
            ///соед
            for (int i = 1; i < 3; i++)
            {
                TableCell cell = CreateCell("Дата");
                if (i == 1)
                {
                    cell.Append(createTableCellProperties("HR"));
                }
                else
                {
                    cell.Append(createTableCellProperties("HC"));
                }
                row.AppendChild(cell);
            }
            ///
            TableCell cellEnd = CreateCell("");
            cellEnd.Append(createTableCellProperties("VC"));
            row.AppendChild(cellEnd);
            return row;
        }

        public static string DateConvert(int day)
        {
            return (day < 10 ? ("0" + day) : day.ToString());
        }


        public static int GetWorkingMonth(DateTime from, DateTime to)
        {
            return  ((to.Year - from.Year) * 12) - from.Month + to.Month +1;
        }


        public static byte[] CreateContractDoc(ContractWordDataModel result, byte[] docForm)
        {
            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                stream.Write(docForm, 0, docForm.Length);
                using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, true))
                {
                    Body body = doc.MainDocumentPart.Document.Body;


                    int WeekendDays = Convert.ToInt32(GetWorkingMonth(Utils.ConvertFromUnixTimestamp(result.dateStart), Utils.ConvertFromUnixTimestamp(result.dateEnd))*2.33);



                    body.ReplaceChild(GetString($"на должность {result.position}, величина ставки {result.rate}; ", 13), body.ChildElements[18]);

                    ///
                    var workNumber = body.ChildElements[21];

                    string def = result.workСontract != null ? result.workСontract : "______________";
 
                    workNumber.ReplaceChild(GetStringRun($"- на определенный срок(на период действия договора № {def} )"), workNumber.ChildElements[1]);
                    ////
                    var dayRecipient = Utils.ConvertFromUnixTimestamp(result.dateOfReceipt).Day;
                    var monthRecipient = GetMonthString(Utils.ConvertFromUnixTimestamp(result.dateOfReceipt).Month);
                    var yearRecipient = Utils.ConvertFromUnixTimestamp(result.dateOfReceipt).Year;
                    body.ReplaceChild(GetString($"г.Пермь				                                                                       «{DateConvert(dayRecipient)}»   {monthRecipient}   {yearRecipient}г.", 13), body.ChildElements[9]);

                    var s = body.ChildElements[23].ChildElements[2].ChildElements[0];
                    s.ReplaceChild(GetString($"Срок действия договора:  с"), s.ChildElements[1]);

                    var dayStart = body.ChildElements[23].ChildElements[2].ChildElements[1];
                    dayStart.ReplaceChild(GetString($"«{DateConvert( Utils.ConvertFromUnixTimestamp(result.dateStart).Day) }»"), dayStart.ChildElements[1]);

                    var dayEnd = body.ChildElements[23].ChildElements[2].ChildElements[3];
                    dayEnd.ReplaceChild(GetString($"«{DateConvert(Utils.ConvertFromUnixTimestamp(result.dateEnd).Day)}»"), dayEnd.ChildElements[1]);

                    var monthStart = body.ChildElements[23].ChildElements[2].ChildElements[2];
                    monthStart.ReplaceChild(GetString($" {GetMonthString(Utils.ConvertFromUnixTimestamp(result.dateStart).Month)}    {Utils.ConvertFromUnixTimestamp(result.dateStart).Year} г. по"), monthStart.ChildElements[1]);

                    var monthEnd = body.ChildElements[23].ChildElements[2].ChildElements[4];
                    monthEnd.ReplaceChild(GetString($" {GetMonthString(Utils.ConvertFromUnixTimestamp(result.dateEnd).Month)}    {Utils.ConvertFromUnixTimestamp(result.dateEnd).Year} г."), monthEnd.ChildElements[1]);

                    var fullName = body.ChildElements[14].ChildElements[2].ChildElements[1];
                    fullName.ReplaceChild(GetString(result.fullName), fullName.ChildElements[1]);

                    var salary = body.ChildElements[29].ChildElements[2].ChildElements[1];
                    salary.ReplaceChild(GetString($"{result.salary}"), salary.ChildElements[1]);

                    var mainDays = body.ChildElements[52].ChildElements[2].ChildElements[1];
                    mainDays.ReplaceChild(GetString($"{WeekendDays}"), mainDays.ChildElements[1]);


                    var additionalDays = GetString($" - дополнительный  ______________  дней."); //var additionalDays = GetString($" - дополнительный  {result.mainVacationDays} дней.");
                    body.ReplaceChild(additionalDays, body.ChildElements[55]);

                    var address = body.ChildElements[96].ChildElements[3].ChildElements[1];
                    address.ReplaceChild(GetString($"Адрес: {result.address}"), address.ChildElements[1]);

                    var passSerial = body.ChildElements[96].ChildElements[6].ChildElements[2];
                    passSerial.ReplaceChild(GetString($" {result.passportSerial}"), passSerial.ChildElements[1]);

                    var passNumber = body.ChildElements[96].ChildElements[6].ChildElements[3];
                    passNumber.ReplaceChild(GetString($"№ {result.passportNumber}"), passNumber.ChildElements[1]);

                    var passDate = body.ChildElements[96].ChildElements[7].ChildElements[2];
                    passDate.ReplaceChild(GetString($" {getDate(result.passportDate)}"), passDate.ChildElements[1]);

                    var passIssued = body.ChildElements[96].ChildElements[8].ChildElements[3];
                    passIssued.ReplaceChild(GetString($" {result.passportIssued}"), passIssued.ChildElements[1]);

                    var inn = body.ChildElements[96].ChildElements[13].ChildElements[2];
                    inn.ReplaceChild(GetString($" {result.INN}"), inn.ChildElements[1]);


                    var snils = body.ChildElements[96].ChildElements[12].ChildElements[1];
                    snils.ReplaceChild(GetString($"№ {result.SNILS}"), snils.ChildElements[1]);
                    body.ChildElements[101].Remove();


                    doc.Save();
                }

                bytes = stream.ToArray();
            }
            return bytes;
        }
        
        public static string getDate(int date)
        {
            var day = DateConvert(Utils.ConvertFromUnixTimestamp(date).Day);
            var month = Utils.ConvertFromUnixTimestamp(date).Month;
            var year = Utils.ConvertFromUnixTimestamp(date).Year;
            return $"{day}.{month}.{year}";
        }

        private static TableRow CreateNumbers()
        {
            TableRow row = new TableRow();
            for (int i = 1; i < 12; i++)
            {
                TableCell cell = GetCell(i.ToString());
                row.AppendChild(cell);
            }
            return row;
        }

        private static TableRow CreateMain()
        {
            TableRow row = new TableRow();

            TableCell cell = GetCell(" ");
            cell.Append(createTableCellProperties("VC"));
            row.AppendChild(cell);

            TableCell cell2 = GetCell(" ");
            row.AppendChild(cell2);
            cell2.Append(createTableCellProperties("VC"));

            TableCell cell3 = GetCell(" ");
            row.AppendChild(cell3);
            cell3.Append(createTableCellProperties("VC"));

            TableCell cell4 = GetCell(" ");
            row.AppendChild(cell4);
            cell4.Append(createTableCellProperties("VC"));

            TableCell cell5 = GetCell("Ежегодный основной оплачиваемый отпуск, календарных дней");
            row.AppendChild(cell5);
            cell5.Append(createTableCellProperties("VC"));

            TableCell cell6 = GetCell("Ежегодный дополнительный оплачиваемый отпуск, учебный, без сохранения заработной платы и другие(указать)");
            row.AppendChild(cell6);
            cell6.Append(createTableCellProperties("VC"));

            TableCell cell7 = GetCell(" ");
            row.AppendChild(cell7);
            cell7.Append(createTableCellProperties("VC"));

            TableCell cell8 = GetCell(" ");
            row.AppendChild(cell8);
            cell8.Append(createTableCellProperties("VC"));

            TableCell cell9 = GetCell("Дата начала");
            row.AppendChild(cell9);
            cell9.Append(createTableCellProperties("VC"));

            TableCell cell10 = GetCell("Дата окончания");
            row.AppendChild(cell10);
            cell10.Append(createTableCellProperties("VC"));

            TableCell cell11 = GetCell(" ");
            row.AppendChild(cell11);
            cell11.Append(createTableCellProperties("VC"));

            return row;
        }

        /// <summary>
        /// создает ячейку со стилем
        /// </summary>
        /// <param name="text"></param>
        /// <param name="widh"></param>
        /// <returns></returns>
        private static TableCell GetCell(string text, string widh = "2000", int fontSize = 14)
        {
            // Run run = GetRun(text);
            Run run = new Run();

            RunProperties runProp = new RunProperties();
            runProp.Append(
                new FontSize { Val = new StringValue((fontSize*2).ToString()) },
                new RunFonts()
                {
                    Ascii = "Times New Roman",
                    HighAnsi = "Times New Roman",
                    EastAsia = "Times New Roman",
                    ComplexScript = "Times New Roman"
                }
                );
            run.PrependChild(runProp);

            run.AppendChild(new Text(text));

            ParagraphProperties ParaProperties = new ParagraphProperties();
            ParaProperties.Append(new Justification() { Val = JustificationValues.Center });

            Paragraph p = new Paragraph();
            p.Append(ParaProperties);
            p.AppendChild(run);
            TableCell tc = new TableCell(p);
            tc.Append(new TableCellProperties(
                   new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = widh }));
            return tc;
        }

        private static TableCell GetCell(string text, int fontSize, ParagraphProperties ParaProperties)
        {
            Run run = new Run();

            RunProperties runProp = new RunProperties();
            runProp.Append(
                new FontSize { Val = new StringValue((fontSize * 2).ToString()) },
                new RunFonts()
                {
                    Ascii = "Times New Roman",
                    HighAnsi = "Times New Roman",
                    EastAsia = "Times New Roman",
                    ComplexScript = "Times New Roman"
                }
                );
            run.PrependChild(runProp);

            run.AppendChild(new Text(text));

            

            Paragraph p = new Paragraph();
            p.Append(ParaProperties);
            p.AppendChild(run);
            TableCell tc = new TableCell(p);
            tc.Append(new TableCellProperties(
                 new TableCellWidth() {  Width = "2000" }));
            return tc;
        }

        private static TableProperties GetTableProperties()
        {
            TableBorders tblBorders = new TableBorders();
            TableProperties tblProperties = new TableProperties();

            TopBorder topBorder = new TopBorder();
            topBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            topBorder.Color = "000000";
            tblBorders.AppendChild(topBorder);

            BottomBorder bottomBorder = new BottomBorder();
            bottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            bottomBorder.Color = "000000";
            tblBorders.AppendChild(bottomBorder);

            RightBorder rightBorder = new RightBorder();
            rightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            rightBorder.Color = "000000";
            tblBorders.AppendChild(rightBorder);

            LeftBorder leftBorder = new LeftBorder();
            leftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            leftBorder.Color = "000000";
            tblBorders.AppendChild(leftBorder);

            InsideHorizontalBorder insideHBorder = new InsideHorizontalBorder();
            insideHBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            insideHBorder.Color = "000000";
            tblBorders.AppendChild(insideHBorder);

            InsideVerticalBorder insideVBorder = new InsideVerticalBorder();
            insideVBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            insideVBorder.Color = "000000";
            tblBorders.AppendChild(insideVBorder);

            tblProperties.AppendChild(tblBorders);
            return tblProperties;
        }



        ////PartTime Template
        ///


        public static string GetMonthString(int num)
        {
            string month = "";
            switch (num)
            {
                case 1:
                    month = "января";
                    break;
                case 2:
                    month = "февраля";
                    break;
                case 3:
                    month = "марта";
                    break;
                case 4:
                    month = "апреля";
                    break;
                case 5:
                    month = "мая";
                    break;
                case 6:
                    month = "июня";
                    break;
                case 7:
                    month = "июля";
                    break;
                case 8:
                    month = "августа";
                    break;
                case 9:
                    month = "сентября";
                    break;
                case 10:
                    month = "октября";
                    break;
                case 11:
                    month = "ноября";
                    break;
                case 12:
                    month = "декабря";
                    break;
                default:
                    break;
            }
            return month;
        }



        public static Run GetStringRun(string text, int fontSize = 14)
        {
            Run run = new Run();
            run.AppendChild(new Text(text));
            RunProperties runProp = new RunProperties();
            runProp.Append(
              new FontSize { Val = new StringValue((fontSize * 2).ToString()) },
              new RunFonts()
              {
                  Ascii = "Times New Roman",
                  HighAnsi = "Times New Roman",
                  EastAsia = "Times New Roman",
                  ComplexScript = "Times New Roman"
              }
              );
            run.PrependChild(runProp);
            return run;
        }

        public static Paragraph GetString(string text, int fontSize = 14)
        {
            Run run = new Run();
            run.AppendChild(new Text(text));
            RunProperties runProp = new RunProperties();
            runProp.Append(
              new Justification() { Val = JustificationValues.Center },
              new FontSize { Val = new StringValue((fontSize*2).ToString()) },
              new RunFonts()
              {
                  Ascii = "Times New Roman",
                  HighAnsi = "Times New Roman",
                  EastAsia = "Times New Roman",
                  ComplexScript = "Times New Roman"
              });
            run.PrependChild(runProp);
            Paragraph p = new Paragraph();
            p.AppendChild(run);
            return p;
        }

        public static Paragraph GetStringCentered(string text, int fontSize = 14)
        {
            Run run = new Run();
            run.AppendChild(new Text(text));
            RunProperties runProp = new RunProperties();
            runProp.Append(
              new Justification() { Val = JustificationValues.Center },
              new FontSize { Val = new StringValue((fontSize * 2).ToString()) },
              new RunFonts()
              {
                  Ascii = "Times New Roman",
                  HighAnsi = "Times New Roman",
                  EastAsia = "Times New Roman",
                  ComplexScript = "Times New Roman"
              });
            run.PrependChild(runProp);
            
            ParagraphProperties paraProperties = new ParagraphProperties();
            paraProperties.Append(new Justification() { Val = JustificationValues.Center }/*, new BottomBorder() { Val = BorderValues.Single }*/);
            Paragraph p = new Paragraph();
            p.Append(paraProperties);
            p.AppendChild(run);
            return p;
        }

        public static Paragraph GetString(string text, ParagraphProperties paraProperties, int fontSize = 14)
        {
            Run run = new Run();
            run.AppendChild(new Text(text));
            RunProperties runProp = new RunProperties();
            runProp.Append(
              new Justification() { Val = JustificationValues.Center },
              new FontSize { Val = new StringValue((fontSize * 2).ToString()) },
              new RunFonts()
              {
                  Ascii = "Times New Roman",
                  HighAnsi = "Times New Roman",
                  EastAsia = "Times New Roman",
                  ComplexScript = "Times New Roman"
              });
            run.PrependChild(runProp);

            Paragraph p = new Paragraph();
            p.Append(paraProperties);
            p.AppendChild(run);
            return p;
        }


        

        public static byte[] CreatePartTimeDoc(PartTimeDataModel result, byte[] docForm)
        {

           //string docDirectory = new FileInfo(@"doc").Directory.FullName + @"\doc\trudovoi dogovor dla sovmestiteley — копия.docx";
            byte[] bytes;// = File.ReadAllBytes(docDirectory);
            using (var stream = new MemoryStream())
            {
                stream.Write(docForm, 0, docForm.Length);
                using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, true))
                {
                    Body bod = doc.MainDocumentPart.Document.Body;

                    DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                    dtDateTime = dtDateTime.AddSeconds(result.dateOfReceipt).ToLocalTime();

                    var texts = bod.SelectMany(p => p.Elements<Run>()).SelectMany(r => r.Elements<Text>());
                    foreach (Text text in texts)
                    {
                        switch (text.Text)
                        {
                            case " _________":
                                //text.Text = $"{result.numContract}";
                                text.Text = " _________";
                                break;
                            case "____»______________":
                                text.Text = $"                                {dtDateTime.Day}» {GetMonthString(dtDateTime.Month)} {dtDateTime.Year} г.";
                                break;
                            case "20       г.":
                                text.Text = "";
                                break;
                            default:
                                break;
                        }
                    }


                    var fullName = bod.ChildElements[15].ChildElements[2].ChildElements[1].ChildElements[1];

                    Run run = new Run();
                    run.AppendChild(new Text($"{result.fullName}"));
                    RunProperties runProp = new RunProperties();
                    runProp.Append(
                      new FontSize { Val = new StringValue("28") },
                      new RunFonts()
                      {
                          Ascii = "Times New Roman",
                          HighAnsi = "Times New Roman",
                          EastAsia = "Times New Roman",
                          ComplexScript = "Times New Roman"
                      }
                      );
                    run.PrependChild(runProp);

                    fullName.AppendChild(run);

                    var contractSize = bod.ChildElements[19].ChildElements[3].ChildElements[1];
                    contractSize.RemoveChild(contractSize.ChildElements[1]);
                    contractSize.AppendChild(GetString($" {result.contractSize} "));

                    var position = bod.ChildElements[19].ChildElements[2].ChildElements[1];
                    position.RemoveChild(position.ChildElements[1]);
                    position.AppendChild(GetString($" {result.position} "));

                    var salary = bod.ChildElements[30].ChildElements[2].ChildElements[1];
                    salary.RemoveChild(salary.ChildElements[1]);
                    salary.AppendChild(GetString($" {(result.salary * (decimal)result.contractSize)} "));

                    var address = bod.ChildElements[85].ChildElements[3].ChildElements[1];
                    address.RemoveChild(address.ChildElements[1]);
                    address.AppendChild(GetString($" Адрес: {result.address } "));



                    var serial = bod.ChildElements[85].ChildElements[6].ChildElements[1];
                    serial.RemoveChild(serial.ChildElements[1]);
                    serial.AppendChild(GetString($"Паспорт: серия {result.passportSerial }"));

                    var number = bod.ChildElements[85].ChildElements[6].ChildElements[2];
                    number.RemoveChild(number.ChildElements[1]);
                    number.AppendChild(GetString($"№ {result.passportSerial }"));

                    var number1 = bod.ChildElements[85].ChildElements[6];
                    number1.RemoveChild(number1.ChildElements[3]);


                    var pDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(result.passportDate);

                    var passportDate = bod.ChildElements[85].ChildElements[7].ChildElements[2];
                    passportDate.RemoveChild(passportDate.ChildElements[1]);
                    passportDate.AppendChild(GetString($" {pDate.ToString("dd-MM-yyyy")} "));

                    var passportIssued = bod.ChildElements[85].ChildElements[9].ChildElements[2];
                    passportIssued.RemoveChild(passportIssued.ChildElements[1]);
                    passportIssued.AppendChild(GetString($" {result.passportIssued} "));

                    var snils = bod.ChildElements[85].ChildElements[12].ChildElements[1];
                    snils.RemoveChild(snils.ChildElements[1]);
                    snils.AppendChild(GetString($" {result.SNILS} "));

                    var INN = bod.ChildElements[85].ChildElements[13].ChildElements[2];
                    INN.RemoveChild(INN.ChildElements[1]);
                    INN.AppendChild(GetString($" {result.INN} "));
                    //величина ставки 19 - 3 - 2
                    // работник - 15 - 2 - 2 
                    //дата - 10 - 3
                    //трудовой договор - 7 - 1 
                    // должность 19 - 2 - 2 
                    // должностной оклад - 30 - 2 - 2 
                    // адрес - 85 - 3 - 2
                    // Паспорт: серия 85 - 6 - 4
                    // Паспорт: номер 85 - 6 - 4
                    // Паспорт: дата выдачи 85 - 9 - 3
                    // Паспорт: кем выдан 85 - 10
                    //ИНН 85 - 13 - 3

                    doc.Save();
                  
                }
                bytes = stream.ToArray();
            }
            return bytes;

        }
    }
}
