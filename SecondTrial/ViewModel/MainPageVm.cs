using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondTrial.Model;
using SecondTrial.View;

namespace SecondTrial.ViewModel
{
    class MainPageVm : NotifyChangedPropertyClass
    {
        
        private ObservableCollection<Category> _categories;

        private readonly SingletonCategory _userSingleton;
        private readonly FrameNavigate _frameNavigate;
        
       public RelayCommand GoToLoginCommand { get; set; }

        public Category _selectedCategory;

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => _categories = value;
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                GoToBoxPage();
            }
        }

        public MainPageVm()
        {
            _userSingleton = SingletonCategory.GetInstance();

            _frameNavigate = new FrameNavigate();

            Categories = new ObservableCollection<Category>()
            {
                new Category("Make up"),
                new Category("Health"),
                new Category("Era"),
                new Category("Geek")

            };

            SelectedCategory = new Category();

            GoToLoginCommand = new RelayCommand(GoToLogin);


        }

        public void GoToBoxPage()
        {
            _userSingleton.SetCategory(SelectedCategory);
            Type type = typeof(ShowBoxes);
            _frameNavigate.ActivateFrameNavigation(type);
        }

        public void GoToLogin()
        {
            Type type = typeof(Login);
            _frameNavigate.ActivateFrameNavigation(type);
        }
    }
}
