using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Cln.Web.Api
{
  public sealed class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }

  public static class DatabaseSeedInitializer
  {
    public static IWebHost Seed(this IWebHost host)
    {
      return host;
    }
  }
}
