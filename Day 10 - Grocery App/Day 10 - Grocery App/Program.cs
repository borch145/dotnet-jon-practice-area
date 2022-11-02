using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Collections.Specialized.BitVector32;
using static System.Formats.Asn1.AsnWriter;

namespace Day_10___Grocery_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Allow a customer to check the store inventory
            // --they can't see the number in stock, unless the item is out of stock
            //Allow an employe to check the store inventory
            //--they can see all the data

            

            bool isCustomer = AreYouCustomer();
            List<Product> inventory = LoadProducts();
            Product product = null;
            Menu();

            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            ConsoleKey input = keyPress.Key;

            switch(input)
            {
                case ConsoleKey.A:
                    inventory = ListDeliOnly(inventory);
                    PrintInventory(isCustomer, inventory);
                    break;
                case ConsoleKey.B:
                    inventory = ListOutOfStockItems(inventory);
                    PrintInventory(isCustomer, inventory);
                    break;
                case ConsoleKey.C:
                    inventory = LessThanFiveDollars(inventory);
                    PrintInventory(isCustomer, inventory);
                    break;
                case ConsoleKey.D:
                    product = GetMostExpensive(inventory);
                    PrintProduct(isCustomer, product);
                    break;
                case ConsoleKey.E:
                    inventory = SortByMostExpensive(inventory);
                    PrintInventory(isCustomer, inventory);
                    break;
                case ConsoleKey.F:
                    inventory = SortByCategoryThenCheapest(inventory);
                    PrintInventory(isCustomer, inventory);
                    break;
                case ConsoleKey.G:
                    GetIdsOfOutOfStockItems(inventory);
                    break;
                case ConsoleKey.H:
                    StockInCategories(inventory);
                    break;
                case ConsoleKey.I:
                    MostExpensiveIdInCategory(inventory);
                    break;
            }

            
            
        }

        public static List<Product> LoadProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Cost = 0.50M,
                    Name = "Banana",
                    NumberInStock = 55,
                    Category = Category.Produce
                },
                new Product()
                {
                    Id = 2,
                    Cost = 0.60M,
                    Name = "Apple",
                    NumberInStock = 16,
                    Category = Category.Produce
                },
                new Product()
                {
                    Id = 3,
                    Cost = 0.78M,
                    Name = "Carrot",
                    NumberInStock = 3,
                    Category = Category.Produce
                },
                new Product()
                {
                    Id = 4,
                    Cost = 1.20M,
                    Name = "Lime",
                    NumberInStock = 1,
                    Category = Category.Produce
                },
                new Product()
                {
                    Id = 5,
                    Cost = 2.50M,
                    Name = "Lemon",
                    NumberInStock = 0,
                    Category = Category.Produce
                },
                new Product()
                {
                    Id = 6,
                    Cost = 5.70M,
                    Name = "Ham",
                    NumberInStock = 21,
                    Category = Category.Deli
                },
                new Product()
                {
                    Id = 7,
                    Cost = 4.50M,
                    Name = "Turkey",
                    NumberInStock = 21,
                    Category = Category.Deli
                },
                new Product()
                {
                    Id = 8,
                    Cost = 3.50M,
                    Name = "Provelone",
                    NumberInStock = 4,
                    Category = Category.Deli
                },
                new Product()
                {
                    Id = 9,
                    Cost = 7.50M,
                    Name = "Corned Beef",
                    NumberInStock = 0,
                    Category = Category.Deli
                },
                new Product()
                {
                    Id = 10,
                    Cost = 12.50M,
                    Name = "Chicken Noodle Soup",
                    NumberInStock = 69,
                    Category = Category.NonPerishable
                },
                new Product()
                {
                    Id = 11,
                    Cost = 7.50M,
                    Name = "Mac 'n Cheese",
                    NumberInStock = 54,
                    Category = Category.NonPerishable
                },
                new Product()
                {
                    Id = 12,
                    Cost = 3.26M,
                    Name = "Egg Noodles",
                    NumberInStock = 67,
                    Category = Category.NonPerishable
                },
                new Product()
                {
                    Id = 13,
                    Cost = 11.22M,
                    Name = "Canned Tuna",
                    NumberInStock = 46,
                    Category = Category.NonPerishable
                },
                new Product()
                {
                    Id = 14,
                    Cost = 4.00M,
                    Name = "Ice",
                    NumberInStock = 0,
                    Category = Category.Specialty
                },
                new Product()
                {
                    Id = 15,
                    Cost = 16.40M,
                    Name = "Toilet Paper",
                    NumberInStock = 0,
                    Category = Category.Specialty
                },
                new Product()
                {
                    Id = 16,
                    Cost = 9.50M,
                    Name = "Napkins",
                    NumberInStock = 0,
                    Category = Category.Specialty
                },

            };
        }

        public static bool AreYouCustomer()
        {
            Console.WriteLine("Are you a customer? Type Yes or No.");
            string customerCheck = Console.ReadLine();
            customerCheck = customerCheck.ToLower();

            if (customerCheck == "yes")
            {
                return true;
            }
            else return false;

        }

        public static void PrintInventory(bool isCustomer, List<Product> inventory)
        {
            Console.Clear();
            Console.WriteLine("The following items are in stock:");
            Console.WriteLine();
            Console.WriteLine();

            foreach (Product product in inventory)
            {
                Console.WriteLine("==================");
                Console.WriteLine(product.Name);
                Console.WriteLine("__________________");
                Console.WriteLine("  Section: " + product.Category);
                Console.WriteLine("  Price: $" + product.Cost);
                if (isCustomer && product.NumberInStock > 0)
                { 
                    Console.WriteLine("  In Stock");
                }
                if (isCustomer && product.NumberInStock == 0)
                {
                    Console.WriteLine("  Out of Stock");
                }
                if (!isCustomer)
                {
                    Console.WriteLine("  " + product.NumberInStock + " In Stock");
                }
                Console.WriteLine("==================");
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        public static void PrintProduct(bool isCustomer, Product product)
        {
            Console.WriteLine("==================");
            Console.WriteLine(product.Name);
            Console.WriteLine("__________________");
            Console.WriteLine("  Section: " + product.Category);
            Console.WriteLine("  Price: $" + product.Cost);
            if (isCustomer && product.NumberInStock > 0)
            {
                Console.WriteLine("  In Stock");
            }
            if (isCustomer && product.NumberInStock == 0)
            {
                Console.WriteLine("  Out of Stock");
            }
            if (!isCustomer)
            {
                Console.WriteLine("  " + product.NumberInStock + " In Stock");
            }
            Console.WriteLine("==================");
        }

        public static List<Product> ListDeliOnly(List<Product> inventory)
        {
            var deliItemsOnly = inventory.Where(p => p.Category == Category.Deli).ToList();
            return deliItemsOnly;   
        }

        public static List<Product> ListOutOfStockItems(List<Product> inventory)
        {
            var outOfStockItemsOnly = inventory.Where(p => p.NumberInStock == 0).ToList();
            return outOfStockItemsOnly;
        }

        public static List<Product> LessThanFiveDollars(List<Product> inventory)
        {
            var lessThanFiveDollarItems = inventory.Where(p => p.Cost < 5.00M).ToList();
            return lessThanFiveDollarItems;
        }

        public static Product GetMostExpensive(List<Product> inventory)
        {
            inventory = inventory.OrderByDescending(p => p.Cost).ToList();
                return inventory.First();
        }

        public static List<Product> SortByMostExpensive(List<Product> inventory)
        {
            return inventory.OrderByDescending(p => p.Cost).ToList();   
        }

        public static List<Product> SortByCategoryThenCheapest(List<Product> inventory)
        {
            inventory = inventory.OrderBy(p => p.Category).ThenBy(p => p.Cost).ToList();
            return inventory;
        }

        public static void GetIdsOfOutOfStockItems(List<Product> inventory)
        {
            
            var ids = inventory.Where(p => p.NumberInStock == 0)
                .Select(p => new
                {
                    JustId = p.Id,
                })
                .ToList();

            //var ids = inventory.Select(product => new
            //{
            //    JustId = product.Id,
            //}).ToList();

            Console.WriteLine("Here are the ID's for the out of stock items:");
            foreach (var id in ids)
            {
                Console.WriteLine(id.JustId);
            }
            Console.ReadKey();
        }

        public static void StockInCategories(List<Product> inventory)
        {
            var categories = inventory.GroupBy(p => p.Category).ToList();



            var finalBoss = categories.Select(p => new
            {
                Groups = p.Key,
                OutOfStock = p.Where(o => o.NumberInStock == 0).Count()
            }).ToList();

            foreach (var category in finalBoss)
            {
                Console.WriteLine(category.OutOfStock + " Out of Stock Products in " + category.Groups);
            }
            Console.ReadKey();
        }

        public static void Menu()
        {
            Console.WriteLine("A: print all products from the deli section");

            Console.WriteLine("B: print a list of all products that are out of stock");

            Console.WriteLine("C: print a list of products that are less than 5.00$");

            Console.WriteLine("D: print the most expensive product");

            Console.WriteLine("E: print a list of all products ordered from most expensive to least expensive");

            Console.WriteLine("F: print a list of products ordered by category, then from least to most expensive");

            Console.WriteLine("G: print the ID and name of all products out of stock");

            Console.WriteLine("H: print a list of each category and how many products are out of stock in that category");

            Console.WriteLine("I: print a list of each category and the name / id of the most expensive product in it");
        }

        public static void MostExpensiveIdInCategory(List<Product> inventory)
        {
            var categories = inventory.GroupBy(product => product.Category).ToList();


            var categoryExpense = categories.Select(lists => new
            {
                CategoryName = lists.Key,
                MostExpensiveId = lists.OrderByDescending(product => product.Cost).First().Id,
                MostExpensiveName = lists.OrderByDescending(product => product.Cost).First().Name
            }); ; ;

            foreach (var category in categoryExpense)
            {
                Console.WriteLine("The most expensive item in " + category.CategoryName + " is " + category.MostExpensiveId + " " + category.MostExpensiveName);
            }
            Console.ReadKey();
        }

        
        
       

    }
}
