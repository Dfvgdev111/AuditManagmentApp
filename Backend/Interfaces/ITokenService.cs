using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface ITokenService
    {

        //string CreateProjectAdmin (AppUser user);

        string CreateProjectUser (AppUser user);

        string CreateTokenBasedOnProject(AppUser user, ProjectPortfolio ProjectPortfolio);

    }
}