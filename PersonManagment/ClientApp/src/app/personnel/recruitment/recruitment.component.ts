import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl,Validators } from '@angular/forms';
import { HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import {MatSnackBar} from '@angular/material/snack-bar';

import { Router, ActivatedRoute, Params } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { PersonnelService } from '../personnel.service';


@Component({
  selector: 'app-recruitment',
  templateUrl: './recruitment.component.html',
  styleUrls: ['./recruitment.component.scss']
})
export class RecruitmentComponent implements OnInit {
  [x: string]: any;
  form: FormGroup; 
  title = 'Оформление на работу';
  positions$: Observable<any[]>;
  units$: Observable<any[]>;
  employmentsTypes$: Observable<any[]>;

  isNew = true;
  recruitment: any; 

  minDate = new Date(1960, 0, 1);

  employmentId: number;
  
  constructor(private service: PersonnelService, private router: Router, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.title = 'Оформление на работу';
    this.form = new FormGroup({
      general: new FormGroup({
        startdate: new FormControl(new Date()),
        subdivision: new FormControl(1, [Validators.required]),
        position: new FormControl(null, [Validators.required]),
        schedule: new FormControl("1", [Validators.required]), 
        vz: new FormControl(null, [Validators.required]),
        probation: new FormControl(0,[Validators.required,Validators.pattern('^[0-9]+$'),
        Validators.minLength(1), Validators.maxLength(1)]),
      }),
      vacation: new FormGroup({
        vacationtype: new FormControl("2", [Validators.required]), 
        vacationdays: new FormControl(28,[Validators.required,Validators.pattern('^[0-9]+$')]),
      }),
      esalary: new FormGroup({
        salary: new FormControl(null,[Validators.required,Validators.pattern('^[+-]?([0-9]*[.])?[0-9]+$')]),
        rates:  new FormControl(null,[Validators.required,Validators.pattern('^[+-]?([0-9]*[.])?[0-9]+$'), Validators.minLength(1)]),
      }),
      econtract: new FormGroup({
        contract: new FormControl(null,[Validators.pattern('^[0-9]+$')]),
        org_contract: new FormControl(null,[Validators.pattern('^[0-9]+$')]),
        sdate: new FormControl(new Date()),
        fdate: new FormControl(new Date()),
      }),
      doc: new FormGroup({
      
      })
    })

    this.form.disable();

    this.positions$ = this.service.getPositions();
    this.units$ = this.service.getUnits();
    this.employmentsTypes$ = this.service.getEmploymentsTypes();

   
    this.route.params.pipe(
      switchMap((params: Params) => {
        if (params['id']) {
          this.employmentId = params['id'];
          return this.service.getRecruitment(this.employmentId);
        } 
        return of(null)
      })
    ).subscribe(
      (recruitment: any) => {
        if (this.employmentId) {
          this.service.getEmployee(this.employmentId).subscribe((person: any) => {
            this.title = `Оформление на работу: ${person.FullName}`;
          });
        }
        
        if (recruitment) {
          this.isNew = false;
          this.recruitment = recruitment;

          this.form.get('general').patchValue({
            startdate: new Date(this.recruitment.DateOfReceipt * 1000),
            subdivision: this.recruitment.UnitId,
            position: this.recruitment.PositionId,
            schedule: ""+this.recruitment.SheduleId, 
            vz: this.recruitment.TypeOfEmploymentId,
            probation: this.recruitment.Probation
          });

          this.form.get('vacation').patchValue({
            vacationtype: ""+this.recruitment.Vacation.VacationEntitlementId,
            vacationdays: this.recruitment.Vacation.VacationDays,
          });

          this.form.get('esalary').patchValue({
            salary: this.recruitment.Salary.Value,
            rates: this.recruitment.Salary.Rates
          });

          this.form.get('econtract').patchValue({
            contract: this.recruitment.Contract.ContractNumber,
            sdate: new Date(this.recruitment.Contract.StartDate * 1000),
            fdate: new Date(this.recruitment.Contract.FinishDate * 1000)
          });
        }
        this.form.enable();
      }, 
      (error) => this._snackBar.open(error.error.message));
  }
  submit(){
    let obs$;
    if (this.form.valid) {
      this.form.disable(); 

      const position = this.form.get('general').get('position').value;
      const startdate:Date = this.form.get('general').get('startdate').value;
      const subdivision = this.form.get('general').get('subdivision').value; 
      const vz = this.form.get('general').get('vz').value; 
      const probation = this.form.get('general').get('probation').value;
      const schedule = this.form.get('general').get('schedule').value;

      const sdate: Date = this.form.get('econtract').get('sdate').value; 
      const fdate: Date = this.form.get('econtract').get('fdate').value; 
      const contract = this.form.get('econtract').get('contract').value;

      const salary = this.form.get('esalary').get('salary').value;
      const rates = this.form.get('esalary').get('rates').value;

      const vacationtype = this.form.get('vacation').get('vacationtype').value;
      const  vacationdays = this.form.get('vacation').get('vacationdays').value;
     
      const data = {
        EmployeeId: ~~this.employmentId,
        PositionId: position,
        DateOfReceipt: ~~(startdate.getTime() / 1000),
        UnitId: subdivision,
        TypeOfEmploymentId: vz,
        SheduleId: ~~schedule,
        Probation: ~~probation,
        Contract: {
          ContractNumber: contract,
          StartDate: ~~(sdate.getTime() / 1000),
          FinishDate: ~~(fdate.getTime() / 1000)
        },
        Salary:{
          Value: salary,
          Rates: rates
        },
        Vacation: {
          VacationEntitlementId: ~~vacationtype,
          VacationDays: vacationdays,
        }
      }

      if (this.isNew) {
        obs$ = this.service.createRecruitment(data);
      } else {
        obs$ = this.service.updateRecruitment(this.employmentId, data);
      }

      obs$.subscribe(
        //при успешном запросе
        (id: any) =>{
          this.form.enable();
          this._snackBar.open('Изменения сохранены', 'Сохранение', {
            duration: 2000,
          });

          if (this.isNew) {
            this.router.navigate([`/personnel/employee/recruitment/${this.employmentId}`]);
          }
        }, 
        //в случае ошибки
        error => {
          this.form.enable();
          this._snackBar.open('Ошибка сохранения', 'Сохранение', {
            duration: 2000,
          });
        }
      );
    } else{
      this.validateAllFormFields(this.form);
      this._snackBar.open('Заполните поля', 'Сохранение', {
        duration: 2000,
      });
    }
  }

  ExportContract(){
    const vz = this.form.get('general').get('vz').value;
    if (vz) {
      //получаем договор по id сотрудника
      this.service.exportContract(this.employmentId, vz).subscribe((data) => {
        //в случае успешного экспорта
        this.downloadFile(data);
      },
      (error) => {
        //в случае ошибки экспорта
        this._snackBar.open("Трудовой договор: ошибка экспорта");
      });
    } else {
      this._snackBar.open("Не указан вид занятости");
    }
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

  calcVacation(){
    const sdate: Date = this.form.get('econtract').get('sdate').value; 
    const fdate: Date = this.form.get('econtract').get('fdate').value;
    
    const days = Math.round((1 + fdate.getMonth() - sdate.getMonth()) * 2.33);

    this.form.get('vacation').get('vacationdays').setValue(days);
  }
}
