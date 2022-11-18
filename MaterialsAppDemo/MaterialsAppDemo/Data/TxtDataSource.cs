using MaterialsAppDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

namespace MaterialsAppDemo.Data
{
    class TxtDataSource : IDataSource
    {
        public List<User> UserList { get; set; }
        string SaveFile = "C:\\Users\\Jonquil\\source\\repos\\dotnet-jon-practice-area\\MaterialsAppDemo\\data.txt";


        public TxtDataSource()
        {
            UserList = new List<User>();
            InitiateSaveFile();
        }
        private void PopulateUsers()
        {
            using (StreamReader streamReader = File.OpenText(SaveFile))
            {
                
                string line = "";
                   

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] userInfo = line.Split(',');
                    User userToAdd = new User()
                    {
                        UserName = userInfo[0],
                        WoodCount = ParseDataToInt(userInfo[1]),
                        StoneCount = ParseDataToInt(userInfo[2]),
                        IronCount = ParseDataToInt(userInfo[3]),
                        GoldCount = ParseDataToInt(userInfo[4])

                    };
                    UserList.Add(userToAdd);
                }
            }
        }
        private void InitiateSaveFile()
        {
            
            if (File.Exists(SaveFile))
            {
                 PopulateUsers();
            }
            else
            {
                File.Create(SaveFile).Close();
            }
        }
        public User Authenticate(string username)
        {
            var user = UserList.SingleOrDefault(user => user.UserName == username);
            return user;
        }
        public User CheckResources(User user)
        {
            return user;
        }
        private void WriteData()
        {
            File.Delete(SaveFile);
            File.Create(SaveFile).Close();

            using (StreamWriter sw = new StreamWriter(SaveFile))
            {
                foreach (User user in UserList)
                {
                    sw.WriteLine($"{user.UserName},{user.WoodCount},{user.StoneCount},{user.IronCount},{user.GoldCount}");
                }
            }
        }

        #region Deposit Methods
        public int DepositGold(User user, int amount)
        {
            user.GoldCount += amount;
            WriteData();
            return user.GoldCount;
        }
        public int DepositIron(User user, int amount)
        {
            user.IronCount += amount;
            WriteData();
            return user.IronCount;
        }
        public int DepositStone(User user, int amount)
        {
            user.StoneCount += amount;
            WriteData();
            return user.StoneCount;
        }
        public int DepositWood(User user, int amount)
        {
            user.WoodCount += amount;
            WriteData();
            return user.WoodCount;
        }
        #endregion
        #region WithdrawMethods
        public int WithdrawGold(User user, int amount)
        {
            user.GoldCount -= amount;
            WriteData();
            return user.GoldCount;
        }
        public int WithdrawIron(User user, int amount)
        {
            user.IronCount -= amount;
            WriteData();
            return user.IronCount;
        }
        public int WithdrawStone(User user, int amount)
        {
            user.StoneCount -= amount;
            WriteData();
            return user.StoneCount;
        }
        public int WithdrawWood(User user, int amount)
        {
            user.WoodCount -= amount;
            WriteData();
            return user.WoodCount;
        }
        private int ParseDataToInt(string toParse)
        {
            bool success = int.TryParse(toParse, out int value);

            if (success)
            {
                return value;
            }
            else
            {
                throw new Exception("Error: ParseDataToInt failed to parse string to integer.");
            }
        }
        #endregion
    }


}
