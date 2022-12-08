using CSHARPFINAL_PCPARTPICKER.Logic;
using CSHARPFINAL_PCPARTPICKER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSHARPFINAL_PCPARTPICKER.Data
{
    internal class InMemoryInventoryAndUsers : IInventoryAndUsers
    {
          
        public Dictionary<int, int> Inventory { get; set; } //key: productId, value: how many in stock
        public List<Part> Parts { get; set; }
        public List<User> Users { get; set; }

        public InMemoryInventoryAndUsers()
        {
            Parts = new List<Part>()
            {
                new Part()
                {
                    Id = 1,
                    Name = "GTX 5020",
                    Category = PartCategory.GPU,
                    Cost = 900.40m,
                    NumberInStock = 150
                },
                new Part()
                {
                    Id = 2,
                    Name = "GTX 4020",
                    Category = PartCategory.GPU,
                    Cost = 400.23m,
                    NumberInStock = 0
                },
                new Part()
                {
                    Id = 3,
                    Name = "Horizon 3050",
                    Category = PartCategory.CPU,
                    Cost = 500.53m,
                    NumberInStock = 23
                },
                new Part()
                {
                    Id = 4,
                    Name = "Horizon 2050",
                    Category = PartCategory.CPU,
                    Cost = 200.74m,
                    NumberInStock = 164
                },
                new Part()
                {
                    Id = 5,
                    Name = "Aero Case",
                    Category = PartCategory.Case,
                    Cost = 152.81m,
                    NumberInStock = 4
                },
                new Part()
                {
                    Id = 6,
                    Name = "Cardboard Box",
                    Category = PartCategory.Case,
                    Cost = 3.33m,
                    NumberInStock = 1
                },
                new Part()
                {
                    Id = 7,
                    Name = "AMX 1050",
                    Category = PartCategory.Motherboard,
                    Cost = 329.35m,
                    NumberInStock = 230
                },
                new Part()
                {
                    Id = 8,
                    Name = "AMX 2040",
                    Category = PartCategory.Motherboard,
                    Cost = 632.05m,
                    NumberInStock = 345
                },
            };

            Inventory = new Dictionary<int, int>() //define the inventory stock by the product id and the stock of that product
            {
                {1, 52 }, //Product with id 1 has 52 items in stock
                {2, 26 },
                {3, 0 },
                {4, 12 },
                {5, 2 },
                {6, 1 },
                {7, 126 },
                {8, 35 }
            };

            Users = new List<User>()
            {
                new User("customer", "test", false)
                {
                      Cart = new List<Part>()
                      {
                          new Part()
                          {
                             Id = 2,
                             Name = "GTX 4020",
                             Category = PartCategory.GPU,
                             Cost = 400.23m
                          },
                          new Part()
                          {
                              Id = 3,
                              Name = "Horizon 3050",
                              Category = PartCategory.CPU,
                              Cost = 500.53m
                          },
                      },
                     OrderHistory = new List<Order>()
                     {
                        new Order()
                        {
                            Parts = new List<Part>()
                            {
                                new Part()
                                {
                                    Id = 6,
                                    Name = "Cardboard Box",
                                    Category = PartCategory.Case,
                                    Cost = 3.33m
                                },
                                new Part()
                                {
                                    Id = 7,
                                    Name = "AMX 1050",
                                    Category = PartCategory.Motherboard,
                                    Cost = 329.35m
                                }
                            },
                            OrderDate = DateTime.Now,
                            Id = 1
                        },
                        new Order()
                        {
                            Parts = new List<Part>()
                            {
                                new Part()
                                {
                                    Id = 4,
                                    Name = "Horizon 2050",
                                    Category = PartCategory.CPU,
                                    Cost = 200.74m
                                },
                                new Part()
                                {
                                    Id = 8,
                                    Name = "AMX 2040",
                                    Category = PartCategory.Motherboard,
                                    Cost = 632.05m,
                                },
                            },
                            OrderDate = new DateTime(2022,10,15),
                            Id = 2
                        }
                     }
                },
                new User("employee", "bossman", true)
            };
            
        }
        public User AuthenticateUser(string username, string password)
        {
            User authenticateUser = Users.SingleOrDefault(x => x.Username == username);
            if (authenticateUser != null && authenticateUser.Password == password)
            {
                return authenticateUser;
            }
            else
            {
                return null;
            }
        }
        public Order ProcessFinalizedOrder(User currentUser)
        {
            Order order = new Order();
            bool enoughStock = VerifyAvailableStockForCart(currentUser.Cart);

            if(!enoughStock)
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
                return order;
            }
        }
        private void WriteFinalizedOrderToInventory(List<Part> currentUserCart)
        {
            var orderedPartsById = currentUserCart.GroupBy(x => x.Id).ToList();

            for(int i = 0; i < orderedPartsById.Count(); i++)
            {
                int totalPartType = orderedPartsById[i].Count();
                Parts.Single(x => x.Id == orderedPartsById[i].Key).NumberInStock -= totalPartType;
            }
        }
        private bool VerifyAvailableStockForCart(List<Part> currentUserCart)
        {
            bool enoughStock = false;

            foreach(Part part in currentUserCart)
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
        public bool VerifySingleItemStock(int inputQuantity, int partId)
        {
            Part part = Parts.Single(x => x.Id == partId);
            if(part.NumberInStock < inputQuantity)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void UpdateStockFromOrderChange(int quantityDelta, int partId)
        {
            Part part = Parts.Single(p => p.Id == partId);
            part.NumberInStock -= quantityDelta;
        }
        public void WriteStockTransfersFromOrderToInventory(Order order)
        {
            foreach (Part orderPart in order.Parts)
            {
                Part inventoryPartToUpdate = Parts.Single(p => p.Id == orderPart.Id);
                inventoryPartToUpdate.NumberInStock += 1;
            }
        }
        #region Unused Live Methods for InMemory
        public void WriteUpdateToOrderHistory(List<Order> orderHistory, string username)
        {
            //this is only needed for live data.
        }
        public void WriteUpdateToCart(List<Part> cart, string username)
        {
            //this is only needed for live data.
        }
        #endregion
    }
}

