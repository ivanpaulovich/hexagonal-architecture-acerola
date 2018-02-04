import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { Account } from '../account';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-my-accounts',
  templateUrl: './my-accounts.component.html',
  styleUrls: ['./my-accounts.component.css']
})
export class MyAccountsComponent implements OnInit {
  accounts: Account[];

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
    .subscribe(accounts => this.accounts = accounts);
  }

}
