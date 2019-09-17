using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyAlfaLive.Startup))]
namespace MyAlfaLive
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
