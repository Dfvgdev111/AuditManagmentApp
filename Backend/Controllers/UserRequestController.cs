using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Attrabutes;
using Backend.Dtos.UserRequestDtos;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/UserRequestController")]
    [ApiController]
    public class UserRequestController : ControllerBase
    {
        private readonly IUserRequsetRepoistory  _userRequestRepo;
        private readonly IUserValidationService _userValid;

        private readonly IProjectPortfolioRepositories _ProjectPortRepo;
        private readonly UserManager<AppUser>  _userManager;
        public UserRequestController(IUserRequsetRepoistory userRequestRepo,IUserValidationService uservalid,UserManager<AppUser> userManager,IProjectPortfolioRepositories ProjectPortRepo){

            _userRequestRepo = userRequestRepo;
            _userValid = uservalid;
            _userManager = userManager;
            _ProjectPortRepo = ProjectPortRepo;
        }


        [HttpPost]
        [ValidateUser("Admin","ProjectManagement","Owner")]
        public async Task<IActionResult> CreateUserRequseFromProject([FromBody] CreateUserRequestDto userRequest){
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
                 var InvitedID = await _userManager.FindByNameAsync(userRequest.UserName);
                if(InvitedID == null){
                    return NotFound("User not found");
                }
                var InviterID = await _userManager.FindByNameAsync(username);
                if(InviterID == null){
                    return NotFound("ProjectUser not Found");
                }
                var UserInProjectValidator = await _ProjectPortRepo.VetifyUserforProject(projectId,InvitedID);
                if(UserInProjectValidator != null){
                    return BadRequest("User is already in the Project");
                }
                var InvitedValidaiton = await _userRequestRepo.FindUserRequestByInvitedUserIdAndProjectId(InvitedID.Id,projectId);
                if(InvitedValidaiton != null){
                    return BadRequest("User has already been invited");
                }
                var newRequest = userRequest.ToUserRequest(InvitedID.Id,InviterID.Id,projectId);
                await _userRequestRepo.CreateUserRequest(newRequest);
            return Ok(userRequest);
        }

            [HttpGet]
            public async Task<IActionResult> GetProjectRequests(){
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if(username == null) return Unauthorized();
                var appuser = await _userManager.FindByNameAsync(username);
                var invites = await _userRequestRepo.GetInvites(appuser.Id);
                var invitesDto = invites.Select(x=> x.ToviewUserRequest()).ToList();
                return Ok(invitesDto);
            }

            [HttpPost("{InviteID:int}")]
            public async Task<IActionResult> AcceptProjectRequest([FromRoute]int InviteID){
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if(username== null) return Unauthorized();
                var Invite = await _userRequestRepo.GetUserRequestById(InviteID);
                if(Invite.Status == Enums.RequestStatus.Declined) return BadRequest("User declined the Invite");
                await _userRequestRepo.AcceptUserRequest(Invite);
                return Ok(Invite);
        }
        
            [HttpGet("ProjectSided")]
            [ValidateUser("Admin","ProjectManagement","Owner")]
            public async Task<IActionResult> GetProjectInvitesProjectSided(){
                var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
                var invites = await _userRequestRepo.GetInvitesProjectSided(projectId);
                var InvitesDto =  invites.Select(x=> x.ToviewUserRequest()).ToList();
                return Ok(InvitesDto);
        }

            [HttpDelete]
            [ValidateUser("Admin","ProjectManagement","Owner")]
             public async Task<IActionResult> DeleteUserRequset( [FromRoute]int InviteID){
            var userRequestDeleted = await _userRequestRepo.DeleteUserRequest(InviteID);
            return Ok(userRequestDeleted);
        }
        
    }
}