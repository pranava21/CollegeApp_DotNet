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
        #endregion

        #region Constructor
        public DepartmentRepository(collegeDatabaseContext context)
        {
            collegeDatabaseContext = context;
        }
        #endregion
        public DepartmentDetailsDM GetDepartmentDetails(string departmentUid)
        {
            var departmentDetails = (from d in collegeDatabaseContext.Departments
                                     where d.DepartmentUid.ToString() == departmentUid
                                     select new DepartmentDetailsDM
                                     {
                                         DepartmentUid = d.DepartmentUid,
                                         DepartmentName = d.DepartmentName
                                     }).FirstOrDefault();
            return departmentDetails;
        }
    }
}
