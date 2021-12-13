using CollegeApp_DotNet.DataAccess.Interface;
using CollegeApp_DotNet.DataAccess.Models;
using CollegeApp_DotNet.DataAccess.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.DataAccess.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        #region Private Variables
        private readonly collegeDatabaseContext collegeDatabaseContext;
        private readonly Serilog.ILogger logger;
        #endregion

        #region Constructor
        public DepartmentRepository(collegeDatabaseContext context, Serilog.ILogger _logger)
        {
            collegeDatabaseContext = context;
            logger = _logger;
        }
        #endregion

        #region Public Methods
        public DepartmentDetailsDM GetDepartmentDetailsById(string departmentUid)
        {
            logger.Information("Module: DepartmentRepository/GetDepartmentDetailsById - Repository: START");
            var departmentDetails = (from d in collegeDatabaseContext.Departments
                                     where d.DepartmentUid.ToString() == departmentUid
                                     select new DepartmentDetailsDM
                                     {
                                         DepartmentUid = d.DepartmentUid,
                                         DepartmentName = d.DepartmentName
                                     }).FirstOrDefault();
            logger.Information("Module: DepartmentRepository/GetDepartmentDetailsById - Repository: END");
            return departmentDetails;
        }

        public List<DepartmentDetailsDM> GetDepartmentDetails()
        {
            logger.Information("Module: DepartmentRepository/GetDepartmentDetails - Repository: START");
            var departmentDetails = (from d in collegeDatabaseContext.Departments
                                     select new DepartmentDetailsDM
                                     {
                                         DepartmentUid = d.DepartmentUid,
                                         DepartmentName = d.DepartmentName,
                                         DepartmentImageUrl = d.DepartmentImageUrl
                                     }).ToList();
            logger.Information("Module: DepartmentRepository/GetDepartmentDetails - Repository: END");
            return departmentDetails;
        }
        #endregion
    }
}
