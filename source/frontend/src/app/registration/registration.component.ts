import { Component, OnInit, Input } from '@angular/core';
import { Register } from '../model/commands/register';
import { CustomerService } from '../customer.service';
import { Customer } from '../model/customer';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerModel: Register = { pin: '', name: '', initialAmount: 0};
  customerModel: Customer;

  constructor(private customerService: CustomerService,
    private router: Router) { }

  ngOnInit() {
  }

  public register(pin: string, name: string, initialAmount: number): void {
    const reg: Register = new Register(pin, name, initialAmount);
    this.customerService.register(reg)
      .subscribe(customerId => {
        this.router.navigate([`/customer/${customerId}` ]);
      });
  }
}
