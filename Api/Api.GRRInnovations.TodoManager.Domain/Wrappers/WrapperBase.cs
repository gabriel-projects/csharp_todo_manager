using System.Text.Json.Serialization;

namespace Api.GRRInnovations.TodoManager.Domain.Wrappers
{
    public class WrapperBase<T>
    {
        [JsonIgnore]
        public virtual T Data { get; set; }

        public WrapperBase()
        {
            Data = Activator.CreateInstance<T>();
        }

        public WrapperBase(T data)
        {
            Data = data;
        }

        public virtual Task Populate(T data)
        {
            Data = data;
            return Task.CompletedTask;
        }

        public virtual Task<T> Result()
        {
            return Task.FromResult(Data);
        }
    }

    public class WrapperBase<T, WT> : WrapperBase<T> where WT : WrapperBase<T, WT>
    {
        public WrapperBase() { }

        public WrapperBase(T data) : base(data) { }


        public static async Task<WT> From(T evt)
        {
            var result = Activator.CreateInstance<WT>();
            await result.Populate(evt).ConfigureAwait(false);

            return result;
        }

        public static async Task<List<WT>> From(IEnumerable<T> evts)
        {
            var result = new List<WT>();

            foreach (var evt in evts)
            {
                var data = await From(evt).ConfigureAwait(false);
                result.Add(data);
            }

            return result;
        }
    }
}
