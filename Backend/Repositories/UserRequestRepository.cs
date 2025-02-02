using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Database;
using Backend.Enums;
using Backend.Interfaces;
using Backend.Models;
using Backend.test;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Backend.Repositories
{
    public class UserRequestRepository : IUserRequsetRepoistory
    {
        private readonly ApplicationDbContext _context;


        public UserRequestRepository(ApplicationDbContext context){
            _context = context;
        }

        public async Task<List<UserRequests>> GetInvites(string userId){

            return await  _context.UserRequests.Where(x=> x.InvitedUserId == userId)
            .Include(x => x.Project)
            .Include(x=> x.InviterUser)
            .ToListAsync();

        }
        public async Task<UserRequests> AcceptUserRequest(UserRequests userRequests)
        {
          var UserAccount = new ProjectPortfolio{
                AppUserId = userRequests.InvitedUserId,
                AppUser = userRequests.InvitedUser,
                ProjectId = userRequests.ProjectId,
                Role = userRequests.Role
            };
            await _context.ProjectPortfolios.AddAsync(UserAccount);

            _context.UserRequests.Remove(userRequests);

            await _context.SaveChangesAsync();
        
            return userRequests;
        }
        
      public async Task<UserRequests> FindUserRequestByInvitedUserIdAndProjectId(string userId, int projectId)
    {
    return await _context.UserRequests.FirstOrDefaultAsync(x => x.InvitedUserId == userId && x.ProjectId == projectId);
    }

        public async Task<UserRequests> CreateUserRequest(UserRequests userRequests)
        {
           await _context.UserRequests.AddAsync(userRequests);
                        
           await _context.SaveChangesAsync();
           return userRequests;

        }   

        public async Task<UserRequests> DeclineUserRequest(UserRequests userRequests)
        {
            userRequests.Status = RequestStatus.Declined;


            _context.UserRequests.Remove(userRequests);
            await _context.SaveChangesAsync();
            

            return  userRequests;

        }

        public async Task<UserRequests> DeleteUserRequest(int UserRequestID)
        {

            var userRequest = await _context.UserRequests.FirstOrDefaultAsync(x=> x.Id == UserRequestID);
            _context.UserRequests.Remove(userRequest);
            await _context.SaveChangesAsync();
            return userRequest;
        }

        public async Task<UserRequests> GetUserRequestById(int UserRequestId){
            return await _context.UserRequests.FirstOrDefaultAsync(x=> x.Id == UserRequestId);
        }

        public async Task<List<UserRequests>> GetInvitesProjectSided(int ProjectId)
        {
             return await _context.UserRequests.Where(x=> x.ProjectId == ProjectId)
            .Include(x => x.Project)
            .Include(x=> x.InviterUser)
            .ToListAsync();
        }
    }  
}