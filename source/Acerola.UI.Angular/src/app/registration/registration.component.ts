import { Component, OnInit, Input } from '@angular/core';
import { Register } from '../commands/register';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  register: Register = {
    PIN: "",
    name: "",
    initialAmount: 500.0
  };

  constructor() { }

  ngOnInit() {
  }

}
