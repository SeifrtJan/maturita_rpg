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

        public HealingItem(string name, string description, int healAmount) : base(name, description)
        {
            this.healAmount = healAmount;
        }

        public override void Equip()
        {
            game.player.hp = game.player.hp + healAmount;
            game.player.inventory.Remove(this);
            game.selectedItemIndex--;
            game.PrintInventory();

            game.WriteIntoActionText("You used the " + name + ". Your hp is now " + game.player.hp);
            game.PrintPlayerInfo();
        }

        public override void PrintInfo()
        {
            game.WriteIntoActionText(name + ":" + description);
        }
    }
}
