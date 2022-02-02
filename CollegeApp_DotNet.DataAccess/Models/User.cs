using System;
using System.Collections.Generic;

namespace CollegeApp_DotNet.DataAccess.Models
{
    public partial class User
    {
        public Guid UserUid { get; set; }
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; } = null!;
        public int Age { get; set; }
        public string EmailId { get; set; } = null!;
        public bool IsFaculty { get; set; }
        public bool IsStudent { get; set; }
        public Guid DepartmentUid { get; set; }

        public virtual Department DepartmentU { get; set; } = null!;
    }
}
