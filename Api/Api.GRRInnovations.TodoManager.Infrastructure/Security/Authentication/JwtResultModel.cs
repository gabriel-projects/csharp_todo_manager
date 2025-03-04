using Api.GRRInnovations.TodoManager.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication
{
    public class JwtResultModel : IJwtResultModel
    {
        public string AccessToken { get; set; }
        public double Expire { get; set; }
        public string Type { get; set; }
    }
}
