namespace Api.GRRInnovations.TodoManager.Infrastructure.Interfaces
{
    public interface IJwtResultModel
    {
        public string AccessToken { get; set; }
        public double Expire { get; set; }
        public string Type { get; set; }
    }
}
