import { NameValue } from './name-value';
import { PINValue } from './pin-value';
import { MoneyValue } from './money-value';
import { Account } from './account';

export class Transaction {
    constructor(
        public id: string,
        public description: string
    ) { }
}
