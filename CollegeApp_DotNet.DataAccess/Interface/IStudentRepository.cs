using CollegeApp_DotNet.DataAccess.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.DataAccess.Interface
{
    public interface IStudentRepository
    {
        List<StudentDetails> GetStudentDetails(string departmentUid);
    }
}
