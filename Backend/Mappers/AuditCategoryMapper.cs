using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Dtos.AuditCategoryDtos;
namespace Backend.Mappers
{
    public static class AuditCategoryMapper
    {
       

        public static AuditCategory CreateAuditCategory(this CreateAuditCategoryDto auditCategoryModel,int id){
            return new AuditCategory{
                Name = auditCategoryModel.Name,
                description = auditCategoryModel.description,
                AuditId = id,
                RiskLevel = auditCategoryModel.RiskLevel
            };
        }

              public static AuditCategoryDetailsDto ShowAuditCategory(this AuditCategory auditCategoryModel){
            return new AuditCategoryDetailsDto{
                Name = auditCategoryModel.Name,
                Description = auditCategoryModel.description,
                AuditQuestions = auditCategoryModel.AuditQuestions.Select(c=> c.FromAuditQuestion()).ToList(),
            };
        }
    }
}