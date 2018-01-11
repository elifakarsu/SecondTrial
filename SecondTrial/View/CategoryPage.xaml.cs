using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SecondTrial.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SecondTrial.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryPage : Page
    {
        private MainPageVm pageVm;
        public CategoryPage()
        {
            this.InitializeComponent();
            pageVm = new MainPageVm();
            DataContext = pageVm;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int change = 1;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);

            timer.Tick += (o, a) =>
            {
                int newIndex = categoriesFlipView.SelectedIndex + change;
                if (newIndex >= categoriesFlipView.Items.Count || newIndex < 0)
                {
                    change *= -1;
                }

                categoriesFlipView.SelectedIndex += change;
            };

            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
