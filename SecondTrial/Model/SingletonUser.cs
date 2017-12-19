using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTrial.Model
{
    class SingletonUser
    {
        public static Employee _Employee;

        private static SingletonUser Instance { get; set; }

        private SingletonUser()
        {
            _Employee  = new Employee();
        }

        public static SingletonUser GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SingletonUser();
            }
            return Instance;
        }

        public void SetEmployee(Employee employee)
        {
            _Employee = employee;
        }

        public Employee GetEmployee()
        {

            return _Employee;
        }


    }
}
