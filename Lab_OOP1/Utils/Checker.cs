using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP1
{
    public class Checker
    {
        public int CheckWithBorders(string input, int low, int big, string PossibleErr)
        {
            int InputNumber = 0;
            bool flag = true;
            while (flag)
            {
                if (Int32.TryParse(input, out InputNumber))
                {
                    if (InputNumber < low || InputNumber > big)
                    {



                        if (!(Console.GetCursorPosition().Top - 2 != 1))
                        {
                            Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 3);
                            Console.SetCursorPosition(0, 0);
                            Console.Write("\r"); 
                            Console.Write(new string(' ', Console.WindowWidth));

                            Console.SetCursorPosition(0, 2);
                            Console.Write("\r"); 
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, 1);
                            Console.Write("\r"); 
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, 0);
                            Console.WriteLine("Ошибка ввода следующей комманды : " + PossibleErr);

                            return -2;
                            
                        }
                        else
                        {
                            int tp = Console.GetCursorPosition().Top;
                            Console.SetCursorPosition(0, tp - 2);
                            Console.SetCursorPosition(0, tp - 1);
                            Console.Write("\r"); 
                            Console.Write(new string(' ', Console.WindowWidth));

                            Console.SetCursorPosition(0, tp - 2);
                            Console.Write("\r"); 
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, tp - 2);
                            Console.WriteLine("Ошибка команды: " + PossibleErr);
                            input = Console.ReadLine();
                            
                        }

                    }
                    else return InputNumber;

                }
                else
                {
                    if (!(Console.GetCursorPosition().Top - 2 != 1))
                    {
                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 3);
                        Console.SetCursorPosition(0, 0);
                        Console.Write("\r"); 
                        Console.Write(new string(' ', Console.WindowWidth));

                        Console.SetCursorPosition(0, 2);
                        Console.Write("\r"); 
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, 1);
                        Console.Write("\r"); 
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Ошибка ввода следующей комманды : " + PossibleErr);
                        return -2;

                    }
                    else
                    {
                        int tp = Console.GetCursorPosition().Top;
                        Console.SetCursorPosition(0, tp - 2);
                        Console.SetCursorPosition(0, tp - 1);
                        Console.Write("\r"); 
                        Console.Write(new string(' ', Console.WindowWidth));

                        Console.SetCursorPosition(0, tp - 2);
                        Console.Write("\r"); 
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, tp - 2);
                        Console.WriteLine("Ошибка ввода следующей комманды : " + PossibleErr);
                        input = Console.ReadLine();
                    }
                }
                
            }
            return InputNumber;
        }
    }
}
