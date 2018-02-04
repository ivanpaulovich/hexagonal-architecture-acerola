import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { RegistrationComponent } from './registration/registration.component';
import { MyAccountsComponent } from './my-accounts/my-accounts.component';

const routes: Routes = [
  { path: '', component: RegistrationComponent },
  { path: 'my-accounts/:customerId', component: MyAccountsComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports : [
    RouterModule
  ]
})
export class AppRoutingModule { }
