import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl,Validators } from '@angular/forms';
import {MatSnackBar} from '@angular/material/snack-bar';
import { Observable, of } from 'rxjs';

import { PersonnelService } from '../personnel.service';
import { BusinessProcessService } from 'src/app/business-process/business-process.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { HttpResponse } from '@angular/common/http';

export interface personnel{
  text: any
  id?: number
}

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.scss']
})
export class CreateEmployeeComponent implements OnInit, AfterViewInit, OnDestroy {
  person: personnel[]=[
    {text: '', id: 1}, 
  ]

  form: FormGroup
  positions$: Observable<any[]>;
  units$: Observable<any[]>;
  employmentsTypes$: Observable<any[]>;
  country$: Observable<any[]>;

  minDate = new Date(1960, 0, 1);

  title: string = 'Сотрудник(создание)';
  isNew = true;
  employee: any;
  processName: any;

  constructor(private service: PersonnelService, private bpservice: BusinessProcessService, private router: Router, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }
  
  ngOnDestroy(): void {

  }
  ngAfterViewInit(): void {
    
  }
 
  ngOnInit(){
    this.form = new FormGroup({
      general: new FormGroup({
        surname: new FormControl(null, [Validators.required]),
        name: new FormControl(null, [Validators.required]),
        patronymic: new FormControl(null, [Validators.required]),
        tnumber: new FormControl(null, [Validators.pattern('^[0-9]+$')])
      }),
      //Личные данные
      personal: new FormGroup({
        birthday: new FormControl(new Date()),
        gender: new FormControl("1", [Validators.required]),
        inn: new FormControl(null, [Validators.required, 
          Validators.minLength(12), Validators.maxLength(12),
          Validators.pattern('^[0-9]+$')]),
        snils: new FormControl(null, [Validators.required, 
          Validators.minLength(11),
          Validators.pattern('^[\+]|[\-]|[\ ]|[\_]\w+$')]),
        country: new FormControl(1, [Validators.required]),
        documentType: new FormControl("1", [Validators.required]),
        series: new FormControl(null, [Validators.required, 
          Validators.minLength(4), Validators.maxLength(4),
          Validators.pattern('^[0-9]+$')]),
        number: new FormControl(null, [Validators.required,
          Validators.minLength(6), Validators.maxLength(6),
          Validators.pattern('^[0-9]+$')]),
        //Кем выдан
        division: new FormControl('', [Validators.required, Validators.pattern('[а-яА-Я ]*')]),
        sdate: new FormControl(new Date()),
        code: new FormControl(0),
        phone: new FormControl (0, [Validators.pattern('^[0-9]+$')]),
        address: new FormControl(''), 
      }),
    education: new FormGroup({
        teducation: new FormControl(1, []),
        university: new FormControl(1, []),
        startdate: new FormControl(new Date()),
        finishdate: new FormControl(new Date()),
        specialty: new FormControl(null, []),
        qualification: new FormControl(1, []),
        type: new FormControl(1, []),
        dseries: new FormControl(null, [
          Validators.minLength(4), Validators.maxLength(4),
          Validators.pattern('^[0-9]+$')]),
        dnumber: new FormControl(null, [ 
          Validators.minLength(4), Validators.maxLength(4),
          Validators.pattern('^[0-9]+$')]),
        degrees: new FormControl(1, []),
        rank: new FormControl(1, []),
        research: new FormControl(1, []),
        invention: new FormControl(1, [])
      }),
  
    });

    this.form.disable();

    this.employmentsTypes$ = this.service.getEmploymentsTypes();
    this.country$ = this.service.getCountry();

    this.route.params.pipe(
      switchMap((params: Params) => {
        if (params['id']) {
          this.isNew = false;
          return this.service.getEmployee(params['id']);
        } 
        return of(null)
      })
    ).subscribe(
      (employee: any) => {
        if (employee) {
          this.employee = employee;
          this.processName =  'Recruitment';
          this.title = `Сотрудник: ${this.employee.FullName}`;

          const fullName: string = this.employee.FullName;
          const nameParts = fullName.split(' ');

          this.form.get('general').patchValue({
            surname: nameParts.length > 0 ? nameParts[0] : '',
            name: nameParts.length > 1 ? nameParts[1] : '',
            patronymic: nameParts.length > 2 ? nameParts[2] : '',
            tnumber: this.employee.PersonnelNumber,
          });

          this.form.get('personal').patchValue({
            birthday: new Date(this.employee.PersonData.DateBirth * 1000),
            gender: ""+this.employee.PersonData.Gender,
            inn: this.employee.PersonData.INN,
            snils: this.employee.PersonData.SNILS,
            country: this.employee.PersonData.Citizenship,
            series: this.employee.PersonData.PassportData.Series,
            number: this.employee.PersonData.PassportData.Number,
            division: this.employee.PersonData.PassportData.Division,
            sdate: new Date(this.employee.PersonData.PassportData.ValidityDocumentDateStart * 1000),
            code: this.employee.PersonData.PassportData.Code,
            phone: this.employee.PersonData.Contacts.Phone,
            address: this.employee.PersonData.Contacts.Address
          });

          this.form.get('education').patchValue({

          })
        } 
        this.form.enable();
    }, 
      (error) => this._snackBar.open(error.error.message)
    );
  }

