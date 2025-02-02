using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/Projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {   
        private readonly IProjectRepositories _ProjectRepo;
        private readonly UserManager<AppUser> _UserManager;
        public ProjectController(IProjectRepositories ProjectRepo,UserManager<AppUser> userManager){
            _UserManager = userManager;
            _ProjectRepo = ProjectRepo;
        }

        
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetProjectID(int id){
            var Project = await _ProjectRepo.GetProjectById(id);
            if(Project == null){
                return NotFound("Project not found");
            }
            return Ok(Project);

        }
        [HttpGet("GetProjectByUser")] 
        public async Task<IActionResult> GetProjectByUser(){
        var ProjectValidation = Convert.ToInt32(User.FindFirst("Project")?.Value);

        var Username = User.FindFirst(ClaimTypes.Name)?.Value;

        if(Username == null){
            return Unauthorized("User is not authenticated");
        }
        var Project = await _ProjectRepo.GetProjectById(ProjectValidation);

        if(Project == null){
            return NotFound("The Project has not been found");
        }

        var AppUser = await _UserManager.FindByNameAsync(Username);

        if(AppUser == null){
            return Unauthorized("User does not exist");
        }

        return Ok(Project.toProjectDto());  
        }
    }

    }



