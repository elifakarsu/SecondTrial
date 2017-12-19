using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTrial.Model
{
    public class PaymentDetails
    {
        public enum CardType
        {
            DinersClubCarteBlanche = 0,
            AmericanExpress = 1,
            DinersClubInternational = 2,
            DinersClubUSACanada = 3,
            InstaPayment = 4,
            Discover = 5,
            Maestro = 6,
            MasterCard = 7,
            Visa = 8,
            VisaElectron = 9
        }

        public PaymentDetails(string cardNumber, string cardName, string securityNumber, string expiryMonth, string expiryYear)
        {
            CardNumber = cardNumber;
            CardName = cardName;
            SecurityNumber = securityNumber;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            _singletonOrder = SingletonOrder.GetInstance();
        }

        public PaymentDetails()
        {
            
           
        }

        private readonly SingletonOrder _singletonOrder;

        private PaymentDetails _paymentDetails; 

        public string CardNumber { get; set; }

        public string CardName { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }

        public string SecurityNumber { get; set; }
        

        public string PaymentDetailCard()
        {

            if (CardNumber.StartsWith("34") || CardNumber.StartsWith("37"))
            {
                return "American Express";
            }
            else if (CardNumber.StartsWith("36"))
            {
                return "Diners Club - International";
            }
            else if (CardNumber.StartsWith("6011") || CardNumber.StartsWith("644") || CardNumber.StartsWith("645") || CardNumber.StartsWith("646") || CardNumber.StartsWith("647") || CardNumber.StartsWith("648") || CardNumber.StartsWith("649") || CardNumber.StartsWith("65") || CardNumber.StartsWith("622126") || CardNumber.StartsWith("622127") || CardNumber.StartsWith("622128") || CardNumber.StartsWith("622129") || CardNumber.StartsWith("62213") || CardNumber.StartsWith("62214") || CardNumber.StartsWith("62215") || CardNumber.StartsWith("62216") || CardNumber.StartsWith("62217") || CardNumber.StartsWith("62218") || CardNumber.StartsWith("62219") || CardNumber.StartsWith("6222") || CardNumber.StartsWith("6223") || CardNumber.StartsWith("6224") || CardNumber.StartsWith("6225") || CardNumber.StartsWith("6226") || CardNumber.StartsWith("6227") || CardNumber.StartsWith("6228") || CardNumber.StartsWith("62290") || CardNumber.StartsWith("62291") || CardNumber.StartsWith("622920") || CardNumber.StartsWith("622921") || CardNumber.StartsWith("622922") || CardNumber.StartsWith("622923") || CardNumber.StartsWith("622924") || CardNumber.StartsWith("622925"))
            {
                return "Discover";
            }
            else if (CardNumber.StartsWith("637") || CardNumber.StartsWith("638") || CardNumber.StartsWith("639"))
            {
                return "InstaPayment";
            }
            else if (CardNumber.StartsWith("5018") || CardNumber.StartsWith("5020") || CardNumber.StartsWith("5038") || CardNumber.StartsWith("5893") || CardNumber.StartsWith("6304") || CardNumber.StartsWith("6759") || CardNumber.StartsWith("6761") || CardNumber.StartsWith("6762") || CardNumber.StartsWith("6763"))
            {
                return "Maestro";
            }
            else if (CardNumber.StartsWith("51") || CardNumber.StartsWith("52") || CardNumber.StartsWith("53") || CardNumber.StartsWith("54") || CardNumber.StartsWith("55") || CardNumber.StartsWith("2221") || CardNumber.StartsWith("23") || CardNumber.StartsWith("24") || CardNumber.StartsWith("25") || CardNumber.StartsWith("26") || CardNumber.StartsWith("27") || CardNumber.StartsWith("2221") || CardNumber.StartsWith("2222") || CardNumber.StartsWith("2223") || CardNumber.StartsWith("2224") || CardNumber.StartsWith("2225") || CardNumber.StartsWith("2226") || CardNumber.StartsWith("2227") || CardNumber.StartsWith("2228") || CardNumber.StartsWith("2229") || CardNumber.StartsWith("223") || CardNumber.StartsWith("224") || CardNumber.StartsWith("225") || CardNumber.StartsWith("226") || CardNumber.StartsWith("227") || CardNumber.StartsWith("228") || CardNumber.StartsWith("229") || CardNumber.StartsWith("23") || CardNumber.StartsWith("24") || CardNumber.StartsWith("25") || CardNumber.StartsWith("26") || CardNumber.StartsWith("270") || CardNumber.StartsWith("271") || CardNumber.StartsWith("2720"))
            {
                return "MasterCard";
            }
            else if (CardNumber.StartsWith("4") && !CardNumber.StartsWith("4026") && CardNumber != "417500" && CardNumber != "4508" && CardNumber != "4844" && CardNumber != "4913" && CardNumber != "4917")
            {
                return "Visa";
            }
            else if (CardNumber.StartsWith("4026") || CardNumber.StartsWith("417500") || CardNumber.StartsWith("4508") || CardNumber.StartsWith("4844") || CardNumber.StartsWith("4913") || CardNumber.StartsWith("4917"))
            {
                return "Visa Electron";
            }
            else if (CardNumber.StartsWith("3528") || CardNumber.StartsWith("3529") || CardNumber.StartsWith("353") || CardNumber.StartsWith("354") || CardNumber.StartsWith("355") || CardNumber.StartsWith("356") || CardNumber.StartsWith("357") || CardNumber.StartsWith("358"))
            {
                return "JCB";
            }
            return "Sorry card is invalid";

        }

        public string validatecardandsecruitynumber()
        {
            if ((SecurityNumber.Length == 3 || SecurityNumber.Length == 4) && (CardNumber.Length >= 13 && CardNumber.Length <= 19))
            {
                return "secruity ok";
            }
            return "secruity wrong";
        }


        public string validateexpirydate()
        {
            int currentmonth = DateTime.Now.Month;
            int currentyear = DateTime.Now.Year;
            int inputMonth = Int16.Parse(ExpiryMonth);
            int inputYear = Int16.Parse(ExpiryYear);
            if ((inputMonth <= currentmonth) && (inputYear >= currentyear))
            {
                return "date is ok";
            }
            return "date is wrong";
        }
        public string CheckCardForDisplayImage(string input)
        {
            string imgPath = "";
            string imgPathFull;
            if (input.StartsWith("34") || input.StartsWith("37"))
            {
                imgPath = "American Express";
            }
            else if (input.StartsWith("36"))
            {
                imgPath = "Diners Club - International";
            }
            else if (input.StartsWith("6011") || input.StartsWith("644") || input.StartsWith("645") || input.StartsWith("646") || input.StartsWith("647") || input.StartsWith("648") || input.StartsWith("649") || input.StartsWith("65") || input.StartsWith("622126") || input.StartsWith("622127") || input.StartsWith("622128") || input.StartsWith("622129") || input.StartsWith("62213") || input.StartsWith("62214") || input.StartsWith("62215") || input.StartsWith("62216") || input.StartsWith("62217") || input.StartsWith("62218") || input.StartsWith("62219") || input.StartsWith("6222") || input.StartsWith("6223") || input.StartsWith("6224") || input.StartsWith("6225") || input.StartsWith("6226") || input.StartsWith("6227") || input.StartsWith("6228") || input.StartsWith("62290") || input.StartsWith("62291") || input.StartsWith("622920") || input.StartsWith("622921") || input.StartsWith("622922") || input.StartsWith("622923") || input.StartsWith("622924") || input.StartsWith("622925"))
            {
                imgPath = "Discover";
            }
            else if (input.StartsWith("637") || input.StartsWith("638") || input.StartsWith("639"))
            {
                imgPath = "InstaPayment";
            }
            else if (input.StartsWith("5018") || input.StartsWith("5020") || input.StartsWith("5038") || input.StartsWith("5893") || input.StartsWith("6304") || input.StartsWith("6759") || input.StartsWith("6761") || input.StartsWith("6762") || input.StartsWith("6763"))
            {
                imgPath = "Maestro";

            }
            else if (input.StartsWith("51") || input.StartsWith("52") || input.StartsWith("53") || input.StartsWith("54") || input.StartsWith("55") || input.StartsWith("2221") || input.StartsWith("23") || input.StartsWith("24") || input.StartsWith("25") || input.StartsWith("26") || input.StartsWith("270") || input.StartsWith("271") || input.StartsWith("2720"))
            {
                imgPath = "MasterCard";
            }
            else if (input.StartsWith("4") && !input.StartsWith("4026") && input != "417500" && input != "4508" && input != "4844" && input != "4913" && input != "4917")
            {
                imgPath = "Visa";
            }
            else if (input.StartsWith("4026") || input.StartsWith("417500") || input.StartsWith("4508") || input.StartsWith("4844") || input.StartsWith("4913") || input.StartsWith("4917"))
            {
                imgPath = "Visa Electron";
            }
            else if (input.StartsWith("3528") || input.StartsWith("3529") || input.StartsWith("353") || input.StartsWith("354") || input.StartsWith("355") || input.StartsWith("356") || input.StartsWith("357") || input.StartsWith("358"))
            {
                imgPath = "JCB";
            }
            imgPathFull = "../Assets/" + imgPath + ".jpg";
            return imgPathFull;
        }
    }
}
