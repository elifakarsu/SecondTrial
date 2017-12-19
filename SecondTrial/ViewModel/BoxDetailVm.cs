 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using SecondTrial.Model;
using SecondTrial.View;

namespace SecondTrial.ViewModel
{
    class BoxDetailVm : NotifyChangedPropertyClass
    {
        //For pass order to other pages
        private readonly SingletonOrder _singletonOrder;
        //For taking selected box from Singleton class
        private readonly SingletonBox _singletonBox;
        //For showing selected box from previous page
        private Box _selectedBox;
        //For subscription
        private string _selectedPlan;
        //For check subscription
        private bool _checkSubscription;
        //For subscription plan
        private int _numberofSubscriptionMonths;
        //For my orders
        private ObservableCollection<Box> _orderList;
        //For go to another page
        private readonly FrameNavigate _frameNavigate;
        //For customer plan
        private List<string> _planList;
        //For adding ordered boxes in an order list
        public RelayCommand AddMyOrder { get; set; }

        public RelayCommand BackToPreviousPageCommand { get; set; }

        //Prop. definitions starts here

        public ObservableCollection<Box> OrderList
        {
            get
            {
                return _orderList;
            }
            set
            {
                _orderList = value;
                OnPropertyChanged(nameof(OrderList));
            }
        }

        public Box SelectedBox
        {
            get
            {
                return _selectedBox;
            }
            set
            {
                _selectedBox = value;
                OnPropertyChanged(nameof(SelectedBox));
            }
        }

        public List<string> PlanList
        {
            get { return _planList; }
            set
            {
               _planList = value;
                OnPropertyChanged(nameof(PlanList));
            }
        }

        public string SelectedPlan
        {
            get
            {
                return _selectedPlan;
            }
            set
            {
                _selectedPlan = value;
                OnPropertyChanged(nameof(SelectedPlan));
                
            }
        }

        public bool CheckSubscription
        {
            get => _checkSubscription;
            set => _checkSubscription = value;
        }

        public int NumberofSubscriptionMonths
        {
            get => _numberofSubscriptionMonths;
            set => _numberofSubscriptionMonths = value;
        }

        //Prop. definitions ends here

        //Ctor

        public BoxDetailVm()
        {
            _singletonBox = SingletonBox.GetInstance();

            _singletonOrder = SingletonOrder.GetInstance();
            
            SelectedBox = _singletonBox.GetBox();
            
            CheckOrderList();

            _frameNavigate = new FrameNavigate();

            PlanList = new List<string>();

            SetPlan();

            AddMyOrder = new RelayCommand(AddItemsBySelectingPlan);

            BackToPreviousPageCommand = new RelayCommand(BackToPreviousPage);

        }

        //Passing order list to singleton and going to shopping page

        public void GoShoppingPage()
        {
            _singletonOrder.SetBoxToOrder(OrderList);
            Type type = typeof(ShoppingPage);
            _frameNavigate.ActivateFrameNavigation(type);
        }

        //Customer can choose plan (1-3-6 Month)

        public void AddItemsBySelectingPlan()
        {
            if (SelectedPlan == null)
            {
                //Shows a message box
                Exception();
            }
            else
            {
                if (SelectedPlan == PlanList.ElementAt(0))
                {
                    SelectedBox.CheckSubscription = false;
                    SelectedBox.NumberofSubscriptionMonths = 1;

                    OrderList.Add(SelectedBox);

                    ShowMessageBox();
                }
                else if (SelectedPlan == PlanList.ElementAt(1))
                {
                    SelectedBox.CheckSubscription = true;
                    SelectedBox.NumberofSubscriptionMonths = 3;
                    SelectedBox.MyInformationAboutBoxes.Price = Convert.ToString(CalculateNewPrice());

                    OrderList.Add(SelectedBox);

                    ShowMessageBox();

                }
                else if (SelectedPlan == PlanList.ElementAt(2))
                {
                    SelectedBox.CheckSubscription = true;
                    SelectedBox.NumberofSubscriptionMonths = 6;
                    SelectedBox.MyInformationAboutBoxes.Price = Convert.ToString(CalculateNewPrice());
                    
                    OrderList.Add(SelectedBox);
                    
                    ShowMessageBox();
                }
            }
        }

        //Continue shopping or go to payment

        public async void ShowMessageBox()
        {
            MessageDialog showDialog = new MessageDialog("Do you want to continue the shopping?");

            showDialog.Commands.Add(new UICommand("Continue Shopping") { Id = 0 });

            showDialog.Commands.Add(new UICommand("Go to shopping bag") { Id = 1 });

            showDialog.DefaultCommandIndex = 0;

            showDialog.CancelCommandIndex = 1;

            var result = await showDialog.ShowAsync();

            if ((int)result.Id == 1)
            {
                GoShoppingPage();
            }
            else if ((int) result.Id == 0)
            {
                _singletonOrder.SetBoxToOrder(OrderList);
                Type type = typeof(MainPage);
                _frameNavigate.ActivateFrameNavigation(type);
            }

        }

        public void SetPlan()
        {
            string plan_1 = "1 Month Subscription" + System.Environment.NewLine + $"{SelectedBox.MyInformationAboutBoxes.Price} €";
            
            string plan_2 = "3 Month Subscription" + System.Environment.NewLine + $"{CalculateNewPrice()*3} €";

            string plan_3 = "6 Month Subscription" + System.Environment.NewLine + $"{CalculateNewPrice()*6} €";

            PlanList.Add(plan_1);
            PlanList.Add(plan_2);
            PlanList.Add(plan_3);
        }

        public double CalculateNewPrice()
        {
            double convertedPrice = Int32.Parse(SelectedBox.MyInformationAboutBoxes.Price);
            double discount = ((convertedPrice / 100) * 5);
            double newPrice = convertedPrice - discount;
            return newPrice;
        }

        public async void Exception()
        {
            MessageDialog showDialog = new MessageDialog("You have to choose a plan");
            await showDialog.ShowAsync();
        }

        public void BackToPreviousPage()
        {
            Type type = typeof(ShowBoxes);
            _frameNavigate.ActivateFrameNavigation(type);

        }
        
        //For control customer has created a order before or not
        public void CheckOrderList()
        {
            if (_singletonOrder.GetBox()==null)
            {
                OrderList = new ObservableCollection<Box>();
            }
            else
            {
                OrderList = _singletonOrder.GetBox();
            }

        }

    }
    
}
