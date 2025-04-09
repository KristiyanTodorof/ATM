using ATMChainOfResponsibility.Handler;
using ATMChainOfResponsibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMChainOfResponsibility.Handlers
{
    public class WithdrawalHandler : ATMHandler
    {
        public override void HandleRequest(ATMRequest request, decimal balance, ref decimal newBalance, ref bool handled)
        {
            if (request.TransactionType == "Withdrawal")
            {
                if (request.RequestAmount <= balance)
                {
                    newBalance = balance - request.RequestAmount;

                    Console.WriteLine($"Withdrawal successful: ${request.RequestAmount}");
                    Console.WriteLine($"Remaining balance: ${newBalance}");

                    handled = true;
                }
                else
                {
                    Console.WriteLine("Insufficient funds for withdrawal.");

                    if (_successor != null)
                    {
                        Console.WriteLine("Checking if you qualify for Fast Credit...");

                        ATMRequest fastCreditRequest = new ATMRequest(
                            "FastCredit",
                            request.RequestAmount,
                            request.Client
                        );

                        _successor.HandleRequest(fastCreditRequest, balance, ref newBalance, ref handled);
                    }
                }
            }
            else if (_successor != null)
            {
                _successor.HandleRequest(request, balance, ref newBalance, ref handled);
            }
        }

    }
}
