using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.BusinessDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp_DotNet.WebServices.Controllers;

[Authorize]
[Route("Faculty")]
[ApiController]
public class FacultyController : Controller
{
	#region Private Variables	
	private readonly Serilog.ILogger logger;
	private readonly IFacultyBL _facultyBl;
	#endregion

	#region Constructor

	public FacultyController(Serilog.ILogger logger, IFacultyBL facultyBl)
	{
		this.logger = logger;
		_facultyBl = facultyBl;
	}
	#endregion
	
	[HttpGet("GetAllFaculty")]
	public IActionResult GetFacultyList(string departmentUid)
	{
		logger.Information("Module: FacultyController/GetAllFaculty - API : START");
		var response = _facultyBl.GetFacultyDetails(departmentUid);
		if (!response.IsSuccess) return NoContent();
		logger.Information("Module: FacultyController/GetAllFaculty - API : END");
		return Ok(response);

	}

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("GetFaculty")]
    public IActionResult GetFaculty(string emailId, string departmentUid)
    {
        logger.Information("Module: StudentController/GetFaculty - API : START");
        ResponseMessageBM<FacultyDetailsBM> response = this._facultyBl.GetFaculty(emailId, departmentUid);
        if (response.IsSuccess == false)
        {
            logger.Information("Module: StudentController/GetFaculty - API : END");
            return NotFound(response);
        }
        else
        {
            logger.Information("Module: StudentController/GetFaculty - API : END");
            return Ok(response);
        }
    }
}