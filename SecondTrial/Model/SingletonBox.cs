using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTrial.Model
{
    class SingletonBox
    {
        public static Box _box;

        private static SingletonBox Instance { get; set; }

        private SingletonBox()
        {
            _box = new Box();
        }

        public static SingletonBox GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SingletonBox();
            }
            return Instance;
        }

        public void SetBox(Box box)
        {
            _box = box;
        }

        public String GetName()
        {
            return _box.MyInformationAboutBoxes.Name;
        }

        public String GetDescription()
        {
            return _box.MyInformationAboutBoxes.Description;
        }
        public String GetPrice()
        {
            return _box.MyInformationAboutBoxes.Price;
        }
        public Box GetBox()
        {
            return _box;
        }

    }
}
