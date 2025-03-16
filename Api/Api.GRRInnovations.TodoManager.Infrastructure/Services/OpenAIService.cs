using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Interfaces.Enuns;
using Api.GRRInnovations.TodoManager.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
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

        public OpenAIService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("OpenAI");
        }

        public async Task<string?> InterpretTaskAsync(string message, string user)
        {
            //todo: rewrite in us
            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new 
                    { 
                        role = "system", 
                        content = @"Você é um assistente especializado em organizar tarefas. 
                        Extraia a informação relevante do texto e devolva no seguinte formato JSON:
                        {
                            ""Title"": ""string"",
                            ""Description"": ""string"",
                            ""Recurrent"": true/false,
                            ""Start"": ""YYYY-MM-DDTHH:mm:ss"",
                            ""End"": ""YYYY-MM-DDTHH:mm:ss"",
                            ""Priority"": ""Low"", ""Medium"", ""High""
                        }
                        Se a tarefa for recorrente, inclua a recorrência corretamente. 
                        Se o horário não for especificado, defina como 09:00 do dia especificado.
                        Caso não haja um prazo final explícito, o campo 'End' pode ser igual ao 'Start'.
                        Retorne apenas o JSON da tarefa." 
                    },
                    new 
                    { 
                        role = "user", 
                        content = message 
                    }
                },
                max_tokens = 100
            };

            string jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("chat/completions", content);

            if (!response.IsSuccessStatusCode) return null;

            string responseString = await response.Content.ReadAsStringAsync();

            try
            {
                var openAIModel = JsonSerializer.Deserialize<OpenAIResponse>(responseString);

                //todo: adjust json
                var taskData = JsonSerializer.Deserialize<TaskModel>(openAIModel.Choices.FirstOrDefault().Message.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (taskData != null)
                {
                    //taskData.UserUid = Guid.Parse(userUid);
                    taskData.Status = EStatusTask.Pending;
                }

                //todo: adjust return
                return null;
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}
