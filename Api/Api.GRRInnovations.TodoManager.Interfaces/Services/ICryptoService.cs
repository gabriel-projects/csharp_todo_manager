using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Interfaces.Services
{
    public interface ICryptoService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}
