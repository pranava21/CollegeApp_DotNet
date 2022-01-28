using AutoMapper;
using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.BusinessDomain.Models;
using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.RepositoryModels;

namespace CollegeApp_DotNet.BusinessDomain.BusinessLogic;

public class AccountsBL : IAccountsBL
{
    #region Private Variables

    private readonly IMapper Mapper;
    private readonly IAccountsRepository _accountsRepository;

    #endregion

    #region Constructor

    public AccountsBL(IAccountsRepository accountsRepository, IMapper mapper)
    {
        this._accountsRepository = accountsRepository;
        this.Mapper = mapper;
    }


    #endregion

    #region Public Methods

    public Response AddUser(AddUserBM userDetailsBm, string facultyOrStudent)
    {
        Response response = new Response();

        var existingEmail = this._accountsRepository.CheckForExistingEmails(userDetailsBm.EmailId);
        if (!existingEmail)
        {
            var existingUserId = this._accountsRepository.CheckForExistingUserId(userDetailsBm.UserId);
            if (!existingUserId)
            {
                var userDetailsDm = Mapper.Map<AddUserDM>(userDetailsBm);
                var result = this._accountsRepository.AddUser(userDetailsDm, facultyOrStudent);
                response.Message = result;
                response.IsSuccess = true;
                return response;
            }
            response.Message = $"User with user id {userDetailsBm.UserId} already exists"; ;
            response.IsSuccess = false;
            return response;
        }
        response.Message = $"User with email {userDetailsBm.EmailId} already exists"; ;
        response.IsSuccess = false;
        return response;    
    }

    #endregion
}