using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Lab2;

namespace OOP_Lab2
{
    public class Checker
    {
        public int CheckWithBorders(string input, int low, int big, string PossibleErr,int borders)
        {
            int InputNumber = 0;
            bool flag = true;
            while (flag)
            {
                if (Int32.TryParse(input, out InputNumber))
                {
                    if (InputNumber < low || InputNumber > big)
                    {

                        int tp = Console.GetCursorPosition().Top;

                        for (int i=tp;i< tp + borders;++i)
                        {
                            Console.SetCursorPosition(0,i-borders);
                            Console.Write("\r");
                            Console.Write(new string(' ', Console.WindowWidth));
                        }
                        Console.SetCursorPosition(0, tp-borders);
                        Console.WriteLine("Ошибка! Введите коррректно команду : " + PossibleErr);
                        input = Console.ReadLine();
                        //break;

                    }
                    else return InputNumber;

                }
                else
                {

                    int tp = Console.GetCursorPosition().Top;

                    for (int i = tp; i < tp + borders; ++i)
                    {
                        Console.SetCursorPosition(0, i - borders);
                        Console.Write("\r");
                        Console.Write(new string(' ', Console.WindowWidth));
                    }
                    Console.SetCursorPosition(0, tp - borders);
                    Console.WriteLine("Ошибка! Введите коррректно команду : " + PossibleErr);
                    input = Console.ReadLine();
                    //break;

                }
                
        }
            return InputNumber;
        }
        public bool CheckStringInCollection(List<User> Users, string info)
        {
            foreach (User usr in Users)
            {
                if (usr.Name == info)
                {
                    
                    //Console.WriteLine("Такой элемент уже есть !");
                    return true;
                }
            }

            return false;
        }
        public bool CheckStringInCollectionDoc(List<Document> Users, string info)
        {
            foreach (Document doc in Users)
            {
                //string[] tmp = doc.name.Split(".");
               // string[] tmp1 = info.Split(".");
                if (doc.name == info)
                {

                    //Console.WriteLine("Такой элемент уже есть !");
                    return true;
                }
            }

            return false;
        }
    }
}
