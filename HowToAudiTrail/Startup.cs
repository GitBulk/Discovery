using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HowToAudiTrail.Startup))]
namespace HowToAudiTrail
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
