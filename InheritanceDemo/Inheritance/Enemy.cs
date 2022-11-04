using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Inheritance
{
    internal class Enemy
    {
        public int MaxHealth    { get; set; }
        public int Health      { get; set; }

        public Enemy(int maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
        }   
    }
}
