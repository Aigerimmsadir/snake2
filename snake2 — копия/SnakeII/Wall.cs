using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SnakeII
{[Serializable]
    public class Wall : GameObject
    {

        public Wall(int k)
        {
            this.sign = '#';

            string fname = string.Format(@"C:\Users\Lenovo\Desktop\Snake2\SnakeII\Level\leve{0}.txt", k);

            string line;
            using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    int colNumber = 0;
                    while ((line = sr.ReadLine()) != null)
                    {

                        for (int rowNumber = 0; rowNumber < line.Length; ++rowNumber)
                        {

                            if (line[rowNumber] == '#')
                            {
                                this.points.Add(new Point(rowNumber, colNumber));
                            }
                        }

                        colNumber++;
                    }
                }
            }
        }


    }
}
