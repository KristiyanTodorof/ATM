using ATMChainOfResponsibility.Handler;
using ATMChainOfResponsibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMChainOfResponsibility.Handlers
{
    public class AuthenticationHandler : ATMHandler
    {
        public override void HandleRequest(ATMRequest request, decimal balance, ref decimal newBalance, ref bool handled)
        {
            Console.WriteLine($"Authenticating client: {request.Client.Name}");

            Console.WriteLine("Authentication successful!");

            if (_successor != null)
            {
                _successor.HandleRequest( request, balance, ref newBalance, ref handled);
            }
        }
    }
}
