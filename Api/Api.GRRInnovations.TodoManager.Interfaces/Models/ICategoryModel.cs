using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Interfaces.Models
{
    public interface ICategoryModel : IBaseModel
    {
        public string Name { get; set; }
    }
}
