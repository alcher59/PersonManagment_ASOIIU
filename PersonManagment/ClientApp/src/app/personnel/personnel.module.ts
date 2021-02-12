import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { NgGanttEditorModule } from 'ng-gantt'; 

import { SharedModule } from '../shared/shared.module';
import { PersonnelPageComponent } from './personnel-page/personnel-page.component';
import { CreateEmployeeComponent } from './create-employee/create-employee.component';
import { EmployeesComponent } from './employees/employees.component';
import { PersonalDataFormComponent } from './personal-data-form/personal-data-form.component';
import { PositionsListComponent } from './positions-list/positions-list.component';
import { RecruitmentComponent } from './recruitment/recruitment.component';
import { VacationComponent } from './vacation/vacation.component';
import { SickLeaveComponent } from './sick-leave/sick-leave.component';
import { EmployeeTransferComponent } from './employee-transfer/employee-transfer.component';
import { WorkTripComponent } from './work-trip/work-trip.component';
import { ChangeWagesComponent } from './change-wages/change-wages.component';
import { ContractComponent } from './contract/contract.component';
import { DismissalComponent } from './dismissal/dismissal.component';
import { StaffingTableComponent } from './staffing-table/staffing-table.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { OrganizationalStructureComponent } from './organizational-structure/organizational-structure.component';



@NgModule({
  declarations: [PersonnelPageComponent, CreateEmployeeComponent, EmployeesComponent, PersonalDataFormComponent, PositionsListComponent, RecruitmentComponent, VacationComponent, SickLeaveComponent, EmployeeTransferComponent, WorkTripComponent, ChangeWagesComponent, ContractComponent, DismissalComponent, StaffingTableComponent, ScheduleComponent, OrganizationalStructureComponent],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgGanttEditorModule,
  ]
})
export class PersonnelModule { }
