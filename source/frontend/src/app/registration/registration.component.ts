import { Component, OnInit, Input } from '@angular/core';
import { Register } from '../model/register';
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

  constructor(private customerService: CustomerService,
    private router: Router) { }

  ngOnInit() {
  }

  public register(): void {
    this.customerService.register(this.registerModel)
      .subscribe(customer => {
        this.router.navigate([`/customer/${customer._id}` ]);
      });
  }
}
