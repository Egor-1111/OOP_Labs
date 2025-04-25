using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab2
{
    class WorkWithUsers
    {
        Checker checker = new Checker();
       
        public void Print(List<User> Users)
        {
            foreach(User usr in Users)
            {
                Console.Write(usr.Name + " ");
            }
            Console.WriteLine();
        }
      
    }
}
