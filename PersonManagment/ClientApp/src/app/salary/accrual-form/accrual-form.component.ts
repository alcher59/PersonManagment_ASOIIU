import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormControlName } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable, of } from 'rxjs';


import { PersonnelService } from '../../personnel/personnel.service';
import { SalaryService } from '../salary.service';
import { switchMap } from 'rxjs/operators';

const titles = {
  salary: 'зарплата',
  vacation: 'отпуск',
  business_trip: 'командировка',
  sick_leave: 'больничный лист',
  prize: 'премия'
}

const types = {
  salary: 1,
  vacation: 5,
  business_trip: 4,
  sick_leave: 2,
  prize: 3
}

@Component({
  selector: 'app-accrual-form',
  templateUrl: './accrual-form.component.html',
  styleUrls: ['./accrual-form.component.scss']
})
export class AccrualFormComponent implements OnInit {

  form: FormGroup
  title: string = 'Создание начисления';
  minDate = new Date(1960, 0, 1);
  employees$: Observable<any[]>;
  //Тип документа
  docType: string;
  isNew: boolean = true;
  accrualId: number;
  accrual: any;
  
  constructor(
    private service: SalaryService, 
    private personalService: PersonnelService, 
    private _snackBar: MatSnackBar, private route: ActivatedRoute, 
    private router: Router) { }

  ngOnInit(): void {
    var now = new Date();
    var last = new Date(now.getFullYear(), now.getMonth() + 1, 0); 
    this.form = new FormGroup({
      general: new FormGroup({
        number: new FormControl(1),
        employee: new FormControl(null, [Validators.required]),
        accrued: new FormControl(null, [Validators.required]),
        withheld: new FormControl(0),
      }),
      vacation: new FormGroup({
        reason: new FormControl("2", [Validators.required]),
        startdate: new FormControl(new Date()),
        finishdate: new FormControl(new Date())
      }),
      salary: new FormGroup({
        type: new FormControl("1", [Validators.required]),
        sum: new FormControl(null, [Validators.required, 
        Validators.pattern('^[+-]?([0-9]*[.])?[0-9]+$')]),
        reason: new FormControl(null, [Validators.required]),
        startdate: new FormControl(new Date(now.getFullYear(), now.getMonth(), 1)),
        finishdate: new FormControl(new Date(now.getFullYear(), now.getMonth(), last.getDate()))
      }),
      btrip: new FormGroup({
        sdate: new FormControl(new Date()),
        fdate: new FormControl(new Date()),
        destination: new FormControl(null,),
        organization: new FormControl(null,),
        reason: new FormControl(null,),
        mission: new FormControl(null, )
      }),
      sicklist: new FormGroup({
        cause: new FormControl("2", [Validators.required]),//todo: нужен список
        startdate: new FormControl(new Date()),
        finishdate: new FormControl(new Date())
      }),
      prize: new FormGroup({
        sum: new FormControl(null, [Validators.required, 
        Validators.pattern('^[+-]?([0-9]*[.])?[0-9]+$')]),
        type: new FormControl(null, [Validators.required]),
      }),
    });

    this.route.params.pipe(
      switchMap((params: Params) => {
        if (params['type']) {
          this.docType = params['type'];

          this.title = `${this.title}: ${titles[this.docType]}`
        } 

        if (params['id']) {
          this.accrualId = params['id'];
          return this.service.getAccrual(types[this.docType], this.accrualId);
        } 
        return of(null)
      })
    ).subscribe(
      (accrual: any) => {
      if (accrual) {
        this.isNew = false;
        this.accrual = accrual;

        this.form.get('general').patchValue({

        });

        this.form.get('vacation').patchValue({
         reason: this.accrual.VacationEntitlementId,
         startdate: new Date (this.accrual.DateStart * 1000),
         finishdate: new Date (this.accrual.dateEnd * 1000),
        });

        this.form.get('salary').patchValue({
          type: this.accrual.TypeAccrualId,
          sum: this.accrual.Amount,
          reason: this.accrual.Cause,
          startdate: new Date(this.accrual.PeriodDateStart * 1000),
          finishdate: new Date(this.accrual.PeriodDateEnd * 1000),

        });

        this.form.get('btrip').patchValue({
          sdate: new Date(this.accrual.DateStart * 1000),
          fdate: new Date(this.accrual.DateEnd * 1000),
          destination: this.accrual.Destination, 
          organization: this.accrual.Organization,
          reason: this.accrual.Reason,
          mission: this.accrual.Mission,
        });

        this.form.get('sicklist').patchValue({
          cause: this.accrual.DisablementIncapacityReasonId,
          startdate: new Date (this.accrual.DateStart * 1000),
          finishdate: new Date (this.accrual.DateEnd * 1000),
        });

        this.form.get('prize').patchValue({
          sum: this.accrual.Amount,
          type: this.accrual.TypeAwardId,
        });
      }
      this.form.enable();
    }, 
    (error) => this._snackBar.open('Ошибка'));
  }

