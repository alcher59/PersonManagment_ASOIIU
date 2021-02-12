import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalaryService {
  

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

  public getAccruals(type: number): Observable<any[]> {

    switch (type) {
      case 1:
        return this.http.get<any[]>('/api/Accruals/Payroll');
      case 2:
        return this.http.get<any[]>('/api/vacation/SickLeaves');
      case 3:
        return this.http.get<any[]>('/api/Recruitment/Awards');
      case 4:
        return this.http.get<any[]>('/api/vacation/BusinessTrips');
      case 5:
        return this.http.get<any[]>('/api/vacation');
      default:
        return this.http.get<any[]>('/api/vacation/AllAccurals');
    }

    /*const data = [
      {"id":1,type: 1,"dateOfCreation":1588259164,"employees":"","number":"12345","documentAccruals":"Зарплата","accrued":10.0,"withheld":10.0,"comment":"com"},
      {"id":2,type: 2,"dateOfCreation":1588259164,"employees":"","number":"12345","documentAccruals":"Больничный лист","accrued":10.0,"withheld":10.0,"comment":"com"},
      {"id":3,type: 3,"dateOfCreation":1588259164,"employees":"","number":"12345","documentAccruals":"Премия","accrued":10.0,"withheld":10.0,"comment":"com"},
      {"id":4,type: 4,"dateOfCreation":1588259164,"employees":"","number":"12345","documentAccruals":"Командировка","accrued":10.0,"withheld":10.0,"comment":"com"},
      {"id":5,type: 5,"dateOfCreation":1588259164,"employees":"","number":"12345","documentAccruals":"Отпуск","accrued":10.0,"withheld":10.0,"comment":"com"},
      {"id":6,type: 2,"dateOfCreation":1588259164,"employees":"","number":"12345","documentAccruals":"Больничный лист","accrued":10.0,"withheld":10.0,"comment":"com"},
      {"id":7,type: 1,"dateOfCreation":1588259164,"employees":"","number":"12345","documentAccruals":"Зарплата","accrued":10.0,"withheld":10.0,"comment":"com"},
      {"id":8,type: 3,"dateOfCreation":1588259164,"employees":"","number":"12345","documentAccruals":"Премия","accrued":10.0,"withheld":10.0,"comment":"com"},
      {"id":9,type: 4,"dateOfCreation":1588259164,"employees":"","number":"12345","documentAccruals":"Командировка","accrued":10.0,"withheld":10.0,"comment":"com"},
      {"id":10,type: 5,"dateOfCreation":1588259164,"employees":"","number":"12345","documentAccruals":"Отпуск","accrued":10.0,"withheld":10.0,"comment":"com"}
    ];

    const filtred = type ? data.filter((item) => { return item.type == type; }) : data;

    return of(filtred);*/
  }

  createAccrual(type: string, data){
    switch (type) {
      case "salary":
        return this.http.post('/api/Accruals/Payroll', data);
      case "sick_leave":
        return this.http.post('/api/vacation/SickLeaves', data);
      case "prize":
        return this.http.post('/api/Recruitment/Awards', data);
      case "business_trip":
        return this.http.post('/api/vacation/BusinessTrips', data);
      case "vacation":
        return this.http.post('/api/vacation', data);
      default:
        break;
    }
  }

  getAccrual(type: number, id: number): Observable<any> {
    switch (type) {
      case 1:
        return this.http.get<any>(`/api/Accruals/Payroll?id=${id}`);
      case 2:
        return this.http.get<any>(`/api/vacation/SickLeaves?id=${id}`);
      case 3:
        return this.http.get<any>(`/api/Recruitment/Awards?id=${id}`);
      case 4:
        return this.http.get<any>(`/api/vacation/BusinessTrips?id=${id}`);
      case 5:
        return this.http.get<any>(`/api/vacation?id=${id}`);
      default:
        break;
    }
  }

  updateAccrual(type: number, id: number, data: any) {
    switch (type) {
      case 1:
        return this.http.put(`/api/Accruals/Payroll?id=${id}`, data);
      case 2:
        return this.http.put(`/api/vacation/SickLeaves?id=${id}`, data);
      case 3:
        return this.http.put(`/api/Recruitment/Awards?id=${id}`, data);
      case 4:
        return this.http.put(`/api/vacation/BusinessTrips?id=${id}`, data);
      case 5:
        return this.http.put(`/api/vacation?id=${id}`, data);
      default:
        break;
    }
  }

  deleteAccrual(type: number, id: number, ) {
    switch (type) {
      case 1:
        return this.http.delete(`/api/Accruals/Payroll?id=${id}`);
      case 2:
        return this.http.delete(`/api/vacation/SickLeaves?id=${id}`);
      case 3:
        return this.http.delete(`/api/Recruitment/Awards?id=${id}`);
      case 4:
        return this.http.delete(`/api/vacation/BusinessTrips?id=${id}`);
      case 5:
        return this.http.delete(`/api/vacation?id=${id}`);
      default:
        break;
    }
  }
  

  getReport(date: number): Observable<any[]> {
    //return this.http.get<any[]>('/api/report');
    return this.http.get<any[]>(`/api/TimeSheet/TimeSheetMonth/${date}`);
  }

  getEmployees(): Observable<any[]> {
    return this.http.get<any[]>('/api/employees')
  }
 
 
}
