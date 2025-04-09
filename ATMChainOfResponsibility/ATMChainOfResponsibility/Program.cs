using ATMChainOfResponsibility.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMChainOfResponsibility
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ATMMachine atm = new ATMMachine(1000);

            atm.ProcessTransaction("Withdrawal", 500);

            atm.ProcessTransaction("Withdrawal", 1100);

            atm.ProcessTransaction("FastCredit", 900);

            atm.ProcessTransaction("FastCredit", 2000);
        }
    }
}
