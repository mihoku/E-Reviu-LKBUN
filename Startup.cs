using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_Reviu_LKBUN.Startup))]
namespace E_Reviu_LKBUN
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
