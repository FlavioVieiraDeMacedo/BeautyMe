using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeautyMe.Startup))]
namespace BeautyMe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
