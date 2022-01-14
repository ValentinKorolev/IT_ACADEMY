using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SushiMarcet.Attributes
{
    public class OrderValidateAttribute : ValidationAttribute
    {
        Logger Logger = new Logger();
        
        public override bool IsValid(object? value)
        {
            if(value is not null)
            {
                Order order = (Order)value;


                if(IsName(order) == false || IsEmail(order) == false || IsPhoneNumber(order) == false || IsAdress(order) == false)
                {
                    Logger.Debug($"Failed validation ({order})");
                    return false;
                }
                else
                {
                    Logger.Debug($"Passed validation ({order})");
                    return true;
                }

            }
            return false;
        }

        private bool IsName(Order order)
        {
            if (string.IsNullOrEmpty(order.NameClient) == false)
            {
                return true;
            }
            else
                return false;
        }

        private bool IsEmail(Order order)
        {
            if (string.IsNullOrEmpty(order.EmailClient) == false)
            {
                try
                {
                    var email = new System.Net.Mail.MailAddress(order.EmailClient);

                    return email.Address == order.EmailClient;
                }
                catch
                {
                    return false;
                }                   
            }
            else
                return false;
        }

        private bool IsPhoneNumber(Order order)
        {
            if (string.IsNullOrEmpty(order.PhoneNumberClient) == false)
            {
                if (Regex.Match(order.PhoneNumberClient, @"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$").Success == true)
                {
                    return true;
                }   
                else
                    return false;
            }
            else 
                return false;
        }

        private bool IsAdress(Order order)
        {

            if (string.IsNullOrEmpty(order.AdressDeliveryClient) || string.IsNullOrWhiteSpace(order.AdressDeliveryClient))
            {
                return false;
            }
            else if (CheckCorrectAdress(order.AdressDeliveryClient) == false)
            {
                return false;
            }
            else 
                return true;
        }

        private bool CheckCorrectAdress(string adress)
        {
            //City: test, House number: test, Street: test, Apartment: test, Comments: test

            string str = adress;
            string city = adress.Substring(6,adress.IndexOf(',') - 6);
            str = adress.Substring(adress.IndexOf(", "));

            string houseNumber = str[16..str.IndexOf(", S")];
            str = str.Substring(str.IndexOf(", S"));

            string street = str.Substring(10, str.IndexOf(", A") - 10);

            if(string.IsNullOrEmpty(city) || string.IsNullOrEmpty(houseNumber)|| string.IsNullOrEmpty(street))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
