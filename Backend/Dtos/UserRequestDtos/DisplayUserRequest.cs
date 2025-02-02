using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.Account;
using Backend.Enums;
using Backend.Models;

namespace Backend.Dtos.UserRequestDtos
{
    public class DisplayUserRequest
    {
        public int Id {get;set;}

        public string ProjectName {get;set;}
        public string Role {get;set;}

        public RequestStatus status {get;set;}

        public ShowUserInfoDto InviterUser {get;set;}
        public DateTime CreatedAt {get;set;}


    }
}