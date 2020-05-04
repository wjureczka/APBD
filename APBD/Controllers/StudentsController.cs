using System;
using System.Collections.Generic;
using System.Linq;
using APBD.DAL;
using APBD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace APBD.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        private readonly IDbService _dbService;

        public StudentsController(IDbService service)
        {
            _dbService = service;
        }

        [HttpGet]
        public IActionResult GetStudent()
        {
            IEnumerable<Student> students = _dbService.GetStudents();
            
            return Ok(students);
        }

        [HttpGet("enrollment/{studentId}")]
        public IActionResult GetStudentEnrollment(string studentId)
        {
            IEnumerable<Enrollment> enrollments = _dbService.GetStudentEnrollment(studentId);

            return Ok(enrollments);
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 2000)}";

            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id)
        {
            return Ok($"Updating finished. Updated id: {id}.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok($"Deleting finished. Deleted id: {id}.");
        }
    }
}