using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Enums;

namespace App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces
{
    public interface IServiceResult<T>
    {
        /// <summary>
        /// Padrão = Resultado.Erro
        /// </summary>
        public EResult ResultType { set; get; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
}