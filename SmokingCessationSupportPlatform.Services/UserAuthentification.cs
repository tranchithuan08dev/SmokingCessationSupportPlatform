using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Services
{
    public class UserAuthentification : IUserAuthentification
    {
        private readonly IUserRepository _userRepository;

        public UserAuthentification(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByIdAsync(int id)
        {
            return _userRepository.GetUserByIdAsync(id);
        }

        public Task<User?> GetUserByIdWithCoachInfoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUsersByIdsAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            return _userRepository.UpdateUserAsync(user);
        }
    }
}
