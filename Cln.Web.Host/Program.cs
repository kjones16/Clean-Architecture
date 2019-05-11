using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Cln.Web.Api
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Seed().Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }

    public static class DatabaseSeedInitializer
    {
        public static IWebHost Seed(this IWebHost host)
        {
            return host;
        }
    }
}
