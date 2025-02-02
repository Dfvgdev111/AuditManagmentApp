using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;
using Backend.Models;

namespace Backend.Dtos.AuditDtoFolder
{
    public class AuditDto
    {

        public int AuditId {get;set;}
        public string AuditTitle { get; set; }

        public DateTime CreatedOn {get;set;}

        public string CreatedBy {get;set;}

        public StatusEnum Status {get;set;}
        
    }
}