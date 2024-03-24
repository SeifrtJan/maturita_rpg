using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.damage = 10;
            totalDamage = damage;
            maxHP = hp;
        }

        public void UpdateStats()
        {
            totalDamage = damage + equipedWeapon.damage;
            armor = equipedArmor.armor;
        }

        private int AttackDamage()
        {
            int attackDamage = 0;

            while (attackDamage == 0)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(intercept: true);
                if (keyPressed.Key == ConsoleKey.D1) //normal attack 
                {
                    attackDamage = totalDamage;
                }
                else if (keyPressed.Key == ConsoleKey.D2) //special attack - chance for crit
                {
                    hp -= 5;
                    if (game.rng.Next(4) == 3) //critical
                    {
                        attackDamage = totalDamage * (game.rng.Next(25, 60) / 10);
                    }
                    else
                        attackDamage = totalDamage;
                }
            }        

            return attackDamage;

        }

        public override void TakeTurn(Character enemy)
        {            
            game.WriteIntoCombatText("normal => 1, chance for crit (-5 hp) => 2");

            enemy.TakeDamage(AttackDamage());
        }

        public override bool IsDead()
        {
            return hp <= 0;
        }

    }
}
