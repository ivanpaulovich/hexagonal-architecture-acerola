import { Transaction } from './transaction';

export class Account {
    constructor(
        public AccountId: string,
        public CustomerId: string,
        public CurrentBalance: number,
        public Transactions: Transaction[]
    ) { }
}
