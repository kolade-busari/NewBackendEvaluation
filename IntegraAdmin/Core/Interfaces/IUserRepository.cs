using IntegraAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> UserExists(string username, string password);
        Task<IList<string>> GetUserRoles(ApplicationUser user);
    }
}
