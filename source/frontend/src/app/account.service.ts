import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Account } from './account';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json',
  'Access-Control-Allow-Origin': '*' })
};

@Injectable()
export class AccountService {

  private accountsUrl = 'http://grape.westus2.cloudapp.azure.com:8000/api/Accounts';

  constructor(
    private http: HttpClient) { }


  /** GET heroes from the server */
  getAccounts(customerId:string): Observable<Account[]> {
    const url = `${this.accountsUrl}/?customerId=${customerId}`;
    return this.http.get<Account[]>(url);
  }
}
