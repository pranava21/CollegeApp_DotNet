﻿using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.BusinessDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp_DotNet.WebServices.Controllers
{
    [Authorize]
    [Route("Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        #region Private Variables
        private readonly IStudentBL studentBL;
        private readonly Serilog.ILogger logger; 
        #endregion

        #region Constructor
        public StudentController(IStudentBL studentBL, Serilog.ILogger logger)
        {
            this.studentBL = studentBL;
            this.logger = logger;
        }
        #endregion

        #region APIs
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{departmentUid}")]
        public IActionResult GetAllStudents(string departmentUid)
        {
            logger.Information("Module: StudentController/GetAllStudents - API : START");
            ResponseMessageBM<List<StudentDepartmentDetails>> response = this.studentBL.GetStudentDetails(departmentUid);
            if(response.IsSuccess == false)
            {
                logger.Information("Module: StudentController/GetAllStudents - API : END");
                return NotFound(response);
            }
            else
            {
                logger.Information("Module: StudentController/GetAllStudents - API : END");
                return Ok(response);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetStudent")]
        public IActionResult GetStudentDetails(string emailId, string departmentUid)
        {
            logger.Information("Module: StudentController/GetStudent - API : START");
            ResponseMessageBM<StudentDepartmentDetails> response = this.studentBL.GetStudent(emailId, departmentUid);
            if (response.IsSuccess == false)
            {
                logger.Information("Module: StudentController/GetStudent - API : END");
                return NotFound(response);
            }
            else
            {
                logger.Information("Module: StudentController/GetStudent - API : END");
                return Ok(response);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("AddStudent")]
        public IActionResult AddStudent([FromBody]AddStudentDetailsBM studentDetailsBM)
        {
            logger.Information("Module: StudentController/AddStudent - API : START");
            Response response = this.studentBL.AddStudent(studentDetailsBM);
            if (response.IsSuccess)
            {
                logger.Information("Module: StudentController/AddStudent - API : END");
                return Ok(response);
            }
            logger.Information("Module: StudentController/AddStudent - API : END");
            return BadRequest(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("TakeAttendance")]
        public IActionResult TakeAttendance([FromBody] List<AttendanceModelBM> attendanceDetails)
        {
            logger.Information("Module: StudentController/Take Attendance - API : START");
            Response response = this.studentBL.TakeAttendance(attendanceDetails);
            if (response.IsSuccess)
            {
                logger.Information("Module: StudentController/Take Attendance - API : END");
                return Ok(response);
            }
            else
            {
                logger.Information("Module: StudentController/Take Attendance - API : END");
                return BadRequest(response);
            }
        }
        #endregion
    }
}
