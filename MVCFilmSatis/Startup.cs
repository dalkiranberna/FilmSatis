using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCFilmSatis.Startup))]
namespace MVCFilmSatis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
