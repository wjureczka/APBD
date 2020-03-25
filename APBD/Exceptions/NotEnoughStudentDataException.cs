using System;

namespace APBD.Exceptions
{
    public class NotEnoughStudentDataException : Exception
    {
        public NotEnoughStudentDataException(string studentData) : base(String.Format("Not enough student data: {0}", studentData))
        {
        }
    }
}