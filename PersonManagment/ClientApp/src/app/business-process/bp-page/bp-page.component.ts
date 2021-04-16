import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl,Validators } from '@angular/forms';
import { HttpResponse } from '@angular/common/http';
import { Observable, of, Subscription } from 'rxjs';
import {MatSnackBar} from '@angular/material/snack-bar';

import { Router, ActivatedRoute, Params } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { BusinessProcessService } from '../business-process.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';


@Component({
  selector: 'app-bp-page',
  templateUrl: './bp-page.component.html',
  styleUrls: ['./bp-page.component.scss']
})
export class BusinessProcessPageComponent implements OnInit {
  instances$: Observable<any[]>;
  sub$: Subscription;
  displayedColumns: string[] = ['processName', 'processInstanceId', 'description'];
  dataSource = new MatTableDataSource<any>();
  description$: string;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  
  constructor(private service: BusinessProcessService, private router: Router, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.instances$ = this.service.getInstances();
    this.sub$ = this.instances$.subscribe((data: any) => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.description$ = data.description;
    });
  }
  ngOnDestroy(): void {
    if (this.sub$) {
      this.sub$.unsubscribe();
    }
  }
  getDescription(employeeId: number): string{
    return this.service.getEmployeeName(employeeId);
  } 
  
  applyFilter(event: Event){
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  } 
}
