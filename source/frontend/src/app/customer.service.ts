import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RegisterCommand } from './model/register-command';
import { DepositCommand } from './model/deposit-command';
import { WithdrawCommand } from './model/withdraw-command';

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

  public register(register: RegisterCommand): Observable<Customer> {
    return this.http.post<Customer>(this.customersUrl, register, httpOptions);
  }

  public getCustomer(customerId: string): Observable<Customer> {
    const url = `${this.customersUrl}/${customerId}`;
    return this.http.get<Customer>(url);
  }

  public deposit(deposit: DepositCommand): void {
    this.http.patch(this.customersUrl + 'Deposit', deposit, httpOptions);
  }

  public withdraw(withdraw: WithdrawCommand): void {
    this.http.patch(this.customersUrl + 'Withdraw', withdraw, httpOptions);
  }
}
