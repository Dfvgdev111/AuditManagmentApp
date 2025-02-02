using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Database;
using Backend.Dtos.AuditQuestionsDto;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class AuditQuestionRepository : IAuditQuestionRepository
    {
        
        private readonly ApplicationDbContext _context;


        public AuditQuestionRepository(ApplicationDbContext context){
            _context = context;
        }
        
    
        public async Task<AuditQuestion> CreateAuditQuestion(AuditQuestion auditQuestion)
        {
            await _context.AddAsync(auditQuestion);
            await _context.SaveChangesAsync();

            return  auditQuestion;
            
        }

        public async Task<AuditQuestion> UpdateAuditQuestion(int questionId,AuditUpdateQuestionDto auditQuestion)
        {
            var existingAudit = await _context.AuditQuestions.FirstOrDefaultAsync(x=> x.Id == questionId);
            if(existingAudit == null){
                return null;
            }
                
            existingAudit.QuestionText = auditQuestion.QuestionText;
            existingAudit.QuestionAnswer = auditQuestion.QuestionAnswer;
            existingAudit.RiskLevel = auditQuestion.RiskLevel;
        
            await _context.SaveChangesAsync();

            return existingAudit;
        }
    }

}
