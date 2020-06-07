using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD.Model
{
    public class Patient
    {
        [DatabaseGenerated((DatabaseGeneratedOption.Identity))]
        public int IdPatient { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }
    }
}