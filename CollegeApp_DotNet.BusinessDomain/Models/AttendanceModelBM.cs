
namespace CollegeApp_DotNet.BusinessDomain.Models
{
    public class AttendanceModelBM
    {
        public int AttendanceId { get; set; }
        public Guid FacultyUid { get; set; }
        public Guid StudentUid { get; set; }
        public Guid DepartmentUid { get; set; }
        public DateTime AttendedOn { get; set; }
        public bool isPresent { get; set; }
    }
}
