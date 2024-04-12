namespace Api.GRRInnovations.TodoManager.Interfaces.Models
{
    public interface IBaseModel
    {
        Guid Uid { get; set; }

        Guid UpdatedBy { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}
