using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Backend.Enums;
namespace Backend.Attrabutes
{
    public class ValidRiskLevelEnumAttrabute :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is RiskLevelEnum riskLevel){
                if(Enum.IsDefined(typeof(RiskLevelEnum),riskLevel))
                {
                    return ValidationResult.Success;
                }
            }

        return new ValidationResult("Invalid Risk Level.");
        }
    }
}