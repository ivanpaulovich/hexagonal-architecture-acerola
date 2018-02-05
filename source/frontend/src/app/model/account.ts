import { Money } from './value-objects/money';

export class Account {
    constructor(
        public _id: string,
        public Version: Number,
        public CustomerId: string,
        public CurrentBalance: Money
    ) { }
}
