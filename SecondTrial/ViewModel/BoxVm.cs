using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using SecondTrial.Model;
using SecondTrial.View;

namespace SecondTrial.ViewModel
{
    class BoxVm : NotifyChangedPropertyClass
    {
        //For get my items from Json file 
        private readonly Serialization _serialize;

        //We have three types of box
        private Box _affordableBox;
        private Box _averageBox;
        private Box _expensiveBox;
        //For keeping all boxes in a place
        private ObservableCollection<Box> _allOfMyBoxes;

        //For keeping which box is selected 
        private Box _selectedBox;

        //For receiving selected category name from singleton class
        private String _selectedCategoryName;
        
        //I will add my items to collection 
        private ObservableCollection<Item> _myItemCollection;

        //I will add my box information to collection 
        private  static ObservableCollection<BoxInformation> _myBoxInformation;

        //For go to another page
        private readonly FrameNavigate _frameNavigate;
        
        //I am using this for get the category name from main page
        private readonly SingletonCategory _singletonCategory;

        private readonly SingletonBox _singletonBox;

        //I am using this collection for items in certain category
        private ObservableCollection<Item> _itemsForMyBox;

        //For putting items in a box
        private Item _myItem;

        public RelayCommand GoToMainPageCommand { get; set; }


        //Definitions for finding items in ItemsForMyBox
        Random rnd = new Random();
        int _position = 0;
        
        //Prop. definitions starts here

        public ObservableCollection<Item> MyItemCollection
        {
            get => _myItemCollection;
            set => _myItemCollection = value;
        }

        public string SelectedCategoryName
        {
        get { return _selectedCategoryName; }

        set
        {
            _selectedCategoryName = value;
            OnPropertyChanged(nameof(SelectedCategoryName));
        }
        }

        public static ObservableCollection<BoxInformation> MyBoxInformation
        {
            get => _myBoxInformation;
            set => _myBoxInformation = value;
        }

        public ObservableCollection<Item> ItemsForMyBox
        {
            get => _itemsForMyBox;
            set => _itemsForMyBox = value;
        }

        public Box AffordableBox
        {
            get => _affordableBox;
            set => _affordableBox = value;
        }

        public Box AverageBox
        {
            get => _averageBox;
            set => _averageBox = value;
        }

        public Box ExpensiveBox
        {
            get => _expensiveBox;
            set => _expensiveBox = value;
        }

        public Item MyItem
        {
            get => _myItem; set => _myItem = value;
        }
        
        public ObservableCollection<Box> AllOfMyBoxes
        {
            get => _allOfMyBoxes;
            set => _allOfMyBoxes = value;
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
                GoToNextPage();
            } 
        }
        
        //Prop. definitions ends here 

        //Ctor

        public BoxVm()
        {
            //Initialize

            AffordableBox = new Box();
            
            AverageBox = new Box();

            ExpensiveBox = new Box();

            AllOfMyBoxes = new ObservableCollection<Box>();
            
            MyItem = new Item();
            
            MyItemCollection = new ObservableCollection<Item>();

            ItemsForMyBox = new ObservableCollection<Item>();

            _singletonBox = SingletonBox.GetInstance();
            
            _frameNavigate = new FrameNavigate();

            SelectedBox = new Box();
            
            _serialize = new Serialization();

            _singletonCategory = SingletonCategory.GetInstance();
            
            SelectedCategoryName = _singletonCategory.GetCategoryName();

            GoToMainPageCommand = new RelayCommand(GoToMainPage);

            //Calling my methods

            //ReadMyItems();
            
            SetInformationToBoxes();

            //ChooseItemsForCategory();
            
            //CreateBoxes();
        }

        //Getting items from Json file  

        //public async void ReadMyItems()
        //{
        //    try
        //    {
        //        MyItemCollection = await _serialize.LoadFromJson();
        //    }
        //    catch (Exception e)
        //    {
        //       MyItemCollection.Clear();
        //    }
        //}

        //Putting items depends on chosen category in a new list

        //public void ChooseItemsForCategory()
        //{
        //    foreach (var item in MyItemCollection)
        //    {
        //        if (SelectedCategoryName == item.Category)
        //            ItemsForMyBox.Add(item);
        //    }
        //}

