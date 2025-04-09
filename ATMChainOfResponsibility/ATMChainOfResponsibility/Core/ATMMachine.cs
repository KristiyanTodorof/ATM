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
        private ATMHandler withdrawalHandler;
        private ATMHandler fastCreditHandler;
        private decimal userBalance;

        public ATMMachine(decimal initialBalance)
        {
            this.userBalance = initialBalance;

            this.withdrawalHandler = new WithdrawalHandler();
            this.fastCreditHandler = new FastCreditHandler();

            this.withdrawalHandler.SetNextHandler(this.fastCreditHandler);
        }
        public void ProcessTransaction(string transactionType, decimal amount)
        {
            Console.WriteLine($"\n--- Processing {transactionType}: ${amount} ---");
            Console.WriteLine($"Current Balance: ${this.userBalance}");

            ATMRequest request = new ATMRequest(transactionType, amount, this.userBalance);
            this.withdrawalHandler.HandleRequest(request);
        }
    }
}
