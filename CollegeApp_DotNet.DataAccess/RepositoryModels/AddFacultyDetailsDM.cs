using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.BusinessDomain.Models
{
    public class AddFacultyDetailsDM
    {
        public string FacultyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string DepartmentUid { get; set; }
    }
}
