using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTrial.Model
{
    public class Employee
    {
        private string _username;
        private string _password;
        private string _name;
        private string _image;
        private string _status;

        public string Username
        {
            get => _username;
            set => _username = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Image
        {
            get => _image;
            set => _image = value;
        }

        public string Status
        {
            get => _status;
            set => _status = value;
        }

        public Employee()
        {

        }

        public Employee(string username, string password, string name, string image, string status)
        {
            _username = username;
            _password = password;
            _name = name;
            _image = image;
            _status = status;

        }
    }
}