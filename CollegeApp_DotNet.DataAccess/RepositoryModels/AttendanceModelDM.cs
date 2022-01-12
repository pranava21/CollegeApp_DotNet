using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.DataAccess.RepositoryModels
{
    public class AttendanceModelDM
    {
        public int AttendanceId { get; set; }
        public Guid FacultyUid { get; set; }
        public Guid StudentUid { get; set; }
        public Guid DepartmentUid { get; set; }
        public DateTime AttendedOn { get; set; }
    }
}
