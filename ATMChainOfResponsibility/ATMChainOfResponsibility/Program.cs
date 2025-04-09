using ATMChainOfResponsibility.Core;
using ATMChainOfResponsibility.Models;
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
            var atmNetwork = new ATMNetwork();

            var downtownATM = new ATMMachine("ATM001", "Downtown");
            var mallATM = new ATMMachine("ATM002", "Shopping Mall");
            var airportATM = new ATMMachine("ATM003", "Airport");

            atmNetwork.AddATM(downtownATM);
            atmNetwork.AddATM(mallATM);
            atmNetwork.AddATM(airportATM);

            var regularClient = new Client("John Smith", "1234-5678", ClientType.Regular);
            var premiumClient = new Client("Jane Doe", "8765-4321", ClientType.Premium);
            var businessClient = new Client("Acme Corporation", "9999-8888", ClientType.Business);

            downtownATM.RegisterAccount(regularClient.AccountNumber, 1000);
            downtownATM.RegisterAccount(premiumClient.AccountNumber, 5000);
            downtownATM.RegisterAccount(businessClient.AccountNumber, 10000);

            mallATM.RegisterAccount(regularClient.AccountNumber, 1000);
            mallATM.RegisterAccount(premiumClient.AccountNumber, 5000);
            mallATM.RegisterAccount(businessClient.AccountNumber, 10000);

            airportATM.RegisterAccount(regularClient.AccountNumber, 1000);
            airportATM.RegisterAccount(premiumClient.AccountNumber, 5000);
            airportATM.RegisterAccount(businessClient.AccountNumber, 10000);

            downtownATM.ProcessTransaction("Withdrawal", 500, regularClient);
            downtownATM.ProcessTransaction("Withdrawal", 1100, regularClient);
            mallATM.ProcessTransaction("FastCredit", 3000, premiumClient);
            airportATM.ProcessTransaction("FastCredit", 8000, businessClient);
            mallATM.ProcessTransaction("Withdrawal", 6000, premiumClient);

            DisplayTransactionHistory(regularClient);
            DisplayTransactionHistory(premiumClient);
            DisplayTransactionHistory(businessClient);
        }

        static void DisplayTransactionHistory(Client client)
        {
            Console.WriteLine($"\n--- Transaction History for {client.Name} ({client.Type}) ---");
            Console.WriteLine("Date\t\t\tAmount\t\tType\t\tNew Balance");

            foreach (var transaction in client.TransactionHistory)
            {
                Console.WriteLine($"{transaction.Date}\t${transaction.Amount}\t{transaction.Type}\t${transaction.NewBalance}");
            }
        }
    }
}
