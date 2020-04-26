using System;
using System.ComponentModel.DataAnnotations;

namespace APBD.DTO.Requests
{
    public class EnrollStudentRequest
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        
        [Required]
        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }
        
        [Required]
        public string Studies { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}