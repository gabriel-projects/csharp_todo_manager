namespace Api.GRRInnovations.TodoManager.Interfaces.Authentication
{
    public interface IJwtResultModel
    {
        public string AccessToken { get; set; }
        public double Expire { get; set; }
        public string Type { get; set; }
    }
}
