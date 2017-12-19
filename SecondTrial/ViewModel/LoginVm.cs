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
    class LoginVm : NotifyChangedPropertyClass
    {
        private ObservableCollection<Employee> _employeeList;
        private Employee _user;
        private string _username;
        private string _password;
        private readonly SingletonSerialize _singletonSerialize;
        private readonly FrameNavigate _frameNavigate;
        private readonly SingletonUser _singletonUser;
        private readonly Serialization _serialize;

        public RelayCommand LoginCommand { get; set; }
        
        public ObservableCollection<Employee> EmployeeList
        {
            get => _employeeList;
            set => _employeeList = value;
        }

        public Employee User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        

        public LoginVm()
        {
            _singletonSerialize = SingletonSerialize.GetInstance();
            _singletonUser = SingletonUser.GetInstance();
            _frameNavigate = new FrameNavigate();
            EmployeeList = new ObservableCollection<Employee>();
            _serialize = new Serialization();
            User = new Employee();
            ReadAdminInfo();
            LoginCommand = new RelayCommand(Login);
           
        }

        public async void ReadAdminInfo()
        {
            EmployeeList = await _serialize.LoadAdminInfo();
        }

        public async void Login()
        {
            foreach (var employee in EmployeeList)
            {
                if (Username == employee.Username)
                {
                    if (Password == employee.Password)
                    {
                        User = employee;
                        _singletonUser.SetEmployee(User);
                        if (employee.Status == "Admin")
                        {

                            GoToAdminPage();
                        }
                        else
                        {
                            GoToEmployeePage();
                        }
                    }
                }
                
            }

            if (User.Password == null)
            {
                MessageDialog incorretLogin = new MessageDialog("Incorrect username/password");
                await incorretLogin.ShowAsync();
            }
        }
 



        public void GoToAdminPage()
        {
            Type type = typeof(AdminView);
            _frameNavigate.ActivateFrameNavigation(type);

        }

        public void GoToEmployeePage()
        {
            Type type = typeof(SeeOrders);
            _frameNavigate.ActivateFrameNavigation(type);

        }
    }
}
