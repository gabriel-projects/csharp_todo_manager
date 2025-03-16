using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Application.Interfaces
{
    public interface IAuthManager
    {
        IActionResult HandleLogin(string provider, string responseAction);
    }
}
