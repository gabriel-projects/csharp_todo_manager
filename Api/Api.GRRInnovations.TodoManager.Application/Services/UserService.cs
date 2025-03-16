using Api.GRRInnovations.TodoManager.Application.Interfaces;
using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Domain.ValueObjects;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;
using Api.GRRInnovations.TodoManager.Security.Interfaces;

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

        public async Task<bool> LoginExistsAsync(string login)
        {
            var options = UserOptions.Create()
                .WithLogins(new List<string> { login })
                .Build();

            var users = await _userRepository.UsersAsync(options);

            return users.Count == 0;
        }

        private Task<bool> CorrectPassword(string localPassword, string remotePassword)
        {
            if (remotePassword == null) return Task.FromResult(false);

            var passwordV1 = _cryptoService.HashPassword(remotePassword);

            if (localPassword == passwordV1) return Task.FromResult(true);
            if (!_cryptoService.VerifyPassword(passwordV1, localPassword)) return Task.FromResult(true);

            return Task.FromResult(false);
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
            var options = UserOptions.Create()
                .WithLogins(new List<string> { login })
                .Build();

            var users = await _userRepository.UsersAsync(options);

            var remoteUser = users.FirstOrDefault();
            if (remoteUser != null && !await CorrectPassword(password, remoteUser.Password))
            {
                remoteUser = null;
            };

            if (remoteUser != null)
            {
                if (remoteUser.BlockedAccess) throw new Exception("Acesso bloqueado.");
                if (remoteUser.PendingConfirm) throw new Exception("Confirmação da conta pendente.");
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
