using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Backend.Dtos.Account
{
    public class LoginDto
    {
        [EmailAddress]
        public string? Email {get; set;}
        [Required]
        public string? Password {get;set;}
    }
}