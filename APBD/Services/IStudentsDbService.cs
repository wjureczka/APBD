using APBD.DTO.Requests;
using APBD.Models;

namespace APBD.Services
{
    public interface IStudentsDbService
    {
        public bool PromoteStudent(PromoteStudentRequest promoteStudentRequest);

        public StudentEnrollment EnrollStudent(EnrollStudentRequest enrollStudentRequest);

        public Studies GetStudy(string studyName);
    }
}