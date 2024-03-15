using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maturita_rpg
{
    internal class HealingItem : Item
    {
        public int healAmount;

        public HealingItem(int healAmount, string name, string description) : base(name, description)
        {
            this.healAmount = healAmount;
        }

        public override void Equip()
        {
            game.player.hp =+ healAmount;

            game.WriteIntoActionText("You used the " + name + ". Your hp is now " + game.player.hp);
            game.PrintPlayerInfo();
        }

        public override void PrintInfo()
        {
            game.WriteIntoActionText(name + ":" + description);
        }
    }
}
