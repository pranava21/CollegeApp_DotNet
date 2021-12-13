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
        private readonly Serilog.ILogger logger;
        #endregion

        #region Constructor
        public DepartmentController(IDepartmentBL departmentBL, Serilog.ILogger _logger)
        {
            this.departmentBL = departmentBL;
            logger = _logger;
        }
        #endregion

        #region APIs
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetDepartments")]
        public IActionResult GetDepartments()
        {
            logger.Information("Module: DepartmentController/GetDepartments - API: Start");
            var response = this.departmentBL.GetDepartmentDetails();
            if (response.IsSuccess)
            {
                logger.Information("Module: DepartmentController/GetDepartments - API: End");
                return Ok(response);
            }
            else
            {
                logger.Information("Module: DepartmentController/GetDepartments - API: End");
                return NotFound(response);
            }
        }
        #endregion
    }
}
