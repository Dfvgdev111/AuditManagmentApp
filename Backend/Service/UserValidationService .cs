using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Service
{
    public class UserValidationService : IUserValidationService
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IProjectPortfolioRepositories _projectrepo;

        public UserValidationService(UserManager<AppUser> userManager, IProjectPortfolioRepositories projectRepo){
            _userManager = userManager;
            _projectrepo = projectRepo;
        }


        
        public async Task<bool> ValidationUserAndProjectAsync(string username, int projectId, string role,List<string> allowedroles)
        {
        if (string.IsNullOrEmpty(username)){
            return false;
        }
        var appUser = await  _userManager.FindByNameAsync(username);
            
        if(appUser == null){
            return false;
        }
          if(!allowedroles.Contains(role)){
            return false;
        }
            return true;
        }



        
    }
}