using Lab_OOP1;

public  class Program
{
    static void Clear(int k)
    {
        for (int i = 0; i < k; ++i)
        {
            Console.SetCursorPosition(0, 2 + i);
            Console.Write("\r");
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
    private static void Main(string[] args)
    {
        
        Drawer draw = new Drawer();
        WorkWithFile worker = new WorkWithFile();
        ChangeAction change = new ChangeAction();
        Checker check = new Checker();
        int CounterOfFigures=0;
        string text1 = "Введите, что хотите сделать -1 - выход 0 - нарисовать фигуру, 1 - удалить фигуру, 2 - залить фигуру, 3 - сдвинуть фигуру, 4 - сохранить в файл, 5 - прочесть из файла, CTRL+X - вернуть предыдущее действие, CTRL+Y - отменить возвращение к предыдущему состоянию  ";
        Console.WriteLine(text1);

        for (int i = 11; i < 45; ++i)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("=");
        }
        for (int i = 11; i < 45; ++i)
        {
            Console.SetCursorPosition(199, i);
            Console.Write("=");
        }
        for (int i = 0; i < 200; ++i)
        {
            Console.SetCursorPosition(i,11 );
            Console.Write("=");
        }

        for (int i = 0; i < 200; ++i)
        {
            Console.SetCursorPosition(i, 44);
            Console.Write("=");
        }
        Console.SetCursorPosition(0, 2);
        change.AddToStorageDelete(new List<IFigure>());
        bool flag = false;
        while (true)
        {

            int parametr = 0;
            
            Console.SetCursorPosition(0, 2);
            
            string paraam = Console.ReadLine();
            if (paraam == "\u0018" || paraam == "\u0019") 
            {
                Clear(1);
                if (paraam == "\u0018")
                {
                    List<IFigure> list = change.Decline();
                    if (list is not null) draw.DrawCanva(list);

                }
                else if (paraam == "\u0019")
                {
                    List<IFigure> list = change.Accept();
                    if (list is not null) draw.DrawCanva(list);

                }
                for (int i = 11; i < 45; ++i)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("=");
                }
                for (int i = 11; i < 45; ++i)
                {
                    Console.SetCursorPosition(199, i);
                    Console.Write("=");
                }
                for (int i = 0; i < 200; ++i)
                {
                    Console.SetCursorPosition(i, 11);
                    Console.Write("=");
                }

                for (int i = 0; i < 200; ++i)
                {
                    Console.SetCursorPosition(i, 44);
                    Console.Write("=");
                }
                Console.SetCursorPosition(0, 2);
            }

            else
            {

                parametr = check.CheckWithBorders(paraam, -1, 5, text1);
                if (parametr == -2) continue;
                
                CounterOfFigures++;
                switch (parametr)
                {
                    
                    case 0:
                        {
                            if (CounterOfFigures > 50)
                            {
                                Clear(1);
                                continue;
                            }
                            text1 = "Введите тип фигуры, 0 - круг (3<R<10) 1 - треугольник равносторонний (3<a<10) 2 - треугольник прямоугольный(3<a<10,3<b<10) 3 - эллипс(3<r1<10,3<r2<10) 4 - прямоугольник(3<a<10,3<b<10) ";
                            Console.WriteLine(text1);
                            int param = check.CheckWithBorders(Console.ReadLine(), 0, 4, text1);
                            switch (param)
                            {
                                case 0:
                                    {
                                        text1 = "Введите радиус круга";
                                        Console.WriteLine(text1);
                                        int radius = check.CheckWithBorders(Console.ReadLine(), 3, 10, text1);
                                        draw.DrawCircle(radius);
                                        Clear(5);

                                        break;
                                    }
                                case 1:
                                    {
                                        text1 = "Введите сторону треугольника";
                                        Console.WriteLine(text1);
                                        int a = check.CheckWithBorders(Console.ReadLine(), 3, 10, text1);
                                        draw.DrawTriangle(a);
                                        Clear(5);

                                        break;
                                    }
                                case 2:
                                    {
                                        text1 = "Введите длину первой стороны";
                                        Console.WriteLine(text1);
                                        int a = check.CheckWithBorders(Console.ReadLine(), 3, 10, text1);
                                        text1 = "Введите длину второй стороны";
                                        Console.WriteLine(text1);
                                        int b = check.CheckWithBorders(Console.ReadLine(), 3, 10, text1);
                                        draw.DrawTriangleRect(a, b);
                                        Clear(7);

                                        break;
                                    }
                                case 3:
                                    {
                                        text1 = "Введите радиус по вертикали";
                                        Console.WriteLine(text1);
                                        int r1 = check.CheckWithBorders(Console.ReadLine(), 3, 10, text1);
                                        text1 = "Введите радиус по горизонтали";
                                        Console.WriteLine(text1);
                                        int r2 = check.CheckWithBorders(Console.ReadLine(), 3, 10, text1);
                                        draw.DrawEllipse(r1, r2);
                                        Clear(7);

                                        break;

                                    }
                                case 4:
                                    {
                                        text1 = "Введите длину первой стороны";
                                        Console.WriteLine(text1);
                                        int a = check.CheckWithBorders(Console.ReadLine(), 3, 10, text1);
                                        text1 = "Введите длину второй стороны";
                                        Console.WriteLine(text1);
                                        int b = check.CheckWithBorders(Console.ReadLine(), 3, 10, text1);
                                        draw.DrawRectangle(a, b);
                                        Clear(7);

                                        break;
                                    }
                            }


                            break;
                        }
                    case 1:
                        {
                            text1 = "Удалить фигуру ";
                            int k = 0;
                            string txt = draw.Showinfo(ref k);
                            if (k == 0)
                            {
                                Clear(1);
                                break;

                            }
                            draw.undraw(check.CheckWithBorders(Console.ReadLine(), 0, k - 1, text1+txt), false);
                            Clear(4);
                            CounterOfFigures--;
                            break;

                        }
                    case 2:
                        {
                            text1 = "Залить фигуру ";
                            int k=0;
                            string txt = draw.Showinfo(ref k);
                            if (k == 0)
                            {
                                Clear(1);
                                break;

                            }
                            int figr = check.CheckWithBorders(Console.ReadLine(), 0, k - 1, text1+txt);
                            text1 = $"Введите опцию заливки :0 - "+" ' ' "+" 1 - / 2 - #";
                            Console.WriteLine(text1);
                            int color = check.CheckWithBorders(Console.ReadLine(), 0, 2, text1);
                            draw.Fill(figr, color);
                            Clear(5);

                            break;
                        }
                    case 3:
                        {
                            text1 = "Сдвинуть фигуру ";
                            int k = 0;
                            string txt = draw.Showinfo(ref k);
                            if (k == 0)
                            {
                                Clear(1);
                                break;

                            }
                            int figr = check.CheckWithBorders(Console.ReadLine(), 0, k - 1, text1 + txt);
                            
                           
                            draw.Change(figr);
                            Clear(5);

                            break;
                        }
                    case 4:
                        {
                            worker.WriteInFile(draw.GetData(), "data.json");
                            Clear(5);

                            break;
                        }
                    case 5:
                        {
                            draw.DrawCanva(worker.ReadFromFile("data.json"));
                            Clear(5);
                            for (int i = 11; i < 45; ++i)
                            {
                                Console.SetCursorPosition(0, i);
                                Console.Write("=");
                            }
                            for (int i = 11; i < 45; ++i)
                            {
                                Console.SetCursorPosition(199, i);
                                Console.Write("=");
                            }
                            for (int i = 0; i < 200; ++i)
                            {
                                Console.SetCursorPosition(i, 11);
                                Console.Write("=");
                            }

                            for (int i = 0; i < 200; ++i)
                            {
                                Console.SetCursorPosition(i, 44);
                                Console.Write("=");
                            }
                            Console.SetCursorPosition(0, 2);
                            break;
                        }
                }
                worker.WriteInFile(draw.GetData(), "data1.json");
                change.AddToStorageDelete(worker.ReadFromFile("data1.json"));


                if (parametr == -1) break;
            }
            Console.SetCursorPosition(0, 0);
            text1 = "Введите, что хотите сделать -1 - выход 0 - нарисовать фигуру, 1 - удалить фигуру, 2 - залить фигуру, 3 - сдвинуть фигуру, 4 - сохранить в файл, 5 - прочесть из файла, CTRL+X - вернуть предыдущее действие, CTRL+Y - отменить возвращение к предыдущему состоянию  ";
            Console.WriteLine(text1);
        }
    }
}