        //Assigning information to boxes 

        public void SetInformationToBoxes()
        {
            BoxInformation affordableMakeupInformation = new BoxInformation("Make up", "Galaxia",
                "Have you ever wished to find the best makeup and beauty related products in one place and help others at the same time? Now you have the chance! With our box you receive ten beauty items from the newest and most popular brands around the globe. The items are surprise and you do not know what are you going to receive except three full-size products and more goodies!",
                "20",true,false,false);
            BoxInformation averageMakeupInformation = new BoxInformation("Make up", "Electra",
                "Our second beauty box is even better. With every box you receive more full-size products and luxury items. Out boxes will make you fall in love with them and experience pleasure in unpacking them and finding out what is inside every wrapped product. Our customer instantly becomes obsessed and impressed with them!",
                "50",false,true,false);
            BoxInformation expensiveMakeupInformation = new BoxInformation("Make up", "Cyclone",
                "That is our most expensive and luxurious box. You receive not only 5 full-size products but every product gives you a code for 10% off of every product you purchase you made on some of the brands website. Out “Cyclone” box will be the best choice for you if you want to try the most high-quality beauty products in the world.",
                "75", false,false,true);
            BoxInformation affordableHealthInformation = new BoxInformation("Health", "Delicious",
                "It is that special time of the month, so why not receive out “Delicious” box and spoil your body with different soaps, scrubs and skin care. You will have everything you need to get through the week in the form of pads and tampons. Of course, with that box we are taking care not only for your comfort during the week but also for keeping your skin healthy and hydrated. No need to worry, we have taken care of everything.",
                "20", true, false, false);
            BoxInformation averageHealthInformation = new BoxInformation("Health", "Eclipse",
                "If you want to feel even better and secured during the week, our “Eclipse” box is the right one for you. There would find snacks, comforting and inspiring messages to make you smile and make you feel special during those times. You could order our box every month and even share it with your friends!.",
                "50", false, true, false);
            BoxInformation expensiveHealthInformation = new BoxInformation("Health", "Nature friendly",
                "Do not let rainforest suffer and receive our “Nature friendly” box. You will get a reusable, eco-friendly, toxic-free DivaCup. You will receive full-size hair, skincare and body products. We have thought about every small detail during that period of time. With the help of our customers we did a survey of what they would like to receive in our future boxes. It is our newest box with a lot of surprises and new products to try out.",
                "75", false, false, true);
            BoxInformation affordableEraInformation = new BoxInformation("Era", "Storybrooke",
                "Do you want to go back in time and experience how the people lived ages ago? Now you have the chance to go back in time with out “Storybrooke” box. We have different item which were popular back in the 90’s. If you are a 90’s kid you will definitely enjoy this box!",
                "20", true, false, false);
            BoxInformation averageEraInformation = new BoxInformation("Era", "Millennial",
                "Our “Millennial” box will give you incredible and unforgettable experience. In the box you will find products regarding the most famous events in history. They will give you the chance to go back in time and show you which were the trends back then.",
                "50", false, true, false);
            BoxInformation expensiveEraInformation = new BoxInformation("Era", "Blockbuster",
                "Our most expensive box will provide exclusive and one of a kind items. Most of them are being the most popular and wanted products in history. They go from emblematic events to rare personal belongings of popular stars. If you are a collector then that’s the box for you!",
                "75", false, false, true);
            BoxInformation affordableGeekInformation = new BoxInformation("Geek", "Boredom",
                "Have you ever had this situation when you do not have enough money for gamin accessories or you find it hard to find everything you need in one place? Well, if yes, this is the box for you. With our first box you could win from a skin in League of legends to a chance to win a free X-box.",
                "20", true, false, false);
            BoxInformation averageGeekInformation = new BoxInformation("Geek", "Fangirl",
                "Our second box – “Fangirl” is more related to our gamer girls. As a company we want everyone to be able to find something for themselves, so with this box you could find everything you want. You might be surprised (as we hope so) what is hiding inside of this precious and girly box.",
                "50", false, true, false);
            BoxInformation expensiveGeekInformation = new BoxInformation("Geek", "Box of dreamers",
                "If you cannot find yourself in one of our previous boxes, for sure you will be captivated by this one. In this box you will receive one brand new gaming console and free tickets for you and your friends for upcoming gaming convections. You think that’s all? You’re wrong. That is just the beginning, our box is full of surprises.",
                "75", false, false, true);

            if (SelectedCategoryName == "Make up")
            {
                AffordableBox.MyInformationAboutBoxes = affordableMakeupInformation;
                AverageBox.MyInformationAboutBoxes = averageMakeupInformation;
                ExpensiveBox.MyInformationAboutBoxes = expensiveMakeupInformation;
                
            }
            else if (SelectedCategoryName == "Health")
            {
                AffordableBox.MyInformationAboutBoxes = affordableHealthInformation;
                AverageBox.MyInformationAboutBoxes = averageHealthInformation;
                ExpensiveBox.MyInformationAboutBoxes = expensiveHealthInformation;
                
            }
            else if (SelectedCategoryName == "Geek")
            {
                AffordableBox.MyInformationAboutBoxes = affordableGeekInformation;
                AverageBox.MyInformationAboutBoxes = averageGeekInformation;
                ExpensiveBox.MyInformationAboutBoxes = expensiveGeekInformation;
            }
            else if (SelectedCategoryName == "Era")
            {
                AffordableBox.MyInformationAboutBoxes = affordableEraInformation;
                AverageBox.MyInformationAboutBoxes = averageEraInformation;
                ExpensiveBox.MyInformationAboutBoxes = expensiveEraInformation;
            }

            AllOfMyBoxes.Add(AffordableBox);
            AllOfMyBoxes.Add(AverageBox);
            AllOfMyBoxes.Add(ExpensiveBox);
        }
        
