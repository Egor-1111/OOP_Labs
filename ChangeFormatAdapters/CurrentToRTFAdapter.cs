using OOP_Lab2;
using OOP_Lab2.FileSafe;
using OOP_Lab2.UserStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOP_Lab2.ChangeFormatAdapters
{
    public class CurrentToRTFAdapter
    {
        public void Parse(IUserStrategy admin, WorkWithCloud w_c, Document document)
        {
            if (document.type == 2)
            {
                document.SetText(FromMarkdownToRtf(document.GetText()));
            }
            if (document.type == 3)
            {
                document.SetText(FromXmlToRtf(document.GetText()));
            }
            document.type = 4;
            string[] tmp = document.name.Split(".");
            int i = 0;
            string txt = "";

            foreach (string s in tmp)
            {
                if (i == tmp.Length - 1) continue;
                else txt += s;
                ++i;
            }
            txt += ".rtf";
            if (admin is AdminStrategy adm) adm.Upload(txt);
            if (admin is EditorStrategy ed) ed.Upload(txt);
            document.name = txt;
            //w_c.UploadFile(document.name, document.type, "17gYVcgPxxoM4UsNsyq-i2uk8K9RGI4Co", "");
            //admin.SaveCloud(document.GetText());
        }
        public static string FromMarkdownToRtf(string markdown)
        {
            var rtf = new StringBuilder();

            string text = markdown;

            text = Regex.Replace(text, @"\*\*\*<u>(.*?)</u>\*\*\*",
                @"\b\i\u$1\ul0\i0\b0",
                RegexOptions.Singleline);

            text = Regex.Replace(text, @"\*\*\*(.*?)\*\*\*",
                @"\b\i$1\i0\b0",
                RegexOptions.Singleline);

            text = Regex.Replace(text, @"\*\*<u>(.*?)</u>\*\*",
                @"\b\u$1\ul0\b0",
                RegexOptions.Singleline);

            text = Regex.Replace(text, @"\*<u>(.*?)</u>\*",
                @"\i\u$1\ul0\i0",
                RegexOptions.Singleline);

            text = Regex.Replace(text, @"\*\*(.*?)\*\*", @"\b$1\b0", RegexOptions.Singleline);
            text = Regex.Replace(text, @"\*(.*?)\*", @"\i$1\i0", RegexOptions.Singleline);
            text = Regex.Replace(text, @"<u>(.*?)</u>", @"\u$1\ul0", RegexOptions.Singleline);


            rtf.Append(text);


            return rtf.ToString();
        }
        public static string FromXmlToRtf(string xml)
        {
            var rtf = new StringBuilder();


            string text = xml;

            text = Regex.Replace(text,
                @"<b><i><u>(.*?)</u></i></b>",
                @"\b\i\ul$1\ul0\i0\b0",
                RegexOptions.Singleline);

            // 2. Двойные комбинации
            text = Regex.Replace(text,
                @"<b><i>(.*?)</i></b>",
                @"\b\i$1\i0\b0",
                RegexOptions.Singleline);
            text = Regex.Replace(text,
                @"<b><u>(.*?)</u></b>",
                @"\b\ul$1\ul0\b0",
                RegexOptions.Singleline);
            text = Regex.Replace(text,
                @"<i><u>(.*?)</u></i>",
                @"\i\ul$1\ul0\i0",
                RegexOptions.Singleline);

            text = Regex.Replace(text, @"<b>(.*?)</b>", @"\b$1\b0", RegexOptions.Singleline);
            text = Regex.Replace(text, @"<i>(.*?)</i>", @"\i$1\i0", RegexOptions.Singleline);
            text = Regex.Replace(text, @"<u>(.*?)</u>", @"\ul$1\ul0", RegexOptions.Singleline);

            text = Regex.Replace(text, @"<\/?(document|text)[^>]*>", "", RegexOptions.Singleline);



            rtf.Append(text);
            return rtf.ToString();

        }
    }
}
