using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maturita_rpg
{
    internal class WinObject : GameObject
    {
        public WinObject(int y, int x) : base(y, x)
        {
            charToPrint = '%';
        }

        public override void TakeEffect(Game game)
        {
            game.gameWon = true;
        }
    }
}
