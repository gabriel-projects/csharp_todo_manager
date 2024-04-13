
namespace Api.GRRInnovations.TodoManager.Interfaces.Models
{
    public interface ICategoryModel : IBaseModel
    {
        public string Name { get; set; }

        public List<ITaskModel> Tasks { get; set; }
    }
}
