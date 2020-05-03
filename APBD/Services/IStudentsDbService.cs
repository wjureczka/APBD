using APBD.DTO.Requests;
using APBD.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD.Services
{
    public interface IStudentsDbService
    {
        public bool PromoteStudent(PromoteStudentRequest promoteStudentRequest);

        public StudentEnrollment EnrollStudent(EnrollStudentRequest enrollStudentRequest);

        public Study GetStudy(string studyName);
    }
}