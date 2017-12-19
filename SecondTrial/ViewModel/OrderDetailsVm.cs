using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondTrial.Model;

namespace SecondTrial.ViewModel
{
    class OrderDetailsVm
    {
        private SingletonOrder _singleton;

        public OrderDetailsVm()
        {
            _singleton = SingletonOrder.GetInstance();
            
        }


    }
}
