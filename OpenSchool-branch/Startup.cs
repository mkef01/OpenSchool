using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OpenSchool.Startup))]
namespace OpenSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
