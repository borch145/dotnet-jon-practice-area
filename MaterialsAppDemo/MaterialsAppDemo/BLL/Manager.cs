using MaterialsAppDemo.Data;
using MaterialsAppDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsAppDemo.BLL
{
    class Manager
    {
        private IDataSource IDataSource { get; set; }

        public Manager(IDataSource dataSource)
        {
            IDataSource = dataSource;  
        }

        public void CheckResources()
        {
            string username = GetUsername();
            User user = IDataSource.Authenticate(username);
            if (user != null)
            {
                IDataSource.CheckResources(user);
                PrintUserResources(user);
            }
            else
            {
                Console.WriteLine("Invalid user. Press any key to return to main menu.");
                Console.ReadKey();
            }
        }

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

        public void DepositResource()
        {
            string username = GetUsername();
            User user = IDataSource.Authenticate(username);
            if (user != null)
            {
                var resourceType = AskResourceType();
                var resourceAmount = AskResourceAmount();
                int newTotal = RouteDeposit(user, resourceType, resourceAmount);
                Console.WriteLine($"Success! {resourceAmount} {resourceType} has been deposited in {user.UserName}'s account. The new {resourceType} balance is {newTotal}.");
                Console.WriteLine("Press any key to return to main menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Invalid user. Press any key to return to main menu.");
                Console.ReadKey();
            }
        }

        private int RouteDeposit(User user, ResourceTypes resourceType, int resourceAmount)
        {
            switch (resourceType)
            {
                case ResourceTypes.Wood:
                    return IDataSource.DepositWood(user, resourceType, resourceAmount);
                    
                    
                case ResourceTypes.Stone:
                    return IDataSource.DepositStone(user, resourceType, resourceAmount);
                    
                  
                case ResourceTypes.Iron:
                    return IDataSource.DepositIron(user, resourceType, resourceAmount);
                   

                case ResourceTypes.Gold:
                    return IDataSource.DepositGold(user, resourceType, resourceAmount);
                  

                default:
                    throw new Exception("Error: RouteDeposit did not route successfully.");

            }
        }
        private int RouteWithdrawal(User user, ResourceTypes resourceType, int resourceAmount)
        {
            switch (resourceType)
            {
                case ResourceTypes.Wood:
                    return IDataSource.WithdrawWood(user, resourceType, resourceAmount);


                case ResourceTypes.Stone:
                    return IDataSource.WithdrawStone(user, resourceType, resourceAmount);


                case ResourceTypes.Iron:
                    return IDataSource.WithdrawIron(user, resourceType, resourceAmount);


                case ResourceTypes.Gold:
                    return IDataSource.WithdrawGold(user, resourceType, resourceAmount);


                default:
                    throw new Exception("Error: RouteWithdrawal did not route successfully.");

            }
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

        private ResourceTypes AskResourceType()
        {
            Console.Clear();
            Console.WriteLine("Press a key 1-4 to choose a resource to deposit: \n\n");
            Console.WriteLine("1. Wood");
            Console.WriteLine("2. Stone");
            Console.WriteLine("3. Iron");
            Console.WriteLine("4. Gold");


           

            var cki = Console.ReadKey(true);

            switch (cki.Key)
            {
                case ConsoleKey.D1:
                    return ResourceTypes.Wood;

                case ConsoleKey.D2:
                    return ResourceTypes.Stone;


                case ConsoleKey.D3:
                    return ResourceTypes.Iron;

                case ConsoleKey.D4:
                    return ResourceTypes.Gold;

                default:
                    break;
            }
            return ResourceTypes.Wood;

        }
        public void WithdrawResource()
        {
            bool sufficientBalance = false;
            string username = GetUsername();
            User user = IDataSource.Authenticate(username);

            
            
                if (user != null)
                {
               
                    var resourceType = AskResourceType();
                    var resourceAmount = AskResourceAmount();
                    sufficientBalance = CheckForSufficientFunds(user, resourceType, resourceAmount);

                     if (sufficientBalance)
                     {
                    int newTotal = RouteWithdrawal(user, resourceType, resourceAmount);
                    Console.WriteLine($"Success! {resourceAmount} {resourceType} has been withdrawn from {user.UserName}'s account. The new {resourceType} balance is {newTotal}.");
                    Console.WriteLine("Press any key to return to main menu.");
                    Console.ReadKey();
                     }
                    else
                    {
                    Console.WriteLine("Insufficient Balance. Press any key to return to main menu.");
                    Console.ReadKey();
                    }
                
                }
            
                else
                {
                    Console.WriteLine("Invalid user. Press any key to return to main menu.");
                    Console.ReadKey();
                }
            
        }

        private bool CheckForSufficientFunds(User user, ResourceTypes resource, int resourceAmount)
        {
            int balance = 0;

            switch (resource)
            {
                case ResourceTypes.Wood:
                    balance = user.WoodCount;
                    break;
                case ResourceTypes.Stone:
                    balance = user.StoneCount;
                    break;
                case ResourceTypes.Iron:
                    balance = user.IronCount;
                    break;
                case ResourceTypes.Gold:
                    balance = user.GoldCount;
                    break;
                default:
                    break;
            }

            if (resourceAmount <= balance)
            {
                return true;
            }
            else return false;
        }

        public string GetUsername()
        {
            Console.WriteLine("Please enter your Username.");
            string input = Console.ReadLine();
            return input;    
        }

    }
}
