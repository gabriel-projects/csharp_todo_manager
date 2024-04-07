namespace Api.GRRInnovations.TodoManager.Domain.Attributes
{
    public class SingletonAttribute : Attribute
    {
        public Type Interface { get; set; }
    }
}
