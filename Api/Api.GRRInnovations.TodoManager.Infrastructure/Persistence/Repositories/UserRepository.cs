using Api.GRRInnovations.TodoManager.Domain.Attributes;
using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Domain.ValueObjects;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext Context;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.Context = applicationDbContext;
        }

        public async Task<IUserModel> CreateAsync(IUserModel userModel)
        {
            if (userModel is not UserModel model) return null;

            await Context.Users.AddAsync(model).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return model;
        }

        public async Task<List<IUserModel>> UsersAsync(UserOptions userOptions)
        {
            return await Query(userOptions).ToListAsync<IUserModel>();
        }

        public async Task<IUserModel> GetAsync(Guid uid)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Uid == uid);
        }
        private IQueryable<UserModel> Query(UserOptions options)
        {
            var query = Context.Users.AsQueryable();

            if (options.FilterLogins.Any()) query = query.Where(p => options.FilterLogins.Contains(p.Login));
            if (options.FilterUsers.Any()) query = query.Where(p => options.FilterUsers.Contains(p.Uid));
            if (options.IncludeUserDetail) query = query.Include(p => p.DbUserDetail);

            return query;
        }
    }
}
