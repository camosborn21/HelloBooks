using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelloBooks.Startup))]
namespace HelloBooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
