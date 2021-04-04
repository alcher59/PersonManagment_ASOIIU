import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { NgGanttEditorModule } from 'ng-gantt'; 

import { SharedModule } from '../shared/shared.module';
import { BusinessProcessPageComponent } from './bp-page/bp-page.component';
import { InstanceComponent } from './instance/instance.component';

@NgModule({
  declarations: [BusinessProcessPageComponent, InstanceComponent],
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
