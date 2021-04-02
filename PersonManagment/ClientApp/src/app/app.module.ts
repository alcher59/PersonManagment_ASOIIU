import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ServiceWorkerModule } from '@angular/service-worker';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { OverviewModule } from './overview/overview.module';
import { PersonnelModule } from './personnel/personnel.module';
import { SalaryModule } from './salary/salary.module';
import { PaymentsModule } from './payments/payments.module';
import { ReportingModule } from './reporting/reporting.module';
import { AdminModule } from './admin/admin.module';
import { RequestModule } from './request/request.module'; 
import { BusinessProcessModule } from './business-process/business-process.module'; 

import { LoginModule } from './login-page/login.module';
import { TokenInterceptor } from './shared/interceptors/token.interceptor';

import { AppComponent } from './app.component';
import { AuthLayoutComponent } from './shared/layouts/auth-layout/auth-layout.component';
import { SiteLayoutComponent } from './shared/layouts/site-layout/site-layout.component';

import { environment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    AuthLayoutComponent,
    SiteLayoutComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    SharedModule,
    OverviewModule,
    PersonnelModule,
    BusinessProcessModule,
    SalaryModule,
    RequestModule, 
    PaymentsModule,
    ReportingModule,
    AdminModule,
    LoginModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      multi: true,
      useClass: TokenInterceptor
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
