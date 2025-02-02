using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AuditQuestionsDto;

namespace Backend.Dtos.AuditCategoryDtos
{
    public class AuditCategoryDetailsDto
    {
        
        public string Name {get;set;}

        public string Description {get;set;}
        
        public List<AuditQuestionDto> AuditQuestions {get;set;}
    }
}