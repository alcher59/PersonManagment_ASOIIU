import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthLayoutComponent } from './shared/layouts/auth-layout/auth-layout.component';
import { SiteLayoutComponent } from './shared/layouts/site-layout/site-layout.component';
import { OverviewPageComponent } from './overview/overview-page/overview-page.component';
import { PersonnelPageComponent } from './personnel/personnel-page/personnel-page.component';
import { AuthorizeGuard } from './shared/guards/authorize.guard';
import { CreateEmployeeComponent } from './personnel/create-employee/create-employee.component';
import { EmployeesComponent } from './personnel/employees/employees.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { PersonalDataFormComponent } from './personnel/personal-data-form/personal-data-form.component';
import { SalaryPageComponent } from './salary/salary-page/salary-page.component';
import { PaymentsPageComponent } from './payments/payments-page/payments-page.component';
import { ReportingPageComponent } from './reporting/reporting-page/reporting-page.component';
import { AdminPageComponent } from './admin/admin-page/admin-page.component';
import { AccrualsComponent } from './salary/accruals/accruals.component';
import { AccrualFormComponent } from './salary/accrual-form/accrual-form.component';
import { PositionsListComponent } from './personnel/positions-list/positions-list.component';
import { RecruitmentComponent } from './personnel/recruitment/recruitment.component';
import { VacationComponent } from './personnel/vacation/vacation.component';
import { SickLeaveComponent } from './personnel/sick-leave/sick-leave.component';
import { EmployeeTransferComponent } from './personnel/employee-transfer/employee-transfer.component';
import { WorkTripComponent } from './personnel/work-trip/work-trip.component'; 
import { ChangeWagesComponent } from './personnel/change-wages/change-wages.component';
import { DismissalComponent } from './personnel/dismissal/dismissal.component';
import { StaffingTableComponent } from './personnel/staffing-table/staffing-table.component';
import { ReportComponent } from './salary/report/report.component';
import { CreateReportComponent } from './salary/create-report/create-report.component';
import { RoleComponent } from './admin/role/role.component'; 
import { ScheduleComponent } from './personnel/schedule/schedule.component';
import { RequestPageComponent } from './request/request-page/request-page.component';
import { RequestsComponent } from './request/requests/requests.component';
import { CreateRequestComponent } from './request/create-request/create-request.component';
import { OrganizationalStructureComponent } from './personnel/organizational-structure/organizational-structure.component';
import { BusinessProcessPageComponent } from './business-process/bp-page/bp-page.component';


const routes: Routes = [
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      { path: '', redirectTo: '/login', pathMatch: 'full' },
      { path: 'login', component: LoginPageComponent }
    ]
  }, 
  {
    path: '',
    component: SiteLayoutComponent,
    children: [
      { path: '', redirectTo: '/personnel', pathMatch: 'full', canActivate: [AuthorizeGuard] },
      { path: 'personnel', component: PersonnelPageComponent, canActivate: [AuthorizeGuard] },
      { path: 'overview', component: OverviewPageComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/staffing-table', component: StaffingTableComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/employee', component: EmployeesComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/employee/new', component: CreateEmployeeComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/recruitment/new', component: RecruitmentComponent, canActivate: [AuthorizeGuard]}, 
      { path: 'personnel/employee/:id', component: CreateEmployeeComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/employee/personal_data/:id', component: PersonalDataFormComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/positions-list', component: PositionsListComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/schedule', component: ScheduleComponent, canActivate: [AuthorizeGuard] },
      { path: 'salary', component: SalaryPageComponent, canActivate: [AuthorizeGuard] },
      { path: 'salary/accruals', component: AccrualsComponent, canActivate: [AuthorizeGuard] },
      { path: 'salary/report', component: ReportComponent, canActivate: [AuthorizeGuard]},
      { path: 'salary/report/new', component: CreateReportComponent, canActivate: [AuthorizeGuard] },
      { path: 'salary/accruals/:type', component: AccrualFormComponent, canActivate: [AuthorizeGuard] }, 
      { path: 'salary/accruals/create/:type', component: AccrualFormComponent, canActivate: [AuthorizeGuard] },
      { path: 'payments', component: PaymentsPageComponent, canActivate: [AuthorizeGuard] },
      { path: 'reporting', component: ReportingPageComponent, canActivate: [AuthorizeGuard] },
      { path: 'request', component: RequestPageComponent, canActivate: [AuthorizeGuard] },
      { path: 'request/requests', component: RequestsComponent, canActivate: [AuthorizeGuard] },
      { path: 'request/request/new', component: CreateRequestComponent, canActivate: [AuthorizeGuard] },
      { path: 'admin', component: AdminPageComponent, canActivate: [AuthorizeGuard] },
      { path: 'admin/role', component: RoleComponent, canActivate: [AuthorizeGuard] }, 
      { path: 'personnel/employee/recruitment/:id', component: RecruitmentComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/employee/vacation/:id', component: VacationComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/employee/sick-leave/:id', component: SickLeaveComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/employee/employee-transfer/:id', component: EmployeeTransferComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/employee/work-trip/:id', component:WorkTripComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/employee/change-wages/:id', component:ChangeWagesComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/employee/dismissal/:id', component:DismissalComponent, canActivate: [AuthorizeGuard] },
      { path: 'personnel/organizational-structure', component:OrganizationalStructureComponent, canActivate: [AuthorizeGuard] },

      { path: 'business-process', component: BusinessProcessPageComponent, canActivate: [AuthorizeGuard] }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
