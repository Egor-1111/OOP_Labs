using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOP_Lab2;

namespace OOP_Lab2.UserStrategy
{
    public interface IUserStrategy
    {
        public void Execute(User user,Document doc,Settings set,List<Document> docs);
        public void SaveLocal(string text);
        public void SaveCloud(string text);

        public string Read();
    }
}