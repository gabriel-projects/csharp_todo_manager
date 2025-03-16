using Api.GRRInnovations.TodoManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Repositories
{
    public interface ICategoryRepository
    {
        Task<ICategoryModel> GetAsync(Guid uid, CategoryOptions categoryOptions);

        Task<ICategoryModel> GetAsync(string name);

        Task<ICategoryModel> CreateAsync(string name);
    }

    public class CategoryOptions
    {
        public List<Guid> FilterTasksUid { get; set; }
    }
}
