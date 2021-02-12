import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs/internal/Observable';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';

import { PersonnelService } from '../../personnel/personnel.service';
import { RequestService } from '../request.service';
import { of } from 'rxjs';

const titles = {
  work: 'ремонтные работы',
  equipment: 'приобретение канцтоваров',
  stationery: 'приобретение оборудования',
}

@Component({
  selector: 'app-create-request',
  templateUrl: './create-request.component.html',
  styleUrls: ['./create-request.component.scss']
})
export class CreateRequestComponent implements OnInit {

  form: FormGroup
  title: string = 'Создание заявки';
  employees$: Observable<any[]>;
  isNew = true;
  request: any;
  requestId: number;
 

  constructor(
    private service: RequestService, 
    private personalService: PersonnelService, 
    private _snackBar: MatSnackBar, private route: ActivatedRoute, 
    private router: Router) { }

  ngOnInit(): void {
    this.employees$ = this.personalService.getEmployees();

    this.form = new FormGroup({
    general: new FormGroup({
      heading: new FormControl(null, [Validators.required]),
      employee: new FormControl(null, [Validators.required]),
      periodOfExecution: new FormControl(new Date()),
    }),
    work: new FormGroup({
      category: new FormControl("2", [Validators.required]),
      description: new FormControl(null, [Validators.required]),
    }),
    equipment: new FormGroup ({
      // e_category: new FormControl("2", [Validators.required]),
      // e_quantity: new FormControl(null, [Validators.required]),
      // e_description: new FormControl(null, [Validators.required]),
      }),
    stationery: new FormGroup ({
      // s_category:  new FormControl("2", [Validators.required]),
      quantity: new FormControl(null, [Validators.required]),
      }),
    })

    this.form.disable();
    
    this.route.params.pipe(
      switchMap((params: Params) => {
        if (params['id']) {
          this.isNew = false;
          return this.service.getRequest(params['id']);
        } 
        return of(null)
      })
    ).subscribe(
      (request: any) => {
        if (request) {
          this.request = request;
          this.title = `Заявка: ${this.request.title}`;

          const title: string = this.request.title;

          this.form.get('general').patchValue({
            heading: this.request.Title,
            periodOfExecution: this.request.PeriodOfExecution,
          });

          this.form.get('work').patchValue({
            category: this.request.RequestCategoryId,
            description: this.request.Description,
          });

          this.form.get('equipment').patchValue({

          });

          this.form.get('stationery').patchValue({
            quantity: this.request.Amount,
          });

        } 
        this.form.enable();
    }, 
      (error) => this._snackBar.open(error.error.message)
    );
  }

    submit () {
     let obs$;
      if (this.form.valid) {
      this.form.disable();

      const heading = this.form.get('general').get('heading').value;
      const periodOfExecution: Date = this.form.get('general').get('periodOfExecution').value;
      const employee = this.form.get('general').get('employee').value;

      const category = this.form.get('work').get('category').value;
      const description = this.form.get('work').get('description').value;
      
      const quantity = this.form.get('stationery').get('quantity').value;

      const data = {
       "Title": heading,
       "PeriodOfExecution":  ~~(periodOfExecution.getTime() / 1000),
       "RequestCategoryId": category,
       "Description": description,
       "Amount": quantity,
       "EmployeeId": employee,
      };

  if (this.isNew) {
        obs$ = this.service.createRequest(data);
      } else {
        obs$ = this.service.updateRequest(this.request.Id, data);
      }

      obs$.subscribe((id: any) =>{
        this.form.enable();
        this._snackBar.open('Изменения сохранены', 'Сохранение', {
          duration: 2000,
        });

        if (this.isNew) {
          this.router.navigate([`/request/requests/`]);
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
  complete() {
    
  }

  delete() {
    if (this.request) {
      this.form.disable();
      this.service.deleteRequest(this.request.Id).subscribe(_ =>{
        this.form.enable();
        this.router.navigate(['/request/requests']);
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

}
