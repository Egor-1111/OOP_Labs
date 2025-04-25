using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP1
{
    public class Ellipse : IFigure
    {
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int r1 { get; set; }
        public int r2 { get; set; }
        public int Color { get; set; }
    }
}
