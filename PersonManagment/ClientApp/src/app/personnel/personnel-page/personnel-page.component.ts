import { Component, OnInit } from '@angular/core';

import { PersonnelService } from '../personnel.service';


@Component({
  selector: 'app-personnel-page',
  templateUrl: './personnel-page.component.html',
  styleUrls: ['./personnel-page.component.scss']
})
export class PersonnelPageComponent implements OnInit {

  constructor(private service: PersonnelService) { }

  ngOnInit(): void {
    
  }

}
