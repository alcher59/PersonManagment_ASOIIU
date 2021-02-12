import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { PersonnelService } from '../personnel.service';

@Component({
  selector: 'app-dismissal',
  templateUrl: './dismissal.component.html',
  styleUrls: ['./dismissal.component.scss']
})
export class DismissalComponent implements OnInit {

  form: FormGroup
  title = 'Увольнение';
  minDate = new Date(1960, 0, 1);
  employmentId: number;
  employmentsTypes$: Observable<any[]>;
  
  employees$: Observable<any[]>;
  personalService: any;
  causes: Array<any> = [];

  constructor(private service: PersonnelService, private router: Router, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.title = 'Увольнение';
    this.form = new FormGroup({
      general: new FormGroup({
        dismissaldate: new FormControl(new Date()),
        datep: new FormControl(new Date()),
        base: new FormControl(null, [Validators.required]), 
      }),
    });

    this.form.disable();
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
      (dismissal: any) => {
        if (this.employmentId) {
          this.service.getEmployee(this.employmentId).subscribe((person: any) => {
            this.title = `Увольнение: ${person.FullName}`;
          });
        }

        this.form.enable();
      },
      (error) => this._snackBar.open(error.error.message));

      this.causes = [
        { id: 1, text: 'по инициативе работника (ст. 80)' },
        { id: 2, text: 'по инициативе работодателя (ст. 71 и ст. 81)' },
        { id: 3, text: 'истечение срока договора (ст. 79)' }
      ];
  }

  submit(){
    let obs$;
    if (this.form.valid) {
      this.form.disable(); 
    
      const dismissaldate:Date = this.form.get('general').get('dismissaldate').value;
      const base = this.form.get('general').get('base').value;

      const cause = this.causes.find(it => { return it.id == base; });

      const data = {
        employeeId: ~~this.employmentId,
        dateOfDismissal: ~~(dismissaldate.getTime() / 1000),
        cause: cause.text,//в бд Основание в виде тексте (string)
      }

      obs$ = this.service.createDismissal(data);
      
      obs$.subscribe(
        //при успешном запросе
        (id: any) =>{
          this.form.enable();
          this._snackBar.open('Изменения сохранены', 'Сохранение', {
            duration: 2000,
          });
        }, 
        //в случае ошибки
        error => {
          this.form.enable();
          this._snackBar.open('Ошибка сохранения', 'Сохранение', {
            duration: 2000,
          });
        }
      );
    } 
  } 
}