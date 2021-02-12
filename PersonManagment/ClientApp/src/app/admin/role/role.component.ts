import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

import { AdminService } from '../admin.service'; 


@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent implements OnInit {

  form: FormGroup;
  roles$: Observable<any[]>;

  constructor(private service: AdminService, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl(null, [Validators.required, Validators.minLength(1)])
    })

    this.roles$ = this.service.getRoles();
  }

  submit() {
    const name = this.form.controls['name'].value;
    const data = {
      Name: name
    };
    this.form.disable();
    this.service.createRole(data).subscribe(() => {
      this.form.enable();

      this._snackBar.open('Изменения сохранены', 'Сохранение', {
        duration: 2000,
      });

      this.form.reset();
      this.roles$ = this.service.getRoles();
    }, error => {
      this.form.enable();
      this._snackBar.open(error ? error.error : 'Ошибка сохранения', 'Сохранение', {
        duration: 2000,
      });
    });
  }

}
