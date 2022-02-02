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
                                      StudentId = s.StudentId,
                                      StudentFirstName = s.FirstName,
                                      StudentLastName = s.LastName,
                                      StudentEmail = s.Email,
                                      PhoneNo = s.Phone
                                  }).ToList();
            logger.Information("Module: StudentRepository/GetStudentDetails - Repository: END");
            return studentDetails;
        }

        public StudentDetails GetStudent(string emailId)
        {
            logger.Information("Module: StudentRepository/GetStudentDetails - Repository: START");
            var studentDetails = (from s in this.context.Students
                where s.Email == emailId
                select new StudentDetails
                {
                    StudentUid = s.StudentUid.ToString(),
                    StudentId = s.StudentId,
                    StudentFirstName = s.FirstName,
                    StudentLastName = s.LastName,
                    StudentEmail = s.Email,
                    PhoneNo = s.Phone
                }).FirstOrDefault();
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

            var studentId = GetStudentDetailsByName(studentDetails.DepartmentUid, studentDetails.FirstName, studentDetails.LastName);
            if (string.IsNullOrEmpty(studentId)) {
                logger.Information("Module: StudentRepository/AddStudent - Repository: Student No Prior Student");
                studentId = "COL" +  1.ToString("D7");
            }
            else
            {
                var num = int.Parse(studentId.Split('L')[1]);
                num += 1;
                studentId = "COL" + num.ToString("D7");
            }

            var student = new Student
            {
                DepartmentUid = Guid.Parse(studentDetails.DepartmentUid),
                FirstName = studentDetails.FirstName,
                LastName = studentDetails.LastName,
                Email = studentDetails.Email,
                Phone = studentDetails.Phone,
                Address = studentDetails.Address,
                StudentId = studentId
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
                    logger.Information("Module: StudentRepository/GetStudentDetails - Repository: " + ex.ToString());
                    logger.Information("Module: StudentRepository/GetStudentDetails - Repository: Student not added");
                    logger.Information("Module: StudentRepository/GetStudentDetails - Repository: END");
                    return false;
                }
            }
        }

        public bool TakeAttendance(List<AttendanceModelDM> attendanceDetails)
        {
            logger.Information("Module: StudentRepository/TakeAttendance - Repository: START");
            const int defaultAttendanceId = 1;
            if (attendanceDetails == null || attendanceDetails.Count == 0) return false;

            var latestId = this.GetLatestAttendanceId();
            if (latestId != 0) latestId += defaultAttendanceId;
            else latestId = defaultAttendanceId;

            foreach (var a in attendanceDetails)
            {
                var details = new Attendance
                {
                    AttendanceId = latestId,
                    DepartmentUid = a.DepartmentUid,
                    FacultyUid = a.FacultyUid,
                    StudentUid = a.StudentUid,
                    AttendedOn = a.AttendedOn.ToString() != "" ? a.AttendedOn :  DateTime.UtcNow,
                    IsPresent = a.isPresent
                };
                this.context.Attendances.Add(details);
                using (var transaction = this.context.Database.BeginTransaction())
                {
                    try
                    {
                        this.context.SaveChanges();
                        transaction.Commit();
                        logger.Information("Module: StudentRepository/TakeAttendance - Repository: " + JsonConvert.SerializeObject(details));
                        logger.Information("Module: StudentRepository/TakeAttendance - Repository: Attendance Record Added");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        logger.Information("Module: StudentRepository/TakeAttendance - Repository: " + ex.ToString());
                        logger.Information("Module: StudentRepository/TakeAttendance - Repository: Student not added");
                        logger.Information("Module: StudentRepository/TakeAttendance - Repository: END");
                        return false;
                    }
                }
            }
            logger.Information("Module: StudentRepository/TakeAttendance - Repository: END");
            return true;
        }
        #endregion

        #region Private Methods
        private string GetStudentDetailsByName(string departmentUid, string firstName, string lastName)
        {
            logger.Information("Module: StudentRepository/GetStudentDetailsByName - Repository: START");
            var studentDetails = (from s in this.context.Students
                select s.StudentId).ToList();
            studentDetails.Sort();
            studentDetails.Reverse();
            logger.Information("Module: StudentRepository/GetStudentDetailsByName - Repository: " + JsonConvert.SerializeObject(studentDetails));
            logger.Information("Module: StudentRepository/GetStudentDetailsByName - Repository: END");
            return studentDetails.FirstOrDefault();
        }
        private int GetLatestAttendanceId()
        {
            var id = (from a in this.context.Attendances
                orderby a.AttendanceId descending
                select a.AttendanceId).FirstOrDefault();
            return id;
        }
        #endregion
    }
}
