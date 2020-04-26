using System;
using System.ComponentModel.DataAnnotations;

namespace APBD.DTO.Requests
{
    public class PromoteStudentRequest
    {
        [Required]
        public string Semester { get; set; }

        [Required]
        public string Studies { get; set; }

    }
}