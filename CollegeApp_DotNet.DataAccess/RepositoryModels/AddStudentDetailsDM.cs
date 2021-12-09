using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.DataAccess.RepositoryModels
{
    public class AddStudentDetailsDM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DepartmentUid { get; set; }
        public string DepartmentName { get; set; }
    }
}
