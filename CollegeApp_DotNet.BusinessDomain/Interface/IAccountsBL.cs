using CollegeApp_DotNet.BusinessDomain.Models;

namespace CollegeApp_DotNet.BusinessDomain.Interface;

public interface IAccountsBL
{
    Response AddUser(AddUserBM userDetailsBm, string facultyOrStudent);
    ResponseMessageBM<AddUserBM?> GetUserDetails(string emailId);
}