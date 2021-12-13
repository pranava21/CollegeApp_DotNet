using AutoMapper;
using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.BusinessDomain.Models;
using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.RepositoryModels;
using Newtonsoft.Json;
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
        private readonly Serilog.ILogger logger;
        #endregion

        #region Constructor
        public DepartmentBL(IDepartmentRepository departmentRepository, IMapper mapper, Serilog.ILogger logger)
        {
            this.departmentRepository = departmentRepository;
            Mapper = mapper;
            this.logger = logger;
        }
        #endregion

        #region Public Methods
        public ResponseMessageBM<List<DepartmentDetailsBM>> GetDepartmentDetails()
        {
            logger.Information("Module: DepartmentBL/GetDepartmentDetails - BL: START");
            ResponseMessageBM<List<DepartmentDetailsBM>> response = new ResponseMessageBM<List<DepartmentDetailsBM>>();
            var departmentDetailsDM = departmentRepository.GetDepartmentDetails();
            var deparmentDetailsBM = Mapper.Map<List<DepartmentDetailsBM>>(departmentDetailsDM);

            if(deparmentDetailsBM.Count > 0)
            {
                response.Response = deparmentDetailsBM;
                response.IsSuccess = true;
                response.Message = "Retreived Department List";
                logger.Information("Module: DepartmentBL/GetDepartmentDetails - BL: " + JsonConvert.SerializeObject(response));
                logger.Information("Module: DepartmentBL/GetDepartmentDetails - BL: END");
                return response;
            }
            else
            {
                response.Response = deparmentDetailsBM;
                response.IsSuccess = false;
                response.Message = "Empty List";
                logger.Information("Module: DepartmentBL/GetDepartmentDetails - BL: " + JsonConvert.SerializeObject(response));
                logger.Information("Module: DepartmentBL/GetDepartmentDetails - BL: END");
                return response;
            }
        }
        #endregion
    }
}
