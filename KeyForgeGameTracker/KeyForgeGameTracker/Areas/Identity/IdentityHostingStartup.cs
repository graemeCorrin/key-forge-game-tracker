using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(KeyForgeGameTracker.Areas.Identity.IdentityHostingStartup))]
namespace KeyForgeGameTracker.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}