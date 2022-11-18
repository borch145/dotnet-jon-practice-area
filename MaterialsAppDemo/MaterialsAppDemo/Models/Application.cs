using MaterialsAppDemo.BLL;
using MaterialsAppDemo.Data;
using MaterialsAppDemo.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsAppDemo.Models
{
    public class Application
    {
        private bool Exit { get; set; } = false;
        private Manager Manager { get; set; }
        private IO IO { get; set; }

        public Application(IDataSource dataSource)
        {
            Manager = new Manager(dataSource);
            IO = new IO(Manager);
        }

        public void Menu()
        {
            while (!Exit)
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
                        IO.CheckRes();

                        break;
                    case ConsoleKey.D2:
                        IO.DepositRes();
                        break;

                    case ConsoleKey.D3:
                        IO.WithdrawRes();
                        break;

                    case ConsoleKey.Escape:
                        Exit = true;
                        break;




                }
            }
        }
    }
}
