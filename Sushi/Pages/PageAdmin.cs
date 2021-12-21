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
        private const string Name = "Admin123";
        private const string Pass = "122345";

        private int _infoId;
        private string _type;
        private string _nameSushi;
        private decimal _price;
        private string _description;

        public PageAdmin(string name, string pass)
        {
            if (Name != name || Pass != pass)
            {
                Clear();
                Bot.SayHello();
                Bot.AskNameUser();
                Bot.ShowMenu();
            }
            else
            {
                _bannerPage = "Administotor Page";

                _options = new string[] {"0.View list sushi",
                                         "1.Add sushi",
                                         "2.To change sushi",
                                         "3.Delete sushi",
                                         "4.View orders" 
                                        };

            }

        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "1.Add sushi":

                    Clear();
                    AddSushi();
                    BackToPageAdmin();

                    break;
                case "2.Upload sushi":
                    break;
                case "3.Delete sushi":
                    break;
                case "4.View orders":
                    break;
                case "0.View list sushi":
                    break;

            }
        }

        private void AddSushi()
        {
            WriteLine("Enter Id Sushi: ");
            _infoId = Convert.ToInt32(ReadLine());

            WriteLine("Enter Type Sushi: ");
            _type = ReadLine();

            WriteLine("Enter Name Sushi: ");
            _nameSushi = ReadLine();

            WriteLine("Enter Price Sushi: ");
            _price = Convert.ToDecimal(ReadLine());

            WriteLine("Enter Descripion Sushi (200 symbol): ");
            _description = ReadLine();

            Sushi sushi = new (_infoId, _type, _nameSushi, _price, _description);

            // SaveToDB should be the first
            SaveToDB(sushi);

            SaveToJson(sushi);            
        }

        private void BackToPageAdmin()
        {
            PageAdmin pageAdmin = new(Name,Pass);
            _ = pageAdmin.Run();
        }

        private void SaveToJson(Sushi sushi)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameSushi))
            {
                var fileName = File.ReadAllText(Observer.FileNameSushi);
                var sushiJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.sushiMenu = sushiJson.sushiMenu;
                model.sushiMenu.Add(sushi);

                File.Delete(Observer.FileNameSushi);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameSushi, _jsonObject);
            }
            else
            {
                model.sushiMenu.Add(sushi);

                string _jsonObject = JsonConvert.SerializeObject(model);

                File.AppendAllText(Observer.FileNameSushi, _jsonObject);
            }            
        }

        private void SaveToDB(Sushi sushi)
        {
            using ApplicationContext db = new ApplicationContext();
            try
            {
                db.Sushi.Add(sushi);
                db.SaveChanges();
                Logger<PageAdmin>.Debug($"Admin added sushi: {sushi}");
            }
            catch (Exception ex)
            {
                Logger<PageAdmin>.Error("Read InnerException", ex.InnerException);
                WriteLine("Error, please look logs!");
                Thread.Sleep(10000);
            }
        }
    }
}
