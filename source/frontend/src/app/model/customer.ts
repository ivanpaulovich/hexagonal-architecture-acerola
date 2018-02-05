import { Name } from './value-objects/name';
import { PIN } from './value-objects/pin';
import { Account } from './account';

export class Customer {
    constructor(
        public id: string,
        public version: Number,
    //    public name: Name,
      //  public pin: PIN,
      //  public Accounts: Account[]
    ) { }
}
