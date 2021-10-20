using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimpleNote.Tests
{
    public class MockTestServerFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        public Action<IServiceCollection> Register { get; set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                Register?.Invoke(services);
            });
        }
    }
}
