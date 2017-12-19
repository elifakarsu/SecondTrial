using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Payments;
using SecondTrial.ViewModel;

namespace SecondTrial.Model
{
    public class Order : NotifyChangedPropertyClass
    {

        private ObservableCollection<Box> _boxes;
        private Customer _customer;
        private string _orderStatus;
        private string _orderNumber;
        private ObservableCollection<DateTimeOffset> _orderDates;
  
        public ObservableCollection<Box> Boxes { get => _boxes; set => _boxes = value; }
        public Customer Customer { get => _customer; set => _customer = value; }
        public string OrderStatus
        {
            get => _orderStatus;
            set
            {
                _orderStatus = value;
                OnPropertyChanged(nameof(OrderStatus));
            } 
        }
        public string OrderNumber { get => _orderNumber; set => _orderNumber = value; }
        public ObservableCollection<DateTimeOffset> OrderDates { get => _orderDates; set => _orderDates = value; }
        
        

        public Order()
        {
            OrderDates = new ObservableCollection<DateTimeOffset>();
        }

        public Order(ObservableCollection<Box> boxes, Customer customer, string orderStatus,string orderNumber, ObservableCollection<DateTimeOffset> orderDates)
        {
            _boxes = boxes;
            _customer = customer;
            _orderStatus = orderStatus;
            _orderNumber = orderNumber;
            _orderDates = orderDates;
           
        }

    }
}
