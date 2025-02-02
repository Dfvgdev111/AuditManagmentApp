using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.Project;
using Backend.Models;

namespace Backend.Mappers
{
    public static class ProjectMappers
    {
        public static ProjectDto toProjectDto (this Project ProjectModel){
            return new ProjectDto{
                ProjectId = ProjectModel.ProjectId,
                Title = ProjectModel.Title,
                Description = ProjectModel.Description,
                Audit = ProjectModel.Audit.Select(c => c.toAuditDto()).ToList(),
            };
        }

        public static Project CreateProjectDto (this CreateProjectDto ProjectModel){
            return new Project{
                Title = ProjectModel.Title,
                Description = ProjectModel.Description
            };
        }


        public static NewProjectDto MapToNewProjectDto(Project project, string token){

            return new NewProjectDto{
                Title = project.Title,
                Description = project.Description,
                Token = token
            };
        }


    }
}