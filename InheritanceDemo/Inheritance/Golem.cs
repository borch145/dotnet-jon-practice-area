using System;
using System.Collections.Generic;
using System.Text;

namespace Inheritance
{
    internal class Golem : Enemy
    {
        public string Message = "I'm Hard.";
        public Golem(int maxHealth) : base(maxHealth)
        {

        }
    }
}
