using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    public class Menu
    {
        private string[] _options;
        private int _selectedIndex;
        private string _bannerPage;
        private ConsoleKey keyPressed;

        public Menu()
        {
            _bannerPage = $"{Observer.nameUser} what do you want?";
            _options = new string[] { "View the menu", "Go out" };
        }

        private void DisplayOptions()
        {
            WriteLine();

            for (int i = 0; i < _options.Length; i++)
            {
                if (i == _selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"{_options[i]}");
            }
            ResetColor();
        }

        public int Run()
        {
            do
            {
                Clear();

                WriteLine(_bannerPage);

                DisplayOptions();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    _selectedIndex--;

                    if (_selectedIndex == -1)
                    {
                        _selectedIndex = _options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    _selectedIndex++;

                    if (_selectedIndex == _options.Length)
                    {
                        _selectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            //TransferPage(keyPressed, _options, _selectedIndex);

            return _selectedIndex;
        }
    }
}
