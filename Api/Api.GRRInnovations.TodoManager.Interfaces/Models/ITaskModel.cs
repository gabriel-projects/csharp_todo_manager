﻿using Api.GRRInnovations.TodoManager.Interfaces.Enuns;

namespace Api.GRRInnovations.TodoManager.Interfaces.Models
{
    public interface ITaskModel : IBaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Recurrent { get; set; }

        public EStatusTask Status { get; set; }

        public EPriorityTask Priority { get; set; }

        public ICategoryModel? Category { get; set; }

        public IUserModel User { get; set; }

        public ITaskRecurrence TaskRecurrence { get; set; }
    }
}
