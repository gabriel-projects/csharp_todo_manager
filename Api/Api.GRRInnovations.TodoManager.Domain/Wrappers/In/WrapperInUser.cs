using Api.GRRInnovations.TodoManager.Domain.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.In
{
    public class WrapperInUser<TUser, TDetail> : WrapperBase<TUser, WrapperInUser<TUser, TDetail>>
        where TUser : IUserModel
        where TDetail : IUserDetailModel
    {
        public WrapperInUser() : base() { }

        public WrapperInUser(TUser data) : base(data) { }

        [JsonPropertyName("login")]
        public string Login
        {
            get => Data.Login;
            set => Data.Login = value;
        }

        [JsonPropertyName("password")]
        public string Password
        {
            get => Data.Password;
            set => Data.Password = value;
        }
    }
}
