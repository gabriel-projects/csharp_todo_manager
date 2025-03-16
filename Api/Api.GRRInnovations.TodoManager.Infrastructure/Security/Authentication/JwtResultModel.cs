using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication
{
    public class JwtResultModel : IJwtResultModel
    {
        public string AccessToken { get; set; }
        public double Expire { get; set; }
        public string Type { get; set; }
    }
}
