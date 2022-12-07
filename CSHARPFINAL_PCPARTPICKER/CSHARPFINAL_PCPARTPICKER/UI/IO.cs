using CSHARPFINAL_PCPARTPICKER.Data;
using CSHARPFINAL_PCPARTPICKER.Logic;
using CSHARPFINAL_PCPARTPICKER.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CSHARPFINAL_PCPARTPICKER.UI
{
    public class IO
    {
        public Manager Manager;

        public IO(IInventoryAndUsers inventoryAndUsers)
        {
            //this will be replaced with .json sourcing....
            //IInventoryAndUsers inventoryAndUsers = new InMemoryInventoryAndUsers();
            Manager = new Manager(inventoryAndUsers);
        }

        #region Display Menu Methods
        public void LoginPage()
        {
           while(true)
           { 
            bool validUser = false;

            
                Console.WriteLine("╔═════════════════════════════════════╗");
                Console.WriteLine("║    Welcome! Please enter your       ║");
                Console.WriteLine("║    credentials to log in.           ║");
                Console.WriteLine("║                                     ║");
                Console.WriteLine("║  Username:                          ║");
                Console.WriteLine("║  Password:                          ║");
                Console.WriteLine("║                                     ║");
                Console.WriteLine("╚═════════════════════════════════════╝");

                while (!validUser)
                {
                    Console.SetCursorPosition(14, 4);
                    Console.CursorVisible = true;
                    string username = Console.ReadLine();
                    Console.SetCursorPosition(14, 5);
                    string password = Console.ReadLine();

                    validUser = Manager.Authenticate(username, password);

                    if (!validUser)
                    {
                        ClearUsernameAndPassword(username, password);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(3, 3);
                        Console.Write("Invalid Credentials. Try again.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Clear();
                        Console.CursorVisible = false;
                        MainMenu();
                    }
                }
           }
        }
        private void MainMenu()
        {
            bool logOff = false;
        
            while (!logOff)
            {
                int usernameLength = Manager.CurrentUser.Username.Length;
                Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                                                                     ║");
                Console.WriteLine("║                                                                                     ║");
                Console.WriteLine("║ ╒═════╕                                                                             ║");        
                Console.WriteLine("║ │  1  │ - Search for parts                                                          ║");
                Console.WriteLine("║ └─────┘                                                                             ║");
                Console.WriteLine("║ ╒═════╕                                                                             ║");
                Console.WriteLine("║ │  2  │ - View Cart & Checkout                                                      ║");
                Console.WriteLine("║ └─────┘                                                                             ║");
                Console.WriteLine("║ ╒═════╕                                                                             ║");
                Console.WriteLine("║ │  3  │ - Order History & Returns                                                   ║");
                Console.WriteLine("║ └─────┘                                                                             ║");
                Console.WriteLine("║ ╒═════╕                                                                             ║");
                Console.WriteLine("║ │ ESC │ - Log off                                                                   ║");
                Console.WriteLine("║ └─────┘                                                                             ║");
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════╝");
                Console.SetCursorPosition(2, 1);
                Console.Write("Welcome " + Manager.CurrentUser.Username + "! Use your keyboard to select an option below.");
                bool validSelection = false;

                while (!validSelection)
                {
                    var key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            Console.Clear();
                            validSelection = true;
                            PartSearchMenu();
                            break;
                        case ConsoleKey.D2:
                            Console.Clear();
                            validSelection = true;
                            CartMenu();
                            break;
                        case ConsoleKey.D3:
                            Console.Clear();
                            validSelection = true;
                            OrderHistoryMenu();
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            validSelection = true;
                            Manager.CurrentUser = null;
                            logOff = true;
                            break;
                        default:
                            break;
                    }
                }
            }

        }
        private void CartMenu()
        {
            string message = "";
            bool exitCart = false;
            while (!exitCart)
            {
                Console.WriteLine("╒══════════════════════════════════════════════════╕");
                Console.WriteLine("│                  Viewing Cart                    │");
                Console.WriteLine("│|ESC| - Go Back  | M | -  Modify | C | Checkout   │");
                Console.WriteLine("│                                                  │");
                Console.WriteLine("│                                                  │");
                Console.WriteLine("╞══════════════════════════════════════════════════╡");
                Console.SetCursorPosition(0, 7);
                Console.Write(RenderCartItems());
                RenderCartDisplay();
                Console.SetCursorPosition(2, 3);
                Console.Write(message);
                Console.CursorVisible = false;


                bool validSelection = false;

                while (!validSelection)
                {
                    var key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            exitCart = true;
                            validSelection = true;
                            Console.Clear();
                            break;
                        case ConsoleKey.M:
                            int[] cartModifiers = GetIdAndQuantityToModify();
                            message = Manager.ModifyCartItem(cartModifiers[0], cartModifiers[1]);
                            validSelection = true;
                            Console.Clear();
                            break;
                        case ConsoleKey.C:
                            ConfirmCheckout();
                            validSelection = true;
                            message = "";
                            Console.Clear();
                            break;
                    }

                }
            }
        }
        private void PartSearchMenu()
        {
            bool leaveMenu = false;

            while (!leaveMenu)
            {
                Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                 Select a category of parts to begin browsing!                       ║");
                Console.WriteLine("║                                                                                     ║");
                Console.WriteLine("║ ╒═════╕                                                                             ║");
                Console.WriteLine("║ │  1  │ - Browse GPU's                                                              ║");
                Console.WriteLine("║ └─────┘                                                                             ║");
                Console.WriteLine("║ ╒═════╕                                                                             ║");
                Console.WriteLine("║ │  2  │ - Browse CPU's                                                              ║");
                Console.WriteLine("║ └─────┘                                                                             ║");
                Console.WriteLine("║ ╒═════╕                                                                             ║");
                Console.WriteLine("║ │  3  │ - Browse Cases                                                              ║");
                Console.WriteLine("║ └─────┘                                                                             ║");
                Console.WriteLine("║ ╒═════╕                                                                             ║");
                Console.WriteLine("║ │  4  │ - Browse Motherboards                                                       ║");
                Console.WriteLine("║ └─────┘                                                                             ║");
                Console.WriteLine("║ ╒═════╕                                                                             ║");
                Console.WriteLine("║ │ ESC │ - Back to Main Menu                                                         ║");
                Console.WriteLine("║ └─────┘                                                                             ║");
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════╝");

                //Console.WriteLine("Select a category to browse part inventory.\n\n");
                //Console.WriteLine("|  1  | - Browse GPU's");
                //Console.WriteLine("|  2  | - Browse CPU's");
                //Console.WriteLine("|  3  | - Browse Cases");
                //Console.WriteLine("|  4  | - Browse Motherboards");
                //Console.WriteLine("| ESC | - Back to Main Menu");

                bool validSelection = false;

                while (!validSelection)
                {
                    var key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            Console.Clear();
                            validSelection = true;
                            DisplayPartsMenu(PartCategory.GPU);
                            break;
                        case ConsoleKey.D2:
                            Console.Clear();
                            validSelection = true;
                            DisplayPartsMenu(PartCategory.CPU);
                            break;
                        case ConsoleKey.D3:
                            Console.Clear();
                            validSelection = true;
                            DisplayPartsMenu(PartCategory.Case);
                            break;
                        case ConsoleKey.D4:
                            Console.Clear();
                            validSelection = true;
                            DisplayPartsMenu(PartCategory.Motherboard);
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            validSelection = true;
                            leaveMenu = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void DisplayPartsMenu(PartCategory partCategory)
        {
            bool exitMenu = false;

            Console.WriteLine("╒══════════════════════════════════════════════════╕");
            Console.WriteLine("│Browsing:                                         │");
            Console.WriteLine("│|ESC| - Go Back  |  A  | -  Input ID              │");
            Console.WriteLine("╞══════════════════════════════════════════════════╡");
            Console.WriteLine("│ Enter a product ID to add a product to your cart!│");
            Console.WriteLine("│ ID:                                              │");
            Console.WriteLine("│                                                  │");
            Console.WriteLine("└──────────────────────────────────────────────────┘");
            Console.SetCursorPosition(12, 1);
            Console.Write(partCategory.ToString());
            Console.SetCursorPosition(0, 9);


            string partList = Manager.GetPartInfoInCategory(partCategory);
            Console.WriteLine(partList);

            //go back and clean this method up to be more like ModifyCart menu logic.
            while (!exitMenu)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {

                    case ConsoleKey.A:
                        ClearDisplayPartsMessageLine();
                        Console.SetCursorPosition(6, 5);
                        Console.CursorVisible = true;
                        string stringInput = Console.ReadLine();
                        bool validInput = int.TryParse(stringInput, out int input);

                        if (validInput)
                        {
                            ClearDisplayPartsIDEntry(stringInput);
                            string message = Manager.AddPartToCart(input, partCategory);
                            Console.SetCursorPosition(3, 6);
                            Console.CursorVisible = false;
                            Console.WriteLine(message);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            ClearDisplayPartsIDEntry(stringInput);
                            Console.SetCursorPosition(3, 6);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Invalid Entry. Press 'a' to retry");
                            Console.CursorVisible = false;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;

                    case ConsoleKey.Escape:
                        Console.Clear();
                        exitMenu = true;
                        break;
                    default:
                        break;
                }
            }
        }
        private void OrderHistoryMenu()
        {
            string message = "";
            bool exitOrderHistory = false;
            while (!exitOrderHistory)
            {
                Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                    Viewing Order History                   ║");
                Console.WriteLine("║  |ESC| - Go Back  | M | -  Modify/View  | D | Delete Order ║");
                Console.WriteLine("║                                                            ║");
                Console.WriteLine("║                                                            ║");
                Console.WriteLine("║            (Orders may be modified for 30 days)            ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
                Console.SetCursorPosition(0, 8);
                Console.Write(RenderOrders());

                bool validSelection = false;
                
                while (!validSelection)
                {
                    var key = Console.ReadKey(true);
                    int inputOrderId = 0;

                    switch (key.Key)
                    {
                        case ConsoleKey.M:
                            inputOrderId = GetOrderIdInput();
                            Console.ForegroundColor = ConsoleColor.White;
                            if(inputOrderId > 0) //0 value means invalid input received.
                            {
                                validSelection = true;
                                Console.Clear();
                                ViewSingleOrderMenu(inputOrderId);
                            }
                            break;
                        case ConsoleKey.D:
                            inputOrderId = GetOrderIdInput();
                            if (inputOrderId > 0)
                            {
                                message = Manager.DeleteOrderByIdNumber(inputOrderId);
                                Console.SetCursorPosition(2, 3);
                                Console.Write(message);
                                Console.ReadKey(true);
                                Console.Clear();
                                validSelection = true;
                            }
                            break;
                        case ConsoleKey.Escape:
                            validSelection = true;
                            exitOrderHistory = true;
                            Console.Clear();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        //fix up display to look better for ViewSingleOrderMenu
        private void ViewSingleOrderMenu(int inputOrderId)
        {
            string message = "";
            bool exitSingleOrder = false;

            while (!exitSingleOrder)
            {
                Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                                            ║");
                Console.WriteLine("║ |ESC| - Go Back   | M | -  Modify Item  | A | Add Item     ║");
                Console.WriteLine("║                                                            ║");
                Console.WriteLine("║                                                            ║");
                Console.WriteLine("║            (Orders may be modified for 30 days)            ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
                Console.SetCursorPosition(15, 1);
                Console.Write($"Viewing Order Number {inputOrderId}");
                Console.SetCursorPosition(0, 8);
                Console.Write(Manager.GetUserPartListInfoToRender(true, inputOrderId));
                Console.SetCursorPosition(2, 3);
                Console.Write(message);

                bool orderedWithin30Days = Manager.CheckReturnWindow(inputOrderId);
                bool validSelection = false;

                while (!validSelection)
                {
                    var key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.M:
                            ClearSingleOrderMenuMessageLine();
                            if (orderedWithin30Days)
                            {
                                int[] cartModifiers = GetIdAndQuantityToModify();
                                message = Manager.ModifyOrderItem(inputOrderId, cartModifiers[0], cartModifiers[1]);
                                validSelection = true;
                                Console.Clear();
                            }
                            else
                            {
                                Console.SetCursorPosition(2, 3);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("Order cannot be modified outside 30 day window.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            break;
                        case ConsoleKey.A:
                            //if (orderedWithin30Days)
                            //{
                            //    AddItemsToOrderMenu(inputOrderId);
                            //}
                            break;
                        case ConsoleKey.Escape:
                            validSelection = true;
                            exitSingleOrder = true;
                            Console.Clear();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        #region GetInput and RenderInfo Methods
        private void ConfirmCheckout()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            ClearCartModifyMessage(true, 0);
            Console.SetCursorPosition(2, 3); 
            Console.Write("Are you sure you want to checkout?");
            Console.SetCursorPosition(2, 4);
            Console.Write("Type 'confirm' or enter to go back: ");
            Console.CursorVisible = true;
            string confirmCheckout = Console.ReadLine().ToLower();

            if(confirmCheckout != "confirm")
            {
                ClearCartModifyMessage(true, 0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(2, 3);
                Console.Write("Order not finalized. Press any key to continue.");
                Console.CursorVisible = true;
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }
            else
            {
                string message = Manager.FinalizeCheckout();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey(true);
            }
        }
        private int[] GetIdAndQuantityToModify()
        {
            int[] idAndQuantity = new int[2];

            bool validInput = false;
            

            //gets ID # from user
            while (!validInput)
            {
                
                Console.SetCursorPosition(2, 3);
                Console.ForegroundColor = ConsoleColor.Cyan;
                string idText = "Enter the ID of the item to modify: ";
                Console.WriteLine(idText);
                Console.SetCursorPosition(idText.Length + 2, 3);
                Console.CursorVisible = true;
                string inputID = Console.ReadLine();

                //parses ID # to int and adds to return array
                validInput = int.TryParse(inputID, out int parsedID);
                if (!validInput)
                {
                    Console.SetCursorPosition(2, 4);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Entry. Please enter an integer.");
                }
                else
                {
                    idAndQuantity[0] = parsedID;
                }
                ClearCartModifyMessage(validInput, idText.Length+2);
            }

            validInput = false;
            //get quantity modification amount from user
            while (!validInput)
            {
                
                Console.SetCursorPosition(2, 3);
                Console.ForegroundColor = ConsoleColor.Cyan;
                string modifyText = "Enter new quantity of item(s): ";
                Console.WriteLine(modifyText);
                Console.SetCursorPosition(modifyText.Length + 2, 3);
                string inputQuantity = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                //parses Quantity # to int and adds to return array
                validInput = int.TryParse(inputQuantity, out int parsedQuantity);
                if (!validInput || parsedQuantity <0)
                {
                    Console.SetCursorPosition(2, 4);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Entry. Please enter a positive integer.");
                    validInput = false;
                }
                else
                {
                    idAndQuantity[1] = parsedQuantity;
                }
                ClearCartModifyMessage(validInput, modifyText.Length + 2);
            }
            return idAndQuantity;
        }
        private int GetOrderIdInput()
        {
            Console.SetCursorPosition(3, 3);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Please enter the Order Number you'd like to modify: ");
            string toParse = Console.ReadLine();

            bool validInput = int.TryParse(toParse, out int parsedInt);
            bool orderIdExists = Manager.CheckOrderHistoryId(parsedInt);

            if (!validInput | !orderIdExists)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(3, 3);
                Console.Write("Invalid entry. Please enter a valid order number.         ");
                Console.ForegroundColor = ConsoleColor.White;
                return 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                return parsedInt;
            }

        }
        //Render cart items: maybe implement a GroupBy function to lump like items on one line.
        private string RenderCartItems()
        { 
          //uses calculations on manager layer to build a string that displays cart items.
            return Manager.GetUserPartListInfoToRender(false, 0);
        }
        private string RenderOrders()
        {
            //uses calculations on manager layer to build a string that displays order items.
            return Manager.GetOrderInfoToRender();
        }
        private void RenderCartDisplay()
        {
            int yPosition = 4;
            int cartSize = Manager.CurrentUser.Cart.Count; 
            //renders the side borders based on how many items printed to cart
            for(int i = 0; i < ((cartSize*3)+3); i++)
            {
                Console.SetCursorPosition(0, yPosition);
                Console.Write("│");
                Console.SetCursorPosition(51, yPosition);
                Console.Write("│");
                yPosition++;
            }
            //finishes rendering the side borders at the bottom of cart display where cost totals show
            Console.SetCursorPosition(51, (cartSize * 3) + 9);
            Console.Write("│");
            Console.SetCursorPosition(51, (cartSize * 3) + 10);
            Console.Write("│");
            Console.SetCursorPosition(51, (cartSize * 3) + 11);
            Console.Write("│");
            Console.SetCursorPosition(51, (cartSize * 3) + 12);
            Console.Write("│");
        }

        #endregion

        #region ClearingMethods
        //possibly reduce these "clear" methods into one method that takes an int[,,] for starting X,Y, and clearLength
        private void ClearCartModifyMessage(bool validInput, int xPosOfInput)
        {
            if (!validInput)
            {
                Console.SetCursorPosition(xPosOfInput, 3);
                Console.Write("             ");
            }
            else
            {
                Console.SetCursorPosition(2, 3);
                Console.Write("                                                 ");
                Console.SetCursorPosition(2, 4);
                Console.Write("                                                 ");
            }
        }
        private void ClearDisplayPartsMessageLine()
        {
            Console.SetCursorPosition(3, 6);
            Console.Write("                                              ");
        }
        private void ClearDisplayPartsIDEntry(string input)
        {
            for (int i = 6; i < input.Length + 6; i++)
            {
                Console.SetCursorPosition(i, 5);
                Console.CursorVisible = false;
                Console.Write(" ");
            }
        }
        private void ClearUsernameAndPassword(string username, string password)
        {
            int usernameLength = username.Length;
            int passwordLength = password.Length;

            for (int i = 14; i < usernameLength+14; i++)
            {
                Console.SetCursorPosition(i, 4);
                Console.Write(" ");
            }

            for (int i = 14; i < passwordLength+14; i++)
            {
                Console.SetCursorPosition(i, 5);
                Console.Write(" ");
            }    
        }
        private void ClearSingleOrderMenuMessageLine()
        {
            Console.SetCursorPosition(2, 3);
            Console.Write("                                                           ");
            Console.SetCursorPosition(2, 4);
            Console.Write("                                                           ");
        }
        #endregion
    }
}
