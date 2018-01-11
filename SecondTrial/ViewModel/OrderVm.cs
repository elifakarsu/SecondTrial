using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Store;
using SecondTrial.Model;
using SecondTrial.View;

namespace SecondTrial.ViewModel
{
    class OrderVm : NotifyChangedPropertyClass
    {
        
        private ObservableCollection<Box> _myNewOrders;

        private ObservableCollection<Order> _myExistingOrders;

        private readonly FrameNavigate _frameNavigate;

        private readonly SingletonOrder _singletonOrder;

        private readonly Serialization _serialize;

        private Customer _customer;

        private Order _newOrder;
        
        private ObservableCollection<String> _textList;

        private int _selectedIndex;

        private double _totalPrice;

        private DateTimeOffset _currentTime;

        public RelayCommand CheckOutCommand { get; set; }

        public RelayCommand ContinueCommand { get; set; }

        public ObservableCollection<Box> MyNewOrders
        {
            get { return _myNewOrders; }
            set
            {
                _myNewOrders = value;
                OnPropertyChanged(nameof(MyNewOrders));
            }
        }
        
        public ObservableCollection<string> TextList
        {
            get { return _textList; }
            set
            {
                _textList = value;
                OnPropertyChanged(nameof(TextList));
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                MyNewOrders.RemoveAt(SelectedIndex);
                TextList.RemoveAt(SelectedIndex);
               }
        }

        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public ObservableCollection<Order> MyExistingOrders
        {
            get => _myExistingOrders;
            set => _myExistingOrders = value;
        }

        public Customer Customer
        {
            get => _customer;
            set => _customer = value;
        }

        public Order NewOrder
        {
            get => _newOrder;
            set => _newOrder = value;
        }

        public DateTimeOffset CurrentTime
        {
            get => _currentTime;
            set => _currentTime = value;
        }

        public int ShippingPrice { get; set; }
        public double TotalPriceWithShipping { get; set; }


        public OrderVm()
        {
            ShippingPrice = 7;

            _serialize = new Serialization();

            NewOrder = new Order();

            Customer = new Customer();

            CurrentTime = new DateTimeOffset();

            _frameNavigate = new FrameNavigate();

            _singletonOrder = SingletonOrder.GetInstance();

            MyNewOrders = new ObservableCollection<Box>();

            MyNewOrders = _singletonOrder.GetBox();

            Customer = _singletonOrder.GetCustomer();
            
            TextList = new ObservableCollection<string>();

            AddTextList();
            
            SetBoxDetails();

            MyExistingOrders = new ObservableCollection<Order>();

            ReadOrders();

            CheckOutCommand = new RelayCommand(GoToCustomerPage);

            ContinueCommand = new RelayCommand(Continue);
        }


        public void AddTextList()
        {
            foreach (var box in MyNewOrders)
            {
                TextList.Add("Delete item");

            }
        }

        public void SetBoxDetails()
        {
            double newPrice = 0;

            foreach (var box in MyNewOrders)
            {
                if (box.CheckSubscription == false)
                {
                    TotalPrice = TotalPrice + Int32.Parse(box.MyInformationAboutBoxes.Price);
                }
                else
                {
                    if (box.NumberofSubscriptionMonths == 3)
                    {
                        newPrice = Convert.ToDouble(box.MyInformationAboutBoxes.Price) * 3;
                        box.MyInformationAboutBoxes.Price = Convert.ToString(newPrice);
                        TotalPrice = TotalPrice + newPrice;
                    }
                    else if (box.NumberofSubscriptionMonths == 6)
                    {
                        newPrice = Convert.ToDouble(box.MyInformationAboutBoxes.Price) * 6;
                        box.MyInformationAboutBoxes.Price = Convert.ToString(newPrice) ;
                        TotalPrice = TotalPrice + newPrice;
                    }
                }

            }

            TotalPriceWithShipping = TotalPrice + ShippingPrice;

        }

        public async void ReadOrders()
        {
            try
            {
                MyExistingOrders = await _serialize.LoadOrders();
            }
            catch (Exception e)
            {
                
            }
        }

        public async void SaveNewOrder()
        {
            await _serialize.SaveOrders(MyExistingOrders);
        }

        

        public void GoToCustomerPage()
        {
            Type type = typeof(CustomerDetails);
            _frameNavigate.ActivateFrameNavigation(type);
        }

        public void Continue()
        {
            _singletonOrder.SetCustomerToOrder(Customer);
            Type type = typeof(Payment);
            _frameNavigate.ActivateFrameNavigation(type);
        }

    }
}
