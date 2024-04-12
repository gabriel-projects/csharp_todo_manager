using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Interfaces.Models
{
    public interface ITaskCategoryModel : IBaseModel
    {
        public Guid TaskUid { get; set; }
        public ITaskModel? Task { get; set; }
        public Guid CategoryUid { get; set; }
        public ICategoryModel? Category { get; set; }
    }
}
