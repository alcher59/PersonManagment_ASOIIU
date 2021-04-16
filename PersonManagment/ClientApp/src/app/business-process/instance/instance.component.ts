import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl,Validators } from '@angular/forms';
import { HttpResponse } from '@angular/common/http';
import { from, Observable, of, Subscription } from 'rxjs';
import {MatSnackBar} from '@angular/material/snack-bar';

import { Router, ActivatedRoute, Params } from '@angular/router';
import { map, switchMap } from 'rxjs/operators';
import { BusinessProcessService } from '../business-process.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import {
  AfterContentInit,
  ElementRef,
  Input,
  OnChanges,
  OnDestroy,
  Output,
  SimpleChanges,
  EventEmitter
} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import * as BpmnJS from 'bpmn-js/dist/bpmn-modeler.production.min.js';
// import { importDiagram } from './rx';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-instance',
  templateUrl: './instance.component.html',
  styleUrls: ['./instance.component.scss']
})
export class InstanceComponent implements OnInit, AfterContentInit, OnChanges, OnDestroy {
  form: FormGroup
  tasks$: Observable<any[]>;
  sub$: Subscription;
  displayedColumns: string[] = ['taskName'];
  dataSource = new MatTableDataSource<any>();
  processInstanceId: string;
  instance$: any;
  taskId$: string;
  taskData: any;
  title: string = 'БП:';
  description: string;
  processXML: string;
  test: string = ``;
  bpmnJS: BpmnJS;
  @ViewChild('ref', { static: true }) el: ElementRef;
  @Output() importDone: EventEmitter<any> = new EventEmitter();
  @Input() url: string;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  
  constructor(private service: BusinessProcessService, private router: Router, private _snackBar: MatSnackBar, private route: ActivatedRoute, private http: HttpClient) { 
    this.bpmnJS = new BpmnJS();

    this.bpmnJS.on('import.done', ({ error }) => {
      if (!error) {
        this.bpmnJS.get('canvas').zoom('fit-viewport');
        console.log("done!!!!");
      }
      else
        {
            console.log(error);
        }
    });
  }

 
  ngOnInit(): void {
      this.form = new FormGroup({});
      this.form.disable();
      // this.loadUrl();
      // this.bpmnJS.attachTo(this.el.nativeElement);
      this.tasks$ = this.route.params.pipe(
        switchMap((params: Params) => {
          let param = params['processInstanceId'];
          if (param) {
            this.processInstanceId = param;
            this.instance$ = this.service.getInstance(param);
            return this.service.getTasks(param);
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
      this.form.disable();
      obs$.subscribe(() =>{
        this.ngOnInit();
        this._snackBar.open('Задача завершена', 'Выполнение', {
          duration: 3000,
        });
      }, 
        error => {
          this._snackBar.open('Задача не выбрана', 'Выполнение', {
            duration: 3000,
          });
        }
      );
    }
    else{
      this._snackBar.open('Задача не завершена', 'Выполнение', {
        duration: 3000,
      });
    }
    this.form.enable();
  };
  
  ngAfterContentInit(): void {
    // this.bpmnJS.attachTo(this.el.nativeElement);
   
  }

  ngOnChanges(changes: SimpleChanges) {
    // re-import whenever the url changes
    if (changes.url) {
      // this.loadUrl();
    }
  }
  
  loadUrl(): any {
    this.processXML = this.service.getDiagramm('Recruitment:1:ae2c26db-988a-11eb-8a18-fcaa14b60bbd');
    
    return (
      this.importDiagram(this.processXML).subscribe(
        (warnings) => {
          this.importDone.emit({
            type: 'success',
            warnings
          });
        },
        (err) => {
          this.importDone.emit({
            type: 'error',
            error: err
          });
        }
      )
    )
  }
  private importDiagram(xml: string): Observable<{warnings: Array<any>}> {
    return from(this.bpmnJS.importXML(xml) as Promise<{warnings: Array<any>}>);
  }
  ngOnDestroy(): void {
    // destroy BpmnJS instance
    this.bpmnJS.destroy();

    // this.viewer.attachTo(this.el.nativeElement);
    if (this.sub$) {
      this.sub$.unsubscribe();
    }
  }


  ToBusinessProcess() {
    this._snackBar.open('Процесс завершён', 'Уведомление', {
      duration: 3000,
    });
    this.router.navigate([`/business-process`]);
  }
    
  stopProcess() {
    // this.ngOnInit();
    this.service.deleteProcessInstance(this.processInstanceId).subscribe(() => {
      this.ToBusinessProcess();
    });
    
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
