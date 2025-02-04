using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Database;
using Backend.Dtos.AuditCategoryDtos;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Backend.Repositories
{
    public class  AuditCategoryRepository : IAuditCategoryRepository
    {

        private readonly ApplicationDbContext _context;
        public AuditCategoryRepository(ApplicationDbContext context){
            _context = context;
        }

        public async Task<List<AuditCategory>> GetByAuditId(int id,int projectid)
        {

            var AuditCategories = await _context.AuditCategories.
            Where(x=> x.Audit.ProjectId == projectid && x.AuditId == id)
            .ToListAsync();

            return AuditCategories;
        }


        public async Task<AuditCategory> CreateAuditCategory(AuditCategory auditCategory){
           await _context.AuditCategories.AddAsync(auditCategory);
           await _context.SaveChangesAsync();
           return auditCategory;
        }

        public async Task<AuditCategory> UpdateAuditCategory(int auditID,int auditCategoryId,UpdatAuditCategory auditCategoryModel){
        var auditCategory = await _context.AuditCategories
        .FirstOrDefaultAsync(x=> x.Id == auditCategoryId && x.AuditId == auditID);

        if(auditCategory == null){
            return null;
        }
        auditCategory.Name = auditCategoryModel.Name;
        auditCategory.description = auditCategoryModel.description;
        auditCategory.RiskLevel = auditCategoryModel.RiskLevel;

        await _context.SaveChangesAsync();

        return auditCategory;


        }       
        public async Task<AuditCategory> RemoveAuditCategory(int AuditId, int AuditCategoryID){
            var auditCategory = await _context.AuditCategories.FirstOrDefaultAsync(x=> x.Id == AuditCategoryID && x.AuditId == AuditId);
            if(auditCategory == null){
                return null;
            }
             _context.Remove(auditCategory);
            await _context.SaveChangesAsync();
            return auditCategory;
            
        }

        public async Task<AuditCategoryDetailsDto> GetSingleCategoryByAudit(int id, int auditID)
        {
                return await _context.AuditCategories
                    .Where(x => x.AuditId == auditID && x.Id == id)
                    .Include(x => x.AuditQuestions)
                    .Select(x => x.ShowAuditCategory()) 
                    .FirstOrDefaultAsync(); 
}

        public async Task<bool> GetAuditCategoryByProjectId(int projectId, int AuditCategoryId, int AuditId)
        {
      var result = await (from audit in _context.Audit
                    join auditCategory in _context.AuditCategories on audit.AuditID equals auditCategory.AuditId
                    join project in _context.Projects on audit.ProjectId equals project.ProjectId
                    where audit.ProjectId == projectId
                          && auditCategory.Id == AuditCategoryId
                          && audit.AuditID == AuditId
                    select new
                    {
                        ProjectId = project.ProjectId,
                        AuditId = audit.AuditID,
                        AuditCategoryId = auditCategory.Id
                    }).AnyAsync();

                    return result;
        }
    }
    }

