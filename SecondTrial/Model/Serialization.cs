using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SecondTrial.Model
{
    class Serialization
    {
        private ObservableCollection<Item> _itemsCatalog;
        private ObservableCollection<BoxInformation> _boxInformation;
        private ObservableCollection<Employee> _employeeInfo;
        private ObservableCollection<Order> _myOrders;
        private ObservableCollection<Order> _myCreatedOrders;

        public async Task SaveOrders(ObservableCollection<Order> order)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var jsonFile = await localFolder.CreateFileAsync("Orders.json", CreationCollisionOption.ReplaceExisting);
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Order>));
            using (var stream = await jsonFile.OpenStreamForWriteAsync())
            {
                jsonSerializer.WriteObject(stream, order);
            }
        }

        public async Task SaveShippedOrders(ObservableCollection<Order> order)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var jsonFile = await localFolder.CreateFileAsync("ShippedOrders.json", CreationCollisionOption.ReplaceExisting);
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Order>));
            using (var stream = await jsonFile.OpenStreamForWriteAsync())
            {
                jsonSerializer.WriteObject(stream, order);
            }
        }

        public async Task SaveCreatedOrders(ObservableCollection<Order> order)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var jsonFile = await localFolder.CreateFileAsync("CreatedOrders.json", CreationCollisionOption.ReplaceExisting);
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Order>));
            using (var stream = await jsonFile.OpenStreamForWriteAsync())
            {
                jsonSerializer.WriteObject(stream, order);
            }
        }

        public async Task<ObservableCollection<Order>> LoadCreatedOrders()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var jsonFile = await localFolder.GetFileAsync("CreatedOrders.json");
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Order>));
            using (var stream = await jsonFile.OpenStreamForReadAsync())
            {
                _myCreatedOrders = jsonSerializer.ReadObject(stream) as ObservableCollection<Order>;
            }

            return _myCreatedOrders;
        }

        public async Task UpdateItemCatalog(ObservableCollection<Item> items)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var jsonFile = await localFolder.CreateFileAsync("itemCatalog.json", CreationCollisionOption.ReplaceExisting);
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Item>));
            using (var stream = await jsonFile.OpenStreamForWriteAsync())
            {
                jsonSerializer.WriteObject(stream, items);
            }
        }
        
        public async Task<ObservableCollection<Item>> LoadFromJson()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var jsonFile = await localFolder.GetFileAsync("itemCatalog.json");
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Item>));
            using (var stream = await jsonFile.OpenStreamForReadAsync())
            {
                _itemsCatalog = jsonSerializer.ReadObject(stream) as ObservableCollection<Item>;
            }

            return _itemsCatalog;
        }
        
        public async Task<ObservableCollection<Employee>> LoadAdminInfo()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var jsonFile = await localFolder.GetFileAsync("Employee.json");
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Employee>));
            using (var stream = await jsonFile.OpenStreamForReadAsync())
            {
                _employeeInfo = jsonSerializer.ReadObject(stream) as ObservableCollection<Employee>;
            }

            return _employeeInfo;
        }

        public async Task<ObservableCollection<Order>> LoadOrders()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var jsonFile = await localFolder.GetFileAsync("Orders.json");
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Order>));
            using (var stream = await jsonFile.OpenStreamForReadAsync())
            {
                _myOrders  = jsonSerializer.ReadObject(stream) as ObservableCollection<Order>;
            }

            return _myOrders;
        }



    }
}
