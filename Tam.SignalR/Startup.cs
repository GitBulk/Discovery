using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tam.SignalR.Startup))]
namespace Tam.SignalR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
