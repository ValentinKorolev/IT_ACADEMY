using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages.PagesAdministrotor
{
    internal class PageAdminOrdersNotReviewed : PageFather
    {
        List<Order> allOrders;
        List<Order> ordersNotConsidered;

        private string _goBack = "\nGo back";

        public PageAdminOrdersNotReviewed()
        {
            _bannerPage = "List of orders not reviewed";

            allOrders = GetAllOrders();

            _options = SetOptions(allOrders);
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "\nGo back":
                    BackToPageAdminOrders();
                    break;
                default:
                    PageAdminViewOrders pageAdminViewOrders = new(allOrders.ElementAt(selectedIndex));
                    _ = pageAdminViewOrders.Run();
                    break;
            }
        }

        private List<Order> GetAllOrders()
        {
            try
            {
                allOrders = GetAllOrdersFromDb();
                return allOrders;

            }
            catch (Exception ex)
            {
                allOrders = GetAllOrdersFromJson();
                return allOrders;
            }
        }

        private string[] SetOptions(List<Order> orders)
        {
            string[] options = new string[orders.Count() + 1];

            int counter = 1;

            for (int i = 0; i < options.Length - 1; i++)
            {
                options[i] = counter + orders.ElementAt(i).ToString() ;
                counter++;
            }
            options[^1] = _goBack;

            return options;
        }

        private List<Order> GetAllOrdersFromDb()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
               return db.Order.Where(_ => _.Status == StatusOrder.NotReviewed).ToList();
            }
        }

        private List<Order> GetAllOrdersFromJson()
        {
            if (File.Exists(Observer.FileNameOrders))
            {
                var fileName = File.ReadAllText(Observer.FileNameOrders);
                var orders = JsonConvert.DeserializeObject<ListOrders>(fileName);
                var listOrders = orders.Orders.Where(_ => _.Status == StatusOrder.NotReviewed).ToList();

                return listOrders;
            }
            else
            {
                WriteLine("File Orders.Json not found");
                BackToPageAdminOrders();
                return null;
            }
        }

        private void BackToPageAdminOrders()
        {
            PageAdminOrders pageAdminOrders = new();
            _ = pageAdminOrders.Run();
        }
    }
}
