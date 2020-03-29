using System.Collections.Generic;
using APBD.Models;


namespace APBD.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}