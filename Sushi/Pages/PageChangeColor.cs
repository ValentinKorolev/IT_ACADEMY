using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal class PageChangeColor : PageFather
    {
        public PageChangeColor()
        {
            _bannerPage = "Change Color";
            _options = new string[] { "Classic", "California", "Matrix", "From dusk to dawn", "\nGo back" };
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "Classic":
                    ChangeColor("Classic");
                    PageChangeColorRun();
                    break;
                case "California":
                    ChangeColor("California");
                    PageChangeColorRun();
                    break;
                case "\nGo back":
                    PageMainMenuRun();
                    break;
                case "Matrix":
                    ChangeColor("Matrix");
                    PageChangeColorRun();
                    break;
                case "From dusk to dawn":
                    ChangeColor("From dusk to dawn");
                    PageChangeColorRun();
                    break;
            }
        }

        private void ChangeColor(string color)
        {            
           Observer.color = color;
        }

        private void PageChangeColorRun()
        {
            PageChangeColor pageChangeColor = new PageChangeColor();
            _ = pageChangeColor.Run();
        }

        private void PageMainMenuRun()
        {
            PageMainMenu pageMainMenu = new PageMainMenu();
            _ = pageMainMenu.Run();
        }
    }
}
