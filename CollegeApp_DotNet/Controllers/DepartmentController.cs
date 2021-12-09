using CollegeApp_DotNet.BusinessDomain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp_DotNet.WebServices.Controllers
{
    [Route("Department")]
    [ApiController]
    public class DepartmentController : Controller
    {
        #region Private Variables
        private readonly IDepartmentBL departmentBL;
        #endregion

        #region Constructor
        public DepartmentController(IDepartmentBL departmentBL)
        {
            this.departmentBL = departmentBL;
        }
        #endregion

        #region APIs
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetDepartments")]
        public IActionResult GetDepartments()
        {
            var response = this.departmentBL.GetDepartmentDetails();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }
        #endregion
    }
}
