import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PersonnelService } from '../personnel.service';
import { MatSnackBar } from '@angular/material/snack-bar';

import { switchMap } from 'rxjs/operators';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-vacation',
  templateUrl: './vacation.component.html',
  styleUrls: ['./vacation.component.scss']
})
export class VacationComponent implements OnInit {
  
  form: FormGroup
  title = 'Отпуск';
  minDate = new Date(1960, 0, 1);
  employmentId: number;
  employmentsTypes$: Observable<any[]>;
  
  employees$: Observable<any[]>;
  personalService: any;
  constructor(private service: PersonnelService, private router: Router, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }


  ngOnInit(): void {
    this.title = 'Отпуск';
    this.form = new FormGroup({
    dismissaldate: new FormControl(new Date()),
    datep: new FormControl(new Date()),
    base: new FormControl(null, [Validators.required]), 
    }) 
       
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
                this.title = `Отпуск: ${person.FullName}`;
              });
            }
          }, 
          (error) => this._snackBar.open(error.error.message));
  }

  submit(){

  }
}
