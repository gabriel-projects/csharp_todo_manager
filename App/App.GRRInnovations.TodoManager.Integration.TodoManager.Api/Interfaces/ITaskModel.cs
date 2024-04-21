using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Enums;

namespace App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces
{
    public interface ITaskModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Recurrent { get; set; }

        public EStatusTask Status { get; set; }

        public EPriorityTask Priority { get; set; }
    }
}
