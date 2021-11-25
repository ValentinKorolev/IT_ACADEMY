using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SushiMarcet
{
    internal class Page : ITransferPage
    {
        protected internal string[] _options;
        protected internal int _selectedIndex;
        protected internal string _bannerPage;
        protected internal ConsoleKey keyPressed;

        protected internal string LastName { get; set; }
        protected internal string FirstName { get; set; }

        public Page()
        {
            SetWindowSize(150, 40);
            SetBufferSize(150, 40);            
            
        }
        public virtual void DisplayOptions()
        {
            for (int i = 0; i < _options.Length; i++)
            {
                string currentOption = _options[i];


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
                SetCursorPosition(65, 10 + i);
                WriteLine($"{currentOption}");
            }
            Console.ResetColor();
        }
        public int Run()
        {
            do
            {
                Clear();

                Write(new string('*', 150));

                //SetCursorPosition(60, 7);
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

            ITransferPage(keyPressed, _options, _selectedIndex);
            
            return _selectedIndex;
        }

        private void ITransferPage(ConsoleKey keyPressed, string[] options, int selectedIndex)
        {
            if (keyPressed == ConsoleKey.Enter && _options[_selectedIndex] == "Registration")
            {
                Clear();
                PageRegistration pageRegistration = new PageRegistration();
                _selectedIndex = pageRegistration.Run();
            }
            else if (keyPressed == ConsoleKey.Enter && _options[_selectedIndex] == "Menu")
            {
                Clear();
                PageMenu pageMenu = new PageMenu();
                _selectedIndex = pageMenu.Run();
            }
            else if (keyPressed == ConsoleKey.Enter && _options[_selectedIndex] == "Exit")
            {
                Clear();
            }
        }
    }
}
