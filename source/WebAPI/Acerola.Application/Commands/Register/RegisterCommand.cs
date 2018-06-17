namespace Acerola.Application.Commands.Register
{
    public class RegisterCommand
    {
        public string Personnummer { get; private set; }
        public string Name { get; private set; }
        public double InitialAmount { get; private set; }

        public RegisterCommand(string personnummer, string name, double initialAmount)
        {
            this.Personnummer = personnummer;
            this.Name = name;
            this.InitialAmount = initialAmount;
        }
    }
}
