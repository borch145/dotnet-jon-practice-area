using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperVowelCatcher
{
    public class Vowel
    {
        private int MaxHealth;
        private int CurrentHealth;
        private int Attack;
        private string Name;
        private int Speed;
        


        public Vowel(int maxHealth, int attack, int speed)
            {
                MaxHealth = maxHealth;
                CurrentHealth = maxHealth;
                Attack = attack;
                Speed = speed;
                
            }
        public void SetName()
        {
            Console.WriteLine("Please name your vowel.");
            Name = Console.ReadLine();
           
        }

        public string GetName()
        {

            return Name;

        }

        public int GetCurrentHealth()
        {
            return CurrentHealth;
        }

        public int GetMaxHealth()
        {
            return MaxHealth;
        }
        
        public int GetSpeed()
        {
            return Speed;
        }

        public static int DealDamage(Vowel damageDealer, Vowel damageTaker)
        {
            damageTaker.CurrentHealth -= damageDealer.Attack;
            return damageDealer.Attack;

        }

       
    }
}

    
