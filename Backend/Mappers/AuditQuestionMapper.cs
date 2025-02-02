using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AuditQuestionsDto;
using Backend.Models;

namespace Backend.Mappers
{
    public static class AuditQuestionMapper
    {
        
        public static AuditQuestion toAuditQuestion(this AuditQuestionDto auditQuestionModel,int AuditCategoryID){
                return new AuditQuestion{
                    QuestionText = auditQuestionModel.QuestionText,
                    AuditCategoryID = AuditCategoryID,
                    QuestionAnswer = auditQuestionModel.QuestionAnswer,
                    RiskLevel = auditQuestionModel.RiskLevel,
    
                };
        }

        public static AuditQuestionDto FromAuditQuestion(this AuditQuestion auditQuestionModel){
            return new AuditQuestionDto{
                    QuestionText = auditQuestionModel.QuestionText,
                    QuestionAnswer = auditQuestionModel.QuestionAnswer,
                    RiskLevel = auditQuestionModel.RiskLevel,    
            };
        }
    }
}