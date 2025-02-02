using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    
    public class ProjectPortfolio
    {
        public string AppUserId {get;set;}
        public int ProjectId {get;set;}
        public AppUser AppUser {get;set;}
        public Project Project {get;set;}
        public string Role {get;set;}
    }
}