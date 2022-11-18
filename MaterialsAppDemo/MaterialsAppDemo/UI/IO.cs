using MaterialsAppDemo.BLL;
using MaterialsAppDemo.Data;
using MaterialsAppDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsAppDemo.UI
{
    public class IO
    { 
        public Manager Manager { get; set; }
        public IO(Manager manager)
        {
            Manager = manager;
        }

        #region Output Methods
        internal void CheckRes()
        {
            WorkflowResponse response = Manager.CheckResources(GetUsername());

            if (!response.Success)
            {
                Console.WriteLine(response.Message);
                Console.ReadKey();
            }
            else
            {
                PrintUserResources(response.User);
            }

        }
        public void DepositRes()
        {
            WorkflowResponse workflowResponse = Manager.DepositResource(GetUsername(), AskResourceType(), AskResourceAmount());

            if (!workflowResponse.Success)
            {
                Console.WriteLine(workflowResponse.Message);
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(workflowResponse.Message);
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
            }
        }
        public void WithdrawRes()
        {
            WorkflowResponse workflowResponse = Manager.WithdrawResource(GetUsername(), AskResourceType(), AskResourceAmount());

            if (!workflowResponse.Success)
            {
                Console.WriteLine(workflowResponse.Message);
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(workflowResponse.Message);
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
            }
        }
        #endregion
        private void PrintUserResources(User user)
        {
            Console.Clear();
            Console.WriteLine($"{user.UserName}'s Materials \n\n");
            Console.WriteLine($"Wood: {user.WoodCount}.");
            Console.WriteLine($"Stone: {user.StoneCount}.");
            Console.WriteLine($"Iron: {user.IronCount}.");
            Console.WriteLine($"Gold: {user.GoldCount}.");
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey();
        }

        #region Input Methods
        public string GetUsername()
        {
            Console.WriteLine("Please enter your Username.");
            string input = Console.ReadLine();
            return input;
        }
        private ResourceTypes AskResourceType()
        {
            Console.Clear();
            Console.WriteLine("Press a key 1-4 to choose a resource to deposit: \n\n");
            Console.WriteLine("1. Wood");
            Console.WriteLine("2. Stone");
            Console.WriteLine("3. Iron");
            Console.WriteLine("4. Gold");


            ResourceTypes resourceType = ResourceTypes.Invalid;
            bool validSelection = false;
            

            while (!validSelection)
            {
                var cki = Console.ReadKey(true);

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        resourceType = ResourceTypes.Wood;
                        validSelection = true;
                        break;
                    case ConsoleKey.D2:
                        resourceType = ResourceTypes.Stone;
                        validSelection = true;
                        break;
                    case ConsoleKey.D3:
                        resourceType = ResourceTypes.Iron;
                        validSelection = true;
                        break;
                    case ConsoleKey.D4:
                        resourceType = ResourceTypes.Gold;
                        validSelection = true;
                        break;
                    default:
                        resourceType = ResourceTypes.Invalid;
                        break;
                }
            }
            return resourceType;


        }
        private int AskResourceAmount()
        {
            bool validEntry = false;
            int resourceAmount = 0;

            while (!validEntry)
            {
                Console.Clear();
                Console.WriteLine("Please enter in an amount: ");
                string input = Console.ReadLine();

                validEntry = int.TryParse(input, out resourceAmount);
                if (resourceAmount <= 0)
                {
                    validEntry = false;
                }
                if (!validEntry)
                {
                    Console.WriteLine("Invalid entry. Press any key to retry.");
                    Console.ReadKey();
                }
            }
            return resourceAmount;
        }
        #endregion

        

        
    }
}
