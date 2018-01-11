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
using SecondTrial.Model;
using SecondTrial.View;
using SecondTrial.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SecondTrial
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
            

            this.myScrollViewer.AddHandler(UIElement.PointerPressedEvent,
                new PointerEventHandler(MyScrollViewer_PointerPressed),
                true /*handledEventsToo*/);
            this.myScrollViewer.AddHandler(UIElement.PointerReleasedEvent,
                new PointerEventHandler(myScrollViewer_PointerReleased),
                true /*handledEventsToo*/);
            this.myScrollViewer.AddHandler(UIElement.PointerCanceledEvent,
                new PointerEventHandler(myScrollViewer_PointerCanceled),
                true /*handledEventsToo*/);
        }

        private void MyScrollViewer_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Pen)
            {
                (myScrollViewer.Content as UIElement).ManipulationMode &= ~ManipulationModes.System;
            }
        }

        private void myScrollViewer_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Pen)
            {
                (myScrollViewer.Content as UIElement).ManipulationMode |= ManipulationModes.System;
            }
        }

        private void myScrollViewer_PointerCanceled(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Pen)
            {
                (myScrollViewer.Content as UIElement).ManipulationMode |= ManipulationModes.System;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CategoryPage));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutUs));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContactUs));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CategoryPage));
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CategoryPage));
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Brands)); 
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutUs));
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContactUs));
        }
    }
    }

