using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using APBD.Models;
using Microsoft.AspNetCore.Authorization;

namespace APBD.DAL
{
    public class DbService: IDbService
    {
        public Student GetStudent(String indexNumber)
        {
            Student student = new Student();

            try
            {
                using(var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17082;Integrated Security=True"))
                using(var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM STUDENT WHERE IndexNumber = @indexNumber";
                    command.Parameters.AddWithValue("indexNumber", indexNumber);
                    
                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                    {
                        return null;
                    }
                    
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                    student.IndexNumber = reader["IndexNumber"].ToString();
                    student.BirthDate = (DateTime)reader["BirthDate"];
                    student.Password = reader["Password"].ToString();
                    student.Salt = reader["Salt"].ToString();
                    student.RefreshToken = reader["RefreshToken"].ToString();
                
                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }

            return student;
        }

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

                    enrollment.Semester =  (int)reader["Semester"];
                    enrollment.IdEnrollment =  (int)reader["IdEnrollment"];
                    enrollment.IdStudy =  (int)reader["IdStudy"];
                    enrollment.StartDate = System.Convert.ToDateTime(reader["StartDate"].ToString());
                    
                    enrollments.Add(enrollment);
                }
                
                connection.Close();
            }

            return enrollments;
        }

        public Study GetStudy(string studyName)
        {
            using(var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17082;Integrated Security=True"))
            using(var command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText = "SELECT * FROM STUDIES WHERE NAME = @studyName";
                command.Parameters.AddWithValue("studyName", studyName);
                
                connection.Open();

                var reader = command.ExecuteReader();

                Study study = new Study();

                if (!reader.Read())
                {
                    return null;
                }
                
                study.IdStudy = (int)reader["IdStudy"];
                study.Name = reader["Name"].ToString();
                
                connection.Close();

                return study;
            }
        }

        public void PutStudentRefreshToken(Student student, string refreshToken)
        {
            using(var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17082;Integrated Security=True"))
            using(var command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText = "UPDATE Student SET RefreshToken=@refreshToken WHERE IndexNumber=@indexNumber";
                command.Parameters.AddWithValue("refreshToken", refreshToken);
                command.Parameters.AddWithValue("indexNumber", student.IndexNumber);
                
                connection.Open();

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }        
        }

        [AllowAnonymous]
        public Student CheckStudentRefreshToken(string refreshToken)
        {
            Student student = new Student();

            try
            {
                using(var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17082;Integrated Security=True"))
                using(var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM STUDENT WHERE RefreshToken = @refreshToken";
                    command.Parameters.AddWithValue("refreshToken", refreshToken);
                    
                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                    {
                        throw new Exception("Invalid");
                    }
                    
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                    student.IndexNumber = reader["IndexNumber"].ToString();
                    student.BirthDate = (DateTime)reader["BirthDate"];
                    student.Password = reader["Password"].ToString();
                    student.RefreshToken = reader["RefreshToken"].ToString();

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception("Invalid");
            }

            return student;
        }

        public async void CreateStudent(Student student)
        {
            try
            {
                using(var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17082;Integrated Security=True"))
                using(var command = new SqlCommand())
                {
                    command.Connection = connection;
                    
                    command.CommandText = "INSERT INTO STUDENT (IndexNumber, FirstName, LastName, BirthDate, Password, Salt) VALUES (@IndexNumber, @FirstName, @LastName, @BirthDate, @Password, @Salt)";
                    command.Parameters.AddWithValue("IndexNumber", student.IndexNumber);
                    command.Parameters.AddWithValue("FirstName", student.FirstName);
                    command.Parameters.AddWithValue("LastName", student.LastName);
                    command.Parameters.AddWithValue("BirthDate", student.BirthDate);
                    command.Parameters.AddWithValue("Password", student.Password);
                    command.Parameters.AddWithValue("Salt", student.Salt);
                    
                    connection.Open();

                    await command.ExecuteNonQueryAsync();
                    
                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception("Invalid");
            }

        }
    }
}