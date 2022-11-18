using MaterialsAppDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsAppDemo.Data
{
    public interface IDataSource
    {

        public int DepositWood(User user, int amount);
        public int DepositStone(User user, int amount);
        public int DepositIron(User user, int amount);
        public int DepositGold(User user, int amount);
        public int WithdrawWood(User user, int amount);
        public int WithdrawStone(User user, int amount);
        public int WithdrawIron(User user, int amount);
        public int WithdrawGold(User user, int amount);
        public User Authenticate(string username);

    }
}
