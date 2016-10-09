using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteDemo.Startup))]
namespace WebsiteDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
