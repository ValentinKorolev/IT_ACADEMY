using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal sealed class PageOrder : PageFather
    {
        private string _name;
        private string _email;
        private string _phoneNumber;
        private string _adressDelivery;

        public PageOrder(string name = null, string email = null, string phoneNumber= null, string adressDelivery = null)
        {
            _name = name;
            _email = email;
            _phoneNumber = phoneNumber;
            _adressDelivery = adressDelivery;

            _bannerPage = Cart.ShowTheContents() + Cart.ReturnOrderAmount() + "\n\nFill in the information about yourself: ";

            _options = new string[] {$"Name: {_name}",
                                     $"Email: {_email}",
                                     $"Phone number: {_phoneNumber}",
                                     $"Adress delivery: {_adressDelivery}",
                                     $"\nTo order",
                                     $"Go back"};
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            if (options[selectedIndex].Contains("Name"))
            {
                GetName();
            }
            else if (options[selectedIndex].Contains("Email"))
            {
                GetEmail();
            }
            else if (options[selectedIndex].Contains("Phone"))
            {
                GetPhoneNumber();
            }
            else if (options[selectedIndex].Contains("Adress"))
            {
                GetAdressDelivery();
            }
            else if (options[selectedIndex].Contains("To order"))
            {

            }
            else if (options[selectedIndex].Contains("Go back"))
            {
                PageCart pageCart = new PageCart();
                _ = pageCart.Run();
            }
        }

        private void GetName()
        {

            Clear();
            Write("Enter your name: ");

            _name = ReadLine();

            while (string.IsNullOrEmpty(_name)||string.IsNullOrWhiteSpace(_name))
            {
                Clear();
                WriteLine("This field is mandatory. Please, enter name: ");
                _name = ReadLine();
            }

            PageOrderRun();
        }

        private void GetEmail()
        {
            Clear();

            Write("Enter your email: ");

            _email = ReadLine();

            while (IsValidEmail(_email) == false)
            {
                WriteLine("Incorrect enter the email!!!Please, enter email again: ");
                _email = ReadLine();
            }

            PageOrderRun();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void GetPhoneNumber()
        {
            Clear();
            Write("Enter your phone number: ");
            _phoneNumber = ReadLine();

            while(IsValidPhoneNumber(_phoneNumber) == false)
            {
                WriteLine("Incorrect enter the phone number!!!Please, enter phone number again: ");
                _phoneNumber = ReadLine();
            }

            PageOrderRun();
        }

        private bool IsValidPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$").Success;
        }

        private void GetAdressDelivery()
        {
            string _city;
            string _street;
            string _houseNumber;
            string _apartment;

            Clear();
            WriteLine("Enter the delivery address");
            WriteLine();

            Write("City: ");
            _city = ReadLine();

            Write("Street: ");
            _street = ReadLine();

            Write("House number: ");
            _houseNumber = ReadLine();

            Write("Apartment (optional): ");
            _apartment = ReadLine();

            _adressDelivery = $"City: {_city}, House number: {_houseNumber}, Street: {_street}, Apartment: {_apartment}";

            PageOrderRun();
        }

        private void PageOrderRun()
        {
                PageOrder pageOrder = new(_name, _email, _phoneNumber, _adressDelivery);
                _ = pageOrder.Run();           
        }
    }
}
