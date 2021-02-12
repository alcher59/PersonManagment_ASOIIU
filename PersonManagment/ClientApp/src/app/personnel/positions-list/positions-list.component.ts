import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

import { PersonnelService } from '../personnel.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';


@Component({
  selector: 'app-positions-list',
  templateUrl: './positions-list.component.html',
  styleUrls: ['./positions-list.component.scss']
})
export class PositionsListComponent implements OnInit {

  positions$: Observable<any[]>;
  form: FormGroup;

  constructor(private service: PersonnelService, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl(null, [Validators.required, Validators.minLength(1)])
    })

    this.positions$ = this.service.getPositions();
  }

  submit() {
    const name = this.form.controls['name'].value;
    const data = {
      Name: name
    };
    this.form.disable();
    this.service.createPosition(data).subscribe(() => {
      this.form.enable();
      this.form.reset();
      this.positions$ = this.service.getPositions();
    });
  }

}
