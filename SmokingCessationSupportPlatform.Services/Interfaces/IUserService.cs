using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUser();
        Task DeleteUserAsync(User user);
        Task<User?> GetUserByIdAsync(int id);
    }
}
