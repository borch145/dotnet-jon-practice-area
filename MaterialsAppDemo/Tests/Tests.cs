using System;
using Xunit;
using MaterialsAppDemo.Data;
using MaterialsAppDemo.Models;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using MaterialsAppDemo.BLL;

namespace Tests
{
    public class Tests
    {

        #region InMemoryDataSources Tests
        [Fact]
        public void InMemoryDataSources_Authenticate_AuthenticationSucceeds()
        {
            InMemoryDataSources dataSource = new InMemoryDataSources();
            User testUser = dataSource.Authenticate("Timmy");

            Assert.Equal("Timmy", testUser.UserName);
        }
        [Fact]
        public void InMemoryDataSources_Authenticate_AuthenticationFails()
        {
            InMemoryDataSources dataSource = new InMemoryDataSources();
            User testUser = dataSource.Authenticate("NotAUser");

            Assert.Null(testUser);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-2)]
        [InlineData(0)]
        public void InMemoryDataSources_DepositWood_WoodAddsToCount(int depositAmount)

        {
            InMemoryDataSources dataSource = new InMemoryDataSources();

            foreach (User user in dataSource.Users)
            {
                int currentAmount = user.WoodCount;
                dataSource.DepositWood(user, depositAmount);
                int expectedBalance = depositAmount + currentAmount;
                Assert.Equal(expectedBalance, user.WoodCount);
            }



        }

        [Theory]
        [InlineData(1)]
        [InlineData(-2)]
        [InlineData(0)]
        public void InMemoryDataSources_DepositStone_StoneAddsToCount(int depositAmount)

        {
            InMemoryDataSources dataSource = new InMemoryDataSources();

            foreach (User user in dataSource.Users)
            {
                int currentAmount = user.StoneCount;
                dataSource.DepositStone(user, depositAmount);
                int expectedBalance = depositAmount + currentAmount;
                Assert.Equal(expectedBalance, user.StoneCount);
            }



        }

        [Theory]
        [InlineData(1)]
        [InlineData(-2)]
        [InlineData(0)]
        public void InMemoryDataSources_DepositIron_IronAddsToCount(int depositAmount)

        {
            InMemoryDataSources dataSource = new InMemoryDataSources();

            foreach (User user in dataSource.Users)
            {
                int currentAmount = user.IronCount;
                dataSource.DepositIron(user, depositAmount);
                int expectedBalance = depositAmount + currentAmount;
                Assert.Equal(expectedBalance, user.IronCount);
            }



        }

        [Theory]
        [InlineData(1)]
        [InlineData(-2)]
        [InlineData(0)]
        public void InMemoryDataSources_DepositGold_GoldAddsToCount(int depositAmount)

        {
            InMemoryDataSources dataSource = new InMemoryDataSources();

            foreach (User user in dataSource.Users)
            {
                int currentAmount = user.GoldCount;
                dataSource.DepositGold(user, depositAmount);
                int expectedBalance = depositAmount + currentAmount;
                Assert.Equal(expectedBalance, user.GoldCount);
            }



        }

        [Theory]
        [InlineData(1)]
        [InlineData(-2)]
        [InlineData(0)]
        public void InMemoryDataSources_WithdrawWood_WoodSubtractsFromCount(int withdrawAmount)

        {
            InMemoryDataSources dataSource = new InMemoryDataSources();

            foreach (User user in dataSource.Users)
            {
                int currentAmount = user.WoodCount;
                dataSource.WithdrawWood(user, withdrawAmount);
                int expectedBalance = currentAmount - withdrawAmount;
                Assert.Equal(expectedBalance, user.WoodCount);
            }



        }

        [Theory]
        [InlineData(1)]
        [InlineData(-2)]
        [InlineData(0)]
        public void InMemoryDataSources_WithdrawStone_StoneSubtractsFromCount(int withdrawAmount)

