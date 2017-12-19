using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondTrial.Model;

namespace SecondTrial.ViewModel
{
    class PaymentVm
    {
        private readonly PaymentDetails _paymentDetails;


        // public RelayCommand AddCommand { get; set; }

        public PaymentVm()
        {

        }
        
        public string CardNumber
        {
            get => PaymentDetails.CardNumber;
            set => PaymentDetails.CardNumber = value;
        }
        public string CardName
        {
            get => PaymentDetails.CardName;
            set => PaymentDetails.CardName = value;
        }
        public string ExpiryMonth
        {
            get => PaymentDetails.ExpiryMonth;
            set => PaymentDetails.ExpiryMonth = value;
        }

        public string ExpiryYear
        {
            get => PaymentDetails.ExpiryYear;
            set => PaymentDetails.ExpiryYear = value;
        }

        public string SecurityNumber
        {
            get => PaymentDetails.SecurityNumber;
            set => PaymentDetails.SecurityNumber = value;
        }
        
        public PaymentDetails PaymentDetails => _paymentDetails;

        // delegate method Check card method


    }
}
