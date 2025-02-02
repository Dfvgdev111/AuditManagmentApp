using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Dtos.AuditQuestionsDto
{
    public class AuditQuestionDtoDisplay
    {
        
        public int Id {get;set;}
        public string QuestionText {get;set;}
        public RiskLevelEnum RiskLevel {get;set;}
        public bool QuestionAnswer {get;set;}

    }
}