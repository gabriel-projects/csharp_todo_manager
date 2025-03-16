using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext Context;

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            this.Context = applicationDbContext;
        }

        public async Task<ICategoryModel> CreateAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            CategoryModel model = new CategoryModel
            {
                Name = name
            };

            await Context.Categories.AddAsync(model).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return model;
        }

        public async Task<ICategoryModel> GetAsync(Guid uid, CategoryOptions categoryOptions)
        {
            if (uid == null) return null;

            return await Context.Categories.FirstOrDefaultAsync(x => x.Uid == uid);
        }

        public async Task<ICategoryModel> GetAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            return await Context.Categories.FirstOrDefaultAsync(x => x.Name.Equals(name));
        }

        private IQueryable<CategoryModel> Query(CategoryOptions options)
        {
            var query = Context.Categories.AsQueryable();


            return query;
        }
    }
}
