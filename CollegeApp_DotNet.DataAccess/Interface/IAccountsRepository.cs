using CollegeApp_DotNet.DataAccess.RepositoryModels;

namespace CollegeApp_DotNet.DataAccess.Interface;

public interface IAccountsRepository
{
    string AddUser(AddUserDM userDetails, string facultyOrStudent);
    bool CheckForExistingEmails(string emailId);
    bool CheckForExistingUserId(string userId);
    AddUserDM? GetUserDetails(string emailId);
}