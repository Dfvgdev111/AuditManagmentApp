using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Dtos.Account;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;

        private readonly ITokenService _TokenService;

        private readonly SignInManager<AppUser> _signinManager;


        public AccountController(UserManager<AppUser> userManager, ITokenService TokenService, SignInManager<AppUser> signInManager ){
            _usermanager = userManager;
            _TokenService = TokenService;
            _signinManager = signInManager;
        }

        
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterDto register){
          try{

            if(!ModelState.IsValid)
            return BadRequest(ModelState);

                var ValidateEmail = await _usermanager.FindByEmailAsync(register.Email);

                if(ValidateEmail != null){
                    return BadRequest("Email is already in use");
                }
            
            var AppUser = new AppUser{
                UserName = register.Username,
                Email = register.Email
            };

      
            var createdUser = await _usermanager.CreateAsync(AppUser,register.Password);

            if(createdUser.Succeeded){
            var roleResult = await _usermanager.AddToRoleAsync(AppUser,"User");

            if(roleResult.Succeeded){
                return Ok(new NewUserDto{
                        UserName = AppUser.UserName,
                        Email = AppUser.Email,
                        Token = _TokenService.CreateProjectUser(AppUser)
                });
            }
            else{
                return StatusCode(500,roleResult.Errors);
            }
            }
            else{
                return StatusCode(500,createdUser.Errors);
            }
          }
            catch(Exception e){
                return StatusCode(500,e);
            }
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginDto loginDto){
            try{

            var user = await _usermanager.Users.FirstOrDefaultAsync(x=> x.Email == loginDto.Email);

            if(user == null) return Unauthorized("Username nor password found");

            var result = await _signinManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

            if(!result.Succeeded) return Unauthorized("Username nor password found");

            if(user== null || !result.Succeeded) return Unauthorized();
            return Ok(new NewUserDto{
                UserName = user.UserName,
                Email = user.Email,
                Token = _TokenService.CreateProjectUser(user),
            });

            }
            catch(Exception e){
                return StatusCode(500,e);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProfile(){

           var  Username = User.FindFirst(ClaimTypes.Name)?.Value;
             if(string.IsNullOrEmpty(Username)){
            return BadRequest("User name claim not found");
           }
            var AppUser =  await _usermanager.FindByNameAsync(Username);
                    

            return Ok(new ShowUserInfoDto{
                UserName = AppUser.UserName,
                email = AppUser.Email
            });
        }
    }
}