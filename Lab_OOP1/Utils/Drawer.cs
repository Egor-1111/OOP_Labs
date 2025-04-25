using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab_OOP1;
public class Drawer
{
   
    int x_c = 120;
    int y_c = 32;
    List<IFigure> list = new List<IFigure>();
    public bool DrawCircle(int radius)
    {
        if (radius < 3 || radius>20) return false;   
        Circle temp = new Circle();
        temp.CenterX =120;
        temp.CenterY = 32;
        temp.radius = radius;
        temp.Color = 0;
        list.Add(temp);
        double thickness = 0.2;
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                double distance = Math.Sqrt(x * x + y * y); 
                double difference = Math.Abs(distance - radius);

                if (difference < thickness) 
                {
                    Console.SetCursorPosition((x_c + x * 2), y_c + y);
                    Console.Write(".");
                }
                else if (distance < radius - thickness) 
                {
                    Console.SetCursorPosition((x_c + x * 2), y_c + y);
                    Console.Write(" "); 
                    Console.SetCursorPosition((x_c + x*2+1), y_c + y);
                    Console.Write(" "); 
                    Console.SetCursorPosition((x_c + x * 2 - 1), y_c + y);
                    Console.Write(" "); 

                }
            }
        }
        return true;
    }
    public void DrawEllipse(int r1,int r2)
    {
        if (r1 < 3 || r2 < 3 || r1>20||r2>20) return;
        Ellipse temp = new Ellipse();
        temp.CenterX = 120;
        temp.CenterY = 32;
        temp.r1 = r1;
        temp.r2 = r2;
        temp.Color = 0;
        list.Add(temp);
        double thickness = 0.1; 

        for (int y = -r1; y <= r1; y++)
        {
            for (int x = -r2; x <= r2; x++)
            {
               
                double distance = Math.Pow(x / (double)r2, 2) + Math.Pow(y / (double)r1, 2);

                if (Math.Abs(distance - 1) < thickness) 
                {
                    Console.SetCursorPosition(temp.CenterX + x * 2, temp.CenterY + y);
                    Console.Write(".");
                }
                else if (distance < 1 - thickness) 
                {
                    Console.SetCursorPosition(temp.CenterX + x * 2, temp.CenterY + y);
                    Console.Write(" "); 
                    Console.SetCursorPosition(temp.CenterX + x * 2+1, temp.CenterY + y);
                    Console.Write(" "); 
                    Console.SetCursorPosition(temp.CenterX + x * 2 - 1, temp.CenterY + y);
                    Console.Write(" "); 
                }
            }
        }
    }
    public void DrawTriangle(int a )
    {

        if (a < 3 ||a>20) return;
        double thickness = 0.1; 
        Triangle temp = new Triangle();
        temp.CenterX = 120;
        temp.CenterY = 32;
        temp.radius = a;
        temp.Color = 0;
        list.Add(temp);
        int height = a;
        
        for (int y = 0; y < height*2; y++) 
        {
            int startX = temp.CenterX - y; 
            int endX = temp.CenterX + y;   
            int currentY = temp.CenterY - height + y; 

            
            if (y == 0)
            {
                Console.SetCursorPosition(temp.CenterX, currentY);
                Console.Write(".");
            }
            
            else if (y == 2*height - 1)
            {
                for (int x = startX; x <= endX; x++)
                {
                    Console.SetCursorPosition(x, currentY);
                    Console.Write(".");
                }
            }
           
            else
            {
                Console.SetCursorPosition(startX, currentY);
                Console.Write(".");

                Console.SetCursorPosition(endX, currentY);
                Console.Write(".");
            }

        }
        for (int y = 1; y < 2*a - 1; y++)
        {
            int startX = temp.CenterX - y + 1; 
            int endX = temp.CenterX + y - 1;   
            int currentY = temp.CenterY - height + y; 
            int smech = 0;

            for (int x = startX; x <= endX; x++)
            {
                Console.SetCursorPosition(x, currentY);
                Console.Write(" "); 
            }
        }
    }
    public void DrawTriangleRect(int a,int b)
    {
        if (a < 3 || b < 3 || b>20 || a>20) return;
        TriangleRect temp = new TriangleRect();
        temp.CenterX = 120;
        temp.CenterY = 32;
        temp.Color = 0;
        temp.a = a;
        temp.b = b;
        list.Add(temp);
        int startX = temp.CenterX - b; 
        int startY = temp.CenterY - a; 

        for (int y = 0; y < a*2; y++)
        {
            int currentWidth = (y * b) / a; 

            for (int x = 0; x <= currentWidth*2; x++)
            {
                int drawX = startX + x;
                int drawY = startY + y;

                
                if (x == 0 || x == currentWidth*2 || y == a*2 - 1)
                {
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(".");
                }
                else
                {
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(" ");
                }
            }
        }
    }
    public void DrawRectangle(int a,int b)
    {
        if (a < 3 || b < 3 || a > 20 || b > 20) return;
        Rectangle temp = new Rectangle();
        temp.CenterX = 120;
        temp.CenterY = 32;
        temp.Color = 0;
        temp.a = a;
        temp.b = b;
        list.Add(temp);
        int startX = temp.CenterX - b; 
        int startY = temp.CenterY - a; 

        for (int y = 0; y < a * 2; y++)
        {
           
            for (int x = 0; x < b * 4; x++)
            {
                int drawX = startX + x;
                int drawY = startY + y;

                if (x == 0 || y == a * 2 - 1 || x==4*b-1 || y==0)
                {
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(".");
                }
                else
                {
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(" ");
                }
            }
        }
    }
 
    public void undraw(int param,bool inner_check)
    {
        if(param>list.Count()-1)
        {
           
            return;

        }
        IFigure tmp = list[param];
        if (tmp is Circle t)
        {
            double thickness = 0.2;
            for (int y = -t.radius; y <= t.radius; y++)
            {
                for (int x = -t.radius; x <= t.radius; x++)
                {
                    double distance = Math.Sqrt(x * x + y * y);
                    double difference = Math.Abs(distance - t.radius); 

                    if (difference < thickness) 
                    {
                        Console.SetCursorPosition((t.CenterX + x * 2), t.CenterY + y);
                        Console.Write(" ");
                    }
                    else if (distance < t.radius - thickness) 
                    {
                        Console.SetCursorPosition((t.CenterX + x * 2), t.CenterY + y);
                        Console.Write(" "); 
                        Console.SetCursorPosition((t.CenterX + x * 2 + 1), t.CenterY + y);
                        Console.Write(" "); 
                        Console.SetCursorPosition((t.CenterX + x * 2 - 1), t.CenterY + y);
                        Console.Write(" "); 
                    }

                }
            }
            
        }
        if (tmp is Triangle tr)
        {
            for (int y = 0; y < tr.radius*2; y++) 
            {
                int startX = tmp.CenterX - y; 
                int endX = tmp.CenterX + y;   
                int currentY = tmp.CenterY - tr.radius + y; 

                
                if (y == 0)
                {
                    Console.SetCursorPosition(tmp.CenterX, currentY);
                    Console.Write(" ");
                }
               
                else if (y == 2*tr.radius - 1)
                {
                    for (int x = startX; x <= endX; x++)
                    {
                        Console.SetCursorPosition(x, currentY);
                        Console.Write(" ");
                    }
                }
                     
                else
                {
                    Console.SetCursorPosition(startX, currentY);
                    Console.Write(" ");

                    Console.SetCursorPosition(endX, currentY);
                    Console.Write(" ");
                }

            }
            for (int y = 1; y < 2 * tr.radius - 1; y++) 
            {
                int startX = tmp.CenterX - y + 1; 
                int endX = tmp.CenterX + y - 1;   
                int currentY = tmp.CenterY - tr.radius + y; 

                for (int x = startX; x <= endX; x ++)
                {
                    Console.SetCursorPosition(x, currentY);
                    Console.Write(" ");
                   
                }
            }
            
        }
        if (tmp is TriangleRect trRect)
        {
            int startX = trRect.CenterX - trRect.b; 
            int startY = trRect.CenterY - trRect.a; 
                                                    
            for (int y = 0; y < 2*trRect.a; y++)
            {
                int currentWidth = (y * trRect.b) / trRect.a; 

                for (int x = 0; x <= currentWidth * 2; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;

                    
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(" ");
                }
            }
        }
        if(tmp is Ellipse el)
        {
            double thickness = 0.1; 

            for (int y = -el.r1; y <= el.r1; y++)
            {
                for (int x = -el.r2; x <= el.r2; x++)
                {
                    
                    double distance = Math.Pow(x / (double)el.r2, 2) + Math.Pow(y / (double)el.r1, 2);

                    
                    
                    
                        Console.SetCursorPosition(el.CenterX + x * 2, el.CenterY + y);
                        Console.Write(" "); 
                    Console.SetCursorPosition(el.CenterX + x * 2+1, el.CenterY + y);
                    Console.Write(" "); 
                    Console.SetCursorPosition(el.CenterX + x * 2 - 1, el.CenterY + y);
                    Console.Write(" "); 
                }
            }
        }
        if(tmp is Rectangle rec)
        {
            int startX = tmp.CenterX - rec.b; 
            int startY = tmp.CenterY - rec.a; 

            for (int y = 0; y < rec.a * 2; y++)
            {
                

                for (int x = 0; x < rec.b * 4; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;

                    
                    
                    
                        Console.SetCursorPosition(drawX, drawY);
                        Console.Write(" ");
                    
                }
            }
        }
        if (!inner_check) list.RemoveAt(param);
        if (!inner_check)
        {
            for (int i = 0; i <list.Count(); ++i)
            {
                Redraw(list[i]);
            }
        }
    }
    public void Redraw(IFigure temp)
    {
        if (list.Count() < 0) return;
        if (temp is Circle t)
        {
            double thickness = 0.2;
            for (int y = -t.radius; y <= t.radius; y++)
            {
                for (int x = -t.radius; x <= t.radius; x++)
                {
                    double distance = Math.Sqrt(x * x + y * y); 
                    double difference = Math.Abs(distance - t.radius); 

                    if (difference < thickness) 
                    {
                        Console.SetCursorPosition((t.CenterX + x * 2), t.CenterY + y);
                        Console.Write(".");
                    }
                    else if (distance < t.radius - thickness)                     {
                        Console.SetCursorPosition((t.CenterX + x*2), t.CenterY + y);
                        if (t.Color == 0) Console.Write(" ");
                        if (t.Color == 1) Console.Write("/"); 
                        if (t.Color == 2) Console.Write("#");
                        Console.SetCursorPosition((t.CenterX + x * 2+1), t.CenterY + y);
                        if (t.Color == 0) Console.Write(" ");
                        if (t.Color == 1) Console.Write("/"); 
                        if (t.Color == 2) Console.Write("#");
                        Console.SetCursorPosition((t.CenterX + x * 2-1), t.CenterY + y);
                        if (t.Color == 0) Console.Write(" ");
                        if (t.Color == 1) Console.Write("/"); 
                        if (t.Color == 2) Console.Write("#");

                    }

                }
            }
        }
       if(temp is Triangle tr)
        {
            for (int y = 0; y < tr.radius*2; y++) 
            {
                int startX = temp.CenterX - y; 
                int endX = temp.CenterX + y;   
                int currentY = temp.CenterY - tr.radius + y; 

                
                if (y == 0)
                {
                    Console.SetCursorPosition(temp.CenterX, currentY);
                    Console.Write(".");
                }
                
                else if (y == 2*tr.radius - 1)
                {
                    for (int x = startX; x <= endX; x++)
                    {
                        Console.SetCursorPosition(x, currentY);
                        Console.Write(".");
                    }
                }
                
                else
                {
                    Console.SetCursorPosition(startX, currentY);
                    Console.Write(".");

                    Console.SetCursorPosition(endX, currentY);
                    Console.Write(".");
                }

            }
            for (int y = 1; y < 2*tr.radius - 1; y++) 
            {
                int startX = temp.CenterX - y + 1; 
                int endX = temp.CenterX + y - 1;   
                int currentY = temp.CenterY - tr.radius + y; 
              

                for (int x = startX; x <= endX; x++) 
                {
                    Console.SetCursorPosition(x, currentY);
                    if (tr.Color == 0) Console.Write(" ");
                    if (tr.Color == 1) Console.Write("/"); 
                    if (tr.Color == 2) Console.Write("#");
                }
            }

        }
        if (temp is TriangleRect trRect)
        {
            int startX = trRect.CenterX - trRect.b; 
            int startY = trRect.CenterY - trRect.a; 
            for (int y = 0; y < 2*trRect.a; y++)
            {
                int currentWidth = (y * trRect.b) / trRect.a; 

                for (int x = 0; x <= currentWidth*2; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;

                    if (!(x == 0 || x == currentWidth*2 || y == 2*trRect.a - 1))
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        if (trRect.Color == 0) Console.Write(" ");
                        if (trRect.Color == 1) Console.Write("/"); 
                        if (trRect.Color == 2) Console.Write("#");
                    }
                    else
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        Console.Write(".");
                    }
                }
            }
        }
        if (temp is Ellipse el)
        {
            double thickness = 0.1; 

            for (int y = -el.r1; y <= el.r1; y++)
            {
                for (int x = -el.r2; x <= el.r2; x++)
                {
                    double distance = Math.Pow(x / (double)el.r2, 2) + Math.Pow(y / (double)el.r1, 2);

                    if (Math.Abs(distance - 1) < thickness) 
                    {
                        Console.SetCursorPosition(el.CenterX + x * 2, el.CenterY + y);
                        Console.Write(".");
                    }
                    else if (distance < 1 - thickness) 
                    {
                        Console.SetCursorPosition(el.CenterX + x * 2, el.CenterY + y);
                        if (el.Color == 0) Console.Write(" ");
                        if (el.Color == 1) Console.Write("/"); 
                        if (el.Color == 2) Console.Write("#");
                        Console.SetCursorPosition(el.CenterX + x * 2-1, el.CenterY + y);
                        if (el.Color == 0) Console.Write(" ");
                        if (el.Color == 1) Console.Write("/");
                        if (el.Color == 2) Console.Write("#");
                        Console.SetCursorPosition(el.CenterX + x * 2 + 1, el.CenterY + y);
                        if (el.Color == 0) Console.Write(" ");
                        if (el.Color == 1) Console.Write("/");
                        if (el.Color == 2) Console.Write("#");
                    }
                }
            }
        }
        if(temp is Rectangle rec)
        {
            int startX = temp.CenterX - rec.b; 
            int startY = temp.CenterY - rec.a; 

            for (int y = 0; y < rec.a * 2; y++)
            {
                

                for (int x = 0; x < rec.b * 4; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;

                    
                    if (x == 0 || y == rec.a * 2 - 1 || x == 4 * rec.b - 1 || y == 0)
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        Console.Write(".");
                    }
                    else
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        if (rec.Color == 0) Console.Write(" ");
                        if (rec.Color == 1) Console.Write("/");
                        if (rec.Color == 2) Console.Write("#");
                    }
                }
            }
        }


    }
    public string Showinfo(ref int k)
    {
        if (list.Count() == 0) return "";
        string txt = "";
        for(int i=0;i<list.Count();++i)
        {
            if (list[i] is Circle t)
            {
                Console.Write($"{i}. - Круг {t.radius} ");
                txt += $"{i}. - Круг {t.radius} ";
            }
            if (list[i] is Triangle tr)
            {
                Console.Write($"{i}. - Треугольник {tr.radius} ");
                txt += $"{i}. - Треугольник {tr.radius} ";
            }
            if (list[i] is TriangleRect trRec)
            {
                Console.Write($"{i}. - Треугольник прямоугольный  {trRec.a} {trRec.b} ");
                txt += $"{i}. - Треугольник прямоугольный  {trRec.a} {trRec.b} ";
            }
            if (list[i] is Ellipse el)
            {
                Console.Write($"{i}. - Эллипс {el.r1} {el.r2} ");
                txt += $"{i}. - Эллипс {el.r1} {el.r2} ";
            }

            if (list[i] is Rectangle rect)
            {
                Console.Write($"{i}. - Прямоугльник  {rect.a} {rect.b} ");
                txt += $"{i}. - Прямоугльник  {rect.a} {rect.b} ";
            }

        }
        k = list.Count();


        Console.WriteLine("");
        return txt;
        
    }
    public bool CheckTransposition(int transpos, bool param)
    {
        if (list.Count() == 0 || Math.Abs(transpos) > 1) return false;
        for (int i = 0; i < list.Count(); ++i)
        {
            if (list[i] is Circle t)
            {
                if (param)
                {
                    if (t.CenterX - t.radius * 2 + transpos <= 3 || t.CenterX + t.radius * 2 + transpos >= 190) return false;
                }
                else
                {
                    if (t.CenterY - t.radius + transpos < 12 || t.CenterY + t.radius + transpos > 43) return false;
                }
            }
            if (list[i] is Triangle tr)
            {
                if (param)
                {
                    if (tr.CenterX - 2*tr.radius+1 <= 3 || tr.CenterX +tr.radius * 2-1 + transpos >= 170) return false;
                }
                else
                {
                    if (tr.CenterY-tr.radius + transpos < 12 || tr.CenterY + tr.radius -1 + transpos > 40) return false;
                }
            }
            if (list[i] is TriangleRect trRect)
            {
                
                if (param)
                {
                    if (trRect.CenterX - trRect.b <= 3 || trRect.CenterX + trRect.b * 2 - 1 + transpos >= 170) return false;
                }
                else
                {
                    if (trRect.CenterY - trRect.a + transpos <= 12 || trRect.CenterY + trRect.a -1 + transpos >40) return false;
                }
            }
            if (list[i] is Ellipse el)
            {
                
                if (param)
                {
                    if (el.CenterX - el.r2*2 <= 3 || el.CenterX + el.r2 * 2 - 1 + transpos >= 170) return false;
                }
                else
                {
                    if (el.CenterY - el.r1 + transpos <= 12 || el.CenterY + el.r1 - 1 + transpos > 40) return false;
                }
            }

            if (list[i] is Rectangle rect)
            {
                
                if (param)
                {
                    if (rect.CenterX - rect.b <= 3 || rect.CenterX + rect.b * 3 - 1 + transpos >= 170) return false;
                }
                else
                {
                    if (rect.CenterY - rect.a + transpos <= 12 || rect.CenterY + rect.a - 1 + transpos > 40) return false;
                }

            }

        }
        return true;
    }
    public void Change(int param)
    {
        if (param > list.Count() - 1)
        {

            return;

        }
        string text = "Выберите, в какую сторону сдвинуть фигуру: 0-left, 1-right, 2-up, 3-down, -1 - выход";
        Console.WriteLine("Выберите, в какую сторону сдвинуть фигуру: 0-left, 1-right, 2-up, 3-down, -1 - выход");
        IFigure tmp = list[param];
        list.RemoveAt(param);
        list.Add(tmp);
        Checker checkReg=new Checker();
        while (true)
        {
            Console.SetCursorPosition(0, 6);
            int choise=checkReg.CheckWithBorders(Console.ReadLine(),-1,3,text);
            Console.SetCursorPosition(0, 6);
            Console.Write("\r"); 
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, 6);
            
            if (choise == -1) break;
            undraw(list.Count() - 1, true);

            if (choise == 0)
            {
                if (!CheckTransposition(-1, true)) Redraw(list[list.Count() - 1]);
                else
                {
                    list[list.Count() - 1].CenterX -= 1;
                    for (int i = 0; i < list.Count(); ++i)
                    {
                        Redraw(list[i]);
                    }
                }
            }
            if (choise == 1)
            {
                if (!CheckTransposition(+1, true)) Redraw(list[list.Count() - 1]);
                else
                {
                    list[list.Count() - 1].CenterX += 1;
                    for (int i = 0; i < list.Count(); ++i)
                    {
                        Redraw(list[i]);
                    }
                }
            }
            if (choise == 2)
            {
                if (!CheckTransposition(-1, false)) Redraw(list[list.Count() - 1]);
                else
                {
                    list[list.Count() - 1].CenterY -= 1;
                    for (int i = 0; i < list.Count(); ++i)
                    {
                        Redraw(list[i]);
                    }
                }
            }
            if (choise == 3)
            {
                if (!CheckTransposition(1, false)) Redraw(list[list.Count() - 1]);
                else
                {
                    list[list.Count() - 1].CenterY += 1;
                    for (int i = 0; i < list.Count(); ++i)
                    {
                        Redraw(list[i]);
                    }
                }
            }
            
        }
        

    }
    public void DrawCanva(List<IFigure> list_in)
    {

        for (int i = 10; i < 45; ++i)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("\r"); 
            Console.Write(new string(' ', Console.WindowWidth)); 
        }
        list = list_in;
        for (int i = 0; i < list.Count(); ++i)
        {
            Redraw(list[i]);
        }
    }
    public void Fill(int param,int color)
    {
        if (param > list.Count() - 1||color>2||color<0)
        {

            return;

        }
        IFigure tmp = list[param];
        
        

        
        if (tmp is Circle t)
        {
            t.Color = color;    
            double thickness = 0.2;
            
            for (int y = -t.radius; y <= t.radius; y++)
            {
                for (int x = -t.radius; x <= t.radius; x++)
                {
                    double distance = Math.Sqrt(x * x + y * y); 
                    double difference = Math.Abs(distance - t.radius); 

                    if (distance < t.radius - thickness) 
                    {
                        Console.SetCursorPosition((t.CenterX + x*2-1), t.CenterY + y);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); 
                        if (color == 2) Console.Write("#");
                        Console.SetCursorPosition((t.CenterX + x*2), t.CenterY + y);
                        if (color ==0 ) Console.Write(" ");
                        if (color==1)Console.Write("/"); 
                        if (color == 2) Console.Write("#");
                        Console.SetCursorPosition((t.CenterX + x * 2+1), t.CenterY + y);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); 
                        if (color == 2) Console.Write("#");
                    }
                }
            }
            
        }
        if (tmp is Triangle tr)
        {
            tr.Color = color;
            for (int y = 1; y < 2 * tr.radius -1; y++) 
            {
                int startX = tmp.CenterX - y + 1; 
                int endX = tmp.CenterX + y - 1;   
                int currentY = tmp.CenterY - tr.radius + y; 

                for (int x = startX; x <= endX; x++) 
                {
                    Console.SetCursorPosition(x, currentY);
                    if (color == 0) Console.Write(" ");
                    if (color == 1) Console.Write("/"); 
                    if (color == 2) Console.Write("#");
                }
            }
        }
        if(tmp is TriangleRect t_rect)
        {
            int startX = t_rect.CenterX - t_rect.b; 
            int startY = t_rect.CenterY - t_rect.a; 
            t_rect.Color = color;
            for (int y = 0; y <2* t_rect.a; y++)
            {
                int currentWidth = (y * t_rect.b) / t_rect.a; 

                for (int x = 0; x <= currentWidth*2; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;
                    if (!(x == 0 || x == currentWidth*2 || y == 2*t_rect.a - 1))
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); 
                        if (color == 2) Console.Write("#");
                    }
                }
            }
        }
        if(tmp is Ellipse el)
        {
            el.Color = color;
            double thickness = 0.1; 
            for (int y = -el.r1; y <= el.r1; y++)
            {
                for (int x = -el.r2; x <= el.r2; x++)
                {
                    
                    double distance = Math.Pow(x / (double)el.r2, 2) + Math.Pow(y / (double)el.r1, 2);

                    
                    if (distance < 1 - thickness) 
                    {
                        Console.SetCursorPosition(el.CenterX + x * 2, el.CenterY + y);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); 
                        if (color == 2) Console.Write("#");
                        Console.SetCursorPosition(el.CenterX + x * 2-1, el.CenterY + y);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); 
                        if (color == 2) Console.Write("#");
                        Console.SetCursorPosition(el.CenterX + x * 2 + 1, el.CenterY + y);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); 
                        if (color == 2) Console.Write("#");
                    }
                }
            }
        }
        if (tmp is Rectangle rec)
        {
            rec.Color = color;
            int startX = tmp.CenterX - rec.b; 
            int startY = tmp.CenterY - rec.a; 

            for (int y = 0; y < rec.a * 2; y++)
            {
                

                for (int x = 0; x < rec.b * 4; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;

                    
                    if (!(x == 0 || y == rec.a * 2 - 1 || x == 4 * rec.b - 1 || y == 0))
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        if (rec.Color == 0) Console.Write(" ");
                        if (rec.Color == 1) Console.Write("/");
                        if (rec.Color == 2) Console.Write("#");
                    }
                    
                }
            }
        }
        list.RemoveAt(param);
        list.Add(tmp);
    }
    public List<IFigure> GetData()
    {
        List<IFigure> tmp = list;
        return tmp.ToList();
    }
};

