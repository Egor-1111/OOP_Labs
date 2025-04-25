using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOP_Lab2.JSONSystem
{
    public class Profile
    {
        [JsonPropertyName("guid")]
        public string Guid { get; set; }

        [JsonPropertyName("hidden")]
        public bool Hidden { get; set; } = false;

        [JsonPropertyName("name")]
        public string Name { get; set; } = "Command Prompt";
    }
}
