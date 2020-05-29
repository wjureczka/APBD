using System.Collections.Generic;
using APBD.Models;


namespace APBD.DAL
{
    public interface IDbService
    {
        public Student GetStudent(string indexNumber);
        public IEnumerable<Student> GetStudents();

        public IEnumerable<Enrollment> GetStudentEnrollment(string studentId);
        
        public Study GetStudy(string studyName);

        public void PutStudentRefreshToken(Student student, string refreshToken);

        public Student CheckStudentRefreshToken(string refreshToken);
    }
}