using System;
using System.Collections.Generic;

namespace CollegeApp_DotNet.DataAccess.Models
{
    public partial class Attendance
    {
        public Guid AttendanceUid { get; set; }
        public Guid FacultyUid { get; set; }
        public Guid StudentUid { get; set; }
        public bool? Present { get; set; }
        public bool? Absent { get; set; }

        public virtual Faculty FacultyU { get; set; } = null!;
        public virtual Student StudentU { get; set; } = null!;
    }
}
