using System;
using System.Collections.Generic;
using System.Text;

namespace Inheritance
{
    internal class Goblin : Enemy
    {
        public string Message { get; set; } = "I'm a goblin!";

        public Goblin(int maxHealth) : base(maxHealth)
        {
            

        }

    }
}
