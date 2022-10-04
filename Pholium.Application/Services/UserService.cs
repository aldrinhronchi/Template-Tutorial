using Pholium.Application.Interfaces;
using Pholium.Application.ViewModels;
using Pholium.Domain.Entities;
using Pholium.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pholium.Application.Services
{
    public class UserService :IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public List<UserViewModel> Get()
        {
            List<UserViewModel> _userViewModels = new List<UserViewModel>();
            IEnumerable<User> _users = this.userRepository.GetAll();
            foreach (var item in _users)
            {
                _userViewModels.Add(new UserViewModel { ID = item.ID, Email = item.Email, Name = item.Name });
            }

            return _userViewModels;
        }
        public bool Post(UserViewModel userViewModel)
        {
            User _user = new User
            {
                ID = Guid.NewGuid(),
                Name = userViewModel.Name,
                Email = userViewModel.Email,
            };
            this.userRepository.Create(_user);

            return true;
        }
    }   
}
