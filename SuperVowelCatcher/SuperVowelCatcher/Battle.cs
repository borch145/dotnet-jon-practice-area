using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperVowelCatcher
{
    public class Battle
    {

       


        public void BattleStart(Vowel firstVowel, Vowel secondVowel)
        {

            string vowelname1 = firstVowel.GetName();
            string vowelname2 = secondVowel.GetName();
            int vowelHealth1 = firstVowel.GetCurrentHealth();
            int vowelHealth2 = secondVowel.GetCurrentHealth();
            int vowelMaxHealth1 = firstVowel.GetMaxHealth();
            int vowelMaxHealth2 = secondVowel.GetMaxHealth();
            bool battleOn = true;
            bool vowel1Turn = TurnChooser(firstVowel, secondVowel);

            Console.WriteLine();
            Console.WriteLine("______________________________________________________");
            Console.WriteLine($"{vowelname1} 's HP: {vowelHealth1} / {vowelMaxHealth1} ");
            Console.WriteLine($"{vowelname2} 's HP: {vowelHealth2} / {vowelMaxHealth2} ");
            Console.WriteLine("______________________________________________________");

            Console.WriteLine($"The Battle is On!! {vowelname1} vs {vowelname2} Begins On Your Mark!");
            Console.ReadKey();
            

            while (battleOn == true)
            {
                Console.Clear();

                
                
                if (vowel1Turn == true)
                {
                    int damageDealt = Vowel.DealDamage(firstVowel, secondVowel);
                    Console.WriteLine($"{vowelname1} dealt {damageDealt} damage to {vowelname2}");

                }
                else
                {
                    int damageDealt = Vowel.DealDamage(secondVowel, firstVowel);
                    Console.WriteLine($"{vowelname2} dealt {damageDealt} damage to {vowelname1}");
                }

                vowelHealth1 = firstVowel.GetCurrentHealth();
                vowelHealth2 = secondVowel.GetCurrentHealth();

                Console.WriteLine();
                Console.WriteLine("______________________________________________________");
                Console.WriteLine($"{vowelname1} 's HP: {vowelHealth1} / {vowelMaxHealth1} ");
                Console.WriteLine($"{vowelname2} 's HP: {vowelHealth2} / {vowelMaxHealth2} ");
                Console.WriteLine("______________________________________________________");

                vowel1Turn = !vowel1Turn;
                Console.WriteLine("Press any key to continue....");
                Console.ReadKey(); 

                if (vowelHealth1 <= 0 || vowelHealth2 <=0)
                {
                    battleOn = false;
                }

            }


        }

        private bool TurnChooser(Vowel one, Vowel two)
        {
            int oneSpeed = one.GetSpeed();
            int twoSpeed = two.GetSpeed();
            if (oneSpeed >= twoSpeed)
            {
                return true;
            }
            else return false;
        }
    }

    
}
