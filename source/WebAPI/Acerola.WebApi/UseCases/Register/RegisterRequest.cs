namespace Acerola.WebApi.UseCases.Register
{
    public class RegisterRequest
    {
        public string Personnummer { get; set; }
        public string Name { get; set; }
        public double InitialAmount { get; set; }
    }
}
