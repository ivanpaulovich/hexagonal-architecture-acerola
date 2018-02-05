import { Component, OnInit, Input } from '@angular/core';
import { Register } from '../commands/register';
import { CustomerService } from '../customer.service';
import { Customer } from '../customer';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  register: Register = {
    pin: "",
    name: "",
    initialAmount: 500.0
  };

  customer: Customer = {
    customerId: "",
    name: "",
    ssn: ""
  };


  constructor(private customerService: CustomerService,
    private router: Router) { }

  ngOnInit() {
  }

  add(pin: string, name: string, initialAmount: number): void {
    const reg: Register = new Register(pin, name, initialAmount);
    this.customerService.register(reg)
      .subscribe(customer => {
        this.customer = customer;
        this.router.navigate([`/my-accounts/${this.customer.customerId}` ]);
      });
  }

}
