import { Component, OnInit } from '@angular/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { Observable, Subscription } from 'rxjs';

import { SalaryService } from '../salary.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: [
    './report.component.scss'
  ]
  
})
export class ReportComponent implements OnInit {
  report$: Observable<any[]>;
  employees$: Observable<any[]>;
  sub$: Subscription;
  data = [];
  months = [];
  dateValue: Date;
  constructor(private service: SalaryService) { }

  ngOnInit(): void {
    this.dateValue = new Date();
    const date = ~~(this.dateValue.getTime() / 1000.0);

    const daysCount = this.daysInMounth(this.dateValue.getDate(), this.dateValue.getFullYear());

    this.months = [];
    for (let index = 0; index < daysCount; index++) {
      this.months.push(index + 1);
    }

    this.employees$ = this.service.getEmployees();

    this.sub$ = this.employees$.subscribe((data: Array<any>) => {
      this.data = data.map(it => {return { Id: it.Id, FullName: it.FullName, values: ['Ф8','Ф8','Ф8','Ф8','Ф8','Ф8','В0','В0','Ф8','Ф8','Ф8','Ф8', 'Ф8','В0','В0','Ф8','Ф8','Ф8','Ф8','В0','В0','Ф8','Ф8','Ф8','Ф8','Ф8','В0','В0','Ф8','Ф8'] }});
    });

    this.report$ = this.service.getReport(date);
  }

  daysInMounth(month: number, year: number){
    return new Date(year, month, 0).getDate();
  }

  chosenMonthHandler(normalizedMonth: Date, datepicker: MatDatepicker<any>) {
    const ctrlValue: Date = this.dateValue;
    ctrlValue.setMonth(normalizedMonth.getMonth());
    this.dateValue = ctrlValue;
    datepicker.close();
  }
}
