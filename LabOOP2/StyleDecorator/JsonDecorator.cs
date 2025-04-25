using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Lab2;

namespace OOP_Lab2.StyleDecorator
{
    public class JsonDecorator : BaseDecorator
    {
        public JsonDecorator (Document doc) : base(doc)
        {
        }
        public override string Compile()
        {
            return doc.GetText();
        }
    }
}
