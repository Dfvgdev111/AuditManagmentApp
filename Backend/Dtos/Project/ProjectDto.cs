using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AuditDtoFolder;
using Backend.Models;
namespace Backend.Dtos.Project
{
    public  class ProjectDto
    {

        public int ProjectId { get; set; }

        public string Title {get;set;}

        public string Description {get;set;}

        public List<AuditDto> Audit {get;set;}

    }
        
    }
