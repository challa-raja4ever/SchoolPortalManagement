using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeacherStudentPortal.Startup))]
namespace TeacherStudentPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
