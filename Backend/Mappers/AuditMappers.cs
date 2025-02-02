using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AuditDtoFolder;
using Backend.Models;

namespace Backend.Mappers
{
    public static class AuditMappers
    {
        public static AuditDto toAuditDto(this Audit AuditModel){

            return new AuditDto(){
                AuditId = AuditModel.AuditID,
                AuditTitle = AuditModel.AuditTitle,
                CreatedOn = AuditModel.CreatedOn,
                CreatedBy = AuditModel.CreatedBy.UserName,
                Status = AuditModel.Status
            };
        }
       
        public static Audit toCreateAudit (this CreateAuditDto AuditModel,AppUser user,int projectId){

            return new Audit(){
                AuditTitle = AuditModel.AuditTitle,
                CreatedBy = user,
                CreatedById = user.Id,
                CreatedOn = DateTime.Now,
                ProjectId = projectId,
                Status = 0,
        
            };
        }

        
    }
}