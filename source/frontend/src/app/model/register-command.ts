export class RegisterCommand {
    constructor(
        public pin: string,
        public name: string,
        public initialAmount: number) { }
}
