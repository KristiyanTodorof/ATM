using ATMChainOfResponsibility.Handler;
using ATMChainOfResponsibility.Handlers;
using ATMChainOfResponsibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMChainOfResponsibility.Core
{
    public class ATMMachine
    {
        //private ATMHandler withdrawalHandler;
        //private ATMHandler fastCreditHandler;
        //private decimal userBalance;

        //public ATMMachine(decimal initialBalance)
        //{
        //    this.userBalance = initialBalance;

        //    this.withdrawalHandler = new WithdrawalHandler();
        //    this.fastCreditHandler = new FastCreditHandler();

        //    this.withdrawalHandler.SetNextHandler(this.fastCreditHandler);
        //}
        //public void ProcessTransaction(string transactionType, decimal amount)
        //{
        //    Console.WriteLine($"\n--- Processing {transactionType}: ${amount} ---");
        //    Console.WriteLine($"Current Balance: ${this.userBalance}");

        //    ATMRequest request = new ATMRequest(transactionType, amount, this.userBalance);
        //    this.withdrawalHandler.HandleRequest(request);
        //}
        private string atmId;
        private string location;
        private ATMHandler firstHandler;
        private Dictionary<string, decimal> accountBalances;

        public ATMMachine(string atmId, string location)
        {
            this.atmId = atmId;
            this.location = location;
            this.accountBalances = new Dictionary<string, decimal>();

            var authHandler = new AuthenticationHandler();
            var withdrawalHandler = new WithdrawalHandler();
            var fastCreditHandler = new FastCreditHandler();

            authHandler.SetNextHandler(withdrawalHandler);
            withdrawalHandler.SetNextHandler(fastCreditHandler);

            firstHandler = authHandler;
        }

        public string GetInfo()
        {
            return $"ATM {this.atmId} at {this.location}";
        }

        public void RegisterAccount(string accountNumber, decimal initialBalance)
        {
            accountBalances[accountNumber] = initialBalance;
        }

        public decimal ProcessTransaction(string transactionType, decimal amount, Client client)
        {
            Console.WriteLine($"\n--- ATM {this.atmId} at {this.location} ---");
            Console.WriteLine($"--- Processing {transactionType}: ${amount} for {client.Name} ---");

            // Get current balance for the account
            if (!accountBalances.ContainsKey(client.AccountNumber))
            {
                Console.WriteLine("Error: Account not registered with this ATM.");
                return 0;
            }

            decimal currentBalance = accountBalances[client.AccountNumber];
            Console.WriteLine($"Current Balance: ${currentBalance}");

            // Create the request
            ATMRequest request = new ATMRequest(transactionType, amount, client);

            // Process through chain of handlers
            decimal newBalance = currentBalance;
            bool handled = false;

            firstHandler.HandleRequest(request, currentBalance, ref newBalance, ref handled);

            if (handled)
            {
                accountBalances[client.AccountNumber] = newBalance;

                decimal transactionAmount = (transactionType == "Withdrawal") ? amount : newBalance - currentBalance;
                client.AddTransaction(transactionAmount, transactionType, newBalance);

                return newBalance;
            }
            else
            {
                Console.WriteLine("Transaction could not be processed.");
                return currentBalance;
            }
        }

    }
}
