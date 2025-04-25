using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;  
using Newtonsoft.Json.Linq;
namespace Lab_OOP1
{
    public class WorkWithFile
    {
        public bool WriteInFile(List<IFigure> list,string filename)
        {

           
            List<Tuple<object, string>> hr=new List<Tuple<object, string>>();
            Tuple<object, string> tmp;
            for (int i=0;i<list.Count;++i)
            {

                if (list[i] is Circle)
                {
                    tmp = new Tuple<object, string>(list[i], "Circle");
                    hr.Add(tmp);
                }
                if (list[i] is Ellipse)
                {
                    tmp = new Tuple<object, string>(list[i], "Ellipse");
                    hr.Add(tmp);
                }
                if (list[i] is Rectangle)
                {
                    tmp = new Tuple<object, string>(list[i], "Rectangle");
                    hr.Add(tmp);
                }
                if (list[i] is Triangle)
                {
                    tmp = new Tuple<object, string>(list[i], "Triangle");
                    hr.Add(tmp);
                }
                if (list[i] is TriangleRect)
                {
                    tmp = new Tuple<object, string>(list[i], "TriangleRect");
                    hr.Add(tmp);
                }

            }
            string json = JsonConvert.SerializeObject(hr, Newtonsoft.Json.Formatting.Indented);
            try
            {
                File.WriteAllText(filename, json);
            }
            catch
            {
                return false;
            }
            return true;

        }
        public List<IFigure> ReadFromFile(string filename)
        {
            try
            {
                string json = File.ReadAllText(filename);
                List<Tuple<object, string>> hr = JsonConvert.DeserializeObject<List<Tuple<object, string>>>(json);
                List<IFigure> list = new List<IFigure>();
                for (int i = 0; i < hr.Count; ++i)
                {
                    if (hr[i].Item2 == "Circle")
                    {
                        Circle temp = new Circle();
                        string tmp = hr[i].Item1.ToString();
                        JObject jsonObj = JObject.Parse(tmp);
                        temp.CenterX = jsonObj["CenterX"]?.Value<int>() ?? 0;
                        temp.CenterY = jsonObj["CenterY"]?.Value<int>() ?? 0;
                        temp.radius = jsonObj["radius"]?.Value<int>() ?? 0;
                        temp.Color = jsonObj["Color"]?.Value<int>() ?? 0;
                        list.Add(temp);
                    }
                    if (hr[i].Item2 == "Ellipse")
                    {
                        Ellipse temp = new Ellipse();
                        string tmp = hr[i].Item1.ToString();
                        JObject jsonObj = JObject.Parse(tmp);
                        temp.CenterX = jsonObj["CenterX"]?.Value<int>() ?? 0;
                        temp.CenterY = jsonObj["CenterY"]?.Value<int>() ?? 0;
                        temp.r1 = jsonObj["r1"]?.Value<int>() ?? 0;
                        temp.r2 = jsonObj["r2"]?.Value<int>() ?? 0;
                        temp.Color = jsonObj["Color"]?.Value<int>() ?? 0;
                        list.Add(temp);
                    }
                    if (hr[i].Item2 == "Triangle")
                    {
                        Triangle temp = new Triangle();
                        string tmp = hr[i].Item1.ToString();
                        JObject jsonObj = JObject.Parse(tmp);
                        temp.CenterX = jsonObj["CenterX"]?.Value<int>() ?? 0;
                        temp.CenterY = jsonObj["CenterY"]?.Value<int>() ?? 0;
                        temp.radius = jsonObj["radius"]?.Value<int>() ?? 0;
                        temp.Color = jsonObj["Color"]?.Value<int>() ?? 0;
                        list.Add(temp);
                    }
                    if (hr[i].Item2 == "TriangleRect")
                    {
                        TriangleRect temp = new TriangleRect();
                        string tmp = hr[i].Item1.ToString();
                        JObject jsonObj = JObject.Parse(tmp);
                        temp.CenterX = jsonObj["CenterX"]?.Value<int>() ?? 0;
                        temp.CenterY = jsonObj["CenterY"]?.Value<int>() ?? 0;
                        temp.a = jsonObj["a"]?.Value<int>() ?? 0;
                        temp.b = jsonObj["b"]?.Value<int>() ?? 0;
                        temp.Color = jsonObj["Color"]?.Value<int>() ?? 0;
                        list.Add(temp);
                    }
                    if (hr[i].Item2 == "Rectangle")
                    {
                        Rectangle temp = new Rectangle();
                        string tmp = hr[i].Item1.ToString();
                        JObject jsonObj = JObject.Parse(tmp);
                        temp.CenterX = jsonObj["CenterX"]?.Value<int>() ?? 0;
                        temp.CenterY = jsonObj["CenterY"]?.Value<int>() ?? 0;
                        temp.a = jsonObj["a"]?.Value<int>() ?? 0;
                        temp.b = jsonObj["b"]?.Value<int>() ?? 0;
                        temp.Color = jsonObj["Color"]?.Value<int>() ?? 0;
                        list.Add(temp);
                    }
                }
                return list;
            }
            catch
            {
                return null;
            }
            
        }
    }
}
