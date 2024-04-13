using Api.GRRInnovations.TodoManager.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<ICategoryModel> GetAsync(Guid uid, CategoryOptions categoryOptions);

        Task<ICategoryModel> GetAsync(string name);

        Task<ICategoryModel> CreateAsync(ICategoryModel categoryModel);
    }

    public class CategoryOptions
    {
        public List<Guid> FilterTasksUid { get; set; }
    }
}
