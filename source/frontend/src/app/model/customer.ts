import { Name } from './value-objects/name';
import { PIN } from './value-objects/pin';
import { Account } from './account';

export class Customer {
    constructor(
        public _id: string,
        public Version: Number,
        public Name: Name,
        public PIN: PIN,
        public Accounts: Account[]
    ) { }
}
