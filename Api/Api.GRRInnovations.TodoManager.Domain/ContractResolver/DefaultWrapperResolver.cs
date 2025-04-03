using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.GRRInnovations.TodoManager.Domain.ContractResolver
{
    public class DefaultDataResolver : DefaultContractResolver
    {
        public override JsonContract ResolveContract(Type type)
        {
            var result = base.CreateContract(type);

            if (type == typeof(WrapperOutTask))
            {
                result.DefaultCreator = () => new WrapperOutTask(new TaskModel());
            }

            return result;
        }

        public static async Task<TResult> Deserialize<TResult>(Stream stream) where TResult : new()
        {
            var result = default(TResult);

            using (var sr = new StreamReader(stream))
            {
                var json = await sr.ReadToEndAsync().ConfigureAwait(false);

                result = Deserialize<TResult>(json);
            }

            return result;
        }

        public static TResult Deserialize<TResult>(string json) where TResult : new()
        {
            var result = JsonConvert.DeserializeObject<TResult>(json, new JsonSerializerSettings
            {
                ContractResolver = new DefaultDataResolver(),
                TypeNameHandling = TypeNameHandling.Auto
            });

            return result;
        }
    }
}
