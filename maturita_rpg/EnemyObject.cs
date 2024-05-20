namespace maturita_rpg
{
    class EnemyObject : GameObject
    {
        public Enemy? enemy;
        public EnemyObject(int y, int x) : base(y, x)
        {
            charToPrint = '§';
        }

        public override void TakeEffect(Game game)
        {
            if (enemy.hp > 0)
            {
                game.Combat(enemy); //starts a fight
            }
            else
                game.WriteIntoActionText("You have defeated this enemy");
            
        }

    }
}
