import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
  })
  export class AuthorizeService {
    private token: string = null;
    constructor(private http: HttpClient){}
    login(userName: string, password: string): Observable<{token: string}> {
      const form = new FormData();
      form.append('username', userName);
      form.append('password', password);
      return this.http.post<{token: string}>('/api/token/login', form)
      .pipe(
        tap(({token}) => {
          localStorage.setItem('auth-token', token);
          this.setToken(token);
        })
      );
    }
    public isAuthenticated(): boolean {
      return !!this.token || !!localStorage.getItem('auth-token');
    }
    logout() {
      this.setToken(null);
      localStorage.clear();
    }
    setToken(token: string) {
      this.token = token;
    }
  
    getToken(): string {
      return this.token || localStorage.getItem('auth-token');
    }
  }