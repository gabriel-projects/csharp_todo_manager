using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Interfaces.Models
{
    public interface IUserDetailModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string FirtsName { get; set; }

        public string LastName { get; set; }
    }
}
