using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maturita_rpg
{
    class Door : GameObject
    {
        public bool open;
        public Door twinDoor;
        public Map entredMap;

        public Door(int y, int x) : base(y, x)
        {
            this.open = true;
            charToPrint = '%';
        }

        public override void TakeEffect(Game game)
        {
            game.currentMap = entredMap;
            game.player.y = twinDoor.y;
            game.player.x = twinDoor.x;
            game.UpdateMapView();
            game.PrintCurrentArea();            
        }
    }
}
