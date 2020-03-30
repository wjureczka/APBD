using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using APBD.Models;

namespace APBD.DAL
{
    public class DbService: IDbService
    {
        public IEnumerable<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            
            using(var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17082;Integrated Security=True"))
            using(var command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText = "SELECT * FROM STUDENT";

                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new Student();

                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                    student.IndexNumber = reader["IndexNumber"].ToString();
                    
                    students.Add(student);
                }
                
                connection.Close();
            }

            return students;
        }

        public IEnumerable<Enrollment> GetStudentEnrollment(string studentId)
        {
            List<Enrollment> enrollments = new List<Enrollment>();

            using(var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17082;Integrated Security=True"))
            using(var command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText = $"SELECT * FROM dbo.ENROLLMENT WHERE IdEnrollment = (SELECT IDENROLLMENT FROM dbo.STUDENT WHERE INDEXNUMBER = @studentId)";
                // SQL Injection -> studentId = 1); DROP TABLE STUDENT; --
                command.Parameters.AddWithValue("studentId", studentId);

                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Enrollment enrollment = new Enrollment();
                    
                    Console.WriteLine(reader);

                    enrollment.Semester = System.Convert.ToInt32(reader["Semester"].ToString());
                    enrollment.IdEnrollment = System.Convert.ToInt32(reader["IdEnrollment"].ToString());
                    enrollment.IdStudy = System.Convert.ToInt32(reader["IdStudy"].ToString());
                    enrollment.StartDate = System.Convert.ToDateTime(reader["StartDate"].ToString());
                    
                    enrollments.Add(enrollment);
                }
                
                connection.Close();
            }

            return enrollments;
        }
    }
}