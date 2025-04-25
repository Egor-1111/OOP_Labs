using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OOP_Lab2;

namespace OOP_Lab2.StyleDecorator
{
    public class XmlDecorator : BaseDecorator
    {
        public XmlDecorator(Document doc) : base(doc)
        {
        }
        public override string Compile()
        {
            string xml = doc.GetText();
            xml = Regex.Replace(xml, @"<b>(.*?)<\/b>",
                m => $"\u001b[1m{m.Groups[1].Value}\u001b[0m");

            xml = Regex.Replace(xml, @"<i>(.*?)<\/i>",
                m => $"\u001b[3m{m.Groups[1].Value}\u001b[0m");

            xml = Regex.Replace(xml, @"<u>(.*?)<\/u>",
                m => $"\u001b[4m{m.Groups[1].Value}\u001b[0m");

            xml = Regex.Replace(xml, @"<([biu])><([biu])>(.*?)<\/\2><\/\1>",
                m => {
                    string styles = "";
                    if (m.Groups[1].Value == "b" || m.Groups[2].Value == "b") styles += "1;";
                    if (m.Groups[1].Value == "i" || m.Groups[2].Value == "i") styles += "3;";
                    if (m.Groups[1].Value == "u" || m.Groups[2].Value == "u") styles += "4;";
                    return $"\u001b[{styles.Trim(';')}m{m.Groups[3].Value}\u001b[0m";
                });
            xml = Regex.Replace(xml, @"<(\/?)([^>]+)>",
                m => $"<{m.Groups[1].Value}{m.Groups[2].Value}>");

            return xml;
        }
    }
}
