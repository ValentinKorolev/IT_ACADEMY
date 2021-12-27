using SushiMarcet.Pages.PagesAdministrotor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal class PageAdminOrders : PageFather
    {
        private const string NameAdmin = "Admin123";
        private const string PassAdmin = "122345";

        public PageAdminOrders()
        {
            _bannerPage = "Orders";

            _options = new string[] {"List of orders not reviewed",
                                     "List of completed orders",
                                     "List of rejected orders",
                                     "List of orders in progress",
                                     "Go back",
                                    };
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "Go back":
                    BackToPageAdmin();
                    break;
                case "List of orders not reviewed":
                    PageAdminOrdersNotReviewed pageAdminOrdersNotConsidered = new();
                    _ = pageAdminOrdersNotConsidered.Run();
                    break;
                case "List of completed orders":
                    break;
                case "List of rejected orders":
                    break;
                case "List of orders in progress":
                    break;
            }
        }

        private void GetAllOrders()
        {
            

        }

        private void BackToPageAdmin()
        {
            PageAdmin pageAdmin = new(NameAdmin, PassAdmin);
            _ = pageAdmin.Run();
        }

        private void PageAdminOrdersRun()
        {
            PageAdminOrders pageAdminOrders = new();
            _ = pageAdminOrders.Run();
        }
    }
}
