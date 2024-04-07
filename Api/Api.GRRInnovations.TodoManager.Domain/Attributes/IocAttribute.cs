namespace Api.GRRInnovations.TodoManager.Domain.Attributes
{
    public class IocAttribute : Attribute
    {
        public Type Interface { get; set; }
    }
}
