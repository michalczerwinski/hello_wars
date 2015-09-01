using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SampleBot.Startup))]
namespace SampleBot
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
