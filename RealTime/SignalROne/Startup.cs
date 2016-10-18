using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignalROne.Startup))]
namespace SignalROne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
