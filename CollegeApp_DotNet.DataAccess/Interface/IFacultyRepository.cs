using CollegeApp_DotNet.BusinessDomain.Models;
using CollegeApp_DotNet.DataAccess.RepositoryModels;

namespace CollegeApp_DotNet.DataAccess.Interface;

public interface IFacultyRepository
{
	List<FacultyDetailsDM> GetFacultyDetails(string departmentUid);
    bool AddFaculty(AddFacultyDetailsDM facultyDetails);
}