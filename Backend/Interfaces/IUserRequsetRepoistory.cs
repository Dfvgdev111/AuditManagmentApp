using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.test;

namespace Backend.Interfaces
{
    public interface IUserRequsetRepoistory
    {
        Task<UserRequests> CreateUserRequest(UserRequests userRequests);

        Task<UserRequests> DeleteUserRequest(int UserRequestId);

        Task<UserRequests> DeclineUserRequest(UserRequests userRequests);

        Task<UserRequests> AcceptUserRequest(UserRequests userRequests);

        Task <UserRequests> FindUserRequestByInvitedUserIdAndProjectId(string userId, int projectId);

        Task<List<UserRequests>> GetInvites(string userId);

        Task<UserRequests> GetUserRequestById(int UserRequestId);

        Task<List<UserRequests>> GetInvitesProjectSided(int ProjectId);
        

    }
}