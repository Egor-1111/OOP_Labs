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
    public class CurrentToXMLAdapter
    {
        public void Parse(IUserStrategy admin, WorkWithCloud w_c, Document document)
        {
            if (document.type == 2)
            {
                document.SetText(FromMarkdownToXml(document.GetText()));
            }
            if (document.type == 4)
            {
                document.SetText(FromRtfToXml(document.GetText()));
            }
            document.type = 3;
            string[] tmp = document.name.Split(".");
            int i = 0;
            string txt = "";

            foreach (string s in tmp)
            {
                if (i == tmp.Length - 1) continue;
                else txt += s;
                ++i;
            }
            txt += ".xml";
            if (admin is AdminStrategy adm) adm.Upload(txt);
            if (admin is EditorStrategy ed) ed.Upload(txt);
            document.name = txt;
            //w_c.UploadFile(document.name, document.type, "17gYVcgPxxoM4UsNsyq-i2uk8K9RGI4Co", "");
            // admin.SaveCloud(document.GetText());
        }
        public static string FromMarkdownToXml(string markdown)
        {

            string xml = markdown;

            xml = Regex.Replace(xml, @"\*\*\*(.*?)\*\*\*", "<b><i>$1</i></b>");

            xml = Regex.Replace(xml, @"\*\*_(.*?)_\*\*", "<b><i>$1</i></b>");

            xml = Regex.Replace(xml, @"_\*\*(.*?)\*\*_", "<i><b>$1</b></i>");

            xml = Regex.Replace(xml, @"\*\*(.*?)\*\*", "<b>$1</b>");

            xml = Regex.Replace(xml, @"__(.*?)__", "<b>$1</b>");

            xml = Regex.Replace(xml, @"\*(.*?)\*", "<i>$1</i>");

            xml = Regex.Replace(xml, @"_(.*?)_", "<i>$1</i>");

            xml = Regex.Replace(xml, @"~~(.*?)~~", "<u>$1</u>");

            xml = Regex.Replace(xml, @"<u>(.*?)</u>", "<u>$1</u>");

            xml = Regex.Replace(xml, @"\*\*<i>(.*?)</i>\*\*", "<b><i>$1</i></b>");
            xml = Regex.Replace(xml, @"_<b>(.*?)</b>_", "<i><b>$1</b></i>");

            return xml;
        }
        public static string FromRtfToXml(string rtf)
        {
            var xml = new StringBuilder();


            string cleanRtf = rtf;

            string text = cleanRtf;

            text = Regex.Replace(text,
                @"\\b\\i\\ul\s?(.*?)\\ul0\\i0\\b0",
                "<b><i><u>$1</u></i></b>",
                RegexOptions.Singleline);

            text = Regex.Replace(text,
                @"\\b\\i\s?(.*?)\\i0\\b0",
                "<b><i>$1</i></b>",
                RegexOptions.Singleline);
            text = Regex.Replace(text,
                @"\\b\\ul\s?(.*?)\\ul0\\b0",
                "<b><u>$1</u></b>",
                RegexOptions.Singleline);
            text = Regex.Replace(text,
                @"\\i\\ul\s?(.*?)\\ul0\\i0",
                "<i><u>$1</u></i>",
                RegexOptions.Singleline);

            // 3. Одиночные стили
            text = Regex.Replace(text, @"\\b\s?(.*?)\\b0", "<b>$1</b>", RegexOptions.Singleline);
            text = Regex.Replace(text, @"\\i\s?(.*?)\\i0", "<i>$1</i>", RegexOptions.Singleline);
            text = Regex.Replace(text, @"\\ul\s?(.*?)\\ul0", "<u>$1</u>", RegexOptions.Singleline);



            xml.Append(text);

            return xml.ToString();
        }
    }
}
