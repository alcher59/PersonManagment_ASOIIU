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

    startProcess(data: any): Observable<any> {
      return this.http.post(`/api/Camunda/StartProcess`, data)
      .pipe(tap(x => x),
        catchError(err => {  
          console.log(err); 
          return throwError(err);
      }));
    }
  
    getInstances(): Observable<any[]> {
      return this.http.get<any[]>('/api/Camunda/GetProcessInstances')
    }  

    getInstance(processInstanceId: string): any {
      return this.http.get<any>(`api/Camunda/GetProcessInstance?processInstanceId=${processInstanceId}`)
    }

    getTasks(processInstanceId: string): Observable<any[]> {
      return this.http.get<any[]>(`/api/Camunda/GetUserTasks?processInstanceId=${processInstanceId}`)
    } 

    // getVariables(processInstanceId: string): any {
    //   return this.http.get<any>(`api/Camunda/GetProcessVariables?processInstanceId=${processInstanceId}`)
    // }

    getEmployeeName(employeeId: number): string{
      if(employeeId != -1)
      {
        this.http.get<any>(`api/Employees?id=${employeeId}`).subscribe((data: any) => {
           return `Сотрудник: ${data.FullName}`;
        });
      }
      else
      {
        return "empty"; 
      }
    }
    completeTask(taskId: string): any {
      return this.http.post(`/api/Camunda/CompleteUserTask?taskId=${taskId}`, null)
      .pipe(tap(x => x),
        catchError(err => {  
          console.log(err); 
          return throwError(err);
      }));
    }

    getDiagramm(processDefinitionId: string): any{
      return this.http.get(`/api/Camunda/GetProcessInstanceXML?processDefinitionId=${processDefinitionId}`, { responseType: 'text'});
    }

    deleteProcessInstance(processInstanceId: string): any{
      return this.http.delete(`/api/Camunda/StopProcess?processInstanceId=${processInstanceId}`)
    }
}
