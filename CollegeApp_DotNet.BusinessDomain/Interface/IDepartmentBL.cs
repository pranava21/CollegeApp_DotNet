using CollegeApp_DotNet.BusinessDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.BusinessDomain.Interface
{
    public interface IDepartmentBL
    {
        ResponseMessageBM<List<DepartmentDetailsBM>> GetDepartmentDetails();
    }
}
