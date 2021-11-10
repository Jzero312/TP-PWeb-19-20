using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PWebTudoDeNovo.Startup))]
namespace PWebTudoDeNovo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
