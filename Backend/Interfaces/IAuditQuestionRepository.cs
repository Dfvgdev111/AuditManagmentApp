using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AuditQuestionsDto;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IAuditQuestionRepository
    {
            Task<AuditQuestion> CreateAuditQuestion(AuditQuestion auditQuestion);     
            Task<AuditQuestion> UpdateAuditQuestion(int Auditid,AuditUpdateQuestionDto auditQuestion);
  
    }
}