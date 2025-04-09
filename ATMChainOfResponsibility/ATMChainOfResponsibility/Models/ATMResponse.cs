using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMChainOfResponsibility.Models
{
    public class ATMResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public decimal AmountProccessed { get; set; }
        public string ProccessedTransactionType { get; set; }

        public ATMResponse(bool isSuccessful, string message, 
            decimal amountProccessed = 0, string proccessedTransactionType = "")
        {
            this.IsSuccessful = isSuccessful;
            this.Message = message;
            this.AmountProccessed = amountProccessed;
            this.ProccessedTransactionType = proccessedTransactionType;
        }
    }
}
