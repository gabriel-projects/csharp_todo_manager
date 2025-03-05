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

        public List<TaskModel>? DbTasks { get; set; }

        public List<ITaskModel>? Tasks
        {
            get => DbTasks?.Cast<ITaskModel>()?.ToList();
            set => DbTasks = value?.Cast<TaskModel>()?.ToList();
        }

        public CategoryModel()
        {
            DbTasks = new List<TaskModel>();
        }

        public bool CanDelete()
        {
            return Tasks.Count == 0;
        }
    }
}
