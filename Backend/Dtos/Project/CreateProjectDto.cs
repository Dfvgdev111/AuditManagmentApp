using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
namespace Backend.Dtos.Project
{
    public class CreateProjectDto
    {
        
        public string Title {get;set;}

        public string Description {get;set;}

    }
}