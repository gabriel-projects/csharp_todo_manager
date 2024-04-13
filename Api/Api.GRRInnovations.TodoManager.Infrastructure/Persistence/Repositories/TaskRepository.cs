using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext Context;

        public TaskRepository(ApplicationDbContext applicationDbContext)
        {
            this.Context = applicationDbContext;
        }

        public async Task<ITaskModel> CreatAsync(ITaskModel taskModel, IUserModel inUser, ICategoryModel inCategory)
        {
            if (taskModel is not TaskModel taskM) return null;
            if (inUser is not UserModel userM) return null;

            if (inCategory != null && inCategory is CategoryModel categoryM)
            {
                var taskCategory = new TaskCategoryModel
                {
                    CategoryUid = inCategory.Uid,
                };

                taskM.DbTasksCategories.Add(taskCategory);
            }

            taskM.User = userM;

            await Context.Tasks.AddAsync(taskM).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return taskM;
        }

        public Task DeleteAsync(Guid Uid)
        {
            throw new NotImplementedException();
        }

        public Task<ITaskModel> GetAllAsync(TaskOptions options)
        {
            throw new NotImplementedException();
        }

        public Task<ITaskModel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ITaskModel> UpdateAsync(ITaskModel task)
        {
            throw new NotImplementedException();
        }


        #region querys
        private IQueryable<UserModel> Query(UserOptions options)
        {
            var query = Context.Users.AsQueryable();

            if (options.FilterLogins != null) query = query.Where(p => options.FilterLogins.Contains(p.Login));
            if (options.FilterUsers != null) query = query.Where(p => options.FilterUsers.Contains(p.Uid));

            return query;
        }

        #endregion
    }
}
