﻿using Api.GRRInnovations.TodoManager.Interfaces.Enuns;
using Api.GRRInnovations.TodoManager.Interfaces.Models;

namespace Api.GRRInnovations.TodoManager.Domain.Entities
{
    public class TaskModel : BaseModel, ITaskModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Recurrent { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public EStatusTask Status { get; set; } = EStatusTask.Pending;
        public EPriorityTask Priority { get; set; } = EPriorityTask.None;
        public UserModel User { get; set; }
        public Guid UserUid { get; set; }

        public List<TaskCategoryModel>? DbTasksCategories { get; set; }

        public List<ITaskCategoryModel>? TasksCategories
        {
            get => DbTasksCategories?.Cast<ITaskCategoryModel>()?.ToList();
            set => DbTasksCategories = value?.Cast<TaskCategoryModel>()?.ToList();
        }

        public TaskModel()
        {
            TasksCategories = new List<ITaskCategoryModel>();
        }
    }
}