        //Creating boxes
        
        //public void CreateBoxes()
        //{
        //    List<Item> affordableItems = new List<Item>();
        //    List<Item> averageItems = new List<Item>();
        //    List<Item> expensiveItems = new List<Item>();

        //    AllOfMyBoxes.Add(AffordableBox);
        //    AllOfMyBoxes.Add(AverageBox);
        //    AllOfMyBoxes.Add(ExpensiveBox);

        //    foreach (var item in ItemsForMyBox)
        //    {
        //        if (item.Affordable == true)
        //        {
        //            affordableItems.Add(item);
        //        }
        //        else if (item.Average == true)
        //        {
        //            averageItems.Add(item);
        //        }
        //        else if (item.Expensive == true)
        //        {
        //            expensiveItems.Add(item);
        //        }
        //    }
        //    foreach (var box in AllOfMyBoxes)
        //    {
        //        if (box == AffordableBox)
        //        {
        //            RandomSelection(AffordableBox, affordableItems);
        //        }
        //        else if (box == AverageBox)
        //        {
        //            RandomSelection(AverageBox, averageItems);
        //        }
        //        else if (box == ExpensiveBox)
        //        {
        //            RandomSelection(ExpensiveBox, expensiveItems);
        //        }
        //    }
        //}
        
        ////Random selection for creating box
                                                                                               
        //public void RandomSelection(Box box,List<Item> itemList)
        //{
        //    for (int i = 0; i < 10; i++)  //I want to put 10 items in a box 
        //    {
        //        try
        //        {
        //            _position = rnd.Next(0, itemList.Count);
        //            MyItem = itemList.ElementAt(_position);
        //            box.ItemsInMyBox.Add(MyItem);
        //            itemList.Remove(MyItem);
        //        }
        //        catch (Exception e)
        //        {
                   
        //        }
               
        //    }
        //}
        
        //Adding Add to card text for each box
        
        public void GoToNextPage()
        {
            _singletonBox.SetBox(SelectedBox);
            Type type = typeof(BoxDetails);
            _frameNavigate.ActivateFrameNavigation(type);
  
        }

        public void GoToMainPage()
        {
            
            Type type = typeof(MainPage);
            _frameNavigate.ActivateFrameNavigation(type);

        }
    }

    }

    

