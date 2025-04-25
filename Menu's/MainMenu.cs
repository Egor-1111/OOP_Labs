using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using OOP_Lab2;

namespace OOP_Lab2
{
    public class MainMenu
    {
        string input = "";
        int choise = 0;
        string output;
        Checker checker = new Checker();
        List<User> Users;
        ManageMemFile<User> manager = new ManageMemFile<User>();
        ManageMemFile<Document> docs = new ManageMemFile<Document>();
        WorkWithUsers Worker = new WorkWithUsers();
        Settings set = new Settings();
        public MainMenu()
        {
            Users = manager.GetCollection("mem.json");
            if (Users == null) Users = new List<User>();
        }
        public void Start()
        {
            while (true) {
                set.SetConsoleFont(14);
                set.SetConsoleTheme("Campbell");
                Users = manager.GetCollection("mem.json");
                if (Users == null) Users = new List<User>();
                output = "0 - Создать пользователя, 1 - Выбрать пользователя для входа, -1 - Выход";
                Console.WriteLine(output);
                input = Console.ReadLine();
                choise = checker.CheckWithBorders(input, -1, 1, output, 2);
                if (choise == 0)
                {
                    output = "Введите имя пользователя";
                    Console.WriteLine(output);
                    string name = Console.ReadLine();
                    while (checker.CheckStringInCollection(Users, name))
                    {
                        Clear(2);
                        Console.WriteLine("Ошибка такой пользователь есть! " + output);
                        name = Console.ReadLine();
                    }
                    User tmp = new User("admin",name,docs.GetCollection("mem1.json"));
                    Users.Add(tmp);
                    Clear(4);
                    Console.WriteLine(name);
                    manager.WriteCollection(Users,"mem.json");
                    tmp.Enter();
                    
                }
                else if( choise == 1 && Users.Count>0)
                {
                    Console.WriteLine("Список всех пользователей");
                    Worker.Print(Users);
                    output = "Введите имя пользователя";
                    Console.WriteLine(output);
                    string UserName = Console.ReadLine();
                    while(!checker.CheckStringInCollection(Users,UserName))
                    {
                        Clear(2);
                        Console.WriteLine("Ошибка такого пользователя нет "+output);
                        UserName = Console.ReadLine();
                    }
                    Clear(6);
                    Console.WriteLine(UserName);

                    foreach(User a in Users)
                    {
                        if (a.Name == UserName) a.Enter();
                    }
                    //Console.WriteLine(UserName);
                }
                else if (choise == -1)
                {
                    manager.WriteCollection(Users,"mem.json");
                    return;
                }
                Console.Clear();
            }
        }
        public void Clear(int count)
        {
            int tp = Console.GetCursorPosition().Top;
            for (int i = tp; i < tp + count ; ++i)
            {
                Console.SetCursorPosition(0, i - count);
                Console.Write("\r");
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, tp-count);
        }
        
    }
}