using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/ValidationTesting")]
    [ApiController]
    public class ValidationController : ControllerBase
    {

        [HttpGet("AdminMessage")]
        public IActionResult AdminMessage(){

            var RoleValidation = User.FindFirst("Role")?.Value;


            if(User.HasClaim(c=> c.Type == "Role" && c.Value == "Admin")){

                return Ok("You are an Admin.");
            }
            return BadRequest("No Admin Role.");
        }

        [HttpGet("UserMessage")]
        public IActionResult UserRoleMessage(){

            var RoleValidation = User.FindFirst("Role")?.Value;

            if(User.HasClaim(c => c.Type == "Role" && (c.Value == "User" || c.Value == "Admin"))){
                return Ok("You can be a Admin or a User");
            }
            return BadRequest("You are not even a User");
    
        }
        
    }
}