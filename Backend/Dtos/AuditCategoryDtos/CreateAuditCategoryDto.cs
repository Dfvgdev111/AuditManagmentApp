using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AuditQuestionsDto;
using Backend.Enums;
using Backend.Models;
using Backend.Attrabutes;
namespace Backend.Dtos.AuditCategoryDtos
{
    public class CreateAuditCategoryDto
    {
        [Required]
        public string Name {get;set;}
        [Required]
        public string description {get;set;}

        [ValidCategoryRiskLevel]
        public double RiskLevel {get;set;}

        [Required]
        public List<AuditQuestionDto> AuditQuestions {get;set;}
    }
}