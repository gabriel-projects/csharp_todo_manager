using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Domain.Wrappers.In
{
    public class WrapperInInterpretTask
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (string.IsNullOrEmpty(Message))
                return (false, "The message is required.");

            return (true, string.Empty);
        }
    }
}
