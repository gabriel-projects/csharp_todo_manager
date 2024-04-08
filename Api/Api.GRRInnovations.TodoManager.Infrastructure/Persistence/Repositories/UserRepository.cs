﻿using Api.GRRInnovations.TodoManager.Domain.Attributes;
using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Persistence.Repositories
{
    [Ioc(Interface = typeof(UserRepository))]
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext Context;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.Context = applicationDbContext;
        }

        public async Task<IUserModel> Create(IUserModel userModel)
        {
            if (userModel is not UserModel model) return null;

            if (model.Password == null) model.Password = Guid.NewGuid().ToString();

            model.Password = ConvertPassword(model.Password);

            if (string.IsNullOrEmpty(model.UserDetail.Name))
            {
                var firsName = model.UserDetail.FirstName ?? "";
                var lastName = model.UserDetail.LastName ?? "";

                model.UserDetail.Name = $"{firsName} {lastName}";
            }

            await Context.Users.AddAsync(model).ConfigureAwait(false);

            return model;
        }

        public async Task<List<IUserModel>> Users(UserOptions userOptions)
        {
            return await Query(userOptions).ToListAsync<IUserModel>();
        }

        private IQueryable<UserModel> Query(UserOptions options)
        {
            var query = Context.Users.AsQueryable();

            if (options.FilterLogins != null) query = query.Where(p => options.FilterLogins.Contains(p.Login));
            if (options.FilterUsers != null) query = query.Where(p => options.FilterUsers.Contains(p.Uid));

            return query;
        }

        private string ConvertPassword(string password)
        {
            if (password.StartsWith("##no|compute##"))
            {
                return password.Replace("##no|compute##", "");
            }

            return BCrypt.Net.BCrypt.HashPassword(password, 10);
        }
    }
}
