using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Motorsazan.CMMS.Client.Startup))]

namespace Motorsazan.CMMS.Client
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) => app.MapSignalR();
    }
}
