using Api.GRRInnovations.TodoManager.Interfaces.Enuns;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.Out
{
    public class WrapperOutTask : WrapperBase<ITaskModel>
    {
        public WrapperOutTask() : base(null) { }

        public WrapperOutTask(ITaskModel data) : base(data)
        {

        }

        [JsonPropertyName("uid")]
        public Guid Uid
        {
            get => Data.Uid;
            set => Data.Uid = value;
        }

        [JsonPropertyName("title")]
        public string Title
        {
            get => Data.Title;
            set => Data.Title = value;
        }

        [JsonPropertyName("description")]
        public string Description
        {
            get => Data.Description;
            set => Data.Description = value;
        }

        [JsonPropertyName("recurrent")]
        public bool Recurrent
        {
            get => Data.Recurrent;
            set => Data.Recurrent = value;
        }

        [JsonPropertyName("start")]
        public DateTime Start
        {
            get => Data.Start;
            set => Data.Start = value;
        }

        [JsonPropertyName("end")]
        public DateTime End
        {
            get => Data.End;
            set => Data.End = value;
        }


        [JsonProperty("status")]
        public string Status
        {
            get => Data.Status.ToString();
            set
            {
                if (Enum.TryParse(value, out EStatusTask status))
                {
                    Data.Status = status;
                }
            }
        }

        [JsonProperty("priority")]
        public string Priority
        {
            get => Data.Priority.ToString();
            set
            {
                if (Enum.TryParse(value, out EPriorityTask priority))
                {
                    Data.Priority = priority;
                }
            }
        }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt
        {
            get => Data.CreatedAt;
            set => Data.CreatedAt = value;
        }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt
        {
            get => Data.UpdatedAt;
            set => Data.UpdatedAt = value;
        }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public WrapperOutCategory Categories { get; set; }

        //public override async Task Populate(ITaskModel data)
        //{
        //    await base.Populate(data).ConfigureAwait(false);

        //    if (data.References != null)
        //    {
        //        References = await WrapperOutContentGroupReference.From(data.References).ConfigureAwait(false);
        //    }

        //    if (data.ca != null)
        //    {
        //        Properties = await WrapperOutDynamicPropertyValue.From(data.Properties).ConfigureAwait(false);
        //    }
        //}

        //public override async Task<IContentGroup> Result()
        //{
        //    var result = await base.Result().ConfigureAwait(false);

        //    if (References != null)
        //    {
        //        var data = new List<IContentGroupReference>();

        //        foreach (var wp in References)
        //        {
        //            var product = await wp.Result().ConfigureAwait(false);
        //            data.Add(product);
        //        }

        //        result.References = data;
        //    }

        //    if (Properties != null)
        //    {
        //        var data = new List<IContentGroupPropertyValue>();

        //        foreach (var wp in Properties)
        //        {
        //            var product = await wp.Result().ConfigureAwait(false);
        //            data.Add((IContentGroupPropertyValue)product);
        //        }

        //        result.Properties = data;
        //    }

        //    if (Entities != null)
        //    {
        //        var data = new List<IContentGroupEntity>();

        //        foreach (var wp in Entities)
        //        {
        //            var wdata = await wp.Result().ConfigureAwait(false);
        //            data.Add((IContentGroupEntity)wdata);
        //        }

        //        result.Entities = data;
        //    }


        //    return result;
        //}


        public new static async Task<WrapperOutTask> From(ITaskModel model)
        {
            var wrapper = new WrapperOutTask();
            await wrapper.Populate(model).ConfigureAwait(false);

            return wrapper;
        }

        public new static async Task<List<WrapperOutTask>> From(List<ITaskModel> models)
        {
            var result = new List<WrapperOutTask>();

            foreach (var model in models)
            {
                var wrapper = await From(model).ConfigureAwait(false);
                result.Add(wrapper);
            }

            return result;
        }
    }
}
