﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maturita_rpg
{
    internal class Chest : GameObject
    {
        public Item content;
        public bool looted;
        public Chest(int y, int x) : base(y, x)
        {
            charToPrint = 'C';
        }

        public override void TakeEffect(Game game)
        {
            if (looted == false)
            {
                game.player.inventory.Add(content);
                content.game = game;
                game.WriteIntoActionText("You looted a chest and got " + content.name);

                looted = true;
                game.currentMap.walls[y, x].isWall = true;
                game.PrintPlayerInfo();
            }
        }
    }
}
