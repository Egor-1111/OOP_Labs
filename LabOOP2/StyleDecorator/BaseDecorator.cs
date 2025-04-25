using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Lab2;

namespace OOP_Lab2.StyleDecorator
{
    public abstract class BaseDecorator
    {
        protected Document doc;
        public BaseDecorator(Document doc)
        {
            this.doc = doc;
        }
        public abstract string Compile();
    }
}
