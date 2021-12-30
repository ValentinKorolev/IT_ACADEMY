using Newtonsoft.Json;
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

        
        
        
        public PageOrder(string? name = null, string? email = null, string? phoneNumber = null, string? adressDelivery = null)
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
                bool _isCorrectOrder = IsCorrectOrder();
                SendOrder(_isCorrectOrder);
            }
            else if (options[selectedIndex].Contains("Go back"))
            {
                PageCart pageCart = new PageCart();
                _ = pageCart.Run();
            }
        }

        private bool IsCorrectOrder()
        {
            if (_name is not null && _email is not null && _phoneNumber is not null && _adressDelivery is not null) 
                return true;
            else 
                return false;
        }

        private void SendOrder(bool isCorrectOrder)
        {
            if(isCorrectOrder == true)
            {
                Order order = new();

                order.NameClient = _name;
                order.EmailClient = _email;
                order.PhoneNumberClient = _phoneNumber;
                order.AdressDeliveryClient = _adressDelivery;

                order.Cheque = Cart.ShowTheContents() + Cart.ReturnOrderAmount();

                Action addOrder = () => AddOrderDb(order); 

                addOrder +=() => AddOrderJson(order);

                addOrder();

                Clear();
                WriteLine("Your order has been accepted for processing");
                Thread.Sleep(4000);

                Cart.cartList.Clear();

                PageMainMenu pageMainMenu = new PageMainMenu();
                _ = pageMainMenu.Run();
            }
            else
            {
                Clear();
                WriteLine("All fields are required! The order is not made.");
                Thread.Sleep(5000);
                PageOrderRun();
            }
        }

        private void AddOrderJson(Order order)
        {
            ListOrders model = new ListOrders();

            if (File.Exists(Observer.FileNameOrders))
            {
                var fileName = File.ReadAllText(Observer.FileNameOrders);
                var ordersJson = JsonConvert.DeserializeObject<ListOrders>(fileName);

                model.Orders = ordersJson.Orders; 

                model.Orders.Add(order);

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameOrders, _jsonObject);
            }
            else
            {
                model.Orders.Add(order);

                string _jsonObject = JsonConvert.SerializeObject(model);

                File.AppendAllText(Observer.FileNameOrders, _jsonObject);
            }
        }

        private void AddOrderDb(Order order)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Order.Add(order);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger<PageOrder>.Error("Read InnerException", ex.InnerException);
                WriteLine("Error, please look logs!");
                Thread.Sleep(10000);
            }            
        }

        private void GetName()
        {
            _name = null;

            Clear();

            Write("Enter your name: ");

            _name = ReadLine();

            if (string.IsNullOrEmpty(_name)||string.IsNullOrWhiteSpace(_name))
            {
                Clear();
                WriteLine("This field is mandatory!!!");

                _name = null;

                Thread.Sleep(4000);

                PageOrderRun();
            }

            PageOrderRun();
        }

        private void GetEmail()
        {
            _email = null;

            Clear();

            Write("Enter your email: ");

            _email = ReadLine();

            if (IsValidEmail(_email) == false)
            {
                Clear();
                WriteLine("Email entered incorrectly!!!");

                _email = null;

                Thread.Sleep(4000);

                PageOrderRun();
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
            _phoneNumber = null;

            Clear();

            Write("Enter your phone number: ");

            _phoneNumber = ReadLine();

            if(IsValidPhoneNumber(_phoneNumber) == false)
            {
                Clear();

                WriteLine("Phone number entered incorrectly!!!");

                _phoneNumber = null;

                Thread.Sleep(4000);

                PageOrderRun();
            }

            PageOrderRun();
        }

        private bool IsValidPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$").Success;
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

            if(IsAdress(_city,_street,_houseNumber) == false)
            {
                Clear();

                WriteLine("Adress entered incorrectly!!!");

                Thread.Sleep(4000);

                PageOrderRun();
            }
            else
            {
                _adressDelivery = $"City: {_city}, House number: {_houseNumber}, Street: {_street}, Apartment: {_apartment}, Comments: {_comment}";

                PageOrderRun();
            }            
        }

        private bool IsAdress(string city, string street, string houseNumber)
        {
            if ((string.IsNullOrEmpty(city) || string.IsNullOrWhiteSpace(city)) || (string.IsNullOrEmpty(street) || string.IsNullOrWhiteSpace(street)) || (string.IsNullOrEmpty(houseNumber) || string.IsNullOrWhiteSpace(houseNumber)))
            {
                return false;
            }
            else
                return true;
        }

        private void PageOrderRun()
        {
                PageOrder pageOrder = new(_name, _email, _phoneNumber, _adressDelivery);
                _ = pageOrder.Run();           
        }
    }
}
