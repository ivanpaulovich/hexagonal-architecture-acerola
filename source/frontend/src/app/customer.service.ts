import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Register } from './commands/register';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { Customer } from './customer';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json',
  'Access-Control-Allow-Origin': '*' })
};

@Injectable()
export class CustomerService {

  private customersUrl = 'http://grape.westus2.cloudapp.azure.com:8000/api/Customers';

  constructor(
    private http: HttpClient) { }

  public register(register: Register): Observable<Customer> {

    return this.http.post<Customer>(
      this.customersUrl,
      register,
      httpOptions);
  }

  public getCustomer(customerId: string): Observable<Customer> {

    return this.http.post<Customer>(
      this.customersUrl,
      customerId,
      httpOptions);
  }
}
