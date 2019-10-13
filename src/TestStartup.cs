using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PlusUltra.Testing
{
    public abstract class TestStartup
    {
        public abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory);
    }
}