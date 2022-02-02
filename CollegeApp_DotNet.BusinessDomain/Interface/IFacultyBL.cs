using CollegeApp_DotNet.BusinessDomain.Models;

namespace CollegeApp_DotNet.BusinessDomain.Interface;

public interface IFacultyBL
{
	ResponseMessageBM<List<FacultyDetailsBM>> GetFacultyDetails(string departmentUid);
    ResponseMessageBM<FacultyDetailsBM> GetFaculty(string emailId, string departmentUid);
}