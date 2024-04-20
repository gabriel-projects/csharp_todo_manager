using App.GRRInnovations.TodoManager.Interfaces.Enuns;

namespace App.GRRInnovations.TodoManager.Interfaces.Enuns
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
