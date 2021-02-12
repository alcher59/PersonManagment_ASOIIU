import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PersonnelService } from '../personnel.service';
import { MatSnackBar } from '@angular/material/snack-bar';

import { switchMap } from 'rxjs/operators';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-sick-leave',
  templateUrl: './sick-leave.component.html',
  styleUrls: ['./sick-leave.component.scss']
})
 
export class SickLeaveComponent implements OnInit {
  
  form: FormGroup
  title = 'Больничный лист';
  minDate = new Date(1960, 0, 1);
  employmentId: number;
  employmentsTypes$: Observable<any[]>;
  
  employees$: Observable<any[]>;
  personalService: any;
  constructor(private service: PersonnelService, private router: Router, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }


  ngOnInit(): void {
    this.title = 'Больничный лист';
    this.form = new FormGroup({
   
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
                this.title = `Больничный лист: ${person.FullName}`;
              });
            }
          }, 
          (error) => this._snackBar.open(error.error.message));
  }

  submit(){

  }
}

