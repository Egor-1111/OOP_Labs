using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab2.Command
{
    public class Frame
    {
        public string txt { get; set; }
        public int l { get; set; }
        public int t { get; set; }

        public string menu { get; set; }
        public Frame(string txt,int l,int t, string menu)
        {
            this.txt = txt;
            this.l = l;
            this.t = t;
            this.menu = menu;
        }
        public void Write()
        {
            Console.Clear();
            Console.WriteLine(txt);
            Console.WriteLine(menu);
            Console.SetCursorPosition(l, t);
        }
    }
}
