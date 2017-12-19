using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondTrial.Model;
using SecondTrial.View;

namespace SecondTrial.ViewModel
{
    class CustomerVm : NotifyChangedPropertyClass
    {
        private SingletonOrder _singleton;
        private Customer _customer;
        private readonly FrameNavigate _frameNavigate;

        public RelayCommand ContinueRelayCommand { get; set; }

        public Customer Customer
        {
            get => _customer;
            set => _customer = value;
        }

        public CustomerVm()
        {
            _singleton = SingletonOrder.GetInstance();

            Customer = new Customer();

            // Frame Navigate Object instance
            _frameNavigate = new FrameNavigate();

            ContinueRelayCommand = new RelayCommand(Continue);
        }

        public void Continue()
        {
            _singleton.SetCustomerToOrder(Customer);
            Type type = typeof(OrderDetailsForCustomer);
            _frameNavigate.ActivateFrameNavigation(type);

        }
    }
}


