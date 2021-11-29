﻿using System;
using System.Collections.Generic;

namespace CollegeApp_DotNet.WebServices
{
    public partial class Student
    {
        public Student()
        {
            Attendances = new HashSet<Attendance>();
        }

        public Guid StudentUid { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public Guid DepartmentUid { get; set; }

        public virtual Department DepartmentU { get; set; } = null!;
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
