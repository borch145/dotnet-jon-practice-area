using MaterialsAppDemo.Data;
using MaterialsAppDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsAppDemo.BLL
{
    public class Manager
    {
        private IDataSource IDataSource { get; set; }

        public Manager(IDataSource dataSource)
        {
            IDataSource = dataSource;  
        }
        public WorkflowResponse CheckResources(string username)
        {
            WorkflowResponse response = new WorkflowResponse();
            try
            {
                
                response.User = IDataSource.Authenticate(username);
                if (response.User != null)
                {
                    response.Success = true;
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid user. Press any key to return to main menu.";
                    return response;
                }
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;

            }
        }
        public WorkflowResponse DepositResource(string username, ResourceTypes resourceType, int resourceAmount)
        {
            WorkflowResponse workflowResponse = new WorkflowResponse();

            try
            {
                workflowResponse.User = IDataSource.Authenticate(username);
                if (workflowResponse.User != null)
                {
                    workflowResponse.Success = true;
                    int newTotal = RouteDeposit(workflowResponse.User, resourceType, resourceAmount);
                    workflowResponse.Message = $"Success! {resourceAmount} {resourceType} has been deposited in {workflowResponse.User.UserName}'s account. The new {resourceType} balance is {newTotal}.";
                    return workflowResponse;
                }
                else
                {
                    workflowResponse.Message = "Invalid user.";
                    return workflowResponse;
                }
            }
            catch(Exception ex)
            {
                workflowResponse.Success = false;
                workflowResponse.Message = ex.Message;
                return workflowResponse;
            }
        }
        public WorkflowResponse WithdrawResource(string username, ResourceTypes resourceType, int resourceAmount)
        {
            WorkflowResponse workflowResponse = new WorkflowResponse();
            try
            {
                workflowResponse.User = IDataSource.Authenticate(username);

                if (workflowResponse.User != null)
                {
                    bool sufficientBalance = CheckForSufficientFunds(workflowResponse.User, resourceType, resourceAmount);

                    if (sufficientBalance)
                    {
                        int newTotal = RouteWithdrawal(workflowResponse.User, resourceType, resourceAmount);
                        workflowResponse.Message = $"Success! {resourceAmount} {resourceType} has been withdrawn from {workflowResponse.User.UserName}'s account. The new {resourceType} balance is {newTotal}.";
                        workflowResponse.Success = true;
                    }
                    else
                    {
                        workflowResponse.Message = "Insufficient Balance. Press any key to return to main menu.";
                        workflowResponse.Success = false;
                    }
                }
                else
                {
                    workflowResponse.Message = "Invalid user. Press any key to return to main menu.";
                    workflowResponse.Success = false;
                }
                return workflowResponse;
            }
            catch(Exception ex)
            {
                workflowResponse.Success=false;
                workflowResponse.Message = ex.Message;
                return workflowResponse;
            }
        }
        public int RouteDeposit(User user, ResourceTypes resourceType, int resourceAmount)
        {
            switch (resourceType)
            {
                case ResourceTypes.Wood:
                    return IDataSource.DepositWood(user, resourceAmount);
                    
                    
                case ResourceTypes.Stone:
                    return IDataSource.DepositStone(user, resourceAmount);
                    
                  
                case ResourceTypes.Iron:
                    return IDataSource.DepositIron(user, resourceAmount);
                   

                case ResourceTypes.Gold:
                    return IDataSource.DepositGold(user, resourceAmount);
                  

                default:
                    throw new Exception("Error: RouteDeposit did not route successfully.");

            }
        }
        public int RouteWithdrawal(User user, ResourceTypes resourceType, int resourceAmount)
        {
            switch (resourceType)
            {
                case ResourceTypes.Wood:
                    return IDataSource.WithdrawWood(user, resourceAmount);


                case ResourceTypes.Stone:
                    return IDataSource.WithdrawStone(user, resourceAmount);


                case ResourceTypes.Iron:
                    return IDataSource.WithdrawIron(user, resourceAmount);


                case ResourceTypes.Gold:
                    return IDataSource.WithdrawGold(user, resourceAmount);


                default:
                    throw new Exception("Error: RouteWithdrawal did not route successfully.");

            }
        }
        public bool CheckForSufficientFunds(User user, ResourceTypes resource, int resourceAmount)
        {
            int balance = 0;

            switch (resource)
            {
                case ResourceTypes.Wood:
                    balance = user.WoodCount;
                    break;
                case ResourceTypes.Stone:
                    balance = user.StoneCount;
                    break;
                case ResourceTypes.Iron:
                    balance = user.IronCount;
                    break;
                case ResourceTypes.Gold:
                    balance = user.GoldCount;
                    break;
                default:
                    throw new Exception("Manager.CheckForSufficientFunds has failed to target an account.");
                    
            }

            if (resourceAmount <= balance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
