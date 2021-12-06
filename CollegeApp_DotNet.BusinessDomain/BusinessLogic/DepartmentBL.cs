using AutoMapper;
using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.BusinessDomain.Models;
using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.BusinessDomain.BusinessLogic
{
    public class DepartmentBL : IDepartmentBL
    {
        #region Private Variables
        private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper Mapper;
        #endregion

        #region Constructor
        public DepartmentBL(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            Mapper = mapper;
        }
        #endregion

        #region Public Methods
        public ResponseMessageBM<List<DepartmentDetailsBM>> GetDepartmentDetails()
        {
            ResponseMessageBM<List<DepartmentDetailsBM>> response = new ResponseMessageBM<List<DepartmentDetailsBM>>();
            var departmentDetailsDM = departmentRepository.GetDepartmentDetails();
            var deparmentDetailsBM = Mapper.Map<List<DepartmentDetailsBM>>(departmentDetailsDM);

            if(deparmentDetailsBM.Count > 0)
            {
                response.Response = deparmentDetailsBM;
                response.IsSuccess = true;
                response.Message = "Retreived Department List";
                return response;
            }
            else
            {
                response.Response = deparmentDetailsBM;
                response.IsSuccess = false;
                response.Message = "Empty List";
                return response;
            }
        }
        #endregion
    }
}
