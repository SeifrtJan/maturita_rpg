using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maturita_rpg
{
    internal class EnemyObject : GameObject
    {
        public Enemy? enemy;
        public EnemyObject(int y, int x) : base(y, x)
        {
            charToPrint = '§';
        }

        public override void TakeEffect(Game game)
        {
            game.EraseCombatText();
            if (enemy.hp > 0)
            {
                game.Combat(enemy);
            }
            else
                game.WriteIntoActionText("You have defeated this enemy");
            
        }

    }
}
