import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl,Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable, of } from 'rxjs';

import { PersonnelService } from '../../personnel/personnel.service';
import { SalaryService } from '../salary.service';

@Component({
  selector: 'app-create-report',
  templateUrl: './create-report.component.html',
  styleUrls: ['./create-report.component.scss']
})
export class CreateReportComponent implements OnInit {
  form: FormGroup

  isNew = true;
  title: string = 'Табель(создание)';
  employees$: Observable<any[]>;

  constructor(private service: SalaryService, private personalService: PersonnelService, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }

  ngOnInit(): void {

    
    this.employees$ = this.personalService.getEmployees();

    this.route.params.subscribe((params: Params) => {
        if (params['type']) {
         
        } 

        this.form.enable();
    }, 
      (error) => this._snackBar.open('Ошибка')
    );
  }
 submit(){
  
 }
}
