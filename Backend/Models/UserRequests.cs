using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;
using Backend.Models;

namespace Backend.test
{
    public class UserRequests
    {
    public int Id { get; set; }
    public int ProjectId { get; set; } //The Id of the project being sent
    public Project Project {get;set;}
    public string InvitedUserId { get; set; } // User receiving the request
    public AppUser InvitedUser { get; set; }
    public string InviterUserId { get; set; } // Owner or Manager sending the request
    public AppUser InviterUser { get; set; }
    public string Role { get; set; } // Role to be assigned (Auditor, Viewer, etc.)
    public RequestStatus Status { get; set; } = RequestStatus.Pending; // Enum: Pending, Accepted, Declined
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}



//User recives request from a user that is in a Project 
//That will go into the Request section within a Users Main Portoflio


//How requests will work
//Requests will be sent from the Project
//This Project 