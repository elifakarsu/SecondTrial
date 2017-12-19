using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTrial.Model
{
    class SingletonSerialize
    {
        public static Serialization Serialize;
        public static ObservableCollection<Item> MyItemCollection;
        public static ObservableCollection<Order> MyOrders;

        private static SingletonSerialize Instance { get; set; }

        private SingletonSerialize()
        {
            Serialize  = new Serialization();
            MyItemCollection = new ObservableCollection<Item>();
            MyOrders = new ObservableCollection<Order>();
            ReadOrders();
            ReadMyItems();
        }

        public static SingletonSerialize GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SingletonSerialize();
            }
            return Instance;
        }

        public async void ReadMyItems()
        {

            MyItemCollection = await Serialize.LoadFromJson();

        }

        public async void ReadOrders()
        {
            MyOrders = await Serialize.LoadOrders();
        }

        public ObservableCollection<Item> GetItemList()
        {
            return MyItemCollection;
        }

        public ObservableCollection<Order> GetOrderList()
        {
            return MyOrders;
        }

    }
}
