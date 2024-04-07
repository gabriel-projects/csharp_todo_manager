using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.In
{
    public class WrapperInLogin
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Login) && string.IsNullOrEmpty(Password));
        }
    }
}
