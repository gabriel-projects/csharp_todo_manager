using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Interfaces.Models
{
    public interface IBaseModel
    {
        Guid Uid { get; set; }

        Guid UpdatedBy { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }

    public interface IBaseModel<TParent> : IBaseModel where TParent : IBaseModel
    {
        Guid ParentUid { get; set; }

        TParent Parent { get; set; }
    }
}
