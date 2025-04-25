using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace OOP_Lab2
{
    class ManageUserFile
    {
        public List<User> GetUsers()
        {
            string json = File.ReadAllText("mem.json");
            List<User> hr = JsonConvert.DeserializeObject<List<User>>(json);
            return hr;
        }
        public void WriteUsers(List<User> us)
        {
            string json = JsonConvert.SerializeObject(us, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("mem.json", json);
        }
           
    }
}
