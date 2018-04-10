using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SSDF_CertificateAdmin.Startup))]
namespace SSDF_CertificateAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
