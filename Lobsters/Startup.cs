using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lobsters.Startup))]
namespace Lobsters
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Models.Startups.Authentication.ConfigureAuth(app);
        }
    }
}
