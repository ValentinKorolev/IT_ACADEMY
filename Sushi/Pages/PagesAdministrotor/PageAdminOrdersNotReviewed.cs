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

        SqlOrdersRepository sqlOrdersRepository = new SqlOrdersRepository();
        JsonOrderRepository jsonOrderRepository = new JsonOrderRepository();

        private string _goBack = "\nGo back";

        public PageAdminOrdersNotReviewed()
        {
            _bannerPage = "List of orders not reviewed";

            allOrders = (List<Order>?)sqlOrdersRepository.GetItemList(StatusOrder.NotReviewed);
            //allOrders = (List<Order>?)jsonOrderRepository.GetItemList(StatusOrder.NotReviewed);

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

        private void BackToPageAdminOrders()
        {
            PageAdminOrders pageAdminOrders = new();
            _ = pageAdminOrders.Run();
        }
    }
}