  submit(){
    let obs$;
    if (this.form.valid) {
      this.form.disable();

      //Главное
      const surname: string = this.form.get('general').get('surname').value;
      const name: string = this.form.get('general').get('name').value;
      const patronymic: string = this.form.get('general').get('patronymic').value;
      const tnumber = this.form.get('general').get('tnumber').value;

      //Личные данные
      //Дата рождения
      const birthday: Date = this.form.get('personal').get('birthday').value;
      //Пол
      const gender = this.form.get('personal').get('gender').value;
      //ИНН
      const inn = this.form.get('personal').get('inn').value;
      //СНИЛС
      const snils = this.form.get('personal').get('snils').value;
      //Гражданство
      const country = this.form.get('personal').get('country').value;
      //Тип документа (Паспорт)
      const documentType = this.form.get('personal').get('documentType').value;
      //Серия
      const series = this.form.get('personal').get('series').value;
      //Номер
      const number = this.form.get('personal').get('number').value;
      //Кем выдан
      const division = this.form.get('personal').get('division').value;
      //Дата выдачи
      const sdate: Date = this.form.get('personal').get('sdate').value;
      //Код подразделения
      const code = this.form.get('personal').get('code').value;
      //Телефон
      const phone = this.form.get('personal').get('phone').value;
      //Адрес
      const address = this.form.get('personal').get('address').value;

      //Образование
      const  teducation = this.form.get('education').get('teducation').value;
      const  university = this.form.get('education').get('university').value;
      const startdate: Date = this.form.get('education').get('startdate').value;
      const finishdate: Date = this.form.get('education').get('finishdate').value;
      const specialty = this.form.get('education').get('specialty').value;
      const  qualification = this.form.get('education').get('qualification').value;
      const type = this.form.get('education').get('type').value;
      const dseries = this.form.get('education').get('dseries').value;
      const dnumber = this.form.get('education').get('dnumber').value;
      const degrees = this.form.get('education').get('degrees').value;
      const rank = this.form.get('education').get('rank');
      const research = this.form.get('education').get('research');
      const  invention = this.form.get('education').get(' invention');

      const fullName = `${surname.trim()} ${name.trim()} ${patronymic.trim()}`;

      const data = {
        "FullName": fullName,
        "PersonnelNumber": tnumber,
        "PersonData": {
          "DateBirth": ~~(birthday.getTime() / 1000),
          "Gender": gender,
          "INN": inn,
          "SNILS": snils,
          "Citizenship": country,//
          "Birthplace": "",
          "DocumentTypeId": documentType,
          "PassportData":{
              "Series": series,
              "Number": number,
              "Division": division,
              "ValidityDocumentDateStart": ~~(sdate.getTime() / 1000),
              "Code": code,
              "InformationСitizenshipDateStart": ~~(this.minDate.getTime() / 1000),
              "DocumentPassportId": 1
          },
          "Contacts": {
            "Phone": phone,
            "Address": address
          }
        }
      };

      if (this.isNew) {
        obs$ = this.service.createEmployee(data);
      } else {
        obs$ = this.service.updateEmployee(this.employee.Id, data);
      }

      obs$.subscribe((id: any) =>{
        this.form.enable();
        this._snackBar.open('Изменения сохранены', 'Сохранение', {
          duration: 2000,
        });

        if (this.isNew) {
          this.router.navigate([`/personnel/employee/${id}`]);
        }
      }, 
        error => {
          this.form.enable();
          this._snackBar.open('Ошибка сохранения', 'Сохранение', {
            duration: 2000,
          });
        }
      );
    }
    else{
      this.validateAllFormFields(this.form);
      this._snackBar.open('Заполните поля', 'Сохранение', {
        duration: 2000,
      });
    }
  };
  
