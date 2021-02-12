import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

import { SalaryService } from '../salary.service';

const titles = {
  1: 'Начисления зарплаты',
  2: 'Больничные листы',
  3: 'Премии',
  4: 'Командировки',
  5: 'Отпуска'
};

@Component({
  selector: 'app-accruals',
  templateUrl: './accruals.component.html',
  styleUrls: ['./accruals.component.scss']
})
export class AccrualsComponent implements OnInit {

  accruals$: Observable<any[]>;
  displayedColumns: string[] = ['dateOfCreation', 'number', 'documentAccruals', 'accrued', 'withheld', 'employees', 'comment'];

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  constructor(private service: SalaryService, private route: ActivatedRoute, private router: Router ) { }
  docType: number = -1;
  title: string = 'Все начисления';
  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.docType = ~~params['type'] || -1;

      if (titles[this.docType]) {
        this.title = titles[this.docType];
      }

      this.accruals$ = this.service.getAccruals(~~params['type']);
    });
  }

  toCreateForm(){
    switch (this.docType) {
      case 1:
        this.router.navigate(['/salary/accruals/create/salary']);
        break;
      case 2:
        this.router.navigate(['/salary/accruals/create/sick_leave']);
        break;
      case 3:
        this.router.navigate(['/salary/accruals/create/prize']);
        break;
      case 4:
        this.router.navigate(['/salary/accruals/create/business_trip']);
        break;
      case 5:
        this.router.navigate(['/salary/accruals/create/vacation']);
        break;
      default:
        break;
    }
  }

}
