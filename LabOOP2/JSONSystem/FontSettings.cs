using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOP_Lab2.JSONSystem
{
    public class FontSettings
    {
        [JsonPropertyName("size")]
        public int Size { get; set; }
    }
}
