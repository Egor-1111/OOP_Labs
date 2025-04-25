using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace OOP_Lab2
{
    class ManageMemFile<T>
    {
        public List<T> GetCollection(string name)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string json = File.ReadAllText(name);
            List<T> hr = JsonConvert.DeserializeObject<List<T>>(json,settings);
            return hr;
        }
        public void WriteCollection(List<T> us,string name)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string json = JsonConvert.SerializeObject(us, Newtonsoft.Json.Formatting.Indented,settings);
            File.WriteAllText(name, json);
        }
           
    }
}
