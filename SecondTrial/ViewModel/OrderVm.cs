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
        
        private ObservableCollection<Box> _myOrders;

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

        public RelayCommand BuyCommand { get; set; }

        public RelayCommand ContinueCommand { get; set; }

        public ObservableCollection<Box> MyOrders
        {
            get { return _myOrders; }
            set
            {
                _myOrders = value;
                OnPropertyChanged(nameof(MyOrders));
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
                MyOrders.RemoveAt(SelectedIndex);
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

        public OrderVm()
        {
            _serialize = new Serialization();

            NewOrder = new Order();

            Customer = new Customer();

            CurrentTime = new DateTimeOffset();

            _frameNavigate = new FrameNavigate();

            _singletonOrder = SingletonOrder.GetInstance();

            MyOrders = new ObservableCollection<Box>();

            MyOrders = _singletonOrder.GetBox();

            Customer = _singletonOrder.GetCustomer();
            
            TextList = new ObservableCollection<string>();

            AddTextList();
            
            SetBoxDetails();

            MyExistingOrders = new ObservableCollection<Order>();

            ReadOrders();

            BuyCommand = new RelayCommand(GoToNextPage);

            ContinueCommand = new RelayCommand(Continue);
        }


        public void AddTextList()
        {
            foreach (var box in MyOrders)
            {
                TextList.Add("Delete item");

            }
        }

        public void SetBoxDetails()
        {
            double newPrice = 0;

            foreach (var box in MyOrders)
            {
                if (box.CheckSubscription == false)
                {
                    TotalPrice = TotalPrice + Int32.Parse(box.MyInformationAboutBoxes.Price);
                    //box.MyInformationAboutBoxes.Price = box.MyInformationAboutBoxes.Price + "€";
                    
                }
                else
                {
                    if (box.NumberofSubscriptionMonths == 3)
                    {
                        //box.MyInformationAboutBoxes.Name = box.MyInformationAboutBoxes.Name + " x 3 months";
                        newPrice = Convert.ToDouble(box.MyInformationAboutBoxes.Price) * 3;
                        //box.MyInformationAboutBoxes.Price = Convert.ToString(newPrice) + "€";
                        box.MyInformationAboutBoxes.Price = Convert.ToString(newPrice);
                        TotalPrice = TotalPrice + newPrice;
                    }
                    else if (box.NumberofSubscriptionMonths == 6)
                    {
                        //box.MyInformationAboutBoxes.Name = box.MyInformationAboutBoxes.Name + " x 6 months";
                        newPrice = Convert.ToDouble(box.MyInformationAboutBoxes.Price) * 6;
                        //box.MyInformationAboutBoxes.Price = Convert.ToString(newPrice) + "€";
                        box.MyInformationAboutBoxes.Price = Convert.ToString(newPrice) ;
                        TotalPrice = TotalPrice + newPrice;
                    }
                }
            }

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

        public void Buy()
        {
           CurrentTime = DateTimeOffset.Now;

            NewOrder.Customer = Customer;
            NewOrder.Boxes = MyOrders;
            NewOrder.OrderStatus = "Waiting for shipping";
            NewOrder.OrderNumber = Convert.ToString((MyExistingOrders.Count)+1);

            foreach (var box in NewOrder.Boxes)
            {
                if (box.CheckSubscription == true)
                {
                    if (box.NumberofSubscriptionMonths == 3)
                    {
                        NewOrder.OrderDates.Add(CurrentTime);
                        for (int i = 1; i < 3; i++)
                        {
                            NewOrder.OrderDates.Add(CurrentTime.AddMonths(i));
                        }
                    }
                    else
                    {
                        NewOrder.OrderDates.Add(CurrentTime);
                        for (int i = 1; i < 6; i++)
                        {
                            NewOrder.OrderDates.Add(CurrentTime.AddMonths(i));
                        }
                    }
                }
                else
                {
                    NewOrder.OrderDates.Add(CurrentTime);
                }
            }

            MyExistingOrders.Add(NewOrder);
            
        }
        public void GoToNextPage()
        {
            Type type = typeof(CustomerDetails);
            _frameNavigate.ActivateFrameNavigation(type);

        }

        public void Continue()
        {
            Buy();
            SaveNewOrder();
            Type type = typeof(Payment);
            _frameNavigate.ActivateFrameNavigation(type);
        }

    }
}
