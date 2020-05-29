using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APBD.DAL;
using APBD.DTO.Requests;
using APBD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace APBD.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        private readonly IDbService _dbService;
        
        public IConfiguration Configuration { get; set; }

        public StudentsController(IDbService service)
        {
            _dbService = service;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetStudents()
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

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var login = request.Login;
            var password = request.Password;

            var student = this._dbService.GetStudent(login);

            if (student == null || !student.Password.Equals(password))
            {
                return Unauthorized();
            }
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "student")
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecretsecretsecretsecretsecretsecret"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer: "polsko",
                audience: "japonska",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            var refreshToken = Guid.NewGuid();

            this._dbService.PutStudentRefreshToken(student, refreshToken.ToString());

            return Ok(new
            {
                accesToken = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken
            });
        }

        [HttpPost("refresh-token/{refreshToken}")]
        public IActionResult RefreshToken(string refreshToken)
        {
            Student student;
            try
            {
                student = this._dbService.CheckStudentRefreshToken(refreshToken);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "student")
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecretsecretsecretsecretsecretsecret"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer: "polsko",
                audience: "japonska",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            var newRefreshToken = Guid.NewGuid();

            this._dbService.PutStudentRefreshToken(student, refreshToken.ToString());
            
            return Ok(new
            {
                accesToken = new JwtSecurityTokenHandler().WriteToken(token),
                newRefreshToken
            });
        }
    }
}