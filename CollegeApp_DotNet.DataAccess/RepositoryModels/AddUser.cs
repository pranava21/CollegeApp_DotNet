using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.DataAccess.RepositoryModels
{
    public class AddUserDM
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public Guid? DepartmentUid { get; set; }
        public bool isFaculty { get; set; }
        public bool isStudent { get; set; }
    }
}
