using CSHARPFINAL_PCPARTPICKER.Data;
using CSHARPFINAL_PCPARTPICKER.UI;
using Microsoft.Extensions.Configuration;
using System;

namespace CSHARPFINAL_PCPARTPICKER
{
    internal class Program
    {
        //QUESTION: Does the creation of a cart fulfil the "add parts to order by category" requirement, or do you wish this to extend to
        //          orders in the past 30 days as well? Carts can currently take in new parts/modify quantites/delete items. Orders can add/remove 
        //          existing items and be fully deleted.
        //QUESTION: Cart and Order display/update logic is *nearly* identical, but are sizeable methods on their own. Better to have two separate
        //          near-twin methods such as GetCartInfoToDisplay() and GetOrderInfoToDisplay(), or add another if statement or two in one main method
        //          to route properly?
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            string dataMode = configuration.GetSection("Settings:DataMode").Value;

            IInventoryAndUsers dataSource = SelectDataSource(dataMode);
            
            IO IO = new IO(dataSource);
            IO.LoginPage();
        }

        private static IInventoryAndUsers SelectDataSource(string dataMode)
        {
            switch (dataMode)
            {
                case "InMemory":
                    InMemoryInventoryAndUsers inMemoryInventoryAndUsers = new InMemoryInventoryAndUsers();
                    return inMemoryInventoryAndUsers;
                case "LiveData":
                    return null;
                default:
                    return null;
            }
        }
    }

    /*
     You are a PC parts store, and your task is to create a PC part ordering app, 
    which will be used by customers. You will have products which you sell, and an 
    inventory which tracks the quantity of each product you have in stock. Users 
    can use the app to create new orders and browse/pick various parts for their 
    order. When users complete orders, their orders are saved, and the inventory 
    should update appropriately. Users can view recent orders for the last 30 days,
    with the ability to browse further back. They can edit and delete orders within
    the last 30 days, but orders further than 30 days old are "locked-in". Basic users
    pay the normal price for parts, but premium users get a flat 10% discount on all orders.
    For parts to be ordered, the number in stock must be greater than the number ordered.


Create:
    A user can create an order:
    -The user can see a list of parts sorted by part category, with displays the part name, 
    cost per unit, and number in stock
        **the user can see parts that are out of stock, but not order any
        **a user cannot order more parts than are in stock
        
    -The user can add multiple parts to their order
    
    -Once a user finishes building an order, they should be shown the parts in their order, 
    and the total cost of the order, and be asked to confirm their order to complete it
    
    -Once an order is completed, the inventory should update its number in stock for ordered 
    parts
    
    -Once an order is completed, the ordered on date should be saved to the order
        

Read:
    -A user can see a list of their pending orders for the last 30 days, with the ability 
    to get earlier orders

Update:
    -A user can edit orders made in the last 30 days:
        -Can modify the quantity of any part (including modifying to 0, removing the part)
        -Can add other parts to the order
        
        **again, once an order is modified, the store inventory should update appropriately

Delete
    -A user can delete an order in its entirety if it was made in the last 30 days
    
        **again, once an order is modified, the store inventory should update appropriately
        
Other specifications:

    -There are basic users and premium users. Premium users get a 10% discount on all parts!
    
    -For all data, there should be in-memory data sources, as well as a "Live" text-file
    data source
        **There needs to be a data source for both the store inventory, and pending orders
        **For text-data, each customer should have their own text file with their orders
    
    -Thorough tests should be implemented for the Logic and Data layers
    
    -Dependency injection should be used to configure the app for in-memory(test) vs 
    live data mode
     */
}
