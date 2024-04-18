using App.GRRInnovations.TodoManager.Interfaces.Enuns;

namespace App.GRRInnovations.TodoManager.Interfaces.ApiCommunic
{
    public interface IResultCommunic<T>
    {
        /// <summary>
        /// Padrão = Resultado.Erro
        /// </summary>
        public EResult ResultType { set; get; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
}