import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Register } from './model/commands/register';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { Customer } from './model/customer';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json',
  'Access-Control-Allow-Origin': '*' })
};

@Injectable()
export class CustomerService {

  private customersUrl = 'http://grape.westus2.cloudapp.azure.com:8000/api/Customers';

  constructor(
    private http: HttpClient) { }

  public register(register: Register): Observable<string> {
    return this.http.post<string>(this.customersUrl, register, httpOptions)
      .pipe(
        map(res => res['id']),
        tap(h => { console.log(h); })
      );
  }

  public getCustomer(customerId: string): Observable<Customer> {
    const url = `${this.customersUrl}/${customerId}`;
    return this.http.get<Customer>(url);
  }
}
