namespace maturita_rpg
{
    abstract class Character
    {
        public string name;
        public int hp;
        public int damage;
        public int armor;
        public Game game; //could be protected

        protected Character(string name, int hp, int damage, int armor, Game game)
        {
            this.name = name;
            this.hp = hp;
            this.damage = damage;
            this.armor = armor;
            this.game = game;
        }

        public void TakeDamage(int attackDamage)
        {
            int damageTaken = (attackDamage + game.rng.Next(- (attackDamage / 10), attackDamage / 10)) - armor; // rng attack +- 10%

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }
            else if (damageTaken > hp)
                hp = 0;
            else
                hp = hp - damageTaken;

            game.WriteIntoCombatText(name + " has taken " + damageTaken + " dmg. Their hp is now " + hp);
        }

        abstract public void TakeTurn(Character enemy);

        abstract public bool IsDead();

    }
}
