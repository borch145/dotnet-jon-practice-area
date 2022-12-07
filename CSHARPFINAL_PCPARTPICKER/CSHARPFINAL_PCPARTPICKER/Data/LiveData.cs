using CSHARPFINAL_PCPARTPICKER.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSHARPFINAL_PCPARTPICKER.Data
{
    internal class LiveData : IInventoryAndUsers
    {
        public Dictionary<int, int> Inventory { get; set; }
        public List<Part> Parts { get; set ; }
        public List<User> Users { get; set; }
        private string UserCredentialSaveFile { get; set; }
        private string InventorySaveFile { get; set; }
        private string UserSaveFolder { get; set; }

        public LiveData()
        {
            UserCredentialSaveFile = "C:\\Users\\Jonquil\\source\\repos\\dotnet-jon-practice-area\\CSHARPFINAL_PCPARTPICKER\\PCPARTPICKERDATA\\UserLogins.txt";
            InventorySaveFile = "C:\\Users\\Jonquil\\source\\repos\\dotnet-jon-practice-area\\CSHARPFINAL_PCPARTPICKER\\PCPARTPICKERDATA\\Inventory.txt";
            UserSaveFolder = "C:\\Users\\Jonquil\\source\\repos\\dotnet-jon-practice-area\\CSHARPFINAL_PCPARTPICKER\\PCPARTPICKERDATA\\USERS";
            Users = PopulateUsers();
            Parts = PopulateParts();
        }

        private List<Part> PopulateParts()
        {
            //% separates parts in part list
            //, separates properties of parts

            var parts = new List<Part>();   
            using (StreamReader sr = File.OpenText(InventorySaveFile))
            {
                string line = "";
                string[] splitParts = new string[] { };

                while ((line = sr.ReadLine()) != null)
                {
                    splitParts = line.Split('%');
                }
                
                for(int i = 0; i < splitParts.Length; i++)
                {
                    string[] splitPartProperties = splitParts[i].Split(',');

                    Part toAdd = new Part()
                    {
                        Id = int.Parse(splitPartProperties[0]),
                        Name = splitPartProperties[1],
                        Category = ParseToEnum(splitPartProperties[2]),
                        Cost = decimal.Parse(splitPartProperties[3]),
                        NumberInStock = int.Parse(splitPartProperties[4]),

                    };

                    parts.Add(toAdd);
                }
            }
            return parts;
        }
        private PartCategory ParseToEnum(string v)
        {
            switch(v)
            {
                case "PartCategory.GPU":
                    return PartCategory.GPU;
                case "PartCategory.CPU":
                    return PartCategory.CPU;
                case "PartCategory.Case":
                    return PartCategory.Case;
                case "PartCategory.Motherboard":
                    return PartCategory.Motherboard;
                default:
                    return PartCategory.Invalid;
            }
        }
        private List<User> PopulateUsers()
        {
            //% separates users
            //, separates properties of users
            var users = new List<User>();
            using (StreamReader sr = File.OpenText(UserCredentialSaveFile))
            {
                string line = "";
                string[] splitParts = new string[] { };

                while ((line = sr.ReadLine()) != null)
                {
                    splitParts = line.Split('%');
                }

                for (int i = 0; i < splitParts.Length; i++)
                {
                    string[] splitPartProperties = splitParts[i].Split(',');

                    User toAdd = new User(splitPartProperties[0], splitPartProperties[1], "true" == splitPartProperties[2]);
                    users.Add(toAdd);
                }
            }
            return users;
        }
        public User AuthenticateUser(string username, string password)
        {
            User authenticateUser = Users.SingleOrDefault(x => x.Username == username);
            if (authenticateUser != null && authenticateUser.Password == password)
            {
                authenticateUser.Cart = PopulateUserCart(username);
                authenticateUser.OrderHistory = PopulateUserOrders(username);
                return authenticateUser;
            }
            else
            {
                return null;
            }
        }
        private List<Order> PopulateUserOrders(string username)
        {
            //§ separates order from order
            //# separates properties of orders
            //% separates parts in order
            //, separtes properties of parts

            var orders = new List<Order>();
            using (StreamReader sr = File.OpenText(UserSaveFolder + "\\" + username + "orders.txt"))
            {
                string line = "";
                string[] splitParts = new string[] { };

                //reads textfile and separates orders into a string array
                while ((line = sr.ReadLine()) != null)
                {
                    splitParts = line.Split('§');
                }

                //cycles through each separated order
                for (int i = 0; i < splitParts.Length; i++)
                {
                    //cycles through individual order and separates properties into string array
                    string[] splitOrderProperties = splitParts[i].Split('#');

                    Order orderToAdd = new Order()
                    {
                        Parts = GetPartsFromString(splitOrderProperties[0]),
                        OrderDate = DateTime.Parse(splitOrderProperties[1]),
                        Id = int.Parse(splitOrderProperties[2]),
                    };
                    orders.Add(orderToAdd);
                }
            }
            return orders;
        }
        private List<Part> GetPartsFromString(string partList)
        {
            List<Part> parts = new List<Part>();    
            string[] splitParts = partList.Split('%');

            for(int i = 0; i < splitParts.Length; i++)
            {
                string[] splitPartProperties = splitParts[i].Split(',');

                Part partToAdd = new Part()
                {
                    Id = int.Parse(splitPartProperties[0]),
                    Name = splitPartProperties[1],
                    Category = ParseToEnum(splitPartProperties[2]),
                    Cost = decimal.Parse(splitPartProperties[3]),
                };
                parts.Add(partToAdd);
            }
            return parts;
        }
        private List<Part> PopulateUserCart(string username)
        {
            //% separates parts in part list
            //, separates properties of parts

            var cart = new List<Part>();
            using (StreamReader sr = File.OpenText(UserSaveFolder +"\\" + username + "cart.txt"))
            {
                string line = "";
                string[] splitParts = new string[] { };

                while ((line = sr.ReadLine()) != null)
                {
                    splitParts = line.Split('%');
                }

                for (int i = 0; i < splitParts.Length; i++)
                {
                    string[] splitPartProperties = splitParts[i].Split(',');

                    Part toAdd = new Part()
                    {
                        Id = int.Parse(splitPartProperties[0]),
                        Name = splitPartProperties[1],
                        Category = ParseToEnum(splitPartProperties[2]),
                        Cost = decimal.Parse(splitPartProperties[3]),
                    };
                    cart.Add(toAdd);
                }
            }
            return cart;
        }
        public Order ProcessFinalizedOrder(User currentUser)
        {
            throw new NotImplementedException();
        }

        public void ReturnFullOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void UpdateStockFromOrderChange(int stockDeltaToUpdate, int partId)
        {
            throw new NotImplementedException();
        }

        public bool VerifySingleItemStock(int inputQuantity, int partId)
        {
            throw new NotImplementedException();
        }
    }
}