  validate() {  

  }

  validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }

  delete() {
    if (this.employee) {
      this.form.disable();
      this.service.deleteEmployee(this.employee.Id).subscribe(_ =>{
        this.form.enable();
        this.router.navigate(['personnel/employee']);
      }, 
        error => {
          this.form.enable();
          this._snackBar.open('Ошибка удаления', 'Сохранение', {
            duration: 2000,
          });
        }
      );
    }
  }

  ToPersonalData(){
    this.router.navigate([`/personnel/employee/personal_data/${this.employee.PersonData.Id}`]);
  }

  ToRecruitment() {
    this.router.navigate([`/personnel/employee/recruitment/${this.employee.Id}`]);
  }

  ToVacation() {
    this.router.navigate([`/personnel/employee/vacation/${this.employee.Id}`]);
  }
  
  ToLeave() {
    this.router.navigate([`/personnel/employee/sick-leave/${this.employee.Id}`]);
  }
  
  ToEmployeeTransfer(){
    this.router.navigate([`/personnel/employee/employee-transfer/${this.employee.Id}`]);
  }

  ToWorkTrip(){
    this.router.navigate([`/personnel/employee/work-trip/${this.employee.Id}`]);
  }
  
  ToChangeWages(){
    this.router.navigate([`/personnel/employee/change-wages/${this.employee.Id}`]);
  }
  ToDismissal() {
    this.router.navigate([`/personnel/employee/dismissal/${this.employee.Id}`])
  }

  StartBusinessProcess(){
    this.form.disable();
    this.bpservice.startProcess(this.processName).subscribe((processName: any) =>{
      this.form.enable();
      this._snackBar.open(`Процесс ${processName} запущен`, 'Уведомление', {
        duration: 2000,
      });
    }, 
      error => {
        this.form.enable();
        this._snackBar.open('Ошибка запуска процесса', 'Уведомление', {
          duration: 2000,
        });
      }
    );
  }

  exportContract() {
    debugger
    this.service.exportContract(this.employee.Id, 1).subscribe((data) => {
      //в случае успешного экспорта
      this.downloadFile(data);
    },
    (error) => {
      //в случае ошибки экспорта
      this._snackBar.open("Трудовой договор: ошибка экспорта");
    });;
  }

  exportPrivateCard(){
    this.service.exportPrivateCard(this.employee.Id).subscribe((data) => {
      //в случае успешного экспорта
      this.downloadFile(data);
    },
    (error) => {
      //в случае ошибки экспорта
      this._snackBar.open("Личная карточка: ошибка экспорта");
    });;
  }

  downloadFile(data: HttpResponse<Blob>) {
    const a = document.createElement("a");
    const blob = new Blob([data.body], { type: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'});
    a.href = window.URL.createObjectURL(blob);

    var contentDisposition = data.headers.get('content-disposition');
    var filename = contentDisposition.split(';')[1].split('filename')[1].split('=')[1].trim();

    a.setAttribute("download", filename);
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
  }

}
