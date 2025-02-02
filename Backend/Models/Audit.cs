using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Models
{
    public class Audit
    {
        public int AuditID { get; set; }
        public string AuditTitle {get;set;}
        public DateTime CreatedOn {get;set;} = DateTime.Now;
        public string CreatedById {get;set;}
        public AppUser CreatedBy {get;set;} 
        public List<AuditCategory> AuditCategories {get;set;}
        public StatusEnum Status {get;set; }
        public double RiskLevel {get;set;}
        public int ProjectId {get;set;}
        public Project Project {get;set;}
    }
}