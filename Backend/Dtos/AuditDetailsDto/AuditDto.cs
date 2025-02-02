using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.Account;
using Backend.Enums;
using Backend.Models;

namespace Backend.Dtos.AuditDetailsDto
{
    public class AuditDto
    {
    public int AuditID { get; set; }
    public string AuditTitle { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedById { get; set; }
    public ShowUserInfoDto CreatedBy { get; set; }

    public double RiskLevel {get;set;}
    public StatusEnum Status { get; set; }
    public int ProjectId { get; set; }
    public List<AuditCategoriesDTO> AuditCategories { get; set; }
    }
}