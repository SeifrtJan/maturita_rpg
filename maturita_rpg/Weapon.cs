using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maturita_rpg
{
    internal class Weapon : Item
    {
        public int damage;

        public Weapon(string name, string description, int damage) : base(name, description)
        {
            this.damage = damage;
        }

        public override void Equip()
        {
            game.player.equipedWeapon = this;

            game.WriteIntoActionText("You equiped " + name + " as your weapon.");
            game.player.UpdateStats();
            game.PrintPlayerInfo();
        }

        public override void PrintInfo()
        {
            game.WriteIntoActionText(name + ": " + description + " (" + damage + ").");
        }
    }
}
