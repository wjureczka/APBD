using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using APBD.DAL;
using APBD.DTO.Requests;
using APBD.Models;

namespace APBD.Services
{
    public class StudentsDbService : IStudentsDbService
    {
        public bool PromoteStudent(PromoteStudentRequest promoteStudentRequest)
        {
            using (var connection =
                new SqlConnection("Data Source=db-mssql;Initial Catalog=s17082;Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.Connection.Open();

                command.Transaction = command.Connection.BeginTransaction();

                try
                {
                    command.CommandText =
                        "SELECT * FROM ENROLLMENT WHERE IdStudy = (SELECT IdStudy FROM STUDIES WHERE NAME = @StudyName) AND Semester = @Semester";
                    command.Parameters.AddWithValue("StudyName", promoteStudentRequest.Studies);
                    command.Parameters.AddWithValue("Semester", promoteStudentRequest.Semester);

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                    {
                        reader.Close();
                        return false;
                    }

                    reader.Close();
                    command.Parameters.Clear();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    command.Transaction.Rollback();
                    return false;
                }

                try
                {
                    command.CommandText = "PromoteStudents";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudiesName", promoteStudentRequest.Studies);
                    command.Parameters.AddWithValue("@OldSemester", promoteStudentRequest.Semester);

                    command.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    command.Transaction.Rollback();
                    return false;
                }
                
                command.Transaction.Commit();
            }

            return true;
        }

        public StudentEnrollment EnrollStudent(EnrollStudentRequest enrollStudentRequest)
        {
            var dbContext = new DbContext();
            
            Student student = new Student
            {
                FirstName = enrollStudentRequest.FirstName,
                LastName = enrollStudentRequest.LastName,
                IndexNumber = enrollStudentRequest.IndexNumber,
                BirthDate = enrollStudentRequest.BirthDate,
            };

            Study study = dbContext.Study.Where(s => s.Name == enrollStudentRequest.Studies).First();


            if (study.Name == null)
            {
                return null;
            }
            

            return new StudentEnrollment()
            {
                Semester = 1,
                LastName = student.LastName,
                StartDate = DateTime.Now
            };
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
                    throw new Exception("No studies");
                }
                
                study.IdStudy = (int)reader["IdStudy"];
                study.Name = reader["Name"].ToString();
                
                connection.Close();

                return study;
            }
        }
    }
}