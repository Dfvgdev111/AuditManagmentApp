using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Database;
using Backend.Dtos.Account;
using Backend.Dtos.AuditDetailsDto;
using Backend.Dtos.AuditQuestionsDto;
using Backend.Dtos.AuditRiskFolder;
using Backend.Dtos.Csv;
using Backend.Helpers;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Backend.Service;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly CsvService _csvService;
        public AuditRepository(ApplicationDbContext context,CsvService csvService){
            _context = context;
            _csvService = csvService;
            
        }
        public async Task<Audit> CreateAudit(Audit audit)
        {
             await _context.Audit.AddAsync(audit);
             await _context.SaveChangesAsync();
             return audit;
        }
        public async Task<List<Audit>> GetAudit()
        {
           return await _context.Audit.ToListAsync();
        }
    
    
       
      

        public async Task<List<Audit>> GetAuditQueryable(QueryObject? query,int Id){
            var Audit =  _context.Audit.Where(s=> s.ProjectId == Id).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.auditTitle)){
                Audit =  Audit.Where(s=> s.AuditTitle.Contains(query.auditTitle));
            }

     
            if(!string.IsNullOrWhiteSpace(query.sortby)){
                if(query.sortby.Equals("AuditTitle",StringComparison.OrdinalIgnoreCase)){

                    Audit = query.isDecsending ? Audit.OrderByDescending(s=>s.AuditTitle): Audit.OrderBy(s=>s.AuditTitle);
                }
            }
            else{
                        Audit = Audit.OrderByDescending(a => a.CreatedOn); // Default sorting

            }

            var skipNumber = (query.pageNumber - 1) * query.pageSize;


            return await Audit.Skip(skipNumber).Take(query.pageSize).ToListAsync();
        }
        
        public async Task<List<Audit>> GetAuditByProject(int id)
        {
            return await _context.Audit.Where(x=> x.ProjectId == id).ToListAsync();            
        }
        public async Task<AuditDto?> GetFullAuditDetails(int id,int projectId){
          return await _context.Audit
        .Where(x => x.AuditID == id && x.ProjectId == projectId)
        .Include(audit => audit.AuditCategories)  // Include AuditCategories
            .ThenInclude(category => category.AuditQuestions)  // Include Questions within AuditCategory
        .Select(audit => new AuditDto
        {
            AuditID = audit.AuditID,
            AuditTitle = audit.AuditTitle,
            CreatedOn = audit.CreatedOn,
            CreatedById = audit.CreatedById,
            CreatedBy = audit.CreatedBy.ShowUserInfoDto(),
            RiskLevel = audit.RiskLevel,
            Status = audit.Status,
            ProjectId = audit.ProjectId,
            AuditCategories = audit.AuditCategories
                .Select(category => new AuditCategoriesDTO
                {
                    AuditCategoryId = category.Id,
                    Name = category.Name,
                    Description = category.description,
                    RiskLevel = category.RiskLevel,
                    QuestionDtos  = category.AuditQuestions
                        .Select(question => new AuditQuestionDtoDisplay
                        {   
                            Id = question.Id,
                            QuestionText = question.QuestionText,
                            QuestionAnswer = question.QuestionAnswer,
                            RiskLevel = question.RiskLevel,
                        })
                        .ToList()
                })
                .ToList()
        })
        .FirstOrDefaultAsync();
        }

        public async Task<Audit> GetAuditById(int id, int projectId){
            return await _context.Audit.FirstOrDefaultAsync(x=> x.AuditID == id && x.ProjectId == projectId);
        }
        public async Task<Audit> DeleteAuditById(int id,int projectId)
        {
            var Audit = await GetAuditById(id,projectId);
            if(Audit == null){
                return null;
            }
            _context.Audit.Remove(Audit);
            await _context.SaveChangesAsync();
            return Audit;
        }

    
        public async Task<AuditRiskDto> UpdateRiskScore(AuditDto audit,double RiskScoreValue){
          var existingAudit =  await _context.Audit.FirstOrDefaultAsync(x=> x.AuditID == audit.AuditID && x.ProjectId == audit.ProjectId);

          if(existingAudit == null) return null;
         existingAudit.RiskLevel = RiskScoreValue; 
          await _context.SaveChangesAsync();
        return new AuditRiskDto {
            AuditID = existingAudit.AuditID,
            ProjectId = existingAudit.ProjectId,
            RiskLevel = existingAudit.RiskLevel,
        };
        }

        public async Task<byte[]> GenereateAuditCsv(int auditId)
        {

            var audit = await _context.Audit
            .Include(a=> a.AuditCategories)
            .ThenInclude(c=> c.AuditQuestions)
            .FirstOrDefaultAsync(a=> a.AuditID == auditId);

            if(audit == null) return null;

            var auditData = new List<AuditCsvDto>();

            foreach(var category in audit.AuditCategories)
            {
                foreach(var question in category.AuditQuestions){
                    auditData.Add(new AuditCsvDto
                    {
                AuditTitle = audit.AuditTitle,
                CreatedOn = audit.CreatedOn,
                AuditRiskLevel = audit.RiskLevel,
                CategoryName = category.Name,
                CategoryRiskLevel = category.RiskLevel,
                QuestionText = question.QuestionText,
                QuestionRisk = (int)question.RiskLevel,
                QuestionAnswer = question.QuestionAnswer ? "Yes" : "No"
                    }
                    );
                }
            }

            return _csvService.GenerateCsv(auditData);
        }
        
    }
}