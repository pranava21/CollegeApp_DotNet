using System;
using System.Collections.Generic;

namespace CollegeApp_DotNet.DataAccess.Models
{
    public partial class Attendance
    {
        public Guid AttendanceUid { get; set; }
        public int AttendanceId { get; set; }
        public Guid FacultyUid { get; set; }
        public Guid StudentUid { get; set; }
        public Guid DepartmentUid { get; set; }
        public DateTime AttendedOn { get; set; }

        public virtual Department DepartmentU { get; set; } = null!;
        public virtual Faculty FacultyU { get; set; } = null!;
        public virtual Student StudentU { get; set; } = null!;
    }
}