        {
            InMemoryDataSources dataSource = new InMemoryDataSources();

            foreach (User user in dataSource.Users)
            {
                int currentAmount = user.StoneCount;
                dataSource.WithdrawStone(user, withdrawAmount);
                int expectedBalance = currentAmount - withdrawAmount;
                Assert.Equal(expectedBalance, user.StoneCount);
            }



        }

        [Theory]
        [InlineData(1)]
        [InlineData(-2)]
        [InlineData(0)]
        public void InMemoryDataSources_WithdrawIron_IronSubtractsFromCount(int withdrawAmount)

        {
            InMemoryDataSources dataSource = new InMemoryDataSources();

            foreach (User user in dataSource.Users)
            {
                int currentAmount = user.IronCount;
                dataSource.WithdrawIron(user, withdrawAmount);
                int expectedBalance = currentAmount - withdrawAmount;
                Assert.Equal(expectedBalance, user.IronCount);
            }



        }

        [Theory]
        [InlineData(1)]
        [InlineData(-2)]
        [InlineData(0)]
        public void InMemoryDataSources_WithdrawGold_GoldSubtractsFromCount(int withdrawAmount)

        {
            InMemoryDataSources dataSource = new InMemoryDataSources();

            foreach (User user in dataSource.Users)
            {
                int currentAmount = user.GoldCount;
                dataSource.WithdrawGold(user, withdrawAmount);
                int expectedBalance = currentAmount - withdrawAmount;
                Assert.Equal(expectedBalance, user.GoldCount);
            }



        }
        #endregion

        #region Manager Tests

        [Theory]
        [InlineData("Timmy", 0)]
        [InlineData("Dimmadome", 1)]
        [InlineData("BogusUser", -1)]
        private void Manager_CheckResources_ReturnsCorrectWorkflowResponses(string username, int listIndex)
        {
            InMemoryDataSources dataSource = new InMemoryDataSources();
            Manager manager = new Manager(dataSource);

            WorkflowResponse testWorkflow = manager.CheckResources(username);
            if (username != "BogusUser")
            {
                Assert.True(testWorkflow.Success);
                Assert.Equal(dataSource.Users[listIndex], testWorkflow.User);
            }
            else
            {
                Assert.False(testWorkflow.Success);
                Assert.Null(testWorkflow.User);
                Assert.Equal("Invalid user. Press any key to return to main menu.", testWorkflow.Message);
            }
        }

        [Theory]
        [InlineData("Timmy", 0, ResourceTypes.Wood, 15)]
        [InlineData("Dimmadome", 1, ResourceTypes.Wood, 15)]
        [InlineData("BogusUser", -1, ResourceTypes.Wood, 15)]
        private void Manager_DepositResources_ReturnsCorrectWorkflowResponses(string username, int listIndex, ResourceTypes resourceType, int resourceAmount)
        {
            InMemoryDataSources dataSource = new InMemoryDataSources();
            Manager manager = new Manager(dataSource);

            WorkflowResponse testWorkflow = manager.DepositResource(username, resourceType, resourceAmount);
            if (username != "BogusUser")
            {
                Assert.True(testWorkflow.Success);
                Assert.Equal(dataSource.Users[listIndex], testWorkflow.User);
                Assert.Equal($"Success! {resourceAmount} {resourceType} has been deposited in {testWorkflow.User.UserName}'s account. The new {resourceType} balance is {testWorkflow.User.WoodCount}.", testWorkflow.Message);
            }
            else
            {
                Assert.False(testWorkflow.Success);
                Assert.Null(testWorkflow.User);
                Assert.Equal("Invalid user.", testWorkflow.Message);
            }

    
        }

