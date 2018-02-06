import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { Account } from '../model/account';
import { ActivatedRoute } from '@angular/router';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-my-accounts',
  templateUrl: './my-accounts.component.html',
  styleUrls: ['./my-accounts.component.css']
})
export class MyAccountsComponent implements OnInit {
  accountsModel: Account[];
  displayedColumns = ['description'];

  constructor(
    private route: ActivatedRoute,
    private accountService: AccountService) { }

  ngOnInit() {
    this.getAccounts();
  }

  getAccounts() {
    const customerId = this.route.snapshot.paramMap.get('customerId');
    this.accountService
      .getAccounts(customerId)
      .subscribe(accounts => this.accountsModel = accounts);
  }

}
