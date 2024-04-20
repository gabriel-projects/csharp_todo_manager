using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.TodoManager.Interfaces.ApiTodoManagerCommunic
{
    public interface IBaseCommunic : IDisposable
    {
        /// <summary>
        /// Método put - atualiza os dados ao server
        /// </summary>
        Task<IResultCommunic<TResult>> PutAsync<TData, TResult>(TData data);

        /// <summary>
        /// Método Post - Grava dados no server
        /// </summary>
        Task<IResultCommunic<TResult>> PostAsync<TData, TResult>(TData data);

        /// <summary>
        /// Método Get - Recebe os dados do server
        /// </summary>
        Task<IResultCommunic<TResult>> GetAsync<TResult>(string actionApi);

        /// <summary>
        /// Método Delete - Exclui registros no Server
        /// </summary>
        Task<IResultCommunic<TResult>> DeleteAsync<TResult>();
    }
}
