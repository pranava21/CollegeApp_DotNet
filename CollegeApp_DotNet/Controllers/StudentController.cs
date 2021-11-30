using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.BusinessDomain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.WebServices.Controllers
{
    [Route("Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentBL studentBL;

        public StudentController(IStudentBL studentBL)
        {
            this.studentBL = studentBL;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{departmentUid}")]
        public IActionResult GetAllStudents(string departmentUid)
        {
            ResponseMessage<List<StudentDepartmentDetails>> response = this.studentBL.GetStudentDetails(departmentUid);
            if(response.IsSuccess == false)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
