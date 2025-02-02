using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AuditQuestionsDto;
using Backend.Enums;
using Backend.Models;

namespace Backend.Dtos.AuditDetailsDto
{
    public class AuditCategoriesDTO
    {
    public int AuditCategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public double RiskLevel {get;set;}
    public List<AuditQuestionDtoDisplay> QuestionDtos {get;set;}
    }
}