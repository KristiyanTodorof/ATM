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
        public override void HandleRequest(ATMRequest request)
        {
            if(request.TransactionType == "Withdrawal")
            {
                if(request.RequestAmount <= request.CurrentBalance)
                {
                    decimal newBalance = request.CurrentBalance - request.RequestAmount;
                    Console.WriteLine($"Withdrawal successful: ${request.RequestAmount}");
                    Console.WriteLine($"Remaining balance: ${newBalance}");
                }
                else
                {
                    Console.WriteLine("Insufficient fund for withdrawal.");

                    if (_successor != null)
                    {
                        Console.WriteLine("Checking if you qualify for Fast Credit...");

                        ATMRequest fastCreditRequest = new ATMRequest("FastCredit",request.RequestAmount, request.CurrentBalance);

                        _successor.HandleRequest(fastCreditRequest);
                    }
                }
            }
            else if (_successor != null)
            {
                _successor.HandleRequest(request);
            }
        }
    }
}
