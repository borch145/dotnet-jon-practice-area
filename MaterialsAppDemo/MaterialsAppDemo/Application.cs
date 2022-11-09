using MaterialsAppDemo.BLL;
using MaterialsAppDemo.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsAppDemo
{
     class Application
    {
        private bool Exit { get; set; } = false;
        private Manager Manager { get; set; }

        public Application(IDataSource dataSource)
        {
            Manager = new Manager(dataSource);
        }
        public void Run()
        {
            while (!Exit)
            {
                Menu();
            }
        }
        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("               ========Materials App=========\n\n");
            Console.WriteLine("--------------------");
            Console.WriteLine("--Menu Selection--");
            Console.WriteLine("--------------------");
            Console.WriteLine("1. Check Resources");
            Console.WriteLine("2. Deposit a Resource");
            Console.WriteLine("3. Withdraw a Resource");
            Console.WriteLine("--------------------\n");
            Console.WriteLine("Press a number to select a menu item or ESC to quit.");

            var cki = Console.ReadKey(true);

            switch (cki.Key)
            {
                case ConsoleKey.D1:
                    Manager.CheckResources();
                    break;
                case ConsoleKey.D2:
                    Manager.DepositResource();
                    break;

                case ConsoleKey.D3:
                    Manager.WithdrawResource();
                  break;

                case ConsoleKey.Escape:
                    Exit = true;    
                    break;

                


            }
        }
    }
}
