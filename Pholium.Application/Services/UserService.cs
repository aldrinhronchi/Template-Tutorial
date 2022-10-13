using AutoMapper;
using Pholium.Application.Interfaces;
using Pholium.Application.ViewModels;
using Pholium.Auth.Services;
using Pholium.Domain.Entities;
using Pholium.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pholium.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public List<UserViewModel> Get()
        {
            List<UserViewModel> _userViewModels = new List<UserViewModel>();
            IEnumerable<User> _users = this.userRepository.GetAll();

            _userViewModels = mapper.Map<List<UserViewModel>>(_users);

            //foreach (var item in _users)
            //{
            //    _userViewModels.Add(mapper.Map<UserViewModel>(item));
            //_userViewModels.Add(new UserViewModel { ID = item.ID, Email = item.Email, Name = item.Name });
            //}

            return _userViewModels;
        }

        public bool Post(UserViewModel userViewModel)
        {
            //User _user = new User
            //{
            //    ID = Guid.NewGuid(),
            //    Name = userViewModel.Name,
            //    Email = userViewModel.Email,
            //};

            User _user = mapper.Map<User>(userViewModel);

            this.userRepository.Create(_user);
            return true;
        }

        public UserViewModel GetById(String id)
        {
            if (!Guid.TryParse(id, out Guid userId))
            {
                throw new Exception("UserID is not valid!");
            }
            User _user = this.userRepository.Find(x => x.ID == userId & !x.IsDeleted);
            if (_user == null)
            {
                throw new Exception("User not found");
            }
            return mapper.Map<UserViewModel>(_user);
        }

        public bool Put(UserViewModel userViewModel)
        {
            User _user = this.userRepository.Find(x => x.ID == userViewModel.ID & !x.IsDeleted);
            if (_user == null)
            {
                throw new Exception("User not found");
            }
            mapper.Map(userViewModel, _user);

            this.userRepository.Update(_user);
            return true;
        }

        public bool Delete(String id)
        {
            if (!Guid.TryParse(id, out Guid userId))
            {
                throw new Exception("UserID is not valid!");
            }
            User _user = this.userRepository.Find(x => x.ID == userId & !x.IsDeleted);
            if (_user == null)
            {
                throw new Exception("User not found");
            }
            return this.userRepository.Delete(_user);
        }

        public UserAuthenticateResponseViewModel Authenticate(UserAuthenticateRequestViewModel user)
        {
            User _user = this.userRepository.Find(x => !x.IsDeleted && x.Email.ToUpper() == user.Email.ToUpper());
            if (_user == null)
            {
                throw new Exception("User not found");
            }
            return new UserAuthenticateResponseViewModel(mapper.Map<UserViewModel>(_user), TokenService.GenerateToken(_user));
        }
    }
}