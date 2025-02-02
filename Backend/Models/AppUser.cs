using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.Account;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class AppUser : IdentityUser
    {
        public List<ProjectPortfolio> ProjectPortfolios {get;set;}



       
    }

}