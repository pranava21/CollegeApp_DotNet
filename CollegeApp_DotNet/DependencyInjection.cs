using CollegeApp_DotNet.BusinessDomain.BusinessLogic;
using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.Repository;

namespace CollegeApp_DotNet.WebServices
{
    public static class DependencyInjection
    {
        public static void RegisterServies(IServiceCollection services)
        {
            services.AddScoped<IStudentBL, StudentBL>();
            services.AddScoped<IStudentRepository, StudentRepository>();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentBL, DepartmentBL>();
            services.AddScoped<IFacultyBL, FacultyBL>();
            services.AddScoped<IFacultyRepository, FacultyRepository>();
            services.AddScoped<IAccountsBL, AccountsBL>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
        }
    }
}
