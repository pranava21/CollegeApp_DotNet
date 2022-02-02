using CollegeApp_DotNet.BusinessDomain.Models;

namespace CollegeApp_DotNet.BusinessDomain.Interface
{
    public interface IStudentBL
    {
        ResponseMessageBM<List<StudentDepartmentDetails>> GetStudentDetails(string departmentUid);
        Response AddStudent(AddStudentDetailsBM studentDetailsBM);
        Response TakeAttendance(List<AttendanceModelBM> attendanceDetails);
        ResponseMessageBM<StudentDepartmentDetails> GetStudent(string emailId, string departmentUid);
    }
}
