using Api.GRRInnovations.TodoManager.Domain.Attributes;
using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using Api.GRRInnovations.TodoManager.Interfaces.Services;
using Api.GRRInnovations.TodoManager.Security.Crypto;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Api.GRRInnovations.TodoManager.Application.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly ICryptoService _cryptoService;

        public UserService(IUserRepository userRepository, ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }

        public async Task<List<IUserModel>> Users()
        {
            return await _userRepository.UsersAsync(new UserOptions());
        }

        public async Task<bool> LoginExistsAsync(string login)
        {
            var users = await _userRepository.UsersAsync(new UserOptions { FilterLogins = new List<string> { login } });

            return users.Count == 0;
        }

        private async Task<bool> CorrectPassword(string remotePassword, string localPassword)
        {
            if (remotePassword == null) return false;

            var passwordV1 = SHA1.Hash(remotePassword);

            if (localPassword == passwordV1) return true;
            if (!_cryptoService.VerifyPassword(remotePassword, localPassword)) return true;

            return false;
        }

        public async Task<IUserModel> CreateAsync(IUserModel userModel)
        {
            if (userModel == null) return null;

            if (string.IsNullOrEmpty(userModel.Password))
                userModel.Password = Guid.NewGuid().ToString();

            if (userModel.UserDetail != null && string.IsNullOrEmpty(userModel.UserDetail?.Name))
            {
                var firsName = userModel.UserDetail?.FirstName ?? "";
                var lastName = userModel.UserDetail?.LastName ?? "";

                userModel.UserDetail.Name = $"{firsName} {lastName}";
            }

            userModel.Password = _cryptoService.HashPassword(userModel.Password);

            return await _userRepository.CreateAsync(userModel);
        }

        public async Task<IUserModel> ValidateAsync(string login, string password)
        {
            var users = await _userRepository.UsersAsync(new UserOptions { FilterLogins = new List<string> { login } });

            var remoteUser = users.FirstOrDefault();
            if (remoteUser != null && !await CorrectPassword(password, remoteUser.Password))
            {
                remoteUser = null;
            };

            if (remoteUser != null)
            {
                //todo: criar ex personalizadas para lançar ex
                //if (remoteUser.BlockedAccess) return new BadRequestObjectResult(new WrapperOutError { Message = "Acesso bloqueado." });
                //if (remoteUser.PendingConfirm) return new BadRequestObjectResult(new WrapperOutError { Message = "Confirmação da conta pendente." });
            }

            return remoteUser;
        }

        public async Task<List<IUserModel>> GetAllAsync(UserOptions userOptions)
        {
            return await _userRepository.UsersAsync(userOptions);
        }

        public async Task<IUserModel> CreateUserModelFromClains(IUserClaimsModel claims)
        {
            if (claims == null) return null;

            var newUser = new UserModel
            {
                Login = claims.Email,
                BlockedAccess = false,
                PendingConfirm = false,
                UserDetail = new UserDetailModel
                {
                    Name = $"{claims.FirstName} {claims.LastName}",
                    Email = claims.Email,
                    FirstName = claims.FirstName,
                    LastName = claims.LastName
                },
            };

            return newUser;
        }
    }
}
