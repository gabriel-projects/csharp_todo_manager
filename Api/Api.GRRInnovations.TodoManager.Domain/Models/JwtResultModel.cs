namespace Api.GRRInnovations.TodoManager.Domain.Models
{
    public class JwtResultModel
    {
        public string AccessToken { get; set; }

        public string Type { get; set; }

        public double Expire { get; set; }
    }
}
