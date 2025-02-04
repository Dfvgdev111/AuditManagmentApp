using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AuditCategoryDtos;
using Backend.Dtos.AuditQuestionsDto;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IAuditCategoryRepository
    {
        Task<bool> GetAuditCategoryByProjectId(int projectId,int AuditCategoryId,int AuditId);
        Task<AuditCategoryDetailsDto> GetSingleCategoryByAudit(int id, int auditID);
        Task<List<AuditCategory>> GetByAuditId(int id,int projectId);
        Task<AuditCategory> CreateAuditCategory(AuditCategory auditCategory);

        Task<AuditCategory> RemoveAuditCategory(int AuditId, int AuditCategoryID);

        Task<AuditCategory> UpdateAuditCategory(int auditID,int auditCategoryId,UpdatAuditCategory auditCategoryModel);
        
    }
}