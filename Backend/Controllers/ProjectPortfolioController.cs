using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Dtos.Account;
using Backend.Dtos.Project;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/ProjectPortfolio")]
    [ApiController]
    public class ProjectPortfolioController : ControllerBase
    {
        private readonly IProjectRepositories _ProjectRepo;
        private readonly IProjectPortfolioRepositories _ProjectPortRepo;

        private readonly UserManager<AppUser>  _userManager;

        private readonly ITokenService _TokenService;
     public ProjectPortfolioController(IProjectRepositories ProjectRepo, IProjectPortfolioRepositories ProjectPortRepo,UserManager<AppUser> usermanger,ITokenService TokenService){
        _ProjectRepo = ProjectRepo;
        _ProjectPortRepo = ProjectPortRepo;
        _userManager = usermanger;
        _TokenService = TokenService;
     }
     [Route("CreateProject")]
     [HttpPost]
     public async Task<IActionResult> CreateProjectToPortfolio([FromBody] CreateProjectDto project){

        if(!ModelState.IsValid){
            BadRequest(ModelState);
        }
        
           var  Username = User.FindFirst(ClaimTypes.Name)?.Value;

           if(Username == null){
            return Unauthorized("User is not authenticated");
           }

        var AppUser = await _userManager.FindByNameAsync(Username);
        if(AppUser == null){
        return Unauthorized("User does not exist");
        }

       
  
        var NewProject = new Project{
            Title = project.Title,
            Description = project.Description,
            Createdby =  AppUser,
            CreatedOn = DateTime.Now,
        };

        var CreatedProjects = await _ProjectRepo.CreateProject(NewProject);

        if(CreatedProjects == null){
            return StatusCode(500,"Error Creating Project");
        }
    
        var ProjectPortfolio = new ProjectPortfolio{
            AppUserId = AppUser.Id,
            ProjectId = CreatedProjects.ProjectId,
            Role = "Owner",
        };

        var portfolioCreationResult  = _ProjectPortRepo.AddProjectPortfolio(ProjectPortfolio);
        

        if(portfolioCreationResult == null){
            return StatusCode(500,"Error creating the project portfolio");
        }
     var token = _TokenService.CreateTokenBasedOnProject(AppUser,ProjectPortfolio);

     var ProjectDto = ProjectMappers.MapToNewProjectDto(NewProject,token);
  return CreatedAtAction(
        actionName: nameof(ProjectController.GetProjectID),
        controllerName: "Project",
        routeValues: new { id = CreatedProjects.ProjectId },
        value: ProjectDto
    );        
     }


    [Route("GetProjectPortfolio")]
    [HttpGet]
     public async Task<IActionResult> GetProjectPortfolio(){
        var Username = User.FindFirst(ClaimTypes.Name)?.Value;
        if(Username == null){
            return BadRequest("User not logged in");
        }
        var AppUser = await _userManager.FindByNameAsync(Username);
    
        var ProjectPortfolio = await _ProjectPortRepo.GetUserProjectPortfolio(AppUser);
        return Ok(ProjectPortfolio);
     }


     [HttpGet("{Projectid:int}")]
     public async Task<IActionResult> EnterProject(int Projectid){

        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        if(username == null){
            return Unauthorized("User is not authenticated");
        }


        var Appuser = await _userManager.FindByNameAsync(username);

        if(Appuser == null){
            return Unauthorized("App User does not exist");
        }

        var projectPortfolio = await _ProjectPortRepo.VetifyUserforProject(Projectid,Appuser);


        if(projectPortfolio == null){
            return Unauthorized("User does not have access to that project");
        }

        var token = _TokenService.CreateTokenBasedOnProject(Appuser,projectPortfolio);

        return Ok(token);
     }


}
}