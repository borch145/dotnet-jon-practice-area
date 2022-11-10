using MaterialsAppDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsAppDemo.Data
{
    interface IDataSource
    {
        public User CheckResources(User user);
        public int DepositWood(User user, ResourceTypes resource, int amount);

        public int DepositStone(User user, ResourceTypes resource, int amount);
        public int DepositIron(User user, ResourceTypes resource, int amount);
        public int DepositGold(User user, ResourceTypes resource, int amount);

        public int WithdrawWood(User user, ResourceTypes resource, int amount);

        public int WithdrawStone(User user, ResourceTypes resource, int amount);
        public int WithdrawIron(User user, ResourceTypes resource, int amount);
        public int WithdrawGold(User user, ResourceTypes resource, int amount);
        public User Authenticate(string username);

    }
}
