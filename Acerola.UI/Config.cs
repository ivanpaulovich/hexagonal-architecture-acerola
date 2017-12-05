namespace MyAccountAPI.Producer.UI
{
    public class Config
    {
        public string SecretKey { get; private set; }
        public string Issuer { get; private set; }

        public Config(string secretKey, string issuer)
        {
            this.SecretKey = secretKey;
            this.Issuer = issuer;
        }
    }
}
