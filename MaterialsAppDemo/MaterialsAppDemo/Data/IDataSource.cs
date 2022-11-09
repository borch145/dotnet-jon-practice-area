using MaterialsAppDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsAppDemo.Data
{
    interface IDataSource
    {
        public User CheckResources(User user);
        public int DepositResource(User user, ResourceTypes resource, int amount);
        public int WithdrawResource(User user, ResourceTypes resource, int amount);
        public User Authenticate(string username);

    }
}
