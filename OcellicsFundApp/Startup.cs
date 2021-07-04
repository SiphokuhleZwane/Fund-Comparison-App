using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OcellicsFundApp.Startup))]
namespace OcellicsFundApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
