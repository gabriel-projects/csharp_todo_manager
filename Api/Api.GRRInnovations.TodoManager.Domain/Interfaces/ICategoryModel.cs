
namespace Api.GRRInnovations.TodoManager.Domain.Models
{
    public interface ICategoryModel : IBaseModel
    {
        public string Name { get; set; }

        public List<ITaskModel> Tasks { get; set; }
    }
}
