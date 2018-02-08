namespace Acerola.IntegrationTests
{
    using Acerola.UI;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using System.Net.Http;
    using Xunit;

    public class CustomerRegistration
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CustomerRegistration()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        //[Fact]
        //public void Test1()
        //{
        //    // Arrange
            
        //}
    }
}
