using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Attrabutes
{
    public class ValidCategoryRiskLevel : ValidationAttribute
    {
        private readonly List<double> RiskValues = new List<double> {0,1.0,1.5,2.5};

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext){

            if(value is null || value is not double risk){
                return new ValidationResult("Risk must be a double");
            }

            if(RiskValues.Contains(risk)){
                return ValidationResult.Success;
            }

            return new ValidationResult("Validation of RiskValues is not valid");
        }

    }
}