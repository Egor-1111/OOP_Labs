using Google.Apis.Drive.v3.Data;
using OOP_Lab2.UserStrategy;
using OOP_Lab2.Command;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OOP_Lab2.FileSafe;
using OOP_Lab2.StyleDecorator;
using OOP_Lab2;

namespace OOP_Lab2.Menu_s
{
    class DocumentMenu
    {
        string RoleToShow;
        WorkWithUsers Worker = new WorkWithUsers();
        List<User> Users;
        Checker checker = new Checker();
        IUserStrategy strat;
        ManageMemFile<User> Manager = new ManageMemFile<User>();
        ManageMemFile<Document> Manag = new ManageMemFile<Document>();
        Document Document;
        Settings set;
        CommandManager cmd;
       // WorkWithCloud cld = new WorkWithCloud();
        WorkWithLocal cldd = new WorkWithLocal();
        MarkdownDecorator md;
        XmlDecorator xd;
        RtfDecorator rd;
        JsonDecorator jd;
        TxtDecorator td;
        //List<Document> docs;
        public DocumentMenu(string role,IUserStrategy admin, Document document,Settings set)
        {
            this.RoleToShow = role;
            this.strat = admin;
            Document = document;
            this.set = set;
            cmd = new CommandManager();
            md = new MarkdownDecorator(Document);
            xd = new XmlDecorator(Document);
            rd = new RtfDecorator(Document);
            jd = new JsonDecorator(Document);
            td = new TxtDecorator(Document);

        }
        public void AdminMenu()
        {
            while (true)
            {
                set.SetConsoleFont(14);
                set.SetConsoleTheme("Campbell");
                Users = Manager.GetCollection("mem.json");
                Console.WriteLine(Document.name);
                string output = "0 - Поменять роль у пользователя, 1 - Редактировать файл, 2 - Просмотреть файл,  3 - Удалить файл, 4 - поменять расширение файла,-1 - Выход из документа";
                Console.WriteLine(output);
                int choise = checker.CheckWithBorders(Console.ReadLine(), -1, 4, output, 2);
                if (choise == 0)
                {
                    Console.WriteLine("Список всех пользователей");
                    Worker.Print(Users);
                    output = "Введите имя пользователя";
                    Console.WriteLine(output);
                    string UserName = Console.ReadLine();
                    while (!checker.CheckStringInCollection(Users, UserName))
                    {
                        Clear(2);
                        Console.WriteLine("Ошибка такого пользователя нет " + output);
                        UserName = Console.ReadLine();
                    }
                    output = "Выберете роль: 0 - reader, 1 - writer ";
                    Console.WriteLine(output);
                    choise = checker.CheckWithBorders(Console.ReadLine(), 0, 1, output, 2);
                    if (choise == 0)
                    {
                        if (strat is AdminStrategy admin)
                        {
                            admin.ChangeRole(Document, Users, "reader", UserName);
                        }
                    }
                    if (choise == 1)
                    {
                        if (strat is AdminStrategy admin)
                        {
                            admin.ChangeRole(Document, Users, "writer", UserName);
                        }
                    }
                    Manager.WriteCollection(Users, "mem.json");
                    //Clear(8);
                }
                else if (choise == 1 )
                {
                    EditorMenu();
                }
                else if(choise == 2)
                {
                    ReaderMenu();
                }
                else if (choise == 3)
                {
                    if (strat is AdminStrategy s) s.Delete();
                    Console.Clear();
                    return;
                }
                
                else if(choise==4)
                {
                    ChangeFormatMenu();
                }
               /* else if(choise==5)
                {
                    Console.Clear();
                    foreach (string s in Document.history)
                    {
                        Console.WriteLine("change");
                        Console.WriteLine(s);
                    }
                    Console.WriteLine("нажмите любую кнопку чтобы продолить");
                    string a = Console.ReadLine();
                    Console.Clear();
                }*/
                else if (choise == -1)
                {
                    Console.Clear();
                    return;
                }
                Console.Clear();
                //set.SetConsoleFont(14);
                //Console.WriteLine(Document.name);
            }
        }
        public void ChangeFormatMenu()
        {
           
            string[] s = { "txt", "json", "md", "xml","rtf" };
            List<string> outpr = new List<string>();
            int k = 0;
            foreach(string str in s )
            {
                if (Document.type != k) outpr.Add(str);
                k++;
            }
            k = 0;
            Console.WriteLine("Выберете доступный формат");
            foreach(string s1 in outpr)
            {
                Console.Write(k.ToString()+" - "+s1+" ");
                k++;
            }
            Console.Write("\r\n");
           int inpp = checker.CheckWithBorders(Console.ReadLine(),0,k-1, "Выберете доступный формат",2);
            if (strat is AdminStrategy sq)
            {
                string t =sq.Read();
                Document.SetText(t);
                sq.ChangeFormat(outpr[inpp]);
            }
            //Manag.WriteCollection(d);
            Console.Clear();
           
            
        }
        public void ClearBuff(ref int p, ref int l, ref string buff,int pos,int p1,int p2)
        {
            buff = "";
            if (p > 0) p = 0;
            if (l > 0) l = 0;
            Console.SetCursorPosition(0,pos+1);
            Clear(1);
            Console.SetCursorPosition(p1, p2);
        }
        public void Clear(int count)
        {
            int tp = Console.GetCursorPosition().Top;
            for (int i = tp; i < tp + count; ++i)
            {
                if (i - count < 0) break;
                Console.SetCursorPosition(0, i - count);
                Console.Write("\r");
                Console.Write(new string(' ', Console.WindowWidth));
            }
            if (tp - count > 0) Console.SetCursorPosition(0, tp - count);
            else Console.SetCursorPosition(0, 0);
        }
        public void ClearAll()
        {
            int tp = Console.GetCursorPosition().Top;
            for (int i = tp; i >-1; --i)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("\r");
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, 0);
        }
        public void EditorMenu()
        {
            string protected_buffer = "";
            bool flag1 = true;
            bool flag = true;
            int pos = 0;
            int pos_r = 0;
            int pos_l = 0;
            string txt="";
            string buff="";
            //Console.Clear();
            //Console.WriteLine(Document.name);
             txt = strat.Read();
            //Console.WriteLine(txt);
            int apos = Console.GetCursorPosition().Top;
            if (apos - 2 > 0) Console.SetCursorPosition(0, 0);
            cmd.Save(new Frame(txt, Console.GetCursorPosition().Left, Console.GetCursorPosition().Top, "esc - выход,ctrl+1 - вниз,ctrl+2 вверх,ctrl+3 - влево,ctrl+4 вправо,ctrl+f - поиск слова,shift+>,shift+< выделить, ctrl+5 вставить пробел,ctr+backspace - удалить символ, ctrl+x отмена, ctrl+o отмена отмены, ctrl+b - копировать,ctrl+v вставить, ctrl+t - удалить выделенную часть,ctrl+q сохранить файл и выход,ctrl+u смена темы,ctrl+h -история документа"));
            while (true)
            {
                if (flag) Console.Clear();
                if (flag) 
                    {
                        Console.WriteLine(txt);
                    }
                string output = "esc - выход,ctrl+1 - вниз,ctrl+2 вверх,ctrl+3 - влево,ctrl+4 вправо,ctrl+f - поиск слова,shift+>,shift+< выделить, ctrl+5 вставить пробел,ctr+backspace - удалить символ, ctrl+x отмена, ctrl+o отмена отмены, ctrl+b - копировать,ctrl+v вставить, ctrl+t - удалить выделенную часть,ctrl+q сохранить файл и выход,ctrl+u смена темы,ctrl+h -история документа ";
                
                if(flag)Console.WriteLine(output);
                if (flag) pos = Console.GetCursorPosition().Top;
               if(flag&&pos-3>=0) Console.SetCursorPosition(0, pos-4);
               else if(flag)
                {
                    Console.SetCursorPosition(0, pos - 4);

                }


                    var k = Console.ReadKey();
                if (k.Key == System.ConsoleKey.Q && k.Modifiers == ConsoleModifiers.Control)
                {
                    Console.Clear();
                    while (true)
                    {
                        output = " 0 - сохранить локально, 1 - сохранить в облаке";
                        Console.WriteLine(output);
                        string inp = Console.ReadLine();
                        if(inp=="0")
                        {
                            Document.SetText(txt);
                            strat.SaveLocal(txt);
                            Document.history.Add(txt);
                            return;
                        }
                        if(inp=="1")
                        {
                            Document.SetText(txt);
                            cldd.Create(Document.name, txt);
                            strat.SaveCloud(txt);
                            Document.history.Add(txt);
                            return;
                        }
                        
                    }
                }
                    if (k.Key == System.ConsoleKey.D2 && k.Modifiers == ConsoleModifiers.Control)
                {
                    ClearBuff(ref pos_l, ref pos_r, ref buff, pos, Console.GetCursorPosition().Left, Console.GetCursorPosition().Top);
                    pos_l = pos_r = 0;
                    flag = false;
                    //Clear(1);
                    if (Console.GetCursorPosition().Top > 0) Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top - 1);

                    //Console.Clear();
                }
                //up
                if (k.Key == System.ConsoleKey.D1 && k.Modifiers == ConsoleModifiers.Control)
                {
                    pos_l = pos_r = 0;
                    ClearBuff(ref pos_l, ref pos_r, ref buff, pos, Console.GetCursorPosition().Left, Console.GetCursorPosition().Top);
                    flag = false;
                    if (Console.GetCursorPosition().Top < pos-4) Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top + 1);
                    //Clear(1);
                    //Console.Clear();
                }
                if (k.Key == System.ConsoleKey.D3 && k.Modifiers == ConsoleModifiers.Control)
                {
                    //Clear(1);
                    ClearBuff(ref pos_l, ref pos_r, ref buff, pos, Console.GetCursorPosition().Left, Console.GetCursorPosition().Top);
                    if (Console.GetCursorPosition().Left > 0) Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, Console.GetCursorPosition().Top);
                    flag = false;
                    //Console.Clear();
                }
                //up
                if (k.Key == System.ConsoleKey.D4 && k.Modifiers == ConsoleModifiers.Control)
                {
                    pos_l = pos_r = 0;
                    ClearBuff(ref pos_l, ref pos_r, ref buff, pos, Console.GetCursorPosition().Left, Console.GetCursorPosition().Top);
                    if (Console.GetCursorPosition().Left < txt.Split("\n")[Console.GetCursorPosition().Top].Length) Console.SetCursorPosition(Console.GetCursorPosition().Left + 1, Console.GetCursorPosition().Top);
                    //Clear(1);
                    flag = false;
                    //Console.Clear();
                }
                if(k.Key == System.ConsoleKey.F && k.Modifiers == ConsoleModifiers.Control)
                {
                    flag = true;
                    pos_l = pos_r = 0;
                    Console.SetCursorPosition(0, pos);
                    output = "-1 - выход, введенная строка и enter - показ";
                    Console.WriteLine(output);
                    while (true)
                    {
                        
                        output = "-1 - выход, введенная строка и enter - показ";
                        
                        string st1 = Console.ReadLine();
                        if (st1 == "-1")
                        {
                            //flag = false;
                            Clear(2);
                            break;
                        }
                        else if(st1.Length==0)
                        {
                            Clear(1);
                            continue;
                        }
                        else
                        {
                            SearchString(txt,st1);
                            Console.WriteLine(output);
                        }
                    }
                    
                }
                if (k.Key == System.ConsoleKey.RightArrow && k.Modifiers == ConsoleModifiers.Shift)
                {
                    if (Console.GetCursorPosition().Left < 170&&Console.GetCursorPosition().Left < txt.Split("\n")[Console.GetCursorPosition().Top].Length && txt.Split("\n")[Console.GetCursorPosition().Top][Console.GetCursorPosition().Left]!='\r')
                    {
                        Console.SetCursorPosition(Console.GetCursorPosition().Left + 1, Console.GetCursorPosition().Top);

                    
                        SetRight(ref txt, output, pos, ref pos_r, ref pos_l, ref buff);
                        flag1 = false;
                        flag = false;
                    }
                }
                if (k.Key == System.ConsoleKey.LeftArrow && k.Modifiers == ConsoleModifiers.Shift)
                {
                    if (Console.GetCursorPosition().Left >0) Console.SetCursorPosition(Console.GetCursorPosition().Left-1, Console.GetCursorPosition().Top);
                    SetLeft(ref txt, output, pos, ref pos_r, ref pos_l, ref buff);
                    flag1 = false;
                    flag = false;
                }
                if((k.KeyChar>=97 && k.KeyChar <=122 )|| (k.KeyChar >= 65 && k.KeyChar <= 90) || (k.KeyChar>=33 && k.KeyChar<=126) )
                {
                    int pos1 = Console.GetCursorPosition().Left;
                    int pos2 = Console.GetCursorPosition().Top;
                    if (strat is AdminStrategy s) s.ModifyUp(ref txt,Console.GetCursorPosition().Top, Console.GetCursorPosition().Left, k.KeyChar.ToString());
                    if (strat is EditorStrategy sq) sq.ModifyUp(ref txt, Console.GetCursorPosition().Top, Console.GetCursorPosition().Left, k.KeyChar.ToString());
                    //Console.Write(k.KeyChar);
                    flag = false;
                    Console.Clear();
                    Console.WriteLine(txt);
                    Console.WriteLine(output);
                    //Console.Write(k.KeyChar);
                    Console.SetCursorPosition(pos1, pos2);
                    
                }
                if(k.Key == System.ConsoleKey.D5 && k.Modifiers == ConsoleModifiers.Control)
                {
                    //Console.Write('\n');
                    int pos1 = Console.GetCursorPosition().Top;
                    if (strat is AdminStrategy s) s.ModifyUp(ref txt, Console.GetCursorPosition().Top, Console.GetCursorPosition().Left, "\r\n");
                    if (strat is EditorStrategy sq) sq.ModifyUp(ref txt, Console.GetCursorPosition().Top, Console.GetCursorPosition().Left, "\r\n");
                    Console.Clear();
                    Console.WriteLine(txt);
                    Console.WriteLine(output);
                    pos++;
                    pos1++;
                    Console.SetCursorPosition(0, pos1);
                    flag = false;
                }
                if(k.Key==System.ConsoleKey.Backspace && k.Modifiers == ConsoleModifiers.Control)
                {
                    int pos1 = Console.GetCursorPosition().Top;
                    int pos2 = Console.GetCursorPosition().Left;
                    
                    if (pos2== 0)
                    {
                        if (strat is AdminStrategy s) s.RemoveString(ref txt, Console.GetCursorPosition().Top, Console.GetCursorPosition().Left);
                        if (strat is EditorStrategy sq) sq.RemoveString(ref txt, Console.GetCursorPosition().Top, Console.GetCursorPosition().Left);
                        if (pos1>0)pos1--;
                    }
                    else
                    {
                        if (strat is AdminStrategy s) s.ModifyUp(ref txt, Console.GetCursorPosition().Top, Console.GetCursorPosition().Left, "");
                        if(strat is EditorStrategy sq) sq.ModifyUp(ref txt, Console.GetCursorPosition().Top, Console.GetCursorPosition().Left, "");
                        if (pos2>0)pos2--;
                    }


                    
                    //Console.Write(k.KeyChar);
                    flag = false;
                    Console.Clear();
                    Console.WriteLine(txt);
                    Console.WriteLine(output);
                    //pos++;
                    
                    Console.SetCursorPosition(pos2, pos1);
                    //Console.Write(k.KeyChar);
                    
                }
                if (k.Key == System.ConsoleKey.B && k.Modifiers == ConsoleModifiers.Control)
                {
                    
                    protected_buffer = buff;
                    //ClearBuff(ref pos_l, ref pos_r, ref buff, pos, Console.GetCursorPosition().Left, Console.GetCursorPosition().Top);
                }
                if(k.Key == System.ConsoleKey.V && k.Modifiers == ConsoleModifiers.Control)
                {
                    ClearBuff(ref pos_l, ref pos_r, ref buff, pos, Console.GetCursorPosition().Left, Console.GetCursorPosition().Top);
                    int pos1 = Console.GetCursorPosition().Top;
                    int pos2 = Console.GetCursorPosition().Left;
                    if (protected_buffer != "")
                    {
                        if (strat is AdminStrategy s) s.ModifyUp(ref txt, Console.GetCursorPosition().Top, Console.GetCursorPosition().Left, protected_buffer);
                        if (strat is EditorStrategy sq) sq.ModifyUp(ref txt, Console.GetCursorPosition().Top, Console.GetCursorPosition().Left, protected_buffer);
                    }
                    flag = false;
                    Console.Clear();
                    Console.WriteLine(txt);
                    Console.WriteLine(output);
                    Console.WriteLine("'"+buff+ "'");
                    //pos++;
                    Console.SetCursorPosition(pos2+ protected_buffer.Length, pos1);
                }
                if (k.Key == System.ConsoleKey.T && k.Modifiers == ConsoleModifiers.Control)
                {
                    int pos1 = Console.GetCursorPosition().Top;
                    int pos2 = Console.GetCursorPosition().Left;
                    int pos3 = pos2;
                    while (buff.Length>0 && pos_r>0)
                    {
                        buff = buff.Remove(buff.Length-1);
                        if (strat is AdminStrategy s) s.ModifyUp(ref txt, Console.GetCursorPosition().Top, pos3, "");
                        if (strat is EditorStrategy sq) sq.ModifyUp(ref txt, Console.GetCursorPosition().Top, pos3, "");
                        pos3--;

                    }
                    pos3 += 1;
                    while (buff.Length > 0 && pos_l > 0)
                    {
                        
                        buff = buff.Remove(buff.Length - 1);
                        if (strat is AdminStrategy s) s.ModifyUp(ref txt, Console.GetCursorPosition().Top, pos3, "");
                        if (strat is EditorStrategy sq) sq.ModifyUp(ref txt, Console.GetCursorPosition().Top, pos3, "");
                        // pos3++;

                    }
                    flag = false;
                    Console.Clear();
                    Console.WriteLine(txt);
                    Console.WriteLine(output);
                    Console.WriteLine("'"+buff+ "'");
                    //pos++;
                    Console.SetCursorPosition(pos2, pos1);
                    buff = "";
                    pos_l = pos_r= 0;
                }
                if (k.Key == System.ConsoleKey.X && k.Modifiers == ConsoleModifiers.Control)
                {
                    string ttxt = cmd.UnDo();
                    if (ttxt != "") txt = ttxt;
                    flag = false;
                }
                else if (k.Key == System.ConsoleKey.O && k.Modifiers == ConsoleModifiers.Control)
                {
                    string ttxt = cmd.ReDo();
                    if (ttxt != "") txt = ttxt;
                    flag = false;
                }
                else if (k.Key == System.ConsoleKey.U && k.Modifiers == ConsoleModifiers.Control)
                {
                    ThemeMenu();
                }
                else if (k.Key == System.ConsoleKey.H && k.Modifiers == ConsoleModifiers.Control)
                {
                    if (Document.history is not null)
                    {
                        Console.Clear();
                        foreach (string s in Document.history)
                        {
                            Console.WriteLine("change");
                            Console.WriteLine(s);
                        }
                        Console.WriteLine("нажмите любую кнопку чтобы продолить");
                        string a = Console.ReadLine();
                        Console.Clear();
                    }
                    flag = true;
                }
                else cmd.Save(new Frame(txt, Console.GetCursorPosition().Left, Console.GetCursorPosition().Top, output));
                
                if (k.Key == System.ConsoleKey.Escape)
                {
                    flag = false;
                    break;
                }


                // return;
            }
        }
        public void SetRight(ref string txt,string ouput,int pos,ref int p,ref int l, ref string buff)
        {
            string[] tmp = txt.Split("\n");
            int pos1 = Console.GetCursorPosition().Top;
            int pos2 = Console.GetCursorPosition().Left;
            Console.SetCursorPosition(0, pos+1);
            if (buff.Length >= 1) Clear(1);
            //Console.R
            if (l > 0)
            {

                if (buff.Length > 0)
                {
                    buff = buff.Remove(0,1);
                    l--;
                }
            }
            else
            {
                if (pos2 - 1 > -1 && pos2<170 && pos2 - 1 <= tmp[pos1].Length - 1)
                {
                    if (tmp[pos1][pos2 - 1] != '\r' && tmp[pos1][pos2 - 1] != '\n')
                    {
                        buff += tmp[pos1][pos2 - 1];

                        p++;
                    }
                }
                else if(pos2 - 1 > tmp[pos1].Length - 1)
                {
                    buff += " ";

                    p++;
                }
            }
            
            Console.SetCursorPosition(0, pos);
           // Clear(1);
            
            Console.Write("'"+buff+ "'");
            Console.SetCursorPosition(pos2,pos1);
        }
        public void SetLeft(ref string txt, string ouput, int pos, ref int p, ref int l, ref string buff)
        {
            string[] tmp = txt.Split("\n");
            int pos1 = Console.GetCursorPosition().Top;
            int pos2 = Console.GetCursorPosition().Left;
            Console.SetCursorPosition(0, pos+1);
            if (buff.Length >= 1) Clear(1);
            //Console.Write(tmp[pos1 - 1][pos2 - 1]);
            if (p > 0)
            {

                if (buff.Length > 0)
                {
                    buff = buff.Remove(buff.Length - 1,1);
                    p--;
                }

            }
            else
            {
                if (pos2 - 1 > -1 && pos2 - 1 <= tmp[pos1].Length - 1)
                {
                    if (tmp[pos1][pos2 - 1] != '\r' && tmp[pos1][pos2] != '\n')
                    {
                       buff= buff.Insert(0, tmp[pos1][pos2].ToString());

                        l++;
                    }
                }
                else if (pos2 - 1 > tmp[pos1].Length - 1)
                {
                    buff += " ";

                    l++;
                }
                else if(buff.Length <= txt.Split("\n")[pos1].Length - 2 || (pos1 == txt.Split("\n").Length - 1 && buff.Length < txt.Split("\n")[pos1].Length) && pos2==0)
                {
                    buff = buff.Insert(0, tmp[pos1][pos2].ToString());
                    l++;
                }
            }
            //Clear(1);
            Console.SetCursorPosition(0, pos);
           
            Console.Write("'"+buff+ "'");
            Console.SetCursorPosition(pos2, pos1);
        }
        public void ThemeMenu()
        {
            short size = 14;
            while (true)
            {
                Console.Clear();
                int pos = Console.GetCursorPosition().Top - 1;
                Document.SetText(strat.Read());
                string txt = "";
                if (Document.type == 0) txt= td.Compile();
                if (Document.type == 1) txt = jd.Compile();
                if (Document.type == 2) txt = md.Compile();
                if (Document.type == 3) txt = xd.Compile();
                if (Document.type == 4) txt = rd.Compile();
               // Console.WriteLine(txt);
                //if (strat is AdminStrategy s) Console.WriteLine(s.Read());
                string output = "ctrl+o - размер шрифта,ctrl+l - сменить тему, -1 выход";
                Console.WriteLine(output);
                string readkey = Console.ReadLine();
                if (readkey == "\f")
                {
                    output = "0 - Campbell, 1 - One Half Dark, 2 - Solarized Dark, 3 - Tango Dark, 4 - Vintage";
                    Console.WriteLine(output);
                    short chose = (short)checker.CheckWithBorders(Console.ReadLine(), 0, 4, output, 2);
                    if (chose == 0)
                    {
                        set.SetConsoleTheme("Campbell");
                    }
                    if (chose == 1)
                    {
                        set.SetConsoleTheme("One Half Dark");
                    }
                    if (chose == 2)
                    {
                        set.SetConsoleTheme("Solarized Dark");
                    }
                    if (chose == 3)
                    {
                        set.SetConsoleTheme("Tango Dark");
                    }
                    if (chose == 4)
                    {
                        set.SetConsoleTheme("Vintage");
                    }
                    Clear(4);
                }
                if (readkey == "\u000f")
                {
                    while (true)
                    {
                        output = "esc - выход,ctrl+1 - увеличить размер шрифта,ctrl+2 уменьшить размер шрифта";
                        Console.WriteLine(output);
                        var k = Console.ReadKey();

                        if (k.Key == System.ConsoleKey.D2 && k.Modifiers == ConsoleModifiers.Control)
                        {

                            if (size > 11)
                            {
                                size--;
                                set.SetConsoleFont(size);

                            }

                        }
                        if (k.Key == System.ConsoleKey.D1 && k.Modifiers == ConsoleModifiers.Control)
                        {


                            if (size < 15)
                            {
                                size++;
                                set.SetConsoleFont(size);
                            }
                        }
                        if (k.Key == System.ConsoleKey.Escape)
                        {
                            Console.Clear();
                            break;

                        }
                        Clear(1);
                    }
                }
               
                if (readkey == "-1")
                {
                    Console.Clear();
                    //ClearAll();

                    return;

                }

            }
        }
        public void ReaderMenu()
        {
            short size = 14;
            while (true)
            {
                Console.Clear();   
                int pos = Console.GetCursorPosition().Top - 1;
                Document.SetText(strat.Read());
                //string txt = compiler.Choose();
                string txt = "";
                if (Document.type == 0) txt = td.Compile();
                if (Document.type == 1) txt = jd.Compile();
                if (Document.type == 2) txt = md.Compile();
                if (Document.type == 3) txt = xd.Compile();
                if (Document.type == 4) txt = rd.Compile();
                Console.WriteLine(txt);
                //if (strat is AdminStrategy s) Console.WriteLine(s.Read());
                string output = "ctrl+k сохранить файл и выйти, ctrl+o - размер шрифта,ctrl+l - сменить тему, 0 - просмотреть историю документа  -1 выход";
                Console.WriteLine(output);
                string readkey = Console.ReadLine();
                if(readkey=="\f")
                {
                    output = "0 - Campbell, 1 - One Half Dark, 2 - Solarized Dark, 3 - Tango Dark, 4 - Vintage";
                    Console.WriteLine(output);
                    short chose = (short)checker.CheckWithBorders(Console.ReadLine(), 0, 4, output, 2);
                    if (chose == 0)
                    {
                        set.SetConsoleTheme("Campbell");
                    }
                    if (chose == 1)
                    {
                        set.SetConsoleTheme("One Half Dark");
                    }
                    if (chose == 2)
                    {
                        set.SetConsoleTheme("Solarized Dark");
                    }
                    if (chose == 3)
                    {
                        set.SetConsoleTheme("Tango Dark");
                    }
                    if (chose == 4)
                    {
                        set.SetConsoleTheme("Vintage");
                    }
                    Clear(4);
                }
                else if(readkey=="\u000f")
                {
                    while (true)
                    {
                        output = "esc - выход,ctrl+1 - увеличить размер шрифта,ctrl+2 уменьшить размер шрифта";
                        Console.WriteLine(output);
                        var k = Console.ReadKey();

                        if (k.Key == System.ConsoleKey.D2 && k.Modifiers == ConsoleModifiers.Control)
                        {

                            if (size > 11)
                            {
                                size--;
                                set.SetConsoleFont(size);

                            }

                        }
                        if (k.Key == System.ConsoleKey.D1 && k.Modifiers == ConsoleModifiers.Control)
                        {


                            if (size < 15)
                            {
                                size++;
                                set.SetConsoleFont(size);
                            }
                        }
                        if(k.Key ==System.ConsoleKey.Escape)
                        {
                            Console.Clear();
                            break;

                        }
                        Clear(1);
                    }
                }
                else if(readkey=="\v")
                {
                    Console.Clear();
                    set.SetConsoleFont(14);
                    set.SetConsoleTheme("Campbell");
                    while (true)
                    {
                        output = " 0 - сохранить локально, 1 - сохранить в облаке";
                        Console.WriteLine(output);
                        string inp = Console.ReadLine();
                        if (inp == "0")
                        {
                            Document.SetText(txt);
                            strat.SaveLocal(txt);
                            return;
                        }
                        if (inp == "1")
                        {
                            Document.SetText(txt);
                            cldd.Create(Document.name, txt);
                            strat.SaveCloud(txt);
                            return;
                        }

                    }
                }
                else if (readkey == "0")
                {
                    if (Document.history is not null)
                    {
                        Console.Clear();
                        foreach (string s in Document.history)
                        {
                            Console.WriteLine("change");
                            Console.WriteLine(s);
                        }
                        Console.WriteLine("нажмите любую кнопку чтобы продолить");
                        string a = Console.ReadLine();
                        Console.Clear();
                    }
                }
                if (readkey == "-1")
                {
                    Console.Clear();
                    //ClearAll();
                    set.SetConsoleFont(14);
                    set.SetConsoleTheme("Campbell");
                    return;
                    
                }

            }
        }
        public void SearchString(string txt,string st1)
        {
            const string grayColor = "\x1b[38;5;214m";
            const string resetColor = "\x1b[0m";
            int index = 0;
            bool[] shouldColor = new bool[txt.Length];
            var output = new StringBuilder();
            while (index <= txt.Length - st1.Length)
            {
                int foundIndex = txt.IndexOf(st1, index, StringComparison.OrdinalIgnoreCase);
                if (foundIndex == -1) break;

                // Помечаем все символы найденной подстроки
                for (int i = foundIndex; i < foundIndex + st1.Length; i++)
                {
                    shouldColor[i] = true;
                }

                index = foundIndex + 1; // Ищем следующее вхождение (даже с перекрытием)
            }

            // Строим итоговую строку с цветами
            bool isColored = false;
            for (int i = 0; i < txt.Length; i++)
            {
                if (shouldColor[i] && !isColored)
                {
                    output.Append(grayColor);
                    isColored = true;
                }
                else if (!shouldColor[i] && isColored)
                {
                    output.Append(resetColor);
                    isColored = false;
                }

                output.Append(txt[i]);
            }

            // Сбрасываем цвет в конце, если строка закончилась окрашенной
            if (isColored)
            {
                output.Append(resetColor);
            }
            Console.Clear();
            Console.WriteLine(output.ToString());

        }
           }
}
//qqqqqq
