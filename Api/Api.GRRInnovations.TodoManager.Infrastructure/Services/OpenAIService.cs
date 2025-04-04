using Api.GRRInnovations.TodoManager.Domain.ContractResolver;
using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Enuns;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;

        public OpenAIService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IUserRepository userRepository, ITaskRepository taskRepository)
        {
            _httpClient = httpClientFactory.CreateClient("OpenAI");
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }

        public async Task<ITaskModel?> InterpretTaskAsync(string message, IUserModel user)
        {
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
                if (openAIModel == null || openAIModel.Choices == null || !openAIModel.Choices.Any())
                {
                    return null;
                }

                var messageResponse = openAIModel.Choices.FirstOrDefault().Message.Content;

                var wrapperTask = DefaultDataResolver.Deserialize<WrapperOutTask>(message);
                var taskModel = await wrapperTask.Result();

                if (taskModel != null)
                {
                    taskModel.User = user;
                    taskModel.Status = EStatusTask.Pending;
                }

                var userModel = await _userRepository.GetAsync(user.Uid);

                taskModel = await _taskRepository.CreatAsync(taskModel, userModel, null);
                return taskModel;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return null;
        }
    }
}
