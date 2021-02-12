import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PersonnelService } from '../personnel.service';
import { MatSnackBar } from '@angular/material/snack-bar';

import { switchMap } from 'rxjs/operators';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-employee-transfer',
  templateUrl: './employee-transfer.component.html',
  styleUrls: ['./employee-transfer.component.scss']
})
export class EmployeeTransferComponent implements OnInit {
  form: FormGroup
  title = 'Кадровый перевод';
  minDate = new Date(1960, 0, 1);
  employmentId: number;
  positions$: Observable<any[]>;
  units$: Observable<any[]>;
  employmentsTypes$: Observable<any[]>;


  constructor(private service: PersonnelService, private router: Router, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.title = 'Кадровый перевод';
    this.form = new FormGroup({
    general: new FormGroup({
    subdivision: new FormControl(1, [Validators.required]),
    position: new FormControl(null, [Validators.required]),
    startdate: new FormControl(null, [Validators.required]),
    }),
    salary: new FormGroup({
    sum: new FormControl(null,[Validators.required,Validators.pattern('^[+-]?([0-9]*[.])?[0-9]+$')]),
    rates:  new FormControl(null,[Validators.required,Validators.pattern('^[+-]?([0-9]*[.])?[0-9]+$'), Validators.minLength(1)]),
    }),
    })
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
                this.title = `Кадровый перевод: ${person.FullName}`;
              });
            }
          }, 
          (error) => this._snackBar.open(error.error.message));
  }

  submit(){

  }

}
