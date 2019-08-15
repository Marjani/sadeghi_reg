using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Registration.Manager.Startup))]
namespace Registration.Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
