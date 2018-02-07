import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Account } from './model/account';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { DepositCommand } from './model/deposit-command';
import { WithdrawCommand } from './model/withdraw-command';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json',
  'Access-Control-Allow-Origin': '*' })
};

@Injectable()
export class AccountService {

  private accountsUrl = 'http://grape.westus2.cloudapp.azure.com:8000/api/Accounts';

  constructor(
    private http: HttpClient) { }

  getAccounts(customerId: string): Observable<Account[]> {
    const url = `${this.accountsUrl}/?customerId=${customerId}`;
    return this.http.get<Account[]>(url);
  }

  getAll(): Observable<Account[]> {
    const url = `${this.accountsUrl}`;
    return this.http.get<Account[]>(url);
  }

  get(accountId: string): Observable<Account> {
    const url = `${this.accountsUrl}/${accountId}`;
    return this.http.get<Account>(url);
  }

  public deposit(deposit: DepositCommand): void {
    this.http.patch(this.accountsUrl + '/Deposit', deposit, httpOptions);
  }

  public withdraw(withdraw: WithdrawCommand): void {
    this.http.patch(this.accountsUrl + '/Withdraw', withdraw, httpOptions);
  }
}
