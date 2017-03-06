
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeII
{
    [Serializable]
    public class Game
    {
        public Worm worm;
        public Wall wall;
        public Food food;

        public void CanEat()
        {
            if (worm.points[0].Equals(food.location))
            {

                Console.Clear();

                worm.points.Add(food.location);
                food = new Food();
                food.WhereisFood(wall,worm);
                Global.score += 10;
                wall.Draw();
                worm.Draw();
                food.Draw();
                Console.SetCursorPosition(wall.points[wall.points.Count - 1].x, wall.points[wall.points.Count - 1].y);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  Your points:");
                Console.WriteLine(Global.score);
                Console.WriteLine("  Your level:");
                Console.WriteLine(Global.level);

            }
        }


        public bool isDead()
        {

            for (int i = 0; i < wall.points.Count; i++)
            {
                if (worm.points[0].Equals(wall.points[i]))
                {
                    return true;
                }
            }

            for (int i = 1; i < worm.points.Count; i++)
            {
                if (worm.points[0].Equals(worm.points[i]))
                {
                    return true;
                }
            }

            return false;
        }

            static void SerializeWorm(Worm worm)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var fStream = new FileStream(@"C:\Users\Lenovo\Desktop\мой новый сайт\dat.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                formatter.Serialize(fStream, worm);
            }
        }
        static Worm DeserializeWorm()
        {
            using (var fStream = File.OpenRead(@"C:\Users\Lenovo\Desktop\мой новый сайт\dat.dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Worm newsavedworm = (Worm)formatter.Deserialize(fStream);
                return newsavedworm;
            }
        }
        static void SerializeLevel()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var fStream = new FileStream(@"C:\Users\Lenovo\Desktop\Tor Browser\dat.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                formatter.Serialize(fStream, Global.level);
            }
        }
        static int DeserializeLevel()
        {
            using (var fStream = File.OpenRead(@"C:\Users\Lenovo\Desktop\Tor Browser\dat.dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                int k = (int)formatter.Deserialize(fStream);
                return k;
            }
        }
        static void SerializeFood(Food food)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var fStream = new FileStream(@"C:\Users\Lenovo\Desktop\work\dat.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                formatter.Serialize(fStream, food);
            }
        }
        static Food DeserializeFood()
        {
            using (var fStream = File.OpenRead(@"C:\Users\Lenovo\Desktop\work\dat.dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
               Food food = (Food)formatter.Deserialize(fStream);
                return food;
            }
        }
        public void Continue()
        {
            Console.Clear();
            worm = DeserializeWorm();
            food = DeserializeFood();
            wall = new Wall(DeserializeLevel()); 
      

            food.Draw();
            wall.Draw();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" Your points:");
            Console.WriteLine(Global.score);
            Console.WriteLine(" Your level:");
            Console.WriteLine(Global.level);
            Thread t2 = new Thread(new ThreadStart(worm.Move));
            t2.Start();

            while (worm.isAlive)
            {

                ConsoleKeyInfo pressedKey = Console.ReadKey();
              
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        worm.dx = 0;
                        worm.dy = -1;
                        break;
                    case ConsoleKey.DownArrow:
                        worm.dx = 0;
                        worm.dy = 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        worm.dx = -1;
                        worm.dy = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        worm.dx = 1;
                        worm.dy = 0;
                        break;
                    case ConsoleKey.Escape:
                        worm.isAlive = false;
                        break;
                    case ConsoleKey.Spacebar:
                        SerializeFood(food);
                        SerializeWorm(worm);
                        SerializeLevel();
                        break;
                    case ConsoleKey.F2:
                        worm = null;
                        t2.Abort();
                        this.Continue();
                        break;
                }

               
                if (worm.points.Count > 3)
                {
                    Global.level++;


                    Global.score += 10;
                    wall.Clear();
                    worm.Clear();
                    for (int i = 1; i < worm.points.Count; i++)
                    {
                        worm.points.Remove(worm.points[i]);
                    }
                    worm.Draw();
                    wall = new Wall(Global.level);
                    wall.Draw();

                   
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("  Your points:");
                    Console.WriteLine(Global.score);
                    Console.WriteLine("  Your level:");
                    Console.WriteLine(Global.level);
                }

            }
            if (!worm.isAlive)
            {
                Global.level = 1;
                Global.score = 0;
                Console.WriteLine("Start new game?");
                Console.WriteLine("yes - 2");
                string opt = Console.ReadLine();
                if (opt == "2")
                {
                    Game game2 = new Game();
                    Console.Clear();
                    game2.Start();
                }
            }

        }
        public void Start()
        {

            worm = new Worm();
            worm.AttachGameLink(this);

            wall = new Wall(Global.level);
            food = new Food();

            food.WhereisFood(wall, worm);
            food.Draw();
            wall.Draw();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" Your points:");
            Console.WriteLine(Global.score);
            Console.WriteLine(" Your level:");
            Console.WriteLine(Global.level);
            Thread t = new Thread(new ThreadStart(worm.Move));
            t.Start();

            while (worm.isAlive)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        worm.dx = 0;
                        worm.dy = -1;
                        break;
                    case ConsoleKey.DownArrow:
                        worm.dx = 0;
                        worm.dy = 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        worm.dx = -1;
                        worm.dy = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        worm.dx = 1;
                        worm.dy = 0;
                        break;
                    case ConsoleKey.Escape:
                        worm.isAlive = false;
                        break;
                    case ConsoleKey.Spacebar:
                        SerializeFood(food);
                        SerializeWorm(worm);
                        SerializeLevel();
                        break;
                    case ConsoleKey.F2:
                        worm = null;
                        t.Abort();
                        
                        this.Continue();
                        break;
                }

                if (worm.points.Count > 6)
                {
                    Global.level++;


                    Global.score += 10;
                    wall.Clear();
                    worm.Clear();
                    for (int i = 1; i < worm.points.Count; i++)
                    {
                        worm.points.Remove(worm.points[i]);
                    }
                    worm.Draw();
                    wall = new Wall(Global.level);
                    wall.Draw();


                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("  Your points:");
                    Console.WriteLine(Global.score);
                    Console.WriteLine("  Your level:");
                    Console.WriteLine(Global.level);
                }
               
            }
            if (!worm.isAlive)
            {
                Global.level = 1;
                Global.score = 0;
                Console.WriteLine("Start new game?");
                Console.WriteLine("yes - 2");
                string opt = Console.ReadLine();
                if (opt == "2")
                {
                    Game game2 = new Game();
                    Console.Clear();
                    game2.Start();
                }
            }
        }
    }
}
