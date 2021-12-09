using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    internal sealed class PageAdmin : PageFather
    {
        private const string _name = "Admin123";
        private const string _pass = "122345";      

        public PageAdmin(string name, string pass)
        {
            if (_name != name || _pass != pass)
            {
                Clear();
                Bot.SayHello();
            }
            else
            {
                _bannerPage = "Administotor Page";
                _options = new string[] {"0.View list Sushi" ,"1.Add sushi", "2.Upload Sushi",
                                         "3.Delete Sushi", "4.View orders" };
            }

        }

        protected override void TransferPage(ConsoleKey keyPressed, string[] options, int selectedIndex)
        {
            if (keyPressed == ConsoleKey.Enter && options[selectedIndex] == "1.Add sushi")
            {
                Clear();

                AddSushi();

                BackToPageAdmin();
            }
            else if (keyPressed == ConsoleKey.Enter && options[selectedIndex] == "2.Upload Sushi")
            {
                Clear();

            }
            else if (keyPressed == ConsoleKey.Enter && options[selectedIndex] == "3.Delete Sushi")
            {
                Clear();

            }
            else if (keyPressed == ConsoleKey.Enter && options[selectedIndex] == "4.View orders")
            {
                Clear();

            }
            else if (keyPressed == ConsoleKey.Enter && options[selectedIndex] == "0.View list Sushi")
            {
                Clear();

            }
        }

        private void AddSushi()
        {
            int _infoId;
            string _type;
            string _name;
            decimal _price;
            string _description;

            WriteLine("Enter Id Sushi: ");
            _infoId = Convert.ToInt32(ReadLine());

            WriteLine("Enter Type Sushi: ");
            _type = ReadLine();

            WriteLine("Enter Name Sushi: ");
            _name = ReadLine();

            WriteLine("Enter Price Sushi: ");
            _price = Convert.ToDecimal(ReadLine());

            WriteLine("Enter Descripion Sushi (200 symbol): ");
            _description = ReadLine();

            Sushi sushi = new Sushi(_infoId,_type, _name, _price, _description);

            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    db.Sushi.Add(sushi);
                    db.SaveChanges();
                    Logger<PageAdmin>.Debug($"Admin added sushi: {sushi}");
                }
                catch (Exception ex)
                {
                    Logger<PageAdmin>.Error("Read InnerException",ex.InnerException);
                    WriteLine("Error, please look logs!");
                    Thread.Sleep(10000);
                }                
            }
        }

        private void BackToPageAdmin()
        {
            PageAdmin pageAdmin = new(_name,_pass);
            _ = pageAdmin.Run();
        }
    }
}
