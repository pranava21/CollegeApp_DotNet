using AutoMapper;
using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.BusinessDomain.Models;
using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.RepositoryModels;
using Serilog;

namespace CollegeApp_DotNet.BusinessDomain.BusinessLogic;

public class FacultyBL : IFacultyBL
{
    #region Private Variables

    private ILogger logger;
    private IFacultyRepository _facultyRepository;
    private readonly IMapper Mapper;

    #endregion

    #region Constructor

    public FacultyBL(ILogger logger, IFacultyRepository facultyRepository, IMapper mapper)
    {
        this.logger = logger;
        _facultyRepository = facultyRepository;
        Mapper = mapper;
    }

    #endregion

    #region Public Methods

    public ResponseMessageBM<List<FacultyDetailsBM>> GetFacultyDetails(string departmentUid)
    {
        ResponseMessageBM<List<FacultyDetailsBM>> response = new ResponseMessageBM<List<FacultyDetailsBM>>();

        var facultyDetails = this._facultyRepository.GetFacultyDetails(departmentUid);
        var facultyDetailsBm = Mapper.Map<List<FacultyDetailsDM>, List<FacultyDetailsBM>>(facultyDetails);
        if (facultyDetails.Count == 0)
        {
            response.Message = "Wrong Department or No faculty available";
            response.IsSuccess = false;
            response.Response = facultyDetailsBm;
        }
        else
        {
            response.Message = "Faculty Details retrieved successfully";
            response.IsSuccess = true;
            response.Response = facultyDetailsBm;
        }

        return response;
    }

    public ResponseMessageBM<FacultyDetailsBM> GetFaculty(string emailId, string departmentUid)
    {
        ResponseMessageBM<FacultyDetailsBM> response = new ResponseMessageBM<FacultyDetailsBM>();

        var facultyDetails = this._facultyRepository.GetFaculty(emailId, departmentUid);
        var facultyDetailsBm = Mapper.Map<FacultyDetailsDM, FacultyDetailsBM>(facultyDetails);
        response.Message = "Faculty Details retrieved successfully";
        response.IsSuccess = true;
        response.Response = facultyDetailsBm;


        return response;
    }

    #endregion
}