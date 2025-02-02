using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Service
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IConfiguration _config;

        private readonly SymmetricSecurityKey _key;


        public TokenService(IConfiguration config, UserManager<AppUser> userManager){
            _config = config;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }

        

        public string CreateProjectUser (AppUser user){

            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
            };

            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);


            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token =  tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    
    
    public string CreateTokenBasedOnProject(AppUser user, ProjectPortfolio ProjectPortfolio){
            try {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Project",ProjectPortfolio.ProjectId.ToString()),
                new Claim(ClaimTypes.Role,ProjectPortfolio.Role),
            };


            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
            };

            var tokenHandler = new  JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
            }
            catch(Exception ex){
                throw new Exception("Error Generating Token",ex);
            }
    }


}

}