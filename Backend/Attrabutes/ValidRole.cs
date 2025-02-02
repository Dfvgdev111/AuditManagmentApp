using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Attrabutes
{
    public class ValidRole : ValidationAttribute
    {
            private readonly List<string> _roles = new List<string> {"Viewer","Auditor","ProjectManagement","Admin"};

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
                if(value is null || value is not string role)
                {
                    return new ValidationResult("Role is requred and must be string");
                }

                if(_roles.Contains(role)){
                    return ValidationResult.Success;
                }

                return new ValidationResult("Enter Valid Role");
        } 
    }
}