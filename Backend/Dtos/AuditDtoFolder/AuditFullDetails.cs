using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AuditCategoryDtos;
using Backend.Enums;

namespace Backend.Dtos.AuditDtoFolder
{
    public class AuditFullDetails
    {
        public string AuditTitle { get; set; }

        public DateTime CreatedOn {get;set;}

        public string CreatedBy {get;set;}

        public StatusEnum Status {get;set;}

        public List<AuditCategoryDetailsDto> AuditCategoryDtos {get;set;}

        

    }
}