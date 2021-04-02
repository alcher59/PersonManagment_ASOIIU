import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { NgGanttEditorModule } from 'ng-gantt'; 

import { SharedModule } from '../shared/shared.module';
import { BusinessProcessPageComponent } from './bp-page/bp-page.component';


@NgModule({
  declarations: [BusinessProcessPageComponent],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgGanttEditorModule,
  ]
})
export class BusinessProcessModule { }
