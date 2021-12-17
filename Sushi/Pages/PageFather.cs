using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    public abstract class PageFather
    {
        protected string[] _options;
        protected int _selectedIndex;
        protected string _bannerPage;
        protected ConsoleKey keyPressed;
        private void DisplayOptions()
        {
            WriteLine();

            for (int i = 0; i < _options.Length; i++)
            {
                if (i == _selectedIndex)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
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

            TransferPage(_options, _selectedIndex);

            return _selectedIndex;
        }

        protected virtual void TransferPage(string[] options, int selectedIndex)
        {
            
        }
    }
}
