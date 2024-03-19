using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maturita_rpg
{
    class Door : GameObject
    {
        public Door twinDoor;
        public Map enteredMap;

        public Door(int y, int x) : base(y, x)
        {
            charToPrint = '%';
        }

        public override void TakeEffect(Game game)
        {
            game.currentMap = enteredMap;
            game.player.y = twinDoor.y;
            game.player.x = twinDoor.x;

            game.UpdateMapView();
            game.PrintCurrentArea();            
        }
    }
}
