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
        private string _nameClient;
        private string _emailClient;
        private string _phoneNumberClient;

        public PageOrder(string name = null, string email = null, string phoneNumber= null)
        {
            _nameClient = name;
            _emailClient = email;
            _phoneNumberClient = phoneNumber;

            _bannerPage = "Fill in the information about yourself: ";

            _options = new string[] {$"Name: {_nameClient}",
                                     $"Email: {_emailClient}",
                                     $"Phone number: {_phoneNumberClient}",
                                     $"Adress delevery: "};
        }

        //public PageOrder(string name, string email, string phoneNumber)
        //{

        //}

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            if (options[selectedIndex].Contains("Name"))
            {
                GetNameClient();
            }
            else if (options[selectedIndex].Contains("Email"))
            {
                GetEmailClient();
            }
            else if (options[selectedIndex].Contains("Phone"))
            {
                GetPhoneNumberClient();
            }
            else if (options[selectedIndex].Contains("Adress"))
            {
                GetAdressClient();
            }
        }

        private void GetNameClient()
        {

            Clear();
            Write("Enter your name: ");

            _nameClient = ReadLine();

            while (string.IsNullOrEmpty(_nameClient)||string.IsNullOrWhiteSpace(_nameClient))
            {
                Clear();
                WriteLine("This field is mandatory. Please, enter name: ");
                _nameClient = ReadLine();
            }

            PageRun();
        }

        private void GetEmailClient()
        {
            Clear();

            Write("Enter your email: ");

            _emailClient = ReadLine();

            while (IsValidEmail(_emailClient) == false)
            {
                WriteLine("Incorrect enter the email!!!Please, enter email again: ");
                _emailClient = ReadLine();
            }

            PageRun();
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

        private void GetPhoneNumberClient()
        {
            Clear();
            Write("Enter your phone number: ");
            _phoneNumberClient = ReadLine();

            while(IsPhoneNumber(_phoneNumberClient) == false)
            {
                WriteLine("Incorrect enter the phone number!!!Please, enter phone number again: ");
                _phoneNumberClient = ReadLine();
            }

            PageRun();
        }

        private bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$").Success;
        }

        private void GetAdressClient()
        {
            Clear();
            Write("Enter the delivery address: ");
            _ = ReadLine();
        }

        private void PageRun()
        {
            if (string.IsNullOrEmpty(_nameClient) && string.IsNullOrEmpty(_emailClient) && string.IsNullOrEmpty(_phoneNumberClient))
            {
                PageOrder pageOrder = new();
                _ = pageOrder.Run();
            }
            else if (_nameClient != null && string.IsNullOrEmpty(_emailClient) && string.IsNullOrEmpty(_phoneNumberClient))
            {
                PageOrder pageOrder = new(name: _nameClient);
                _ = pageOrder.Run();
            }
            else if (_nameClient != null && _emailClient != null && string.IsNullOrEmpty(_phoneNumberClient))
            {
                PageOrder pageOrder = new(name: _nameClient, email: _emailClient);
                _ = pageOrder.Run();
            }
            else if (_nameClient != null && _emailClient != null && _phoneNumberClient != null)
            {
                PageOrder pageOrder = new(name: _nameClient, email: _emailClient, phoneNumber: _phoneNumberClient);
                _ = pageOrder.Run();
            }
        }
    }
}
