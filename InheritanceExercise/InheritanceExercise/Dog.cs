using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceExercise
{
    public class Dog
    {
        public bool IsMale { get; set; }
        public string Name { get; set; }
        public bool IsNaughty { get; set; }
        public string Bark { get; set; }   

        public Dog(bool isMale, string name, bool isNaughty, string bark)
        {
            IsMale = isMale;
            Name = name;
            IsNaughty = isNaughty;
            Bark = bark;

        }


    }
}
