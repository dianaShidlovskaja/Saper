using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Classes
{
    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Bomb { get; set; }
        public int Number { get; set; }
        public bool Click { get; set; } 

        public Field(int x, int y, bool bomb)
        {
            X = x;
            Y = y;
            Bomb = bomb;
        }
    }
}
