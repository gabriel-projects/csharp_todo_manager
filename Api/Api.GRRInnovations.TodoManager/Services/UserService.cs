using Api.GRRInnovations.TodoManager.Domain.Attributes;
using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using Api.GRRInnovations.TodoManager.Security.Crypto;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Services
{
    public class UserService
    {
        public IUserRepository UserRepository { get; }

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<List<IUserModel>> Users()
        {
            return await UserRepository.UsersAsync(new UserOptions());
        }

        public async Task<bool> LoginAvailable(string login)
        {
            var users = await UserRepository.UsersAsync(new UserOptions { FilterLogins = new List<string> { login } });

            return users.Count == 0;
        }

        public async Task<IUserModel> Create(IUserModel userModel)
        {
            if (userModel == null) return null;

            return await UserRepository.CreateAsync(userModel);
        }

        public async Task<IUserModel> Validade(string login, string password)
        {
            var users = await UserRepository.UsersAsync(new UserOptions { FilterLogins = new List<string> { login } });

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

        private async Task<bool> CorrectPassword(string remotePassword, string localPassword)
        {
            if (remotePassword == null) return false;

            var passwordV1 = SHA1.Hash(remotePassword);

            if (localPassword == passwordV1) return true;//todo: mover cript para o projeto de segurança
            if (localPassword.StartsWith("$2") && BCrypt.Net.BCrypt.Verify(remotePassword, localPassword)) return true;

            await Task.CompletedTask;

            return false;
        }
    }
}
