using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyShopDesign.Startup))]
namespace MyShopDesign
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
