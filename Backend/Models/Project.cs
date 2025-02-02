using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string Title {get;set;}

        public string Description {get;set;} //description

        public DateTime CreatedOn {get;set;} = DateTime.Now;

        public AppUser Createdby {get;set;} 

        public List<ProjectPortfolio> ProjectPortfolios {get;set;} = new List<ProjectPortfolio>();

        public List<Audit> Audit {get;set;} = new List<Audit>();

    }
}