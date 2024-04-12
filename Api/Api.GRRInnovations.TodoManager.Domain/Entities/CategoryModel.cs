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

        public List<TaskCategoryModel>? DbTasksCategories { get; set; }

        public List<ITaskCategoryModel>? TasksCategories
        {
            get => DbTasksCategories?.Cast<ITaskCategoryModel>()?.ToList();
            set => DbTasksCategories = value?.Cast<TaskCategoryModel>()?.ToList();
        }

        public CategoryModel()
        {
            TasksCategories = new List<ITaskCategoryModel>();
        }
    }
}
