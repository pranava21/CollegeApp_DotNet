using CollegeApp_DotNet.BusinessDomain.Interface;
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
}