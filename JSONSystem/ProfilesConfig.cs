using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOP_Lab2.JSONSystem
{
    public class ProfilesConfig
    {
        [JsonPropertyName("defaults")]
        public ProfileDefaults Defaults { get; set; } = new ProfileDefaults();

        [JsonPropertyName("list")]
        public Profile[] List { get; set; } = Array.Empty<Profile>();
    }
}
