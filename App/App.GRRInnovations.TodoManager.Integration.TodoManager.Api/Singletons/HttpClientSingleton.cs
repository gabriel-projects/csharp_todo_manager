namespace App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Singletons
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
