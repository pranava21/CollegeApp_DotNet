using System;
using System.Collections.Generic;

namespace CollegeApp_DotNet.DataAccess.Models
{
    public partial class Department
    {
        public Department()
        {
            Attendances = new HashSet<Attendance>();
            Faculties = new HashSet<Faculty>();
            Students = new HashSet<Student>();
        }

        public Guid DepartmentUid { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string? DepartmentCode { get; set; }
        public string? DepartmentImageUrl { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Faculty> Faculties { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
