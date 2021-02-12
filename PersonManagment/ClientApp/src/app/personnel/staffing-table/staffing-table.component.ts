import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable, Subscription } from 'rxjs';

import { PersonnelService } from '../personnel.service';

@Component({
  selector: 'app-staffing-table',
  templateUrl: './staffing-table.component.html',
  styleUrls: ['./staffing-table.component.scss']
})
export class StaffingTableComponent implements OnInit {
  [x: string]: any;
  displayedColumns: string[] = ['title', 'Position', 'Unit' ];
  dataSource = new MatTableDataSource<any>();
  sub$: Subscription;
  StaffingTable$: Observable<any[]>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
 
  constructor(private service: PersonnelService) { }

  ngOnInit(): void {
    this.StaffingTable$ = this.service.getStaffingTable();

    this.sub$ = this.StaffingTable$.subscribe((data) => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }


}
