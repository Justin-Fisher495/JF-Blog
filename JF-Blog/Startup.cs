using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JF_Blog.Startup))]
namespace JF_Blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
