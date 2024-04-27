using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces.Models;

namespace App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces.Services
{
    public interface ISigninService
    {
        Task<ITokenModel> SigninAsync();
    }
}
