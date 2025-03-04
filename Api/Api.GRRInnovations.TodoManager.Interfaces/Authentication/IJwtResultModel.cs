using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Interfaces.Authentication
{
    public interface IJwtResultModel
    {
        public string AccessToken { get; set; }
        public double Expire { get; set; }
        public string Type { get; set; }
    }
}
