using App.GRRInnovations.TodoManager.Infrastructure.Singletons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.TodoManager.Infrastructure.Helpers
{
    public class ApiServiceHelper<T>
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public ApiServiceHelper(string baseUrl)
        {
            _baseUrl = baseUrl;
            _client = HttpClientSingleton.Instance;
        }

        public async Task<T> GetAsync(string endpoint)
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}/{endpoint}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(json) == false)
                    {
                        return JsonConvert.DeserializeObject<T>(json);
                    }

                }
            }
            catch (Exception ex)
            {
                // Tratamento de erro adequado
                Console.WriteLine($"Erro ao buscar dados: {ex.Message}");
            }

            return default;
        }

        public async Task<bool> PostAsync(string endpoint, T data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_baseUrl}/{endpoint}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Tratamento de erro adequado
                Console.WriteLine($"Erro ao enviar dados: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> PutAsync(string endpoint, T data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync($"{_baseUrl}/{endpoint}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Tratamento de erro adequado
                Console.WriteLine($"Erro ao atualizar dados: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _client.DeleteAsync($"{_baseUrl}/{endpoint}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Tratamento de erro adequado
                Console.WriteLine($"Erro ao deletar dados: {ex.Message}");
                return false;
            }
        }
    }
}
