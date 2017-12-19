using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondTrial.ViewModel;

namespace SecondTrial.Model
{
    public class Box : NotifyChangedPropertyClass
    {
        private ObservableCollection<Item> _itemsInMyBox;
        private BoxInformation _myInformationAboutBoxes;
        private bool _checkSubscription;
        private int _numberofSubscriptionMonths;
        

        public ObservableCollection<Item> ItemsInMyBox
        {
            get => _itemsInMyBox;
            set
            {
                _itemsInMyBox = value;
                OnPropertyChanged(nameof(ItemsInMyBox));
            }

        }

        public BoxInformation MyInformationAboutBoxes
        {
            get => _myInformationAboutBoxes;
            set => _myInformationAboutBoxes = value;
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
        

        public Box( ObservableCollection<Item> itemsInMyBox, BoxInformation myInformationAboutBoxes, bool checkSubscription, int numberofSubscriptionMonths)
        {   
            _itemsInMyBox = itemsInMyBox;
            _myInformationAboutBoxes = myInformationAboutBoxes;
            _checkSubscription = checkSubscription;
            _numberofSubscriptionMonths = numberofSubscriptionMonths;
        }
        public Box()
        {
            MyInformationAboutBoxes = new BoxInformation();
            ItemsInMyBox = new ObservableCollection<Item>();
            
        }
    }
}
