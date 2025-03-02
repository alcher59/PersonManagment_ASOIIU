import { Injectable } from '@angular/core';
import { Observable, throwError, of } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  
    constructor(private http: HttpClient) { }
    /**
    * Handle Http operation that failed.
    * Let the app continue.
    * @param operation - name of the operation that failed
    * @param result - optional value to return as the observable result
    */
   private handleError<T> (operation = 'operation', result?: T) {
     return (error: any): Observable<T> => {
 
       // TODO: send the error to remote logging infrastructure
       console.error(error); // log to console instead
 
       // TODO: better job of transforming error for user consumption
       console.log(`${operation} failed: ${error.message}`);
 
       // Let the app keep running by returning an empty result.
       return of(result as T);
     };
   }

  getRoles():Observable<any[]> {
    return this.http.get<any[]>('/api/UserRoles/Index');
  }

  createRole(data: any) {
    return this.http.post('/api/UserRoles/CreateRole', data)
    .pipe(catchError(this.handleError('CreateRole', data)));
  }
}