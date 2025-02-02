using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AuditDetailsDto;
using Backend.Dtos.AuditRiskFolder;
using Backend.Helpers;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IAuditRepository
    {
        Task<Audit> CreateAudit(Audit audit); 

        Task<List<Audit>> GetAudit ();

        Task<List<Audit>> GetAuditByProject(int id);

        Task<AuditDto?> GetFullAuditDetails(int id,int projectid);

        Task<Audit> GetAuditById(int id, int projectid);

        Task<Audit> DeleteAuditById(int id,int projectId);

        Task<List<Audit>> GetAuditQueryable(QueryObject? query,int Id);

        Task<AuditRiskDto> UpdateRiskScore(AuditDto audit,double RiskScoreValue);

        Task<byte[]> GenereateAuditCsv(int auditId);
        
    }
}