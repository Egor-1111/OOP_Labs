using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOP_Lab2.JSONSystem
{
    public class TerminalSettings
    {
        [JsonPropertyName("$help")]
        public string Help { get; set; } = "https://aka.ms/terminal-documentation";

        [JsonPropertyName("$schema")]
        public string Schema { get; set; } = "https://aka.ms/terminal-profiles-schema";

        [JsonPropertyName("profiles")]
        public ProfilesConfig Profiles { get; set; } = new ProfilesConfig();
    }
}
