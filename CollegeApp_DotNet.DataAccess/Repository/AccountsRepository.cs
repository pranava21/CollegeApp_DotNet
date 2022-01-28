using System.Collections.Generic;
using System.Linq;
using CollegeApp_DotNet.BusinessDomain.Models;
using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.Models;
using CollegeApp_DotNet.DataAccess.RepositoryModels;
using Newtonsoft.Json;

namespace CollegeApp_DotNet.DataAccess.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        #region Private Variables

        private readonly collegeDatabaseContext _context;
        private readonly IStudentRepository _studentRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly Serilog.ILogger _logger;

        #endregion

        #region Constructor
        public AccountsRepository(collegeDatabaseContext context, Serilog.ILogger logger, IStudentRepository studentRepository, IFacultyRepository facultyRepository)
        {
            this._context = context;
            this._logger = logger;
            _studentRepository = studentRepository;
            _facultyRepository = facultyRepository;
        }
        #endregion

        #region Public Methods
        public string AddUser(AddUserDM userDetails, string facultyOrStudent)
        {
            _logger.Information("Module: AccountsRepository/AddUser - Repository: START");

            var user = new User
            {
                UserId = userDetails.UserId,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Phone = userDetails.Phone,
                EmailId = userDetails.EmailId,
                Address = userDetails.Address,
                Age = userDetails.Age,
                DepartmentUid = (Guid)userDetails.DepartmentUid,
                IsFaculty = userDetails.isFaculty,
                IsStudent = userDetails.isStudent
            };

            this._context.Users.Add(user);
            using var transaction = this._context.Database.BeginTransaction();
            try
            {
                this._context.SaveChanges();
                transaction.Commit();
                _logger.Information("Module: AccountsRepository/AddUser - Repository: " +
                                   JsonConvert.SerializeObject(user));
                _logger.Information("Module: AccountsRepository/AddUser - Repository: Student Added");
                _logger.Information("Module: AccountsRepository/AddUser - Repository: END");

                if (facultyOrStudent.ToLower().Equals("student"))
                {
                    var student = new AddStudentDetailsDM
                    {
                        FirstName = userDetails.FirstName,
                        LastName = userDetails.LastName,
                        Email = userDetails.EmailId,
                        Address = userDetails.Address,
                        DepartmentUid = userDetails.DepartmentUid.ToString(),
                        Phone = userDetails.Phone
                    };
                    _ = _studentRepository.AddStudent(student);
                }
                else if (facultyOrStudent.ToLower().Equals("faculty"))
                {
                    var faculty = new AddFacultyDetailsDM()
                    {
                        FirstName = userDetails.FirstName,
                        LastName = userDetails.LastName,
                        EmailId = userDetails.EmailId,
                        Phone = userDetails.Phone,
                        Address = userDetails.Address,
                        DepartmentUid = userDetails.DepartmentUid.ToString()
                    };
                    _ = this._facultyRepository.AddFaculty(faculty);
                }
                return "User Added Successfully";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.Information("Module: AccountsRepository/AddUser - Repository: " + ex.ToString());
                _logger.Information("Module: AccountsRepository/AddUser - Repository: User not added");
                _logger.Information("Module: AccountsRepository/AddUser - Repository: END");
                return "User not saved";
            }
        }

        public bool CheckForExistingEmails(string emailId)
        {
            var emails = (from u in this._context.Users
                          where u.EmailId == emailId
                          select u).ToList();

            if (emails.Count > 0) return true;
            return false;
        }

        public bool CheckForExistingUserId(string userId)
        {
            var userIds = (from u in this._context.Users
                           where u.UserId == userId
                           select u).ToList();

            if (userIds.Count != 0) return true;
            return false;
        } 
        #endregion
    }
}
