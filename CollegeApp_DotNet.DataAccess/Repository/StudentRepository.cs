using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.Models;
using CollegeApp_DotNet.DataAccess.RepositoryModels;
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
        #endregion

        #region Constructor
        public StudentRepository(IDepartmentRepository departmentRepository, collegeDatabaseContext _context)
        {
            this.departmentRepository = departmentRepository;
            this.context = _context;
        }
        #endregion

        #region Public Methods
        public List<StudentDetails> GetStudentDetails(string departmentUid)
        {
            var studentDetails = (from s in this.context.Students
                                  where s.DepartmentUid.ToString() == departmentUid
                                  select new StudentDetails
                                  {
                                      StudentUid = s.StudentUid.ToString(),
                                      StudentName = s.Name,
                                      StudentEmail = s.Email
                                  }).ToList();

            return studentDetails;
        }

        public bool AddStudent(AddStudentDetailsDM studentDetails)
        {
            if (studentDetails == null) return false;

            var departmentDetails = this.departmentRepository.GetDepartmentDetailsById(studentDetails.DepartmentUid);
            if(departmentDetails == null) return false;

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
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        #endregion
    }
}
