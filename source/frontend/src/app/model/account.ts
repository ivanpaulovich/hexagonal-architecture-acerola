import { MoneyValue } from './money-value';

export class Account {
    constructor(
        public _id: string,
        public Version: number,
        public CustomerId: string,
        public CurrentBalance: MoneyValue
    ) { }
}
