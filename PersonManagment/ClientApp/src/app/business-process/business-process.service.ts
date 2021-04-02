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

  startProcess(processName: string, data: any): Observable<any> {
      return this.http.post(`/api/Camunda/StartProcess?processName=${processName}`, data)
      .pipe(tap(x => x),
        catchError(err => {  
          console.log(err); 
          return throwError(err);
      }));
    }
  getEmployees(): Observable<any[]> {
    return this.http.get<any[]>('/api/employees')
  }
  getEmployee(id: number): Observable<any> {
    return this.http.get(`/api/employees/${id}`);
  }
  createEmployee(data: any) {
    return this.http.post('/api/employees/', data)
    .pipe(tap(x => x),
      catchError(err => {  
        console.log(err); 
        return throwError(err);
    }));
  }
  updateEmployee(id: number, data: any) {
    return this.http.put(`/api/employees/${id}`, data);
  }
  deleteEmployee(id: number) {
    return this.http.delete(`/api/employees/${id}`);
  }
  getPositions(): Observable<any[]> {
    return this.http.get<any[]>('/api/Recruitment/position');
  }
  createPosition(data: any): Observable<any> {
    return this.http.post('/api/Recruitment/position', data)
    .pipe(tap(x => x),
      catchError(err => {  
        console.log(err); 
        return throwError(err);
    }));
  }
  getUnits(): Observable<any[]> {
    return this.http.get<any[]>('/api/employees/unit');
  }
  getEmploymentsTypes(): Observable<any[]> {
    return this.http.get<any[]>('/api/employees/TypeOfEmployment');
  }
  getStatuses(): Observable<any[]> {
    return this.http.get<any[]>('/api/employees/status');
  }
  getCountry(): Observable<any[]> {
    return this.http.get<any[]>('/api/PersonData/Country') 
  }
  getPersonData(id: number): Observable<any> {
    return this.http.get(`/api/persondata/${id}`);
  }
  updatePesonData(id: number, data: any) {
    return this.http.put(`/api/persondata/${id}`, data);
  }
  createRecruitment(data: any): Observable<any> {
    return this.http.post('/api/Recruitment', data)
    .pipe(tap(x => x),
      catchError(err => {  
        console.log(err); 
        return throwError(err);
    }));
  }
  getRecruitment(id: number): Observable<any> {
    return this.http.get(`/api/Recruitment/${id}`);
  }
  updateRecruitment(id: number, data: any){
    return this.http.put(`/api/Recruitment/${id}`, data);
  }
  
  exportContract(id: number, type: number): Observable<HttpResponse<Blob>> {
    if(type === 1) { //1 - основное место работы
      return this.http.get<Blob>(`/api/Doc/ExportContractDoc/${id}`, { observe: 'response', responseType: 'blob' as 'json'});
      
    } else {//иначе договор для совместителя
      return this.http.get<Blob>(`/api/Doc/PartTimeTemplate/${id}`, { observe: 'response', responseType: 'blob' as 'json'});
    }
  }

  exportPrivateCard(id: number) {
    return this.http.get<Blob>(`api/doc/ExportEmployeeCard/${id}`, { observe: 'response', responseType: 'blob' as 'json'});
  }

  getStaffingTable(): Observable<any[]> {
    return this.http.get<any[]>('/api/StaffingTable');
  }
  createDismissal(data: any): Observable<any> {
    return this.http.post('/api/Recruitment/dismissal', data)
    .pipe(tap(x => x),
      catchError(err => {  
        console.log(err); 
        return throwError(err);
    }));
  }

  updateDismissal(id: number, data: any){
    return this.http.put(`/api/Recruitment/dismissal${id}`, data);
  }
}
