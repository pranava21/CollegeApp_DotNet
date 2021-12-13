using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.Models;
using CollegeApp_DotNet.DataAccess.RepositoryModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.DataAccess.Repository
{
    public class StudentRepository : IStudentRepository
    {
        #region Private Variables
        private readonly IDepartmentRepository departmentRepository;
        private readonly collegeDatabaseContext context;
        private readonly Serilog.ILogger logger;
        #endregion

        #region Constructor
        public StudentRepository(IDepartmentRepository departmentRepository, collegeDatabaseContext _context, Serilog.ILogger _logger)
        {
            this.departmentRepository = departmentRepository;
            this.context = _context;
            this.logger = _logger;
        }
        #endregion

        #region Public Methods
        public List<StudentDetails> GetStudentDetails(string departmentUid)
        {
            logger.Information("Module: StudentRepository/GetStudentDetails - Repository: START");
            var studentDetails = (from s in this.context.Students
                                  where s.DepartmentUid.ToString() == departmentUid
                                  select new StudentDetails
                                  {
                                      StudentUid = s.StudentUid.ToString(),
                                      StudentName = s.Name,
                                      StudentEmail = s.Email,
                                      PhoneNo = s.Phone
                                  }).ToList();
            logger.Information("Module: StudentRepository/GetStudentDetails - Repository: END");
            return studentDetails;
        }

        public bool AddStudent(AddStudentDetailsDM studentDetails)
        {
            logger.Information("Module: StudentRepository/AddStudent - Repository: START");
            if (studentDetails == null) return false;

            var departmentDetails = this.departmentRepository.GetDepartmentDetailsById(studentDetails.DepartmentUid);
            if (departmentDetails == null) {
                logger.Information("Module: StudentRepository/AddStudent - Repository: Department does not exist");
                logger.Information("Module: StudentRepository/AddStudent - Repository: END");
                return false; 
            }

            var studentListByName = this.GetStudentDetailsByName(studentDetails.DepartmentUid, studentDetails.Name);
            if (studentListByName.Count > 0) {
                logger.Information("Module: StudentRepository/AddStudent - Repository: Student already exists");
                logger.Information("Module: StudentRepository/AddStudent - Repository: END");
                return false; 
            }

            var student = new Student
            {
                DepartmentUid = Guid.Parse(studentDetails.DepartmentUid),
                Name = studentDetails.Name,
                Email = studentDetails.Email,
                Phone = studentDetails.Phone,
                Address = studentDetails.Address
            };
            this.context.Students.Add(student);
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try 
                { 
                    this.context.SaveChanges();
                    transaction.Commit();
                    logger.Information("Module: StudentRepository/GetStudentDetails - Repository: " + JsonConvert.SerializeObject(student));
                    logger.Information("Module: StudentRepository/GetStudentDetails - Repository: Student Added");
                    logger.Information("Module: StudentRepository/GetStudentDetails - Repository: END");
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    logger.Information("Module: StudentRepository/GetStudentDetails - Repository: Student not added");
                    logger.Information("Module: StudentRepository/GetStudentDetails - Repository: END");
                    return false;
                }
            }
        }
        #endregion

        #region Private Methods
        private List<StudentDetails> GetStudentDetailsByName(string departmentUid, string name)
        {
            logger.Information("Module: StudentRepository/GetStudentDetailsByName - Repository: START");
            var studentDetails = (from s in this.context.Students
                                  where s.DepartmentUid.ToString() == departmentUid && s.Name.ToLower() == name.ToLower()
                                  select new StudentDetails
                                  {
                                      StudentUid = s.StudentUid.ToString(),
                                      StudentName = s.Name,
                                      StudentEmail = s.Email,
                                      PhoneNo = s.Phone
                                  }).ToList();
            logger.Information("Module: StudentRepository/GetStudentDetailsByName - Repository: " + JsonConvert.SerializeObject(studentDetails));
            logger.Information("Module: StudentRepository/GetStudentDetailsByName - Repository: END");
            return studentDetails;
        }
        #endregion
    }
}
