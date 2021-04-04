import { Injectable } from '@angular/core';
import { Observable, throwError, of } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BusinessProcessService {

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

    startProcess(processName: string): Observable<any> {
      return this.http.post(`/api/Camunda/StartProcess?processName=${processName}`, {})
      .pipe(tap(x => x),
        catchError(err => {  
          console.log(err); 
          return throwError(err);
      }));
    }
  
    getInstances(): Observable<any[]> {
      return this.http.get<any[]>('/api/Camunda/GetProcessInstances')
    }  

    getTasks(processInstanceId: string): Observable<any[]> {
      return this.http.get<any[]>(`/api/Camunda/GetUserTasks?processInstanceId=${processInstanceId}`)
    } 

    completeTask(data: any) {
      this.http.post('/api/Camunda/CompleteUserTask', data)
      .pipe(tap(x => x),
        catchError(err => {  
          console.log(err); 
          return throwError(err);
      }));
    }
}
