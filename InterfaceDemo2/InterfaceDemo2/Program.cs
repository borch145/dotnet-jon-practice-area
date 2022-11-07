using InterfaceDemo2.ChoiceGetters;
using System;

namespace InterfaceDemo2
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Play(new Computer(), new Player());
        }
    }
}
