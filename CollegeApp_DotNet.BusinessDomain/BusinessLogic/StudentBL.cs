﻿using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.BusinessDomain.Models;
using CollegeApp_DotNet.DataAccess.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.BusinessDomain.BusinessLogic
{
    public class StudentBL : IStudentBL
    {
        private readonly IStudentRepository studentRespository;
        private readonly IDepartmentRepository departmentRepository;

        public StudentBL(IStudentRepository studentRespository, IDepartmentRepository departmentRepository)
        {
            this.studentRespository = studentRespository; 
            this.departmentRepository = departmentRepository;
        }

        public ResponseMessage<List<StudentDepartmentDetails>> GetStudentDetails(string departmentUid)
        {
            ResponseMessage<List<StudentDepartmentDetails>> responseMessage = new ResponseMessage<List<StudentDepartmentDetails>>();
            var departmentDetails = departmentRepository.GetDepartmentDetails(departmentUid);
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
                    student.DepartmentUid = departmentDetails.DepartmentUid.ToString();
                    student.DepartmentName = departmentDetails.DepartmentName;

                    details.Add(student);
                }
            }

            if(details.Count > 0)
            {
                responseMessage.Message = "Successfully retrieved student details";
                responseMessage.IsSuccess = true;
                responseMessage.Response = details;
                return responseMessage;
            }
            else
            {
                responseMessage.Message = "No Data Found";
                responseMessage.IsSuccess = false;
                responseMessage.Response = details;
                return responseMessage;
            }
        }
    }
}
