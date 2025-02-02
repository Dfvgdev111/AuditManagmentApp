using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IProjectRepositories
    {
        Task<Project> CreateProject(Project project);    

        Task<Project> GetProjectById(int id);
    }
}