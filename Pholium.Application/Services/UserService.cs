using AutoMapper;
using Pholium.Application.Interfaces;
using Pholium.Application.ViewModels;
using Pholium.Auth.Services;
using Pholium.Domain.Entities;
using Pholium.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
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

            return _userViewModels;
        }

        public bool Post(UserViewModel userViewModel)
        {
            if (userViewModel.ID != Guid.Empty)
            {
                throw new Exception("UserID must be empty!");
            }

            Validator.ValidateObject(userViewModel, new ValidationContext(userViewModel), true);

            User _user = mapper.Map<User>(userViewModel);
            _user.Password = EncryptPassword(_user.Password);

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
            if (userViewModel.ID == Guid.Empty)
            {
                throw new Exception("ID is invalid");
            }

            User _user = this.userRepository.Find(x => x.ID == userViewModel.ID & !x.IsDeleted);
            if (_user == null)
            {
                throw new Exception("User not found");
            }
            mapper.Map(userViewModel, _user);
            _user.Password = EncryptPassword(_user.Password);

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
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                throw new Exception("Email/Password are required.");
            }

            user.Password = EncryptPassword(user.Password);

            User _user = this.userRepository.Find(x => !x.IsDeleted && x.Email.ToUpper() == user.Email.ToUpper()
                                                  && x.Password.ToUpper() == user.Password.ToUpper());
            if (_user == null)
            {
                throw new Exception("User not found");
            }
            return new UserAuthenticateResponseViewModel(mapper.Map<UserViewModel>(_user), TokenService.GenerateToken(_user));
        }

        private string EncryptPassword(string password)
        {
            HashAlgorithm sha = new SHA1CryptoServiceProvider();

            byte[] encryptedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                stringBuilder.Append(caracter.ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }
}