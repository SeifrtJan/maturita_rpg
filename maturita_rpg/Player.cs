namespace maturita_rpg
{
    internal class Player : Character
    {
        public int y;
        public int x;
        public int totalDamage;
        public List<Item> inventory;
        public Weapon equipedWeapon;
        public Armor equipedArmor;
        public int maxHP;

        public Player(int y, int x, string name, int hp, int damage, int armor, Game game) : base(name, hp, damage, armor, game)
        {
            this.y = y;
            this.x = x;
            inventory = new List<Item>();
            equipedArmor = new Armor("none", "", 0);
            equipedWeapon = new Weapon("none", "", 0);
            totalDamage = damage;
            maxHP = hp;
        }

        public void UpdateStats()
        {
            totalDamage = damage + equipedWeapon.damage;
            armor = equipedArmor.armor;
        }

        //handles attack choice, calculates attack damage
        private int AttackDamage()
        {
            int attackDamage = 0;

            while (attackDamage == 0)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(intercept: true);
                if (keyPressed.Key == ConsoleKey.D1 || keyPressed.Key == ConsoleKey.NumPad1) //normal attack 
                {
                    attackDamage = totalDamage;
                }
                else if (keyPressed.Key == ConsoleKey.D2 || keyPressed.Key == ConsoleKey.NumPad2) //special attack - chance for crit
                {
                    hp -= 5;
                    game.PrintPlayerInfo();
                    if (game.rng.Next(4) == 3) //critical
                    {
                        attackDamage = totalDamage * (game.rng.Next(25, 80) / 10); //critical ranges from 2,5 to 8 times totalDamage
                    }
                    else
                        attackDamage = totalDamage;
                }
            }        

            return attackDamage;

        }

        public override void TakeTurn(Character opponent)
        {            
            game.WriteIntoCombatText("1 => basic attack, 2 => chance for crit (-5 HP)");

            Enemy enemy = opponent as Enemy;
            enemy.TakeDamage(AttackDamage()); //this is where the player chooses attacks
            enemy.PrintInfo();
        }

        public override bool IsDead()
        {
            return hp <= 0;
        }

    }
}
