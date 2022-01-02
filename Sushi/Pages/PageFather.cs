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

        private string color = Observer.color;

        public int Run()
        {
            do
            {
                DisplayOptions(color);

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

        private void DisplayOptions(string color)
        {
            if (color == "From dusk to dawn")
            {
                Clear();
                ForegroundColor = ConsoleColor.Red;
                BackgroundColor = ConsoleColor.Black;

                WriteLine(_bannerPage);

                ResetColor();

                DisplayOptionsWhiteRed();
            }
            else if (color == "California")
            {
                Clear();
                ForegroundColor = ConsoleColor.Yellow;
                BackgroundColor = ConsoleColor.Black;

                WriteLine(_bannerPage);

                ResetColor();

                DisplayOptionsBlackYellow();
            }
            else if(color == "Matrix")
            {
                Clear();
                ForegroundColor = ConsoleColor.Green;
                BackgroundColor = ConsoleColor.Black;

                WriteLine(_bannerPage);
                
                ResetColor();

                DisplayOptionsBlackGreen();
            }
            else
            {
                Clear();

                WriteLine(_bannerPage);

                DisplayOptionsClassic();
            }
        }

        private void DisplayOptionsWhiteRed()
        {

            WriteLine();

            for (int i = 0; i < _options.Length; i++)
            {
                if (i == _selectedIndex)
                {
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Red;
                }
                else
                {
                    ForegroundColor = ConsoleColor.Red;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"{_options[i]}");
            }
            ResetColor();
        }

        private void DisplayOptionsBlackGreen()
        {
            WriteLine();

            for (int i = 0; i < _options.Length; i++)
            {
                if (i == _selectedIndex)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.Green;
                }
                else
                {
                    ForegroundColor = ConsoleColor.Green;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"{_options[i]}");
            }
            ResetColor();
        }

        private void DisplayOptionsBlackYellow()
        {
            WriteLine();

            for (int i = 0; i < _options.Length; i++)
            {
                if (i == _selectedIndex)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    ForegroundColor = ConsoleColor.Yellow;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"{_options[i]}");
            }
            ResetColor();
        }

        private void DisplayOptionsClassic()
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
    }
}
