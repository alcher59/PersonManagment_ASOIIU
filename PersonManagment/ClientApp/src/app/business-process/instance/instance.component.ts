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
  selector: 'app-instance',
  templateUrl: './instance.component.html',
  styleUrls: ['./instance.component.scss']
})
export class InstanceComponent implements OnInit {
  form: FormGroup
  tasks$: Observable<any[]>;
  sub$: Subscription;
  displayedColumns: string[] = ['taskName'];
  dataSource = new MatTableDataSource<any>();
  processInstanceId: string;
  instance$: any;
  taskId$: string;
  taskData: any;
  title: string = 'БП';
  processXML: string;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  
  constructor(private service: BusinessProcessService, private router: Router, private _snackBar: MatSnackBar, private route: ActivatedRoute) { }


  
  ngOnInit(): void {
      this.form = new FormGroup({});
      this.form.disable();
      
      this.tasks$ = this.route.params.pipe(
        switchMap((params: Params) => {
          if (params['processInstanceId']) {
            this.processInstanceId = params['processInstanceId'];
            return this.service.getTasks(params['processInstanceId']);
          } 
          return of(null)
        })
      );

      this.instance$ = this.route.params.pipe(
        switchMap((params: Params) => {
          if (params['processInstanceId']) {
            this.processInstanceId = params['processInstanceId'];
            return this.service.getInstance(params['processInstanceId']);
          } 
          return of(null)
        })
      );
      this.sub$ = this.tasks$.subscribe(
        (data) => {
          if (data) {
            this.dataSource.data = data;
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
          } 
          
          this.form.enable();
      }, 
        (error) => this._snackBar.open(error.error.message)
      );
      this.instance$.subscribe(
        (data) => {
          if (data) {
            this.title = 'БП: ' + data.processDefinitionName;
          } 
          
          this.form.enable();
      }, 
        (error) => this._snackBar.open(error.error.message)
      );
  }
  submit(){
    let obs$;
    obs$ = this.service.completeTask(this.taskId$);
    if (obs$) {
      // this.form.disable();
      obs$.subscribe(() =>{
        this.form.enable();
        this._snackBar.open('Задача завершена', 'Выполнение', {
          duration: 2000,
        });
      }, 
        error => {
          this.form.enable();
          this._snackBar.open('Задача не завершена!', 'Выполнение', {
            duration: 2000,
          });
        }
      );
     
    }
    else{
      this._snackBar.open('Задача не выбрана', 'Выполнение', {
        duration: 2000,
      });
    }
  };
  ngOnDestroy(): void {
    if (this.sub$) {
      this.sub$.unsubscribe();
    }
  }

 

  applyFilter(event: Event){
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  selectRow(row){
    row.highlighted = !row.highlighted;
    this.taskId$ = row.taskId;
  }
}
