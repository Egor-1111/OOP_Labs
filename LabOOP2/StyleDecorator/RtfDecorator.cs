using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OOP_Lab2;

namespace OOP_Lab2.StyleDecorator
{
    public class RtfDecorator : BaseDecorator
    {
        public RtfDecorator(Document doc) : base(doc)
        {
        }
        public override string Compile()
        {
            string text = doc.GetText();
            text = Regex.Replace(text, @"\\b(.+?)\\b0", m => $"\u001b[1m{m.Groups[1].Value}\u001b[0m");
            text = Regex.Replace(text, @"\\i(.+?)\\i0", m => $"\u001b[3m{m.Groups[1].Value}\u001b[0m");
            text = Regex.Replace(text, @"\\ul(.+?)\\ul0", m => $"\u001b[4m{m.Groups[1].Value}\u001b[0m");
            text = Regex.Replace(text, @"\\b\\i(.+?)\\i0\\b0", m => $"\u001b[1;3m{m.Groups[1].Value}\u001b[0m");
            text = Regex.Replace(text, @"\\b\\ul(.+?)\\ul0\\b0", m => $"\u001b[1;4m{m.Groups[1].Value}\u001b[0m");
            text = Regex.Replace(text, @"\\i\\ul(.+?)\\ul0\\i0", m => $"\u001b[3;4m{m.Groups[1].Value}\u001b[0m");
            text = Regex.Replace(text, @"\\b\\i\\ul(.+?)\\ul0\\i0\\b0", m => $"\u001b[1;3;4m{m.Groups[1].Value}\u001b[0m");


            return text;
        }
    }
}
