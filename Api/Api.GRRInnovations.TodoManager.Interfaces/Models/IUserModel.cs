namespace Api.GRRInnovations.TodoManager.Interfaces.Models
{
    public interface IUserModel : IBaseModel
    {
        string Login { get; set; }

        string Password { get; set; }

        bool BlockedAccess { get; set; }

        bool PendingConfirm { get; set; }
    }
}
