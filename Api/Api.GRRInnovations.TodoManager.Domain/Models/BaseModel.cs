using Api.GRRInnovations.TodoManager.Interfaces.Models;

namespace Api.GRRInnovations.TodoManager.Domain.Models
{
    public class BaseModel : IBaseModel
    {
        public Guid Uid { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class BaseModel<TParentImplementation, TParentInterface> : BaseModel, IBaseModel<TParentInterface>
        where TParentImplementation : class, TParentInterface
        where TParentInterface : IBaseModel
    {
        public virtual Guid ParentUid { get; set; }

        public virtual TParentInterface Parent
        {
            get => DbParent;
            set => DbParent = value as TParentImplementation;
        }

        public virtual TParentImplementation DbParent { get; set; }
    }

    public class BaseModel<TParentImplementation> : BaseModel, IBaseModel<TParentImplementation>
        where TParentImplementation : class, IBaseModel
    {
        public virtual Guid ParentUid { get; set; }

        public virtual TParentImplementation Parent { get; set; }
    }
}
