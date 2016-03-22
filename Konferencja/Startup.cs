using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Konferencja.Startup))]
namespace Konferencja
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
