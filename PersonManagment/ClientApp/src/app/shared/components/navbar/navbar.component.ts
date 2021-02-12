import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthorizeService } from 'src/app/shared/services/authorize.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  public isAuth: boolean = false;
  constructor(private service: AuthorizeService, private router: Router) { }

  ngOnInit(): void {
    this.isAuth = this.service.isAuthenticated();
  }

  logout(event: Event) {
    event.preventDefault();
    this.service.logout();
    this.router.navigate(['/login']);
  }
}
