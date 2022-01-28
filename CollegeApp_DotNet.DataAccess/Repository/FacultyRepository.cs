using CollegeApp_DotNet.BusinessDomain.Models;
using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.Models;
using CollegeApp_DotNet.DataAccess.RepositoryModels;
using Newtonsoft.Json;
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
					FirstName = f.FirstName,
					LastName = f.LastName,
					FacultyUid = f.FacultyUid.ToString(),
					Address = f.Address,
					DepartmentUid = f.DepartmentUid.ToString(),
					Email = f.Email,
					Phone = f.Phone
				}).ToList();

		return facultyDetails;
	}

    public bool AddFaculty(AddFacultyDetailsDM facultyDetails)
    {
        Faculty faculty = new Faculty()
        {
			FirstName = facultyDetails.FirstName,
			LastName = facultyDetails.LastName,
			Email = facultyDetails.EmailId,
			Phone = facultyDetails.Phone,
			Address = facultyDetails.Address,
			DepartmentUid = Guid.Parse(facultyDetails.DepartmentUid)
        };

        this.collegeDatabaseContext.Faculties.Add(faculty);
        using var transaction = this.collegeDatabaseContext.Database.BeginTransaction();
        try
        {
            this.collegeDatabaseContext.SaveChanges();
            transaction.Commit();
            logger.Information("Module: FacultyRepository/AddFaculty - Repository: " + JsonConvert.SerializeObject(faculty));
            logger.Information("Module: FacultyRepository/AddFaculty - Repository: Faculty Added");
            logger.Information("Module: FacultyRepository/AddFaculty - Repository: END");
            return true;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            logger.Information("Module: FacultyRepository/AddFaculty - Repository: " + ex.ToString());
            logger.Information("Module: FacultyRepository/AddFaculty - Repository: Faculty not added");
            logger.Information("Module: FacultyRepository/AddFaculty - Repository: END");
            return false;
        }
    }

	#endregion
}