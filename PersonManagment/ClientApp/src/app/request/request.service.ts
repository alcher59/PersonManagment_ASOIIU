import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
  })
  export class RequestService {
    
  
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
    
    //Получить список заявок
    getRequests(): Observable<any[]> {
      return this.http.get<any[]>('api/Employees/Request');
    }
    //получить заявку по id
    getRequest(id: number): Observable<any[]> {
      return this.http.get<any[]>(`api/Employees/Request?id=${id}`);
    }
    //создать заявку
    createRequest(data: any) {
      return this.http.post('/api/Employees/Request', data)
      .pipe(tap(x => x),
        catchError(err => {  
          console.log(err); 
          return throwError(err);
      }));
    }
    //обновить заявку (редактирование)
    updateRequest(id: number, data: any) {
      return this.http.put(`/api/Employees/Request?id=${id}`, data);
    }
    //удалить заявку
    deleteRequest(id: number) {
      return this.http.delete(`/api/Employees/Request?id=${id}`);
    }
    //получить список архивных заявок 
    getRequestArchive(): Observable<any[]> {
      return this.http.get<any[]>('api/employees/RequestArchive');
    }

  completeRequest(id: number, data: any) {
     return this.http.post(`api/employees/RequestComplete/Request?id=${id}`, data);
  }

   
}