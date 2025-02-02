using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Database;
using Backend.Dtos.Account;
using Backend.Dtos.Project;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Backend.Repositories
{
    public class ProjectPortfolioRepositories : IProjectPortfolioRepositories
    {
        private readonly ApplicationDbContext _context;
        public ProjectPortfolioRepositories(ApplicationDbContext context){
            _context = context;
        }

        public async Task<ProjectPortfolio> AddProjectPortfolio(ProjectPortfolio ProjectPortfolioModel){
            await _context.ProjectPortfolios.AddAsync(ProjectPortfolioModel);
            await _context.SaveChangesAsync();
            return ProjectPortfolioModel;
        }

        public async Task<ProjectPortfolio> GetProjectPortfolioByAppuserId(string userId, int ProjectId,string Role)
        {
            var UserValidation =  await _context.ProjectPortfolios.
            FirstOrDefaultAsync(x=> x.AppUserId == userId && x.ProjectId == ProjectId && x.Role == Role);
                return UserValidation;
                
        }

        public async Task<List<ProjectWithUserInfoDto>> GetUserProjectPortfolio(AppUser user){
            return await _context.ProjectPortfolios.Where(u=> u.AppUserId == user.Id)
            .Select(project => new ProjectWithUserInfoDto
            {
             ProjectId = project.ProjectId,
             Title = project.Project.Title,
             Description = project.Project.Description,
             Createdby = project.Project.Createdby.ShowUserInfoDto(),
             CreatedOn = project.Project.CreatedOn,
            }).ToListAsync();
        }

        public async Task<ProjectPortfolio> VetifyUserforProject(int id, AppUser user)
        {
            return await _context.ProjectPortfolios.FirstOrDefaultAsync(u=> u.AppUserId == user.Id && u.ProjectId == id);
            

        }
    }
}