using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.Account;

namespace Backend.Dtos.Project
{
    public class ProjectWithUserInfoDto
    {
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ShowUserInfoDto Createdby { get; set; } // Use DTO here
    public DateTime CreatedOn { get; set; }

    }
}