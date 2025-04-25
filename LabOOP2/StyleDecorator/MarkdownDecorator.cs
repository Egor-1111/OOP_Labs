using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OOP_Lab2;

namespace OOP_Lab2.StyleDecorator
{
    public class MarkdownDecorator : BaseDecorator
    {
        public MarkdownDecorator(Document doc) : base(doc)
        {
        }
        public override string Compile()
        {
            string markdown = doc.GetText();
            markdown = Regex.Replace(markdown, @"<u>([^<]+)<\/u>",
                m => $"\u001b[4m{m.Groups[1].Value}\u001b[0m");
            markdown = Regex.Replace(markdown, @"\*\*\*(.+?)\*\*\*",
                m => $"\u001b[1;3m{m.Groups[1].Value}\u001b[0m");
            markdown = Regex.Replace(markdown, @"\*\*(.+?)\*\*",
                m => $"\u001b[1m{m.Groups[1].Value}\u001b[0m");
            markdown = Regex.Replace(markdown, @"(?<!\\)\*(?![\s\\])(.+?)(?<!\s)\*(?!\*)",
                m => $"\u001b[3m{m.Groups[1].Value}\u001b[0m");
            return markdown;
        }
    }
}
