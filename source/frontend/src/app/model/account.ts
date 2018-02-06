import { MoneyValue } from './money-value';
import { Transaction } from './transaction';

export class Account {
    constructor(
        public _id: string,
        public Version: number,
        public CustomerId: string,
        public CurrentBalance: MoneyValue,
        public Transactions: Transaction[]
    ) { }
}
