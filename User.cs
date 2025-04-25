using OOP_Lab2;
using OOP_Lab2.UserStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Lab2
{
    public class User 
    {
        public string Name { get; set; }

        private Document? TemporaryDocument { get; set; }
        public string? CurrentStrategy { get; set; }
        IUserStrategy? Strategy { get; set; }
        
        public string Logs { get; set; }
        public Settings editor;
        private UserMenu Menu;

        private List<Document> docs;
        public User(string strat,string name,List<Document> docs)
        {
            this.Name = name;
            this.Logs = "";
            this.CurrentStrategy = strat;
            SetStrategy();
            Menu = new UserMenu(this);
            if (editor == null)
            {
                editor = new Settings();
                editor.FontSize = 14;
                editor.Theme = "default";
            }

            this.docs = docs;
        }
        public void SetStrategy()
        {
            if(CurrentStrategy == "admin")
            {
                Strategy = new AdminStrategy();
            }
            if (CurrentStrategy == "reader")
            {
                Strategy = new ReaderStrategy();
            }
            if (CurrentStrategy == "writer")
            {
                Strategy = new EditorStrategy();
            }
            
        }
        public void Enter()
        {
            Menu.Start();
        }
        public void ExecuteStrategy(Document doc,List<Document> docss)
        {
            
            Strategy?.Execute(this,doc,this.editor,docss);
        }
        public void SetDocument(Document doc)
        {
            TemporaryDocument = doc;
        }
        public int CheckLogs()
        {
            string[] tmp = Logs.Split("\n");
            
            int res = 0;
            foreach(string t in tmp)
            {
                if (t != "")
                {
                    res++;
                    Console.WriteLine(t);
                }
            }
            return res;
        }
    }
}