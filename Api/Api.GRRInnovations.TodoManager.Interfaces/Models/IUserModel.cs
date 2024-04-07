namespace Api.GRRInnovations.TodoManager.Interfaces.Models
{
    public interface IUserModel : IBaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public bool BlockedAccess { get; set; }

        public bool PendingConfirm { get; set; }
    }
}