  submit(){
    let obs$;
    if (this.form.valid) {
      this.form.disable(); 

    const number = this.form.get('general').get('number').value;
    const employee = this.form.get('general').get('employee').value;
    const accrued = this.form.get('general').get('accrued').value;
    const withheld = this.form.get('general').get('withheld').value;

    var data = {
      Accruals: {
        //Номер
        number: number,
        //Тип документа
        documentAccrualsId: 0,
        //начисилено
        accrued: accrued,
        //удержано
        withheld: withheld,
        dateOfCreation: ~~(new Date().getTime() / 1000),
        AccrualsEmployee:[
          {
            //Сотрудник
            employeeId: employee
          }
        ]
      },
    };
    switch (this.docType) {
      case "salary":
        {
          data.Accruals.documentAccrualsId = 1;

          const type = this.form.get('salary').get('type').value;
          const sum = this.form.get('salary').get('sum').value;
          const reason = this.form.get('salary').get('reason').value;
          const startdate: Date = this.form.get('salary').get('startdate').value;
          const finishdate: Date = this.form.get('salary').get('finishdate').value;

          data = Object.assign(data, {
            typeAccrualId: type,
            employeeId: employee,
            amount: sum,
            cause: reason,
            periodDateStart: ~~(startdate.getTime() / 1000),//к unix-time формату,
            periodDateEnd: ~~(finishdate.getTime() / 1000),//к unix-time формату
          })
        }
        break;
      case "sick_leave":
        {
          const cause = this.form.get('sicklist').get('cause').value;
          const startdate: Date = this.form.get('sicklist').get('startdate').value;
          const finishdate: Date = this.form.get('sicklist').get('finishdate').value;

          data.Accruals.documentAccrualsId = 2;

          data = Object.assign(data, {
            //Причина нетрудоспособности
            disablementIncapacityReasonId: ~~cause,
            //Дата начала
            dateStart: ~~(startdate.getTime() / 1000),//к unix-time формату
            //Дата окончания
            dateEnd: ~~(finishdate.getTime() / 1000),//к unix-time формату
          })
        }
        break;
      case "prize":
        {
          const type = this.form.get('prize').get('type').value;
          const sum = this.form.get('prize').get('sum').value;

          data.Accruals.documentAccrualsId = 3;

          data = Object.assign(data, {
            employeeId: employee,
            //Тип
            typeAwardId: ~~type,
            amount: sum,
           })
        }
        break;
      case "business_trip":
        {
          const sdate: Date = this.form.get('btrip').get('sdate').value;
          const fdate: Date = this.form.get('btrip').get('fdate').value;
          const destination = this.form.get('btrip').get('destination').value;
          const organization = this.form.get('btrip').get('organization').value;
          const reason = this.form.get('btrip').get('reason').value;
          const mission = this.form.get('btrip').get('mission').value;

          data.Accruals.documentAccrualsId = 4;

          data = Object.assign(data, {
            //Дата начала
            dateStart: ~~(sdate.getTime() / 1000),//к unix-time формату
            //Дата окончания
            dateEnd: ~~(fdate.getTime() / 1000),//к unix-time формату
            //Место назначения
            destination: destination,
            //Организация
            organization: organization,
            //Основание
            reason: reason,
            //Цель
            mission: mission,
          });


        };
        break;
      case "vacation":
        {
          const startdate: Date = this.form.get('vacation').get('startdate').value;
          const finishdate: Date = this.form.get('vacation').get('finishdate').value;
          const reason = this.form.get('vacation').get('reason').value;

          data.Accruals.documentAccrualsId = 5;

          data = Object.assign(data, {
            //Дата начала
            dateStart: ~~(startdate.getTime() / 1000),//к unix-time формату
            //Дата окончания
            dateEnd: ~~(finishdate.getTime() / 1000),//к unix-time формату
            //Основание
            vacationEntitlementId: ~~reason
          });
       };
        break;
      default:
        break;
    }

    if (this.isNew) {
      obs$ =  this.service.createAccrual(this.docType, data);
    } else {
      obs$ =  this.service.updateAccrual(types[this.docType], this.accrualId, data) 
    }
    obs$.subscribe(
      //при успешном запросе
      (id: any) =>{
        this.form.enable();
        this._snackBar.open('Изменения сохранены', 'Сохранение', {
          duration: 2000,
        });

        if (this.isNew) {
          this.router.navigate([`/salary/accruals`], { queryParams: { type: types[this.docType] } });
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

  isVisible(field: string) {
    return field === this.docType;
  }
 
  delete() {
    if (this.accrual) {
      this.form.disable();
      this.service.deleteAccrual(this.accrual.type, this.accrual.Id).subscribe(_ =>{
        this.form.enable();
        this.router.navigate(['/salary/accruals']);
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
}