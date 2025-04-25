using OOP_Lab2;
using OOP_Lab2.ChangeFormatAdapters;
using OOP_Lab2.FileSafe;
using OOP_Lab2.Menu_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Lab2.UserStrategy
{
    public class ReaderStrategy : IUserStrategy
    {
        private EventManager Manager;
        private User CurrentUser;
        Document CurrDoc;
        DocumentMenu menu;
        Settings settings;
        List<Document> docs;
        ManageMemFile<Document> manager = new ManageMemFile<Document>();
        WorkWithCloud w_c = new WorkWithCloud();
        WorkWithLocal w_l = new WorkWithLocal();
        public  void Execute(User user, Document doc,Settings set, List<Document> d)
        {
            Manager = new EventManager();
            CurrDoc = doc;
            CurrentUser = user;
            menu = new DocumentMenu("reader", this, CurrDoc, set);
            settings = set;
            docs = d;
           
            menu.ReaderMenu();
        }
        public string Read()
        {
            return System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + CurrDoc.name);
        }
        public void SaveLocal(string text)
        {
            w_l.Create(CurrDoc.name, text);
        }
        public void SaveCloud(string text)
        {
            //w_l.Create(CurrDoc.name, text);
            CurrDoc.SetText(text);
            string outp = w_c.UploadFile(CurrDoc.name, CurrDoc.type, "1Iiy2UToZeMaFiviRQRwIf8aZJoxncAws", "");
            if (outp == "-1") w_c.UpdateFile(CurrDoc.name, CurrDoc.type, "1Iiy2UToZeMaFiviRQRwIf8aZJoxncAws", "", CurrDoc.FileId);
            else CurrDoc.FileId = outp;
            //w_l.Delete(CurrDoc.name);
        }
    }
}