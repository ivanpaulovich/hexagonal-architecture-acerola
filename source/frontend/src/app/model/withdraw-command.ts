export class WithdrawCommand {
    constructor(
        public CustomerId: string,
        public AccountId: string,
        public Amount: number
    ) { }
}
