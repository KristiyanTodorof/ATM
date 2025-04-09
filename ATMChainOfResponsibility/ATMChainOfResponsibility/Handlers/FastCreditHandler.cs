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
        public override void HandleRequest(ATMRequest request)
        {
            if (request.TransactionType == "FastCredit")
            {
                decimal maxCredit = request.CurrentBalance * 0.6m;

                if (request.RequestAmount <= maxCredit)
                {
                    Console.WriteLine($"Fast Credit approved for: ${request.RequestAmount}");
                    Console.WriteLine($"Your total balance after credit: ${request.CurrentBalance + request.RequestAmount}");
                    Console.WriteLine("Note: Interest will apply to this credit amount.");
                }
                else
                {
                    decimal reducedOffer = request.RequestAmount * 0.7m;

                    if (reducedOffer <= maxCredit)
                    {
                        Console.WriteLine($"You don't qualify for the full requested amount (${request.RequestAmount}).");
                        Console.WriteLine($"We can offer you: ${reducedOffer}");
                        Console.WriteLine("Would you like to accept this reduced offer? (Yes/No)");

                        string userResponse = "Yes";

                        if (userResponse == "Yes")
                        {
                            Console.WriteLine($"Fast Credit approved for: ${reducedOffer}");
                            Console.WriteLine($"Your total balance after credit: ${request.CurrentBalance + reducedOffer}");
                            Console.WriteLine("Note: Interest will apply to this credit amount.");
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
                // If it's not a fast credit request, pass to the next handler
                _successor.HandleRequest(request);
            }

        }

    }
}
