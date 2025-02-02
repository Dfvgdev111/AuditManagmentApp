using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.Account;
using Backend.Models;

namespace Backend.Mappers
{
    public static class AppuserMapper
    {
        

        public static ShowUserInfoDto ShowUserInfoDto(this AppUser appUser){
            return new ShowUserInfoDto{
                UserName = appUser.UserName,
                email = appUser.Email,
            };
        }
    }
}