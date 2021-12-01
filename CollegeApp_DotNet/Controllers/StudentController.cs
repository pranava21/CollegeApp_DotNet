using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.BusinessDomain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp_DotNet.WebServices.Controllers
{
    [Route("Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentBL studentBL;
        private readonly Serilog.ILogger logger;

        public StudentController(IStudentBL studentBL, Serilog.ILogger logger)
        {
            this.studentBL = studentBL;
            this.logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{departmentUid}")]
        public IActionResult GetAllStudents(string departmentUid)
        {
            logger.Information("Module: StudentController/GetAllStudents - API : START");
            ResponseMessage<List<StudentDepartmentDetails>> response = this.studentBL.GetStudentDetails(departmentUid);
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
    }
}
