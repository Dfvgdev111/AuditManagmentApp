using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class AuditCategory
    {
        [Key]
        public int Id {get;set;}
        public string Name {get;set;}
        public string description {get;set;}
        public int AuditId {get;set;}
        public double RiskLevel {get;set;}
        public Audit Audit {get;set;}
        public List<AuditQuestion> AuditQuestions {get;set;}
    }
}