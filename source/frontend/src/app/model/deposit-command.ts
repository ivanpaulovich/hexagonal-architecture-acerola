export class DepositCommand {
    constructor(
        public CustomerId: string,
        public AccountId: string,
        public Amount: number
    ) { }
}
