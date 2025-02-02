using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.Project;
using Backend.Models;



namespace Backend.Interfaces
{
    public interface IProjectPortfolioRepositories
    {
        Task<List<ProjectWithUserInfoDto>> GetUserProjectPortfolio(AppUser user);

        Task<ProjectPortfolio> AddProjectPortfolio(ProjectPortfolio project);

        Task<ProjectPortfolio> GetProjectPortfolioByAppuserId (string AppUserId,int id,string Role);

        
        Task<ProjectPortfolio> VetifyUserforProject(int id,AppUser user);

    
    }
}