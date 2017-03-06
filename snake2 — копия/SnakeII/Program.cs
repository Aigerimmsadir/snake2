using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SnakeII
{
    [Serializable]
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(40, 47);
            Console.SetBufferSize(40, 47);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Continue - 1");
            Console.WriteLine("New game - 2");
            Console.WriteLine("Pause - space");
            string option = Console.ReadLine();
            Game g = new Game();
            if (option == "2")
            {
                Console.Clear();
              
                g.Start();
              

            }
            else
            {
                Console.Clear();

              
                g.Continue();
            }
            }
        }
    }

