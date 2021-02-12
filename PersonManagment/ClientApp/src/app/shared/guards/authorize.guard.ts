import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivateChild } from '@angular/router';
import { Observable, of } from 'rxjs';

import { AuthorizeService } from '../services/authorize.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeGuard implements CanActivate, CanActivateChild {
  constructor(
    private auth: AuthorizeService,
    private router: Router
  ) {}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | boolean {

      if (this.auth.isAuthenticated()) {
        return of(true);
      } else {
        this.router.navigate(['/login'], {
          queryParams: {
            accessDenied: true
          }
        });
        return of(false);
      }
  }
  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | Observable<boolean> {
    return this.canActivate(next, state);
  }
}
