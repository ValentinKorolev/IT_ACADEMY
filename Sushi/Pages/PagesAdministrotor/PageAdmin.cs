using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    internal sealed class PageAdmin : PageFather
    {
        private const string NameAdmin = "Admin123";
        private const string PassAdmin = "122345";

        private MyLogger<PageAdmin> logger = new();

        public PageAdmin(string name, string pass)
        {
            if (NameAdmin != name || PassAdmin != pass)
            {
                Clear();
                Bot.SayHello();
                Bot.AskNameUser();
                Bot.ShowMenu();
            }
            else
            {
                logger.Info("The administrator has logged into the program");

                _bannerPage = "Administrator Page";
                
                _options = new string[] {$"1.Orders",
                                         "2.Sushi",
                                         "3.Sauces and side dishes",
                                         "4.Drinks"
                                        };

            }
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            

            switch (options[selectedIndex])
            {
                case "1.Orders":
                    PageAdminOrders pageAdminOrders = new();
                    _ = pageAdminOrders.Run();
                    break;
                case "2.Sushi":
                    PageAdminSushi pageAdminSushi = new();
                    _ = pageAdminSushi.Run();
                    break;
                case "3.Sauces and side dishes":
                    PageAdminDishes pageAdminDishes = new();
                    _ = pageAdminDishes.Run();
                    break;
                case "4.Drinks":
                    PageAdminDrinks pageAdminDrinks = new();
                    _ = pageAdminDrinks.Run();
                    break;
            }
        }
    }
}
