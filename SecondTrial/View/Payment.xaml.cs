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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using SecondTrial.Model;
using SecondTrial.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SecondTrial.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Payment : Page
    {
        private PaymentDetails _pd;
        private PaymentVm _pageVm;
        public Payment()
        {
            this.InitializeComponent();
            _pageVm = new PaymentVm();
            DataContext = _pageVm;
            _pd = new PaymentDetails();
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // check the credit card 

            if (!CheckNullInputs())
            {
                TxtVisibleData.Text = "You can't leave spaces blank";
            }
            else
            {
                string month = TxtMonth.Text;
                string year = TxtYear.Text;

                PaymentDetails detail = new PaymentDetails(TxtCardNo.Text, TxtCardName.Text, Txtcvv.Text, month, year);
                string msg = detail.PaymentDetailCard();
                string msg1 = detail.validateexpirydate();
                string msg2 = detail.validatecardandsecruitynumber();

                // if you meet the required level of condition then you are able to redirect the page to another page 

                if ((msg == "JCB" || msg == "MasterCard" || msg == "Diners Club -Carte Blanche" || msg == "American Express" || msg == "Diners Club - International" || msg == "Diners Club - USA & Canada" || msg == "Discover" || msg == "InstaPayment" || msg == "Maestro" || msg == "Visa" || msg == "Visa Electron" || msg == "Payment Accepted") && msg2 == "secruity ok" && msg1 == "date is ok")
                {
                    Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    TxtVisibleData.Text = msg;
                }
            }

        }

        private void TxtCardNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            string input = null;
            if (txt != null)
            {
                input = (string)txt.Text;
                if (input != "")
                {
                    string imgpath = _pd.CheckCardForDisplayImage(input);
                    Image.Source = new BitmapImage(new Uri(this.BaseUri, imgpath));
                }


            }

            // ../Assets/Visa.jpg
            // Get the TextBox inputted text and display it on TextBlock
            //TextBlock1.Text = "You inputted:\n" + textBox.Text;

        }

        public bool CheckNullInputs()
        {
            string Name = TxtCardName.Text;
            string cardNumber = TxtCardNo.Text;
            string month = TxtMonth.Text;
            string year = TxtYear.Text;
            string cvv = Txtcvv.Text;

            if (Name != "" && cardNumber != "" && month != "" && year != "" && cvv != "")
            {
                return true;
            }
            return false;

        }
    }
}
