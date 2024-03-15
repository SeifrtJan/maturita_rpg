using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maturita_rpg
{
    internal class Coord
    {
        public int y;
        public int x;
        public bool isWall;

        public Coord(int y, int x, bool isWall)
        {
            this.y = y;
            this.x = x;
            this.isWall = isWall;
        }
    }
}
