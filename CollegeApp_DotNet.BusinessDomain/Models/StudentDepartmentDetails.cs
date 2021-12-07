using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.BusinessDomain.Models
{
    public class StudentDepartmentDetails
    {
        public string StudentUid { get; set; }
        public string StudentName { get; set;}
        public string StudentEmail { get; set;}
        public string PhoneNo { get; set;}
        public string DepartmentUid { get; set; }
        public string DepartmentName { get; set; }
    }
}
