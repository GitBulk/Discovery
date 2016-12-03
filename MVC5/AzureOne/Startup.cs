using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AzureOne.Startup))]
namespace AzureOne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
