using MaterialsAppDemo.BLL;
using MaterialsAppDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialsAppDemo.Data
{
    public class InMemoryDataSources : IDataSource
    {
        public List<User> Users { get; set; }
        public InMemoryDataSources()
        {
            Users = new List<User>()
            {
                new User()
                {
                    UserName = "Timmy",
                    WoodCount = 0,
                    StoneCount = 0,
                    IronCount = 0,
                    GoldCount = 0
                },
                new User()
                {
                    UserName = "Dimmadome",
                    WoodCount = 5000,
                    StoneCount = 1000,
                    IronCount = 3000,
                    GoldCount = 100000
                }
            };
        }
       
        public User Authenticate(string username)
        {
            var user = Users.SingleOrDefault(user => user.UserName == username);
            return user;
        }

        #region Deposit Methods
        public int DepositWood(User user, int amount) => user.WoodCount += amount;
        public int DepositStone(User user, int amount) => user.StoneCount += amount;
        public int DepositIron(User user, int amount) => user.IronCount += amount;
        public int DepositGold(User user, int amount) => user.GoldCount += amount;
        #endregion

        #region Withdraw Methods
        public int WithdrawWood(User user, int amount) => user.WoodCount -= amount;
        public int WithdrawStone(User user, int amount) => user.StoneCount -= amount;
        public int WithdrawIron(User user, int amount) => user.IronCount -= amount;
        public int WithdrawGold(User user, int amount) => user.GoldCount -= amount;
        #endregion
    }
}
