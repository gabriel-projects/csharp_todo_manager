using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Enums;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces;
namespace App.GRRInnovations.TodoManager.Integration.TodoManager.Api.ServicesConfiguration
{
    public class ServiceResult<T> : IServiceResult<T>
    {
        public EResult ResultType { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
}
