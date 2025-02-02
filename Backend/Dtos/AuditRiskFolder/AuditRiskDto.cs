using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos.AuditRiskFolder
{
    public class AuditRiskDto
{
    public int AuditID { get; set; }

    public int ProjectId {get;set;}

    public double RiskLevel {get;set;}
    
}

}