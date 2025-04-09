using ATMChainOfResponsibility.Handler;
using ATMChainOfResponsibility.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMChainOfResponsibility.Handlers
{
    public class FastCreditHandler : ATMHandler
    {
        public override void HandleRequest(ATMRequest request, decimal balance, ref decimal newBalance, ref bool handled)
        {
            if (request.TransactionType == "FastCredit")
            {
                decimal creditRatio = GetCreditRatioByClientType(request.Client.Type);
                decimal maxCredit = balance * creditRatio;

                if (request.RequestAmount <= maxCredit)
                {
                    newBalance = balance + request.RequestAmount;

                    Console.WriteLine($"Fast Credit approved for: ${request.RequestAmount}");
                    Console.WriteLine($"Your total balance after credit: ${newBalance}");
                    Console.WriteLine("Note: Interest will apply to this credit amount.");

                    handled = true;
                }
                else
                {
                    decimal reducedOfferRatio = GetReducedOfferRatioByClientType(request.Client.Type);
                    decimal reducedOffer = request.RequestAmount * reducedOfferRatio;

                    if (reducedOffer <= maxCredit)
                    {
                        Console.WriteLine($"You don't qualify for the full requested amount (${request.RequestAmount}).");
                        Console.WriteLine($"We can offer you: ${reducedOffer}");
                        Console.WriteLine("Would you like to accept this reduced offer? (Yes/No)");

                        string userResponse = "Yes";

                        if (userResponse == "Yes")
                        {
                            newBalance = balance + reducedOffer;

                            Console.WriteLine($"Fast Credit approved for: ${reducedOffer}");
                            Console.WriteLine($"Your total balance after credit: ${newBalance}");
                            Console.WriteLine("Note: Interest will apply to this credit amount.");

                            handled = true;
                        }
                        else
                        {
                            Console.WriteLine("Fast Credit offer declined.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you don't qualify for Fast Credit at this time.");
                        Console.WriteLine($"Maximum credit available: ${maxCredit}");
                    }
                }
            }
            else if (_successor != null)
            {
                _successor.HandleRequest(request, balance, ref newBalance, ref handled);
            }
        }

        private decimal GetCreditRatioByClientType(ClientType type)
        {
            switch (type)
            {
                case ClientType.Premium:
                    return 0.75m;  
                case ClientType.Business:
                    return 0.85m; 
                default:
                    return 0.60m;
            }
        }

        private decimal GetReducedOfferRatioByClientType(ClientType type)
        {
            switch (type)
            {
                case ClientType.Premium:
                    return 0.80m;  // Premium clients get 80% of requested amount as reduced offer
                case ClientType.Business:
                    return 0.85m;  // Business clients get 85% of requested amount as reduced offer
                default:
                    return 0.70m;  // Regular clients get 70% of requested amount as reduced offer
            }
        }
    }
   
}
