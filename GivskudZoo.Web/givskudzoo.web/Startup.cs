using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GivskudZoo.Web.Startup))]
namespace GivskudZoo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
