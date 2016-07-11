using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(summer_school_mvc.Startup))]
namespace summer_school_mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
