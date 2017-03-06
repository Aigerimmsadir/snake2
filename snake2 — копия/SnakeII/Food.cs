using SnakeII;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeII
{
    [Serializable]
    public class Food
    {
     

        public Point location;
        public char sign = '♥';
        public void WhereisFood(Wall wall, Worm worm)
        {
            location = new Point(new Random().Next() % 30, new Random().Next() % 30);
            for (int i = 0; i < wall.points.Count; i++)
            {

                if (location.Equals(wall.points[i]))
                {
                    WhereisFood(wall, worm);
                }
            }
            for (int i = 0; i < worm.points.Count; i++)
            {
                if (location.Equals(worm.points[i]))
                {
                    WhereisFood(wall, worm);
                }
            }

        }
        public void Clear()
        {
            Console.SetCursorPosition(location.x, location.y);
            Console.Write(' ');

        }
        public void Draw()
        {
            Console.SetCursorPosition(location.x, location.y);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(sign);
        }
        
    }
}
