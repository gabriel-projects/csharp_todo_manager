using Api.GRRInnovations.TodoManager.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.Out
{
    public class WrapperOutUser : WrapperBase<IUserModel, WrapperOutUser>
    {
        public WrapperOutUser() : base(null) { }

        public WrapperOutUser(IUserModel data) : base(data) { }

        [JsonPropertyName("uid")]
        public Guid Uid
        {
            get => Data.Uid;
            set => Data.Uid = value;
        }

        //[JsonPropertyName("detail")]
        //public WrapperOutUserDetailLimited Detail { get; set; }

        [JsonPropertyName("pending_confirm")]
        public bool PendingConfirm
        {
            get => Data.PendingConfirm;
            set => Data.PendingConfirm = value;
        }

        [JsonPropertyName("blocked_access")]
        public bool BlockedAccess
        {
            get => Data.BlockedAccess;
            set => Data.BlockedAccess = value;
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

        public override async Task Populate(IUserModel data)
        {
            await base.Populate(data).ConfigureAwait(false);

            //if (data.Detail != null)
            //{
            //    Detail = await WrapperOutUserDetailLimited.From(data.Detail).ConfigureAwait(false);
            //}
        }

        public static async Task<WrapperOutUser> From<TUser>(TUser data) where TUser : IUserModel
        {
            var result = new WrapperOutUser(data);
            await result.Populate(data);
            return result;
        }

        public static async Task<List<WrapperOutUser>> From<TUser>(List<TUser> data) where TUser : IUserModel
        {
            var result = new List<WrapperOutUser>();

            foreach (var prop in data)
            {
                var wd = await From(prop).ConfigureAwait(false);
                result.Add(wd);
            }

            return result;
        }
    }
}
