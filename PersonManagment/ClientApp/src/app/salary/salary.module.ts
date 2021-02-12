import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import {TableModule} from 'primeng/table';
import {InputTextModule} from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';

import { SalaryPageComponent } from './salary-page/salary-page.component';
import { AccrualsComponent } from './accruals/accruals.component';
import { SharedModule } from '../shared/shared.module';
import { AccrualFormComponent } from './accrual-form/accrual-form.component';
import { ReportComponent } from './report/report.component';
import { CreateReportComponent } from './create-report/create-report.component';


@NgModule({
  declarations: [SalaryPageComponent, AccrualsComponent, AccrualFormComponent, ReportComponent, CreateReportComponent, ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    TableModule,
    InputTextModule,
    CalendarModule,
    SharedModule
  ]
})
export class SalaryModule { }
