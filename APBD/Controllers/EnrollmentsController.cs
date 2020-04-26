using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using APBD.DAL;
using APBD.DTO.Requests;
using APBD.DTO.Responses;
using APBD.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public EnrollmentsController(IDbService service)
        {
            _dbService = service;
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
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
                    command.CommandText = "SELECT * FROM ENROLLMENT WHERE IdStudy = (SELECT IdStudy FROM STUDIES WHERE NAME = @StudyName) AND Semester = @Semester";
                    command.Parameters.AddWithValue("StudyName", request.Studies);
                    command.Parameters.AddWithValue("Semester", request.Semester);

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                    {
                        reader.Close();
                        return NotFound();
                    }

                    reader.Close();
                    command.Parameters.Clear();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    command.Transaction.Rollback();
                    return BadRequest();
                }

                try
                {
                    command.CommandText = "PromoteStudents";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudiesName", request.Studies);
                    command.Parameters.AddWithValue("@OldSemester", request.Semester);
                    
                    command.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    command.Transaction.Rollback();
                    return BadRequest();
                }
                
                command.Transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            Student student = new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IndexNumber = request.IndexNumber,
                BirthDate = request.BirthDate,
            };

            EnrollStudentResponse response = new EnrollStudentResponse();

            Study study = this._dbService.GetStudy(request.Studies);

            if (study == null)
            {
                return BadRequest("Study unavailable");
            }

            DateTime enrollmentStartDate = DateTime.Now;

            using (var connection =
                new SqlConnection("Data Source=db-mssql;Initial Catalog=s17082;Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.Connection.Open();
                var transaction = command.Connection.BeginTransaction();
                command.Transaction = transaction;


                try
                {
                    command.CommandText =
                        "SELECT IdEnrollment FROM ENROLLMENT WHERE IdStudy = @IdStudy AND Semester = 1";
                    command.Parameters.AddWithValue("IdStudy", study.IdStudy);

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                    {
                        command.CommandText =
                            "INSERT INTO ENROLLMENT (Semester, IdStudy, StartDate) VALUES (1, @IdStudy, @StartDate)";
                        command.Parameters.AddWithValue("IdStudy", study.IdStudy);
                        command.Parameters.AddWithValue("StartDate", enrollmentStartDate);

                        command.ExecuteNonQuery();
                    }

                    reader.Close();
                }
                catch (Exception error)
                {
                    transaction.Rollback();
                    Console.WriteLine(error);
                    return BadRequest("Enrollment insertion and read error");
                }

                try
                {
                    command.CommandText = "SELECT * FROM STUDENT WHERE IndexNumber = @studentIndexNumber";
                    command.Parameters.AddWithValue("studentIndexNumber", student.IndexNumber);

                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        transaction.Rollback();
                        return BadRequest("Student already exists");
                    }

                    reader.Close();
                }
                catch (Exception error)
                {
                    transaction.Rollback();
                    Console.WriteLine(error);
                    return BadRequest("Student fetch failed");
                }

                try
                {
                    command.CommandText =
                        "SELECT IdEnrollment FROM ENROLLMENT WHERE IdStudy = @IdStudy AND Semester = 1";

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                    {
                        reader.Close();
                        transaction.Rollback();
                        return BadRequest("No enrollment");
                    }

                    var enrollmentId = (int) reader["IdEnrollment"];
                    reader.Close();

                    command.CommandText =
                        "INSERT INTO STUDENT (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) VALUES (@IndexNumber, @FirstName, @LastName, @BirthDate, @IdEnrollment)";
                    command.Parameters.AddWithValue("IndexNumber", student.IndexNumber);
                    command.Parameters.AddWithValue("FirstName", student.FirstName);
                    command.Parameters.AddWithValue("LastName", student.LastName);
                    command.Parameters.AddWithValue("BirthDate", student.BirthDate);
                    command.Parameters.AddWithValue("IdEnrollment", enrollmentId);

                    command.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    transaction.Rollback();
                    Console.WriteLine(error);
                    return BadRequest("Could not insert new student");
                }

                transaction.Commit();
            }

            EnrollStudentResponse enrollStudentResponse = new EnrollStudentResponse()
            {
                Semester = 1,
                LastName = student.LastName,
                StartDate = enrollmentStartDate
            };

            return Ok(enrollStudentResponse);
        }
    }
}