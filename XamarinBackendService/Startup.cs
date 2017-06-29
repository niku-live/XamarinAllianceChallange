using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(XamarinBackendService.Startup))]

namespace XamarinBackendService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}