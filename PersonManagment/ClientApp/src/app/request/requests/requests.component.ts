import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';


import { switchMap } from 'rxjs/operators';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { RequestService } from '../request.service';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.scss']
})
export class RequestsComponent implements OnInit {
  displayedColumns: string[] = ['title', 'employee', 'category', 'description', ];
  dataSource = new MatTableDataSource<any>();
  sub$: Subscription;
  Request$: Observable<any[]>;
  Archive$: Observable<any[]>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private service: RequestService, private route: ActivatedRoute, private router: Router) { }
  requestType: number = -1;

  ngOnInit(): void {
    this.Request$ = this.service.getRequests();
    this.Archive$ = this.service.getRequestArchive();

    this.sub$ = this.Request$.subscribe((data) => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
 
}
