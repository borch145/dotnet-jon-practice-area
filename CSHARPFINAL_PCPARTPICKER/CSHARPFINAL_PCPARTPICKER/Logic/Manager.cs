using CSHARPFINAL_PCPARTPICKER.Data;
using CSHARPFINAL_PCPARTPICKER.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace CSHARPFINAL_PCPARTPICKER.Logic
{
    public class Manager
    {
        public IInventoryAndUsers Datasource { get; set; }
        
        public User CurrentUser;

        public Manager(IInventoryAndUsers dataSource)
        {
            Datasource = dataSource;
        }
        public bool Authenticate(string username, string password)
        {
            CurrentUser = Datasource.AuthenticateUser(username, password);
            if (CurrentUser != null)
            {
                return true;
            }
            return false;
        }
        public string GetPartInfoInCategory(PartCategory partCategory)
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (Part part in Datasource.Parts)
            {
                if (part.Category == partCategory)
                {
                    string discountCost = $"{Math.Round(part.Cost * .90m, 2, MidpointRounding.ToEven)} 10% OFF!";
                    sb.AppendLine($"  ID: {part.Id}  Part: {part.Name} Price: {(CurrentUser.IsEmployee ?  discountCost : part.Cost.ToString())} \n  Category: {part.Category} No. In Stock: {part.NumberInStock}\n\n");
                }
            }
            return sb.ToString();   
        }
        public string GetUserPartListInfoToRender(bool isCompletedOrder, int orderId) 
        {
            decimal costTotal = 0.00m;
            string discountCost = "";
            List<Part> parts;

            
            if (isCompletedOrder)
            {
                parts = CurrentUser.OrderHistory.Single(o => o.Id == orderId).Parts;
            }
            else
            {
                parts = CurrentUser.Cart;
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (Part part in parts)
            {
                if (CurrentUser.IsEmployee)
                {
                    discountCost = $"Price: {part.Cost} ------10% Discout Price: {Math.Round(part.Cost*.90m,2, MidpointRounding.ToEven)}\n\n";
                }
                else
                {
                    discountCost = $"----------------------------------Price: {part.Cost}\n\n";
                }
                stringBuilder.Append($"  Qty. 1 ID: {part.Id} {part.Category}: {part.Name}\n  {discountCost}");
                costTotal += part.Cost;
            }
            stringBuilder.Append("│                                                  │\n" +
                                 "╞══════════════════════════════════════════════════╡\n" +
                                $"│                        Coupon Savings: {Math.Round(costTotal*.10m,2, MidpointRounding.ToEven)}         \n" +
                                $"│                              Subtotal: {Math.Round(costTotal,2, MidpointRounding.ToEven)}                        \n" +
                                $"│                      7.125% Sales Tax: {Math.Round(costTotal *.07125m,2, MidpointRounding.ToEven)}                    \n" +
                                $"│                           Grand Total: {Math.Round(costTotal *1.07125m,2, MidpointRounding.ToEven)}                     \n" +
                                 "╘══════════════════════════════════════════════════╛");
            return stringBuilder.ToString();
        }
        internal string AddPartToCart(int input, PartCategory partCategory)
        {
            Part partToAdd = Datasource.Parts.SingleOrDefault(p => p.Id == input && p.Category ==partCategory);

            if (partToAdd != null)
            {
                if (partToAdd.NumberInStock > 0)
                {
                    CurrentUser.Cart.Add(partToAdd);
                    Console.ForegroundColor = ConsoleColor.Green;
                    return $"Success! {partToAdd.Name} has been added to cart!";
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    return $"Cannot add to cart. {partToAdd.Name} is out of stock.";
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return $"Item not found in {partCategory} category.";
            }
        }
        //add inventory check to ModifyCartItem() method against data layer for adding cart item?
        internal string ModifyCartItem(int inputID, int inputQuantity)
        {
            string message = string.Empty;  

            int inCart = CurrentUser.Cart.Where(p => p.Id == inputID).Count();
            
            //checks if any part ids matching the input ID exist in the cart.
            if (inCart != 0)
            {
                int partIndexStart = CurrentUser.Cart.FindIndex(p => p.Id == inputID);
                string partName = CurrentUser.Cart[partIndexStart].Name;
                //removes all items from cart if input is set to zero.
                if (inputQuantity == 0)
                {
                    CurrentUser.Cart.RemoveAll(p => p.Id == inputID);
                    message = $"{inCart} {partName} removed from cart!";
                }
                //reduces number of items in cart by difference if input is less than cart items.
                else if (inCart > inputQuantity)
                {
                    for (int i = 0; i < (inCart - inputQuantity); i++)
                    {
                        CurrentUser.Cart.RemoveAt(partIndexStart);
                    }
                    message = $"{inCart - inputQuantity} {partName} removed from cart.";
                }
                //increases number of items in cart by difference if input is less than cart items.
                else if (inCart < inputQuantity)
                {
                    for (int i = 0; i < (inputQuantity-inCart); i++)
                    {
                        CurrentUser.Cart.Add(CurrentUser.Cart[partIndexStart]);
                    }
                    message = $"{inputQuantity - inCart} {partName} added to cart.";
                }
                else if (inCart == inputQuantity)
                {
                    message = $"There is already {inputQuantity} {partName} in your order.";
                }

            }
            else
            {
                message = $"No item ID matching {inputID} found in cart.";
            }
            return message;
        }
        internal string ModifyOrderItem(int inputOrderId, int inputID, int inputQuantity)
        {
            string message = string.Empty;
            Order order = CurrentUser.OrderHistory.Single(o => o.Id == inputOrderId);
            Part part = order.Parts.Find(o => o.Id == inputID);

            int inOrder = order.Parts.Where(p => p.Id == inputID).Count();
            bool sufficientInventory = VerifySufficientStock(inputQuantity, inOrder, inputID);
            int stockDeltaToUpdate = (inputQuantity-inOrder);

            if (sufficientInventory)
            {
                //checks if any part ids matching the input ID exist in the order.
                if (inOrder != 0)
                {
                    int partIndexStart = order.Parts.FindIndex(p => p.Id == inputID);
                    string partName = order.Parts[partIndexStart].Name;
                    //removes all items from order if input is set to zero.
                    if (inputQuantity == 0)
                    {
                        order.Parts.RemoveAll(p => p.Id == inputID);
                        message = $"{inOrder} {partName} removed from order!";
                        Datasource.UpdateStockFromOrderChange(stockDeltaToUpdate, inputID);
                    }
                    //reduces number of items in order by difference if input is less than order items.
                    else if (inOrder > inputQuantity)
                    {
                        for (int i = 0; i < (inOrder - inputQuantity); i++)
                        {
                            order.Parts.RemoveAt(partIndexStart);
                        }
                        message = $"{inOrder - inputQuantity} {partName} removed from order.";
                        Datasource.UpdateStockFromOrderChange(stockDeltaToUpdate, inputID);
                    }
                    //increases number of items in cart by difference if input is less than order items.
                    else if (inOrder < inputQuantity)
                    {
                        for (int i = 0; i < (inputQuantity - inOrder); i++)
                        {
                            order.Parts.Add(order.Parts[partIndexStart]);
                        }
                        message = $"{inputQuantity - inOrder} {partName} added to order.";
                        Datasource.UpdateStockFromOrderChange(stockDeltaToUpdate, inputID);
                    }
                    //informs user they have not requested a modification from existing order.
                    else if (inOrder == inputQuantity)
                    {
                        message = $"There is already {inputQuantity} {partName} in your order.";
                    }
                }
                else
                {
                    message = $"No item ID matching {inputID} found in order.";
                }
            }
            else
            {
                message = "Insufficient inventory remain in stock to process request.";
            }
            return message;
        }
        private bool VerifySufficientStock(int inputQuantity, int inOrder, int partId)
        {
            if(inputQuantity > inOrder)
            {
                int numberToCheck = (inputQuantity - inOrder);
                bool sufficientStock = Datasource.VerifySingleItemStock(numberToCheck, partId);
                return sufficientStock;
            }
            else
            {
                return true;
            }
        }
        internal string FinalizeCheckout()
        {
            Order order = Datasource.ProcessFinalizedOrder(CurrentUser);
            StringBuilder stringBuilder = new StringBuilder();

            if (order == null)
            {
                return "Order not processed. Insufficient stock remains for an item in your cart.";
            }
            else
            {
                CurrentUser.OrderHistory.Add(order);
                CurrentUser.Cart.Clear();
                return "╔════════════════════════════════╗\n║  Order completed successfuly!  ║\n║  Press any key to return.      ║\n╚════════════════════════════════╝";
            }
        }
        internal string GetOrderInfoToRender()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Order order in CurrentUser.OrderHistory)
            {
                stringBuilder.Append($"Order Number: {order.Id}  Order Date: {order.OrderDate} Total Items: {order.Parts.Count()}\n");
            }
            return stringBuilder.ToString();
        }
        internal bool CheckOrderHistoryId(int inputOrderId)
        {
            return CurrentUser.OrderHistory.Any(o => o.Id == inputOrderId);
        }
        internal string DeleteOrderByIdNumber(int inputOrderId)
        {
            Order order = CurrentUser.OrderHistory.SingleOrDefault(o => o.Id == inputOrderId);
            TimeSpan timeFromOrder = DateTime.Now.Subtract(order.OrderDate);
            TimeSpan returnPeriod = new TimeSpan(30,0,0,0);
            
            if (order != null)
            {
                if (timeFromOrder <= returnPeriod)
                {
                    Datasource.ReturnFullOrder(order);
                    CurrentUser.OrderHistory.Remove(order);
                    return "Order deleted. Press any key to refresh.                  ";
                }
                else
                    return "Order date exceeds 30 days ago. Press any key to continue.";
            }
            else
            {
                return "Order number does not exist. Press any key to continue.";
            }
        }
        internal bool CheckReturnWindow(int inputOrderId)
        {
            Order order = CurrentUser.OrderHistory.SingleOrDefault(o => o.Id == inputOrderId);
            TimeSpan timeFromOrder = DateTime.Now.Subtract(order.OrderDate);
            TimeSpan returnPeriod = new TimeSpan(30, 0, 0, 0);

            if (timeFromOrder <= returnPeriod)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
