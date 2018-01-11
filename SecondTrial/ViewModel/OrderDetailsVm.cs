using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondTrial.Model;

namespace SecondTrial.ViewModel
{
   public class OrderDetailsVm : NotifyChangedPropertyClass
    {
        private SingletonOrder _singleton;
        private Order _order;
        private string ShippingPrice;
        private DateTimeOffset _currentDateTimeOffset;

        public Order Order
        {
            get { return _order; }
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        public DateTimeOffset CurrentDateTimeOffset
        {
            get { return _currentDateTimeOffset; }
            set
            {
                _currentDateTimeOffset = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        public OrderDetailsVm()
        {
            _currentDateTimeOffset = new DateTimeOffset(DateTime.Now.Date);
            _singleton = SingletonOrder.GetInstance();
            Order  = _singleton.GetOrder();
        }


    }
}
