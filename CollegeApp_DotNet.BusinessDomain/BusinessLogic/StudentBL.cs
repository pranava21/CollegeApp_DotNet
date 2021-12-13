using AutoMapper;
using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.BusinessDomain.Models;
using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.RepositoryModels;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.BusinessDomain.BusinessLogic
{
    public class StudentBL : IStudentBL
    {
        #region Private Variables
        private readonly IStudentRepository studentRespository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper Mapper;
        private readonly Serilog.ILogger logger;
        #endregion

        #region Constructor
        public StudentBL(IStudentRepository studentRespository, IDepartmentRepository departmentRepository, IMapper Mapper, Serilog.ILogger _logger)
        {
            this.studentRespository = studentRespository; 
            this.departmentRepository = departmentRepository;
            this.Mapper = Mapper;
            logger = _logger;
        }
        #endregion

        #region Public Methods
        public ResponseMessageBM<List<StudentDepartmentDetails>> GetStudentDetails(string departmentUid)
        {
            logger.Information("Module: StudentBL/GetStudentDetails - BL: Start");
            ResponseMessageBM<List<StudentDepartmentDetails>> responseMessage = new ResponseMessageBM<List<StudentDepartmentDetails>>();
            var departmentDetails = departmentRepository.GetDepartmentDetailsById(departmentUid);
            var studentDetails = studentRespository.GetStudentDetails(departmentUid);
            List<StudentDepartmentDetails> details = new List<StudentDepartmentDetails>();
            if (departmentDetails != null && studentDetails != null)
            {
                foreach (var studentDetail in studentDetails)
                {
                    StudentDepartmentDetails student = new StudentDepartmentDetails();
                    student.StudentName = studentDetail.StudentName;
                    student.StudentUid = studentDetail.StudentUid;
                    student.StudentEmail = studentDetail.StudentEmail;
                    student.PhoneNo = studentDetail.PhoneNo;
                    student.DepartmentUid = departmentDetails.DepartmentUid.ToString();
                    student.DepartmentName = departmentDetails.DepartmentName;
                    logger.Information("Module: StudentBL/GetStudentDetails - BL: " + JsonConvert.SerializeObject(student));
                    details.Add(student);
                }
            }

            if(details.Count > 0)
            {
                responseMessage.Message = "Successfully retrieved student details";
                responseMessage.IsSuccess = true;
                responseMessage.Response = details;
                logger.Information("Module: StudentBL/GetStudentDetails - BL: " + JsonConvert.SerializeObject(responseMessage));
                logger.Information("Module: StudentBL/GetStudentDetails - BL: End");
                return responseMessage;
            }
            else
            {
                responseMessage.Message = "No Data Found";
                responseMessage.IsSuccess = false;
                responseMessage.Response = details;
                logger.Information("Module: StudentBL/GetStudentDetails - BL: " + JsonConvert.SerializeObject(responseMessage));
                logger.Information("Module: StudentBL/GetStudentDetails - BL: End");
                return responseMessage;
            }
        }

        public Response AddStudent(AddStudentDetailsBM studentDetailsBM)
        {
            logger.Information("Module: StudentBL/AddStudent - BL: Start");
            Response responseMessage = new Response();
            if(studentDetailsBM != null)
            {
                var studentDetailsDM = Mapper.Map<AddStudentDetailsDM>(studentDetailsBM);
                var isStudentAdded = this.studentRespository.AddStudent(studentDetailsDM);
                if (isStudentAdded)
                {
                    responseMessage.IsSuccess = true;
                    responseMessage.Message = "Student Added Successful";
                    logger.Information("Module: StudentBL/AddStudent - BL: " + JsonConvert.SerializeObject(responseMessage));
                    logger.Information("Module: StudentBL/AddStudent - BL: End");
                    return responseMessage;
                }
                else
                {
                    responseMessage.IsSuccess = false;
                    responseMessage.Message = "Student not Added";
                    logger.Information("Module: StudentBL/AddStudent - BL: " + JsonConvert.SerializeObject(responseMessage));
                    logger.Information("Module: StudentBL/AddStudent - BL: End");
                    return responseMessage;
                }
            }
            responseMessage.IsSuccess = false;
            responseMessage.Message = "Student not Added";
            logger.Information("Module: StudentBL/AddStudent - BL: " + JsonConvert.SerializeObject(responseMessage));
            logger.Information("Module: StudentBL/AddStudent - BL: End");
            return responseMessage;

        }
        #endregion
    }
}
