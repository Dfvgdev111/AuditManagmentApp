using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Database;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class ProjectRepositories : IProjectRepositories
    {

        private readonly ApplicationDbContext _context;
        public ProjectRepositories(ApplicationDbContext context){
                _context = context;
        }

        public async Task<Project> CreateProject(Project project)
        {
            await _context.AddAsync(project); //Adds the Project in the method
            await _context.SaveChangesAsync(); //Saves it to the database
            return project; //Returns the Project
        }


        public async Task<Project> GetProjectById(int id){
        return await _context.Projects.FirstOrDefaultAsync(x=> x.ProjectId == id);
        }

        

    }
}