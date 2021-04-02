import { Component, OnInit } from '@angular/core';

import { BusinessProcessService } from '../business-process.service';


@Component({
  selector: 'app-bp-page',
  templateUrl: './bp-page.component.html',
  styleUrls: ['./bp-page.component.scss']
})
export class BusinessProcessPageComponent implements OnInit {

  constructor(private service: BusinessProcessService) { }

  ngOnInit(): void {
    
  }

}
