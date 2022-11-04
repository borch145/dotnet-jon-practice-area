using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceExercise
{
    public class Retriever : Dog
    {
        public int Size;
        public string Activity;

        public Retriever(bool isMale, string name, string bark, bool isNaughty, int size, string activity) : base(isMale, name, isNaughty, bark)
        {
            Size = size;
            Activity = activity;
        }
    }
}
