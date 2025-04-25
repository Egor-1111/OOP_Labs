using Google.Apis.Drive.v3.Data;
using OOP_Lab2;
using Newtonsoft.Json;
using OOP_Lab2.FileSafe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOP_Lab2
{
    class UserMenu
    {
        Checker checker = new Checker();
        List<Document> docs;
        ManageMemFile<Document> manager = new ManageMemFile<Document>();
        ManageMemFile<User> managerUs = new ManageMemFile<User>();
        User currUser;
        Settings set = new Settings();
        WorkWithLocal w_l = new WorkWithLocal();
        public UserMenu(User user)
        {
            if (docs == null) docs = manager.GetCollection("mem1.json");
            if (docs == null) docs = new List<Document>();
            currUser = user;
           
        }
        public void Start()
        {
            List<User> UserColl = managerUs.GetCollection("mem.json");
            foreach (Document doc in docs)
            {
                doc.Connection(UserColl);
            }
            while (true)
            {
                UserColl = managerUs.GetCollection("mem.json");
                string output = "0 - Создать файл, 1 - Выбрать файл из уже созданных, 2 - Просмотреть логи, -1 - Выход ";
                Console.WriteLine(output);
                int choise = checker.CheckWithBorders(Console.ReadLine(), -1, 2, output, 2);
                if (choise == 0)
                {
                    output = "Введите имя файла - без расширения";
                    Console.WriteLine(output);
                    string name = Console.ReadLine();
                    while (checker.CheckStringInCollectionDoc(docs, name))
                    {
                        Clear(2);
                        Console.WriteLine("Ошибка такой файл есть! " + output);
                        name = Console.ReadLine();
                    }
                    //Worker.Add(Users, name);

                    //Clear(4);
                    output = "0 - txt, 1 - json, 2 - md, 3 - xml,4 - rtf";
                    Console.WriteLine(output);
                    choise = checker.CheckWithBorders(Console.ReadLine(), 0,4 , output, 2);
                    Document doc =new Document(null,null,0);
                    if (choise == 0)
                    {
                        doc = new Document(currUser, name + ".txt", 0);
                        docs.Add(doc);
                        currUser.SetDocument(doc);
                    }
                    if (choise == 1)
                    {
                        doc = new Document(currUser, name + ".json", 1);
                        docs.Add(doc);
                        currUser.SetDocument(doc);
                       //string json = JsonConvert.SerializeObject("s", Formatting.Indented);
                        //doc.SetText(json);
                    }
                    if (choise == 2)
                    {
                        doc = new Document(currUser, name + ".md", 2);
                        docs.Add(doc);
                        currUser.SetDocument(doc);
                    }
                    if (choise == 3)
                    {
                        doc = new Document(currUser, name + ".xml", 3);
                        docs.Add(doc);
                        currUser.SetDocument(doc);
                    }
                    Console.Clear();
                    currUser.CurrentStrategy = "admin";
                    currUser.SetStrategy();
                    manager.WriteCollection(docs, "mem1.json");
                    //manager.WriteCollection(currUser, "mem.json");
                    w_l.Create(doc.name,"");
                    currUser.ExecuteStrategy(doc,docs);
                    manager.WriteCollection(docs, "mem1.json");

                }
                else if (choise==1&& docs.Count>0)
                {
                    output = "Список всех файлов :";
                    Console.WriteLine(output);
                    foreach(Document dc in docs)
                    {
                        Console.Write(dc.name+" ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Выберете файл");
                    string DocName = Console.ReadLine();
                    while (!checker.CheckStringInCollectionDoc(docs, DocName))
                    {
                        Clear(2);
                        Console.WriteLine("Ошибка такого файла нет " + output);
                        DocName = Console.ReadLine();
                    }
                    Console.Clear();
                    Document doc = new Document(null,null,1);
                    foreach (Document dc in docs)
                    {
                        if (DocName == dc.name) doc = dc;
                    }
                    //Console.WriteLine(doc.name);
                    currUser.CurrentStrategy = doc.CheckRole(currUser.Name);
                    currUser.SetStrategy();
                    currUser.ExecuteStrategy(doc,docs);
                    manager.WriteCollection(docs, "mem1.json");

                }
                else if(choise==2)
                {
                    int cnt = currUser.CheckLogs();
                    while(true)
                    {
                        output = "-1 - Выход ";
                        Console.WriteLine(output);
                        choise = checker.CheckWithBorders(Console.ReadLine(), -1, -1, output, 2);
                        if (choise == -1) break;
                    }
                    Console.Clear();
                }
                else if (choise == -1)
                {
                    Console.Clear();
                    manager.WriteCollection(docs, "mem1.json");
                    foreach(Document doc in docs)
                    {
                        System.IO.File.WriteAllText(doc.name,doc.GetText());
                    }
                    return;

                }
                if (docs.Count == 0) Console.Clear();
                Console.WriteLine(currUser.Name);
                set.SetConsoleFont(14);
            }
        }
        public void ClearAll()
        {
            int tp = Console.GetCursorPosition().Top;
            for (int i = tp; i > -1; --i)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("\r");
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, 0);
        }
        public void Clear(int count)
        {
            int tp = Console.GetCursorPosition().Top;
            for (int i = tp; i < tp + count; ++i)
            {
                Console.SetCursorPosition(0, i - count);
                Console.Write("\r");
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, tp - count);
        }
    }
}
