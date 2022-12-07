using CSHARPFINAL_PCPARTPICKER.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPFINAL_PCPARTPICKER.Data
{
    public interface IInventoryAndUsers
    {
        public Dictionary<int, int> Inventory { get; set; }
        public List<Part> Parts { get; set; }
        public List<User> Users { get; set; }

        public User AuthenticateUser(string username, string password);
        public Order ProcessFinalizedOrder(User currentUser);
        public void ReturnFullOrder(Order order);
        public void UpdateStockFromOrderChange(int stockDeltaToUpdate, int partId);
        public bool VerifySingleItemStock(int inputQuantity, int partId);

    }
}
