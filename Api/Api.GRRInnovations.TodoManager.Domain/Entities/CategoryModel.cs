using Api.GRRInnovations.TodoManager.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Domain.Entities
{
    public class CategoryModel : BaseModel, ICategoryModel
    {
        public string Name { get; set; }

        public List<TaskCategoryModel>? TasksCategories { get; set; }

        public CategoryModel()
        {
            TasksCategories = new List<TaskCategoryModel>();
        }
    }
}
