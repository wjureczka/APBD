using System;

namespace APBD.Exceptions
{
    public class NotValidStudentData : Exception
    {

        public NotValidStudentData(string studentData) : base(String.Format("Invalid student data: {0}", studentData))
        {
        }
    }
}