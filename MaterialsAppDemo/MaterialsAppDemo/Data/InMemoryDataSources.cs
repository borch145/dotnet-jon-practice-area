using MaterialsAppDemo.BLL;
using MaterialsAppDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialsAppDemo.Data
{
    class InMemoryDataSources : IDataSource
    {
        private List<User> Users { get; set; }
       

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

        public User CheckResources(User user)
        {
            return user;
        }
        public int DepositWood(User user, ResourceTypes resource, int amount) => user.WoodCount += amount;

        public int DepositStone(User user, ResourceTypes resource, int amount) => user.StoneCount += amount;
        public int DepositIron(User user, ResourceTypes resource, int amount) => user.IronCount += amount;
        public int DepositGold(User user, ResourceTypes resource, int amount) => user.GoldCount += amount;
        
        public int WithdrawWood(User user, ResourceTypes resource, int amount) => user.WoodCount -= amount;

        public int WithdrawStone(User user, ResourceTypes resource, int amount) => user.StoneCount -= amount;
        public int WithdrawIron(User user, ResourceTypes resource, int amount) => user.IronCount -= amount;
        public int WithdrawGold(User user, ResourceTypes resource, int amount) => user.GoldCount -= amount;
        public User Authenticate(string username)
        {
                var user = Users.SingleOrDefault(user => user.UserName == username);
                return user;
        }
    }
}
