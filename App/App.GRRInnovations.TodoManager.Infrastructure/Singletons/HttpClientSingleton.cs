using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.TodoManager.Infrastructure.Singletons
{
    public abstract class HttpClientSingleton
    {
        public static HttpClient Instance { get; } = new HttpClient();

        // Construtor privado para prevenir instanciação
        private HttpClientSingleton()
        {
        }

        static HttpClientSingleton()
        {
        }
    }
}
