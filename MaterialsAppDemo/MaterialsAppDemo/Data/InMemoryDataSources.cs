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
        public int DepositResource(User user, ResourceTypes resource, int amount)
        {
            switch(resource)
            {
                case ResourceTypes.Wood:
                    user.WoodCount += amount;
                    return user.WoodCount;
                    
                case ResourceTypes.Stone:
                    user.StoneCount += amount;
                    return user.StoneCount;
                    ;
                case ResourceTypes.Iron:
                    user.IronCount += amount;
                    return user.IronCount;
                    
                case ResourceTypes.Gold:
                    user.GoldCount += amount;
                    return user.GoldCount;
                    
                default:
                    return 0;   
                    
            }
            
        }
        public int WithdrawResource(User user, ResourceTypes resource, int amount)
        {
            switch (resource)
            {
                case ResourceTypes.Wood:
                    user.WoodCount -= amount;
                    return user.WoodCount;

                case ResourceTypes.Stone:
                    user.StoneCount -= amount;
                    return user.StoneCount;
                    ;
                case ResourceTypes.Iron:
                    user.IronCount -= amount;
                    return user.IronCount;

                case ResourceTypes.Gold:
                    user.GoldCount -= amount;
                    return user.GoldCount;

                default:
                    return 0;

            }
        }
        public User Authenticate(string username)
        {
            var user = Users.SingleOrDefault(user => user.UserName == username);
            return user;
        }
    }
}
