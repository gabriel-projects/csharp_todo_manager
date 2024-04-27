namespace App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces
{
    public interface IServiceBase : IDisposable
    {
        /// <summary>
        /// Método put - atualiza os dados ao server
        /// </summary>
        Task<IServiceResult<TResult>> PutAsync<TData, TResult>(TData data);

        /// <summary>
        /// Método Post - Grava dados no server
        /// </summary>
        Task<IServiceResult<TResult>> PostAsync<TData, TResult>(TData data);

        /// <summary>
        /// Método Get - Recebe os dados do server
        /// </summary>
        Task<IServiceResult<TResult>> GetAsync<TResult>(string actionApi);

        /// <summary>
        /// Método Delete - Exclui registros no Server
        /// </summary>
        Task<IServiceResult<TResult>> DeleteAsync<TResult>();
    }
}
