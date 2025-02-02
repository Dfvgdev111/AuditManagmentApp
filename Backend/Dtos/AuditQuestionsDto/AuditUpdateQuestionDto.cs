using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Attrabutes;
using Backend.Enums;
namespace Backend.Dtos.AuditQuestionsDto
{
    public class AuditUpdateQuestionDto
    {        
        [Required]
        public string QuestionText {get;set;}
        [Required]
        public bool QuestionAnswer {get;set;}
        [Required]
        [ValidRiskLevelEnumAttrabute]
        public RiskLevelEnum RiskLevel {get;set;}
    }
}