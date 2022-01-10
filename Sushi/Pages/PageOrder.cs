using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal sealed class PageOrder : PageFather
    {
        Logger Logger = new Logger();

        SqlOrdersRepository sqlOrdersRepository;
        JsonOrderRepository jsonOrderRepository;
        

        private string _name;
        private string _email;
        private string _phoneNumber;
        private string _adressDelivery;
                        
        public PageOrder(string? name = null, string? email = null, string? phoneNumber = null, string? adressDelivery = null)
        {
            _name = name;
            _email = email;
            _phoneNumber = phoneNumber;
            _adressDelivery = adressDelivery;

            _bannerPage = Cart.ShowTheContents() + Cart.OrderAmount() + "\n\nFill in the information about yourself: ";

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
                PageOrderRun();
            }
            else if (options[selectedIndex].Contains("Email"))
            {
                GetEmail();
                PageOrderRun();
            }
            else if (options[selectedIndex].Contains("Phone"))
            {
                GetPhoneNumber();
                PageOrderRun();
            }
            else if (options[selectedIndex].Contains("Adress"))
            {
                GetAdressDelivery();
                PageOrderRun();
            }
            else if (options[selectedIndex].Contains("To order"))
            {
                Order order = new();
                order.NameClient = _name;
                order.EmailClient = _email;
                order.PhoneNumberClient = _phoneNumber;
                order.AdressDeliveryClient = _adressDelivery;
                order.Cheque = Cart.ShowTheContents() + Cart.OrderAmount();

                ValidateOrderAndToOrder(order);
            }
            else if (options[selectedIndex].Contains("Go back"))
            {
                PageCartRun();
            }
        }

        private void ToOrder(Order order)
        {

            sqlOrdersRepository = new SqlOrdersRepository();
            sqlOrdersRepository.Create(order);
            sqlOrdersRepository.Dispose();

            jsonOrderRepository = new JsonOrderRepository();
            jsonOrderRepository.Create(order);

            Logger.Debug($"The user {Observer.nameUser} ordered {order.ShowData()}");

            Clear();
            WriteLine("Your order has been accepted for processing");
            Thread.Sleep(4000);

            Cart.cartList.Clear();

            PageMainMenuRun();
        }

        private void GetName()
        {
            _name = null;

            Clear();
            Write("Enter your name: ");

            _name = ReadLine();
        }

        private void GetEmail()
        {
            _email = null;

            Clear();
            Write("Enter your email: ");

            _email = ReadLine();
        }

        private void GetPhoneNumber()
        {
            _phoneNumber = null;

            Clear();
            Write("Enter your phone number: ");

            _phoneNumber = ReadLine();
        }

        private void GetAdressDelivery()
        {
            _adressDelivery = null;

            string _city;
            string _street;
            string _houseNumber;
            string _apartment;
            string _comment;

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

            Write("Comments (optional): ");
            _comment = ReadLine();

            _adressDelivery = $"City: {_city}, House number: {_houseNumber}, Street: {_street}, Apartment: {_apartment}, Comments: {_comment}"; 
        }

        private void ValidateOrderAndToOrder(Order order)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(order);

            if (!Validator.TryValidateObject(order, context, result, true))
            {
                Clear();

                foreach(var error in result)
                {
                    
                    WriteLine($"{error} - incorrect input or not all fields are required! The order is not made.");
                    Thread.Sleep(4000);
                }
                PageOrderRun();
            }
            else
            {
                ToOrder(order);
            }
        }

        private void PageOrderRun()
        {
                PageOrder pageOrder = new(_name, _email, _phoneNumber, _adressDelivery);
                _ = pageOrder.Run();           
        }

        private void PageMainMenuRun()
        {
            PageMainMenu pageMainMenu = new PageMainMenu();
            _ = pageMainMenu.Run();
        }

        private void PageCartRun()
        {
            PageCart pageCart = new PageCart();
            _ = pageCart.Run();
        }
    }
}
