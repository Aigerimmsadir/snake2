using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SnakeII
{[Serializable]
    public abstract class GameObject : IDrawable
    {
        public List<Point> points = new List<Point>();
        public char sign;

        public void Clear()
        {
            for (int i = 0; i < points.Count; ++i)
            {
                Console.SetCursorPosition(points[i].x, points[i].y);
                Console.Write(' ');
            }
        }
        public void Draw()
        {
         
            if (this.GetType() == typeof(Wall)) { 
                for (int i = 0; i < points.Count; ++i)
                {
                    Console.SetCursorPosition(points[i].x, points[i].y);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(sign);
                }
            }
            else
            {
                Console.SetCursorPosition(points[0].x, points[0].y);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write('☺');
                for (int i = 1; i < points.Count; ++i)
                {
                    Console.SetCursorPosition(points[i].x, points[i].y);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(sign);
                }
            }
        }  
    

        public void Save()
        {
            XmlSerializer xs = new XmlSerializer(this.GetType());
            using (FileStream fs = new FileStream(string.Format("{0}.xml", this.GetType().Name), FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                xs.Serialize(fs, this);
            }
        }
    }
}
