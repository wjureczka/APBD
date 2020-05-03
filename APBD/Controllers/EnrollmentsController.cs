using APBD.DTO.Requests;
using APBD.DTO.Responses;
using APBD.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IStudentsDbService _dbService;

        public EnrollmentsController(IStudentsDbService service)
        {
            _dbService = service;
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            if (this._dbService.PromoteStudent(request))
            {
                return Ok();
            };

            return BadRequest();
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            StudentEnrollment studentEnrollment = this._dbService.EnrollStudent(request);
            
            EnrollStudentResponse enrollStudentResponse = new EnrollStudentResponse()
            {
                Semester = studentEnrollment.Semester,
                LastName = studentEnrollment.LastName,
                StartDate = studentEnrollment.StartDate
            };

            return Ok(enrollStudentResponse);
        }
    }
}