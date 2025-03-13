using Api.GRRInnovations.TodoManager.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string OpenAiUrl = "https://api.openai.com/v1/chat/completions";

        public OpenAIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenAI:ClientSecret"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string?> InterpretTaskAsync(string message, string user)
        {
            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                new { role = "system", content = "Você é um assistente que ajuda a organizar tarefas." },
                new { role = "user", content = message }
            },
                max_tokens = 100
            };

            string jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(OpenAiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}
