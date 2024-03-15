﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace maturita_rpg
{
    abstract class Character
    {
        public string name;
        public int hp;
        public int damage;
        public int armor;
        public Game? game;

        protected Character(string name, int hp, int damage, int armor, Game? game)
        {
            this.name = name;
            this.hp = hp;
            this.damage = damage;
            this.armor = armor;
            this.game = game;
        }

        public void TakeDamage(int attackDamage)
        {
            int damageTaken = attackDamage - armor;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            if (damageTaken > hp)
                hp = 0;
            else
                hp = hp - damageTaken;

            game.WriteIntoCombatText(name + " has taken " + damageTaken + " dmg. Their hp is now " + hp);
        }

        abstract public void TakeTurn(Character enemy);

        abstract public bool IsDead();

    }
}
