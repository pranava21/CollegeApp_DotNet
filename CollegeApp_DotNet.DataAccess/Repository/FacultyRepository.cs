using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.Models;
using CollegeApp_DotNet.DataAccess.RepositoryModels;
using Serilog;

namespace CollegeApp_DotNet.DataAccess.Repository;

public class FacultyRepository : IFacultyRepository
{
	#region Private Variables

	private ILogger logger;
	private readonly collegeDatabaseContext collegeDatabaseContext;
	#endregion

	#region Constructor

	public FacultyRepository(ILogger _logger, collegeDatabaseContext collegeDatabaseContext)
	{
		logger = _logger;
		this.collegeDatabaseContext = collegeDatabaseContext;
	}

	#endregion

	#region Public Methods

	public List<FacultyDetailsDM> GetFacultyDetails(string departmentUid)
	{
		List<FacultyDetailsDM> facultyDetails = new List<FacultyDetailsDM>();
		if(string.IsNullOrEmpty(departmentUid)) return facultyDetails;
		facultyDetails = (from f in this.collegeDatabaseContext.Faculties
				where f.DepartmentUid.ToString() == departmentUid
				select new FacultyDetailsDM
				{
					Name = f.Name,
					FacultyUid = f.FacultyUid.ToString(),
					Address = f.Address,
					DepartmentUid = f.DepartmentUid.ToString(),
					Email = f.Email,
					Phone = f.Phone
				}).ToList();

		return facultyDetails;
	}

	#endregion
}