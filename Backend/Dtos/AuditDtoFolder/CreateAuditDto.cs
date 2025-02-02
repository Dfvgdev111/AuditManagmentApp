using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos.AuditDtoFolder
{
    public class CreateAuditDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Title must be atleast 5 characters long")]
        [MaxLength(280,ErrorMessage ="Title cannot be over 280 characters")]
        public string AuditTitle { get; set; }   
    }
}
