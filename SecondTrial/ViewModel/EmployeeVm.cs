using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using SecondTrial.Model;
using SecondTrial.View;

namespace SecondTrial.ViewModel
{
    class EmployeeVm : NotifyChangedPropertyClass
    {
        private readonly FrameNavigate _frameNavigate;
        //Singleton classes 
        private readonly SingletonSerialize _singletonSerialize;
        private readonly SingletonUser _singletonUser;

        private bool _check;
        private bool _check2;

        //Lists
        private ObservableCollection<Order> _myOrders;
        private ObservableCollection<Item> _myItemCollection;
        private ObservableCollection<Order> _loadedOrders;
        private ObservableCollection<Order> _createdOrders;
        private ObservableCollection<Order> _thisMonthsOrders;
        private ObservableCollection<Order> _nextMonthsOrders;
        private ObservableCollection<Order> _chosenOrders;
        private List<string> _adminOptions;
        private List<int> _monthsList;

        //For putting items in a box
        private Item _myItem;
        private Employee _employee;

        private readonly Serialization _serialize;

        //For selected admin option
        private string _selectedOption;

        private int _selectedMonth;
        private Order _selectedOrder;
        private Box _selectedBox;
        
        public RelayCommand ShipCommand { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoOrderDetailCommand { get; set; }
        public RelayCommand GoToCreateBoxesCommand { get; set; }
        public RelayCommand ShowThisMonthsOrderCommand { get; set; }
        public RelayCommand ShowNextMonthsOrderCommand { get; set; }
        public RelayCommand CreateAllBoxesCommand { get; set; }

        //Prop. definitions start here
        public bool Check
        {
            get { return _check; }
            set
            {
                _check = value;
                OnPropertyChanged(nameof(Check));
            }
        }

        public bool Check2
        {
            get { return _check2; }
            set
            {
                _check2 = value;
                OnPropertyChanged(nameof(Check2));
            }
        }
        
        public Item MyItem
        {
            get => _myItem; set => _myItem = value;
        }

        public ObservableCollection<Order> MyOrders
        {
            get => _myOrders;
            set
            {
                _myOrders = value;
                OnPropertyChanged(nameof(MyOrders));
            } 
        }

        public ObservableCollection<Item> MyItemCollection
        {
            get => _myItemCollection;
            set
            {
                _myItemCollection = value;
                OnPropertyChanged(nameof(MyItemCollection));
            }
        }

        public ObservableCollection<Order> LoadedOrders
        {
            get { return _loadedOrders; }
            set
            {
                _loadedOrders = value;
                OnPropertyChanged(nameof(LoadedOrders));
            }
        }

        public ObservableCollection<Order> CreatedOrders
        {
            get { return _createdOrders; }
            set
            {
                _createdOrders = value;
                OnPropertyChanged(nameof(CreatedOrders));
            }
        }

        public ObservableCollection<Order> ThisMonthsOrders
        {
            get => _thisMonthsOrders;
            set
            {
                _thisMonthsOrders = value;
                OnPropertyChanged(nameof(ThisMonthsOrders));
            }
            
        }

        public ObservableCollection<Order> NextMonthsOrders
        {
            get => _nextMonthsOrders;
            set
            {
                _nextMonthsOrders = value;
                OnPropertyChanged(nameof(NextMonthsOrders));
            }
        }

        public ObservableCollection<Order> ChosenOrders
        {
            get => _chosenOrders;
            set
            {
                _chosenOrders = value;
                OnPropertyChanged(nameof(ChosenOrders));
            }
        }
        
        public Employee Employee
        {
            get => _employee;
            set => _employee = value;
        }

        public List<string> AdminOptions
        {
            get => _adminOptions;
            set
            {
                _adminOptions = value;
            }
        }
        
        public List<int> MonthsList
        {
            get => _monthsList;
            set
            {
                _monthsList = value;
            }
        }

        public string SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
            }
        }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            } 
        }

        public Box SelectedBox
        {
            get => _selectedBox;
            set
            {
                _selectedBox = value;
                OnPropertyChanged(nameof(SelectedBox));
            }
        }

        public int SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                OnPropertyChanged(nameof(SelectedMonth));
                SearchOrders();
            }
        }
        //Prop. definitions end here

        //Definitions for finding items in the list
        Random rnd = new Random();
        int _position = 0;

        public EmployeeVm()
        {
            //Initialize
            Check = false;
            Check2 = false;
            SelectedOrder = new Order();
            SelectedBox = new Box();
            ThisMonthsOrders = new ObservableCollection<Order>();
            NextMonthsOrders = new ObservableCollection<Order>();
            ChosenOrders = new ObservableCollection<Order>();
            LoadedOrders = new ObservableCollection<Order>();
            CreatedOrders = new ObservableCollection<Order>();
            AdminOptions = new List<string>();
            MonthsList = new List<int>();
            _frameNavigate = new FrameNavigate();
            _serialize = new Serialization();

            //AddOptions();
            AddMonths();

            Employee = new Employee();
            _singletonUser = SingletonUser.GetInstance();
            Employee = _singletonUser.GetEmployee();

            _singletonSerialize = SingletonSerialize.GetInstance();
            MyOrders = new ObservableCollection<Order>();
            MyItemCollection = new ObservableCollection<Item>();
            //Take orders from file
            MyOrders = _singletonSerialize.GetOrderList();
            //Read items from ItemCatalog
            MyItemCollection = _singletonSerialize.GetItemList();
            //Seperating orders by dates
            SeperateOrders();

            ReadMyOrders();

            CreateAllBoxesCommand = new RelayCommand(CreateBoxes);
            GoOrderDetailCommand = new RelayCommand(GoOrderDetail);
            GoToCreateBoxesCommand = new RelayCommand(GoToCreateBoxes);
            ShowNextMonthsOrderCommand = new RelayCommand(ShowNextMonthsOrder);
            ShowThisMonthsOrderCommand = new RelayCommand(ShowThisMonthsOrder);
            GoBackCommand = new RelayCommand(GoBack);
            ShipCommand = new RelayCommand(ShipProcess);
        }

        //public void AddOptions()
        //{
            
        //    AdminOptions.Add("See Orders and Create Boxes");
        //    AdminOptions.Add("Check Inventory");
        //}

        public void AddMonths()
        {
        
            for (int i = 1; i < 13; i++)
            {
                MonthsList.Add(i);
            }
        }

        //Seperate orders
        public void SeperateOrders() 
        {
            DateTimeOffset currentDateTime = new DateTimeOffset(DateTime.Today);

            foreach (var order in MyOrders)
            {
                foreach (var date in order.OrderDates)
                {
                    if (date.Month == currentDateTime.Month)
                    {
                        if (!ThisMonthsOrders.Contains(order))
                        {
                            ThisMonthsOrders.Add(order);
                        }
                    }
                    else if (date.Month == currentDateTime.AddMonths(1).Month)
                    {
                        if (!NextMonthsOrders.Contains(order))
                        {
                            NextMonthsOrders.Add(order);
                        }
                    }
                }
            }
        }

        
        //Create boxes

        List<Item> _affordableItems = new List<Item>();
        List<Item> _averageItems = new List<Item>();
        List<Item> _expensiveItems = new List<Item>();

        public async void CreateBoxes()
        {
            foreach (var order in ChosenOrders)
            {
                foreach (var box in order.Boxes)
                {
                    var categoryname = box.MyInformationAboutBoxes.Category;

                    SearchItems(categoryname);

                    if (box.MyInformationAboutBoxes.AffordableCheck == true)
                    {
                        RandomSelection(box, _affordableItems);
                    }
                    else if (box.MyInformationAboutBoxes.AverageCheck == true)
                    {
                        RandomSelection(box, _averageItems);
                    }
                    else
                    {
                        RandomSelection(box, _expensiveItems);
                    }
                }
            }

            foreach (var order in ChosenOrders)
            {
                foreach (var box in order.Boxes)
                {
                    if (box.ItemsInMyBox.Count != 10)
                    {
                        foreach (var item in box.ItemsInMyBox)
                        {
                            MyItemCollection.Add(item);
                        }
                        MessageDialog msgDialog = new MessageDialog($"Inventory doesnt have enough items to create {box.MyInformationAboutBoxes.Name} Box in Order: {order.OrderNumber} ");
                        await msgDialog.ShowAsync();
                    }
                    else
                    {
                        MessageDialog msgDialog = new MessageDialog($"The {box.MyInformationAboutBoxes.Name} Box in Order: {order.OrderNumber} created successfully ");
                        await msgDialog.ShowAsync();
                        CreatedOrders.Add(order);
                        ChosenOrders.Remove(order);
                    }
                }
            }

            await _serialize.SaveCreatedOrders(CreatedOrders);

            await _serialize.UpdateItemCatalog(MyItemCollection);

        }

        public void SearchItems(String category)
        {
            _affordableItems.Clear();
            _averageItems.Clear();
            _expensiveItems.Clear();

            List<Item> itemsForMyBox = new List<Item>();

            foreach (var item in MyItemCollection)
            {
                if (item.Category == category)
                {
                    itemsForMyBox.Add(item);
                }
            }

            foreach (var item in itemsForMyBox)
            {
                if (item.Affordable)
                {
                    _affordableItems.Add(item);
                }
                else if (item.Average)
                {
                    _averageItems.Add(item);
                }
                else
                {
                    _expensiveItems.Add(item);
                }
            }
            itemsForMyBox.Clear();
        }
        
        //Random selection for creating box

        public void RandomSelection(Box box, List<Item> itemList)
        {
            for (int i = 0; i < 10; i++)  //I want to put 10 items in a box 
            {
                try
                {
                    _position = rnd.Next(0, itemList.Count);
                    MyItem = itemList.ElementAt(_position);
                    box.ItemsInMyBox.Add(MyItem);
                    itemList.Remove(MyItem);
                    MyItemCollection.Remove(MyItem);
                    
                }
                catch (Exception e)
                {

                }

            }
        }

        public void ShowThisMonthsOrder()
        {
            Check2 = false;
            //ChosenOrders.Clear();
            ChosenOrders = ThisMonthsOrders;
        }

        public void ShowNextMonthsOrder()
        {
            Check = false;
            //ChosenOrders.Clear();
            ChosenOrders = NextMonthsOrders;
        }

        public void SearchOrders()
        {
            Check = false;
            Check2 = false;
            ObservableCollection<Order> _newCollection = new ObservableCollection<Order>();
            //ChosenOrders.Clear();

            foreach (var order in MyOrders)
            {
                foreach (var date in order.OrderDates)
                {
                    if (SelectedMonth == date.Month)
                    {
                        if (!_newCollection.Contains(order) )
                        {
                            _newCollection.Add(order);
                        }                         
                    }
                }
            }
            ChosenOrders = _newCollection;
        }

        public void GoToCreateBoxes()
        {
            Type type = typeof(CreateBoxes);
            _frameNavigate.ActivateFrameNavigation(type);
        }

        public void GoOrderDetail()
        {
            Type type = typeof(OrderDetailsForAdmin);
            _frameNavigate.ActivateFrameNavigation(type);
        }

        public void GoBack()
        {
            Type type = typeof(Login);
            _frameNavigate.ActivateFrameNavigation(type);
        }

        public async void ReadMyOrders()
        {
            LoadedOrders = await _serialize.LoadCreatedOrders();
        }

        public async void ShipProcess()
        {
            SelectedOrder.OrderStatus = "Shipped";

            MessageDialog myMessageDialog = new MessageDialog("Order has shipped.");
            await myMessageDialog.ShowAsync();

            foreach (var order in LoadedOrders)
            {
                if (order.Boxes.Contains(SelectedBox))
                {
                    order.Boxes.Remove(SelectedBox);
                }

            }
            Check = false;

        }

    }
}
