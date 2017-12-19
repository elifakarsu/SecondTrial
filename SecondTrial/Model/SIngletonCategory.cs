using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTrial.Model
{
    class SingletonCategory
    {
        public static Category _category;

        private static SingletonCategory Instance { get; set; }

        private SingletonCategory()
        {
            _category = new Category();
        }

        public static SingletonCategory GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SingletonCategory();
            }
            return Instance;
        }

        public void SetCategory(Category category)
        {
            _category = category;
        }

        public string GetCategoryName()
        {
            return _category.Name;
        }

    }
}
