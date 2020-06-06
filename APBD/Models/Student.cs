using System;

namespace APBD.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string IndexNumber { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string Password { get; set; }
        
        public string Salt { get; set; }
        
        public string RefreshToken { get; set; }
    }
}