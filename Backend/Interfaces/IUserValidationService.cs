using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Interfaces
{
    public interface IUserValidationService
    {
        Task<bool> ValidationUserAndProjectAsync(string username,int projectId,string role,List<string> allowedroles);
    }
}