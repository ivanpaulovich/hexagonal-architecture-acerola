import { NameValue } from './name-value';
import { PINValue } from './pin-value';
import { Account } from './account';

export class Customer {
    constructor(
        public _id: string,
        public Version: Number,
        public Name: NameValue,
        public PIN: PINValue
    ) { }
}
