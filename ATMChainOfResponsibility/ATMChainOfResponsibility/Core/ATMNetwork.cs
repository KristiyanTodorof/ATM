using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMChainOfResponsibility.Core
{
    public class ATMNetwork
    {
        private List<ATMMachine> atmMachines;

        public ATMNetwork()
        {
            atmMachines = new List<ATMMachine>();
        }

        public void AddATM(ATMMachine atm)
        {
            atmMachines.Add(atm);
        }

        public ATMMachine GetATMByLocation(string location)
        {
            return atmMachines.Find(atm => atm.GetInfo().Contains(location));
        }

        public List<ATMMachine> GetAllATMs()
        {
            return atmMachines;
        }
    }
}
