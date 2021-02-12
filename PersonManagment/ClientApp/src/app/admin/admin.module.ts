import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms'; 

import { SharedModule } from '../shared/shared.module';

import { AdminPageComponent } from './admin-page/admin-page.component';
import { RoleComponent } from './role/role.component';




@NgModule({
  declarations: [AdminPageComponent, RoleComponent, ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule
  ]
})
export class AdminModule { }
