using ATMChainOfResponsibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMChainOfResponsibility.Handler
{
    public abstract class ATMHandler
    {
        protected ATMHandler _successor;

        public void SetNextHandler(ATMHandler successor)
        {
            this._successor = successor;
        }
        public abstract void HandleRequest(ATMRequest request);
    }
}
