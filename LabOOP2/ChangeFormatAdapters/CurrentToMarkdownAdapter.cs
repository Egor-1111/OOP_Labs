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
    public class CurrentToMarkdownAdapter
    {
        public void Parse(IUserStrategy admin, WorkWithCloud w_c, Document document)
        {
            if(document.type==3)
            {
                document.SetText(FromXMLToMarkdown(document.GetText()));
            }
            if (document.type == 4)
            {
                document.SetText(FromRtfToMarkdown(document.GetText()));
            }
            document.type = 2;
            string[] tmp = document.name.Split(".");
            int i = 0;
            string txt = "";
            
            foreach (string s in tmp)
            {
                if (i == tmp.Length - 1) continue;
                else txt += s;
                ++i;
            }
            txt += ".md";
            if (admin is AdminStrategy adm) adm.Upload(txt);
            if (admin is EditorStrategy ed) ed.Upload(txt);
            document.name = txt;
           
           // w_c.UploadFile(document.name, document.type, "17gYVcgPxxoM4UsNsyq-i2uk8K9RGI4Co", "");
            //admin.SaveCloud(document.GetText());
        }
        public string FromXMLToMarkdown(string xml)
        {
            string markdown = xml;
            markdown = Regex.Replace(markdown, @"<b><i>(.*?)</i></b>", "***$1***");
            markdown = Regex.Replace(markdown, @"<i><b>(.*?)</b></i>", "***$1***");
            markdown = Regex.Replace(markdown, @"<b>(.*?)</b>", "**$1**");
            markdown = Regex.Replace(markdown, @"<i>(.*?)</i>", "*$1*");
            markdown = Regex.Replace(markdown, @"<b><u>(.*?)</u></b>", "**<u>$1</u>**");
            markdown = Regex.Replace(markdown, @"<i><u>(.*?)</u></i>", "*<u>$1</u>*");
            markdown = Regex.Replace(markdown, @"<b><i><u>(.*?)</u></i></b>", "***<u>$1</u>***");
            return markdown.Trim();


        }
        public static string FromRtfToMarkdown(string rtf)
        {
            string result = rtf;

           
            result = Regex.Replace(result,
                @"\\b\\i\\u(.+?)\\ul0\\i0\\b0",
                "***<u>$1</u>***");

            
            result = Regex.Replace(result,
                @"\\b\\i(.+?)\\i0\\b0",
                "***$1***");
            result = Regex.Replace(result,
                @"\\b\\u(.+?)\\ul0\\b0",
                "**<u>$1</u>**");
            result = Regex.Replace(result,
                @"\\i\\u(.+?)\\ul0\\i0",
                "*<u>$1</u>*");

            
            result = Regex.Replace(result, @"\\b(.+?)\\b0", "**$1**");
            result = Regex.Replace(result, @"\\i(.+?)\\i0", "*$1*");
            result = Regex.Replace(result, @"\\u(.+?)\\ul0", "<u>$1</u>");

            
            result = result.Replace("<u>>", "<u>")
                          .Replace("<</u>", "</u>").Replace("<<u>", "<u>").Replace("</u>>", "</u>");

            return result;
        }
    }
}
