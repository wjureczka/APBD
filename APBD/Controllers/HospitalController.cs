using APBD.DAL;
using APBD.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APBD.Controlers
{
    [ApiController]
    [Route("api/")]
    public class HospitalController : ControllerBase
    {

        private readonly HospitalContext _hospitalContext;

        public HospitalController(HospitalContext hospitalContext)
        {
            this._hospitalContext = hospitalContext;
        }

        [HttpGet("doctors")]
        public IActionResult GetDoctors()
        {
            var doctors = this._hospitalContext.Doctor;


            return Ok(doctors);
        }

        [HttpPost("doctors")]
        public IActionResult AddDoctor(Doctor doctor)
        {
            Doctor newDoctor = new Doctor
            {
                Email = doctor.Email,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
            };

            this._hospitalContext.Add(newDoctor);
            this._hospitalContext.SaveChanges();

            return Ok();
        }

        [HttpPut("doctors")]
        public IActionResult UpdateDoctor(Doctor doctor)
        {
            this._hospitalContext.Update(doctor);

            this._hospitalContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("doctors/{idDoctor}")]
        public IActionResult DeleteDoctor(int idDoctor)
        {

            Console.Write(idDoctor);

            Doctor doctor = this._hospitalContext.Doctor.Find(idDoctor);

            this._hospitalContext.Remove(doctor);

            this._hospitalContext.SaveChanges();

            return Ok();
        }
    }
}