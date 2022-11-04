using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceExercise
{
    public class Poodle : Dog
    {
        public int Size;
        public string Activity;

        public Poodle(bool isMale, string name, string bark, bool isNaughty, int size, string activity) : base(isMale, name, isNaughty, bark)
        {
            Size = size;
            Activity = activity;
        }
    }
}
