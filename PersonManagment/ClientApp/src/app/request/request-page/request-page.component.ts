import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-request-page',
  templateUrl: './request-page.component.html',
  styleUrls: ['./request-page.component.scss']
})
export class RequestPageComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router) { }
  requestType: number = -1;

  ngOnInit(): void {
  }

  

}
