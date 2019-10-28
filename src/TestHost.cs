using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PlusUltra.Testing
{
    public abstract class TestHost<T> where T : TestStartup
    {
        public TestHost()
        {
            //Configuration
            var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true)
                .Build();

            Configuration = configurationBuilder;

            var services = new ServiceCollection();

            services.AddSingleton(Configuration);
            services.AddLogging(opt => opt.AddConsole());

            var startup = Activator.CreateInstance<T>();

            startup.ConfigureServices(services, Configuration);

            ServiceProvider = services.BuildServiceProvider();
        }

        private IServiceProvider ServiceProvider { get; }

        private IServiceScope Scope => ServiceProvider.CreateScope();

        public IConfiguration Configuration { get; }
        public E GetService<E>() => Scope.ServiceProvider.GetRequiredService<E>();

        public virtual void Dispose()
		{
			Scope.Dispose();
		}
    }
}