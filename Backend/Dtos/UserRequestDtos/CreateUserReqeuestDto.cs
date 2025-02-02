using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Attrabutes;
namespace Backend.Dtos.UserRequestDtos
{
    public class CreateUserRequestDto
    {
        public string UserName {get;set;}

        [ValidRole]
        public string Role {get;set;}
    }
}