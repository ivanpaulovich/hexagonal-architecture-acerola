import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { RegistrationComponent } from './registration/registration.component';
import { MyAccountsComponent } from './my-accounts/my-accounts.component';
import { CustomerComponent } from './customer/customer.component';

const routes: Routes = [
  { path: '', component: RegistrationComponent },
  { path: 'customer/:customerId', component: CustomerComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports : [
    RouterModule
  ]
})
export class AppRoutingModule { }
