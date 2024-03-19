using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maturita_rpg
{
    internal class Enemy : Character
    {
        public Enemy(string name, int hp, int damage, int armor, Game game) : base(name, hp, damage, armor, game)
        {
        }

        public override void TakeTurn(Character player)
        {
            player.TakeDamage(damage);
            game.WriteIntoCombatText(" ");
        }

        public override bool IsDead()
        {
            if (hp <= 0)
            {
                game.WriteIntoCombatText("You have defeated " + name);
                game.WriteIntoActionText("You have defeated " + name);

                return true;
            }
            return false;   
        }

        public void PrintInfo()
        {
            game.PrintBorders();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(game.mapOffsetLeft + 1, game.mapOffsetTop - 1);
            Console.Write("{0} (hp:{1})", name, hp);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
