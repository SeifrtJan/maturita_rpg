namespace maturita_rpg
{
    internal class Enemy : Character
    {
        public Enemy(string name, int hp, int damage, int armor, Game game) : base(name, hp, damage, armor, game)
        {
        }

        //called each turn of combat
        public override void TakeTurn(Character player)
        {
            player.TakeDamage(damage); //deals damage to player
            game.PrintPlayerInfo();
            game.WriteIntoCombatText(" ");
        }

        //checks if enemy is dead
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

        //prints enemy info in combat
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
