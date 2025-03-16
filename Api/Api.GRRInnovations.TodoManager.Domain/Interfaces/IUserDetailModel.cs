namespace Api.GRRInnovations.TodoManager.Domain.Models
{
    public interface IUserDetailModel : IBaseModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IUserModel User { get; set; }
    }
}
