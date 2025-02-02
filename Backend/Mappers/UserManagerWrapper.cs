using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.UserRequestDtos;
using Backend.Models;
using Backend.test;

namespace Backend.Mappers
{
    public static class UserManagerWrapper
    {
        
        public static UserRequests ToUserRequest(this CreateUserRequestDto UserRequest,string InvitedUserId, string InviterUserId,int projectId){
            return new UserRequests{
                InvitedUserId = InvitedUserId,
                Role =  UserRequest.Role,
                InviterUserId = InviterUserId,
                ProjectId = projectId,
            };
        }

        public static DisplayUserRequest ToviewUserRequest(this UserRequests requestModel){
            return new DisplayUserRequest{
                Id =  requestModel.Id,
                ProjectName =  requestModel.Project.Title,
                Role = requestModel.Role,
                status = requestModel.Status,
                CreatedAt = requestModel.CreatedAt,
                InviterUser = requestModel.InviterUser.ShowUserInfoDto(),
            };
        }

    }
}