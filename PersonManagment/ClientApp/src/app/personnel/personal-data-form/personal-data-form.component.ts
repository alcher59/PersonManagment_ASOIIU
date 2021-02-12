import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl,Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { PersonnelService } from '../personnel.service';

@Component({
  selector: 'app-personal-data-form',
  templateUrl: './personal-data-form.component.html',
  styleUrls: ['./personal-data-form.component.scss']
})
export class PersonalDataFormComponent implements OnInit {

  form: FormGroup;
  country$: Observable<any[]>; 
  title = 'Личные данные';
  personalData: any;

  constructor(private service: PersonnelService, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }
    
  minDate = new Date(1960, 0, 1);

  ngOnInit(): void {
    this.title = 'Личные данные';
    this.form = new FormGroup({
    series: new FormControl(null, [Validators.required, 
      Validators.minLength(4), Validators.maxLength(4),
      Validators.pattern('^[0-9]+$')]),
    number: new FormControl(null, [Validators.required,
      Validators.minLength(6), Validators.maxLength(6),
      Validators.pattern('^[0-9]+$')]),
    country: new FormControl(1, [Validators.required]),
    division: new FormControl(null, [Validators.required, 
      Validators.pattern('[а-яА-Я ]*')]),
    documentType: new FormControl("1", [Validators.required]),
    code: new FormControl(null, [Validators.required, 
      Validators.minLength(6), Validators.maxLength(6),
      Validators.pattern('^[0-9]+$')]),
    place: new FormControl(null, [Validators.required,]),
    validity: new FormControl(new Date()),
    sdate: new FormControl(new Date()),
      
    }) 
    this.country$ = this.service.getCountry();

    this.route.params.pipe(
      switchMap((params: Params) => {
        if (params['id']) {
          return this.service.getPersonData(params['id']);
        } 
        return of(null)
      })
    ).subscribe(
      (personalData: any) => {
        if (personalData) {
          this.personalData = personalData;

          this.service.getEmployee(this.personalData.EmployeeId).subscribe((person: any) => {
            this.title = `${person.FullName}: Личные данные`;
          });

          //this.title = `Сотрудник: ${this.employee.FullName}`;
          this.form.patchValue({
            place: this.personalData.Birthplace
          });
        } 
        this.form.enable();
    }, 
      (error) => this._snackBar.open(error.error.message)
    );
  }

  submit() {
    let obs$;
    if (this.form.valid) {
      this.form.disable();

      const place = this.form.controls['place'].value;
      const documentType = this.form.controls['documentType'].value;
      const series = this.form.controls['series'].value;
      const number = this.form.controls['number'].value;

      const data = {
        Birthplace: place,
        DocumentTypeId: documentType,
        EmployeeId: this.personalData.EmployeeId,
        PersonAddressId: this.personalData.PersonAddressId,
        PersonContactsId: this.personalData.PersonContactsId
      };

      obs$ = this.service.updatePesonData(this.personalData.Id, data);

      obs$.subscribe((id: any) =>{
        this.form.enable();
        this._snackBar.open('Изменения сохранены', 'Сохранение', {
          duration: 2000,
        });
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
