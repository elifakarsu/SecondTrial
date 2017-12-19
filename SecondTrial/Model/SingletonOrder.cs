using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Payments;

namespace SecondTrial.Model
{
    class SingletonOrder
    {
        public static Order Order;
        public static Customer Customer;
        public static ObservableCollection<Box> OrderList;
        public static PaymentDetails PaymentDetails;

        private static SingletonOrder Instance { get; set; }

        private SingletonOrder()
        {
           Order = new Order();
           Customer = new Customer();
           OrderList = new ObservableCollection<Box>();
           //PaymentDetails = new PaymentDetails();
        }

        public static SingletonOrder GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SingletonOrder();
            }
            return Instance;
        }
        
        public void SetBoxToOrder(ObservableCollection<Box> boxes)
        {
            Order.Boxes = boxes;
        }
        
        public ObservableCollection<Box> GetBox()
        {
                return Order.Boxes;
           
        }

        public void SetCustomerToOrder(Customer customer)
        {
            Order.Customer = customer;
        }

        //public void SetPaymentToCustomer(PaymentDetails paymentDetails )
        //{
        //    Order.Customer.PaymentDetails = paymentDetails;
        //}

        public Customer GetCustomer()
        {
            return Customer;
        }
        
       


    }
}
