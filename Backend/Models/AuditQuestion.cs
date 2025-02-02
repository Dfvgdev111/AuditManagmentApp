using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Models
{
    public class AuditQuestion
    {
        [Key]
        public int Id {get;set;}
        public string QuestionText {get;set;}

        public RiskLevelEnum RiskLevel {get;set;}
        public bool QuestionAnswer {get;set;}
        public int AuditCategoryID {get;set;}
        public AuditCategory AuditCategory {get;set;}


    }
}