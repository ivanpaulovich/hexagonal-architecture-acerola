import { Money } from "./money";

export class Account
{
    constructor(
        public _id: string,
        public customerId: string,
        public currentBalance: Money
    ) { }
}