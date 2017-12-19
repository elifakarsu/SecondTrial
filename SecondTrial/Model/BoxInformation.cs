using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTrial.Model
{
    public class BoxInformation
    {
       
        //private string _image;
        private string _category;
        private string _name;
        private string _description;
        private string _price;
        private bool _affordableCheck;
        private bool _averageCheck;
        private bool _expensiveCheck;

        //public string Image
        //{
        //    get => _image;
        //    set => _image = value;
        //}

        public string Category
        {
            get => _category;
            set => _category = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public string Price
        {
            get => _price;
            set => _price = value;
        }

        public bool AffordableCheck
        {
            get => _affordableCheck; set => _affordableCheck = value;
        }

        public bool AverageCheck
        {
            get => _averageCheck;
            set => _averageCheck = value;
        }

        public bool ExpensiveCheck
        {
            get => _expensiveCheck;
            set => _expensiveCheck = value;
        }

        public BoxInformation(string category, string name, string description, string price,bool affordableCheck,bool averageCheck,bool expensiveCheck)
        {
            //_image = image;
            Name = name;
            Price = price;
            Category = category;
            Description = description;
            AffordableCheck  = affordableCheck;
            AverageCheck = averageCheck;
            ExpensiveCheck = expensiveCheck;
        }

        public BoxInformation()
        {



            
        }

        


    }
}