        [Theory]
        [InlineData("Timmy", 0, ResourceTypes.Wood, 15)]
        [InlineData("Timmy", 0, ResourceTypes.Wood, 0)]
        [InlineData("Dimmadome", 1, ResourceTypes.Wood, 15)]
        [InlineData("BogusUser", -1, ResourceTypes.Wood, 15)]
        private void Manager_WithdrawResources_ReturnsCorrectWorkflowResponses(string username, int listIndex, ResourceTypes resourceType, int resourceAmount)
        {
            InMemoryDataSources dataSource = new InMemoryDataSources();
            Manager manager = new Manager(dataSource);

            WorkflowResponse testWorkflow = manager.WithdrawResource(username, resourceType, resourceAmount);
            if (username != "BogusUser")
            {
                
                Assert.Equal(dataSource.Users[listIndex], testWorkflow.User);
                if (testWorkflow.User.WoodCount >= resourceAmount)
                {
                    Assert.Equal($"Success! {resourceAmount} {resourceType} has been withdrawn from {testWorkflow.User.UserName}'s account. The new {resourceType} balance is {testWorkflow.User.WoodCount}.", testWorkflow.Message);
                    Assert.True(testWorkflow.Success);
                }
                else
                {
                    Assert.Equal("Insufficient Balance. Press any key to return to main menu.", testWorkflow.Message);
                    Assert.False(testWorkflow.Success);
                }    
            }
            else
            {
                Assert.False(testWorkflow.Success);
                Assert.Null(testWorkflow.User);
                Assert.Equal("Invalid user. Press any key to return to main menu.", testWorkflow.Message);
            }


        }

        [Theory]
        [InlineData(ResourceTypes.Gold)]
        [InlineData(ResourceTypes.Wood)]
        [InlineData(ResourceTypes.Stone)]
        [InlineData(ResourceTypes.Iron)]
        [InlineData(ResourceTypes.Invalid)]
        private void Manager_RouteDeposit_RouteCompletes(ResourceTypes resource)
        {
            InMemoryDataSources dataSource = new InMemoryDataSources();
            Manager manager = new Manager(dataSource);

            
            int expectedResult = 5;

            try
            {
                int resourceDeposit = manager.RouteDeposit(dataSource.Users[0], resource, 5);
                Assert.Equal(expectedResult, resourceDeposit);
            }
            catch(Exception)
            {
                //catches exception for invalid case
            }
        }

        [Theory]
        [InlineData(ResourceTypes.Gold)]
        [InlineData(ResourceTypes.Wood)]
        [InlineData(ResourceTypes.Stone)]
        [InlineData(ResourceTypes.Iron)]
        [InlineData(ResourceTypes.Invalid)]
        private void Manager_RouteWithdraw_RouteCompletes(ResourceTypes resource)
        {
            InMemoryDataSources dataSource = new InMemoryDataSources();
            Manager manager = new Manager(dataSource);

            
            int expectedResult = -5;

            try
            {
                int resourceDeposit = manager.RouteWithdrawal(dataSource.Users[0], resource, 5);
                Assert.Equal(expectedResult, resourceDeposit);
            }
            catch(Exception)
            {
                //catches exception for invalid case
            }
        }

        [Theory]
        [InlineData(ResourceTypes.Gold,100000, true)]
        [InlineData(ResourceTypes.Wood,5, true)]
        [InlineData(ResourceTypes.Stone,5, true)]
        [InlineData(ResourceTypes.Iron,5, true)]
        [InlineData(ResourceTypes.Invalid,5, false)]
        [InlineData(ResourceTypes.Gold, 200000, false)]
        [InlineData(ResourceTypes.Wood, 6000, false)]
        [InlineData(ResourceTypes.Stone, 2000, false)]
        [InlineData(ResourceTypes.Iron, 6000, false)]
        
        public void Manager_CheckForSufficientFunds_ProperlyVerifiesFunds(ResourceTypes resource, int withdrawAmount, bool expectedResult)
        {
            InMemoryDataSources dataSource = new InMemoryDataSources();
            Manager manager = new Manager(dataSource);

            

            try
            {
                bool sufficientFunds = manager.CheckForSufficientFunds(dataSource.Users[1], resource, withdrawAmount);
                Assert.Equal(expectedResult, sufficientFunds);
            }
            catch (Exception ex)
            {
                //catches the exception for invalid resource.
            }
            
        }

        #endregion


    }
}
