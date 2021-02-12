import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import {TableModule} from 'primeng/table';
import {InputTextModule} from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';
import { SharedModule } from '../shared/shared.module';

import { RequestPageComponent } from './request-page/request-page.component';
import { RequestsComponent } from './requests/requests.component';
import { CreateRequestComponent } from './create-request/create-request.component';


@NgModule({
  declarations: [RequestPageComponent, RequestsComponent, CreateRequestComponent, ],
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
export class RequestModule { }