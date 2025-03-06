using Api.GRRInnovations.TodoManager.Interfaces.Services;

namespace Api.GRRInnovations.TodoManager.Application.Services
{
    public class CryptoService : ICryptoService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 10);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return hash.StartsWith("$2") && BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
