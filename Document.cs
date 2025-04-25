
using OOP_Lab2.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOP_Lab2.FileSafe;
using System.Text.Json.Serialization;

namespace OOP_Lab2
{
    public class Document
    {
        public string name {get;set;}
        [JsonIgnore]
        private List<User> ListOfAllUsers { get; set; }
        [JsonIgnore]
        private User Admin { get; set; }
        public Dictionary<string, string> Roles { get; set; }
        public string UserName { get; set; }
        
        public int type { get; set; }
        private CloudStorage Cstorage;
        //private LocalStorage Lstorage;
        public string FileId = "0";
        public string SysFileId = "0";
        WorkWithCloud w_c;
        WorkWithLocal w_l;
        private string text;
        CloudStrategy CloudStr;
        public List<string> history; 
        //public int FontSize { get; set; }
        // public string Theme { get; set; }
        public Document(User admin,string name,int type)
        {
           if(admin!=null) this.Admin = admin;
            ListOfAllUsers = new List<User>();
            this.name = name;
            w_c = new WorkWithCloud();
            w_l = new WorkWithLocal();
            Roles = new Dictionary<string, string>();
            this.type = type;
            if (admin != null) UserName = admin.Name;
            history = new List<string>();
          
        }
        public void SetText(string text)
        {
            this.text = text;
        }
        public void ExecuteCloud(User usr)
        {
            CloudStr = new CloudStrategy();
            Cstorage = new CloudStorage();
            CloudStr.Execute(Cstorage,usr,this, "1Iiy2UToZeMaFiviRQRwIf8aZJoxncAws");
        }
        public string CheckRole(string username)
        {
            if (!Roles.ContainsKey(username))
            {
                Set(username, "reader");
                return "reader";
            }
            return Roles[username];
        }
        public void Set(string username,string role)
        {
            Roles[username] = role;
        }
        public string GetText()
        {
            return text;
        }
        public User GetAdmin()
        {
            return Admin;
        }
        
        public void Connection(List<User> users)
        {
            Roles[UserName] = "admin";
            foreach (User us in users)
            {
                if (us.Name == UserName)
                {
                    if (Admin != null)
                    {
                        Admin = us;

                    }
                }
            }
                ListOfAllUsers = users;
        }
    }
}