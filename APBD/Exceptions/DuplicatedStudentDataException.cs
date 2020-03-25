using System;

namespace APBD.Exceptions
{
    public class DuplicatedStudentDataExceptionException : Exception
    {
        public DuplicatedStudentDataExceptionException(string studentData) : base(String.Format("Duplicated student data: {0}", studentData))
        {
        }
    }
}