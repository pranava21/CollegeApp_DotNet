using CollegeApp_DotNet.BusinessDomain.Models;

namespace CollegeApp_DotNet.BusinessDomain.Interface
{
    public interface IStudentBL
    {
        ResponseMessage<List<StudentDepartmentDetails>> GetStudentDetails(string departmentUid);
    }
}
