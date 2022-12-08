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
        private List<Part> PopulateUserCart(string username)
        {
            //% separates parts in part list
            //, separates properties of parts

            var cart = new List<Part>();
            using (StreamReader sr = File.OpenText(UserSaveFolder + "\\" + username + "cart.txt"))
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
        //is this ParseToEnum method even necessary?
        private PartCategory ParseToEnum(string v)
        {
            switch (v)
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
        public Order ProcessFinalizedOrder(User currentUser)
        {
            Order order = new Order();
            bool enoughStock = VerifyAvailableStockForCart(currentUser.Cart);

            if (!enoughStock)
            {
                order = null;
                return order;
            }
            else
            {
                order.Id = currentUser.OrderHistory.Last().Id + 1;
                order.OrderDate = DateTime.Now;
                order.Parts = new List<Part>(currentUser.Cart);
                WriteFinalizedOrderToInventory(currentUser.Cart);
                WriteFinalizedOrderToOrderHistory(order, currentUser.Username);
                return order;
            }
        }
        private void WriteFinalizedOrderToOrderHistory(Order order, string username)
        {
            using (StreamWriter sw = File.AppendText($"{UserSaveFolder}\\{username}orders.txt"))
            {
                sw.BaseStream.Seek(0, SeekOrigin.End);
                
                sw.Write("§");

                foreach (Part part in order.Parts)
                {
                    if (Parts.Last().Id == part.Id)
                    {
                        sw.Write($"{part.Id},{part.Name},PartCategory.{part.Category},{part.Cost},{part.NumberInStock}");
                    }
                    else
                    {
                        sw.Write($"{part.Id},{part.Name},PartCategory.{part.Category},{part.Cost},{part.NumberInStock}%");
                    }
                }
                sw.Write($"#{order.OrderDate}#{order.Id}");
            }
        }
        private void WriteFinalizedOrderToInventory(List<Part> currentUserCart)
        {
            //updates the inventory stock in the Parts property of LiveData
            var orderedPartsById = currentUserCart.GroupBy(x => x.Id).ToList();

            for (int i = 0; i < orderedPartsById.Count(); i++)
            {
                int totalPartType = orderedPartsById[i].Count();
                Parts.Single(x => x.Id == orderedPartsById[i].Key).NumberInStock -= totalPartType;
            }
            
            //overwrites the existing save file with the updated data of Parts.
            File.Create(InventorySaveFile).Close();
            using (StreamWriter sw = File.AppendText(InventorySaveFile))
            {
                foreach (Part part in Parts)
                {
                    if (Parts.Last().Id == part.Id)
                    {
                        sw.Write($"{part.Id},{part.Name},PartCategory.{part.Category},{part.Cost},{part.NumberInStock}");
                    }
                    else
                    {
                        sw.Write($"{part.Id},{part.Name},PartCategory.{part.Category},{part.Cost},{part.NumberInStock}%");
                    }
                }
            }
        }
        private bool VerifyAvailableStockForCart(List<Part> currentUserCart)
        {
            //I DONT LIKE THIS WAY OF DOING IT BECAUSE OF MULTIPLE USER QUERIES IN IRL SETTING.
            //BETTER WAYS WITH SQL I'M SURE--BUT THINK THIS ADEQUATE FOR THE ASSIGNMENT.
            bool enoughStock = false;

            foreach (Part part in currentUserCart)
            {
                int orderedPartCount = currentUserCart.Where(x => x.Id == part.Id).Count();
                int inStockPartCount = Parts.Single(x => x.Id == part.Id).NumberInStock;

                if (orderedPartCount > inStockPartCount)
                {
                    return false;
                }
                else
                {
                    enoughStock = true;
                }
            }
            return enoughStock;
        }
        public void ReturnFullOrder(Order order)
        {
            var orderParts = order.Parts;
            //updates the inventory stock in the Parts property of LiveData
            var orderedPartsById = orderParts.GroupBy(x => x.Id).ToList();

            for (int i = 0; i < orderedPartsById.Count(); i++)
            {
                int totalPartType = orderedPartsById[i].Count();
                Parts.Single(x => x.Id == orderedPartsById[i].Key).NumberInStock += totalPartType;
            }

            //overwrites the existing save file with the updated data of Parts.
            File.Create(InventorySaveFile).Close();
            using (StreamWriter sw = File.AppendText(InventorySaveFile))
            {
                foreach (Part part in Parts)
                {
                    if (Parts.Last().Id == part.Id)
                    {
                        sw.Write($"{part.Id},{part.Name},PartCategory.{part.Category},{part.Cost},{part.NumberInStock}");
                    }
                    else
                    {
                        sw.Write($"{part.Id},{part.Name},PartCategory.{part.Category},{part.Cost},{part.NumberInStock}%");
                    }
                }
            }
        }
        public void UpdateStockFromOrderChange(int stockDeltaToUpdate, int partId)
        {
          
                Parts.Single(x => x.Id == partId).NumberInStock -= stockDeltaToUpdate;
            

            //overwrites the existing save file with the updated data of Parts.
            File.Create(InventorySaveFile).Close();
            using (StreamWriter sw = File.AppendText(InventorySaveFile))
            {
                foreach (Part part in Parts)
                {
                    if (Parts.Last().Id == part.Id)
                    {
                        sw.Write($"{part.Id},{part.Name},PartCategory.{part.Category},{part.Cost},{part.NumberInStock}");
                    }
                    else
                    {
                        sw.Write($"{part.Id},{part.Name},PartCategory.{part.Category},{part.Cost},{part.NumberInStock}%");
                    }
                }
            }
        }
        public bool VerifySingleItemStock(int inputQuantity, int partId)
        {
            Part part = Parts.Single(x => x.Id == partId);
            if (part.NumberInStock < inputQuantity)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
