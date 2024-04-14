using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
                taskM.Category = categoryM;
            }

            taskM.User = userM;

            await Context.Tasks.AddAsync(taskM).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return taskM;
        }

        public async Task<bool> DeleteAsync(ITaskModel model)
        {
            var data = model as TaskModel;
            if (data == null) return false;

            Context.Tasks.Remove(data);

            var result = await Context.SaveChangesAsync().ConfigureAwait(false);
            return result > 0;
        }

        public async Task<List<ITaskModel>> GetAllAsync(TaskOptions options)
        {
            return await Query(options).ToListAsync<ITaskModel>();
        }

        public async Task<ITaskModel> GetAsync(Guid id, TaskOptions options)
        {
            return await Query(options).FirstOrDefaultAsync(x => x.Uid == id);
        }

        public async Task<ITaskModel> TaskCompletedAsync(ITaskModel model)
        {
            var data = model as TaskModel;
            if (data == null) return null;

            data.Status = Interfaces.Enuns.EStatusTask.Completed;

            Context.Tasks.Update(data);
            await Context.SaveChangesAsync();

            return data;
        }

        public async Task<ITaskModel> UpdateAsync(string json, ITaskModel model)
        {
            var data = model as TaskModel;
            if (data == null) return null;

            var wp = new WrapperInTask<TaskModel>();
            await wp.Populate(data).ConfigureAwait(false);

            JsonConvert.PopulateObject(json, wp);
            data = await wp.Result().ConfigureAwait(false);

            Context.Tasks.Update(data);
            await Context.SaveChangesAsync();

            return data;
        }


        #region querys
        private IQueryable<TaskModel> Query(TaskOptions options)
        {
            var query = Context.Tasks.AsQueryable();

            if (options.FilterUsers != null) query = query.Where(p => options.FilterUsers.Contains(p.Uid));

            return query;
        }

        #endregion
    }
}
