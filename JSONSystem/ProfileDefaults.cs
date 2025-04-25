using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOP_Lab2.JSONSystem
{
    public class ProfileDefaults
    {
        [JsonPropertyName("font")]
        public FontSettings Font { get; set; } = new FontSettings();

        [JsonPropertyName("colorScheme")]
        public string Schema { get; set; } = "";
    }
}
