using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace maturita_rpg
{
    internal class GameMenu
    {
        public void StartMenu()
        {
            PrintInTheCenter("PRESS ANY KEY TO PLAY");
            Console.ReadKey(true);
            Console.Clear();
        }

        public void EndMenu()
        {
            Console.Clear();
            PrintInTheCenter("YOU DIED!");
            Console.ReadKey(true);
            Console.Clear();
        }

        public void PauseMenu()
        {
            Console.Clear();
            PrintInTheCenter("game paused - press esc to continue");

            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (true)
            {
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    break;
                }
                //if (keyPressed.Key == ConsoleKey.Delete)
                {
                    //Console.
                }

                keyPressed = Console.ReadKey(true);
            }
            Console.Clear();
        }
        
        private void PrintInTheCenter(string text)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.WindowHeight / 2);
            Console.WriteLine(text);
        }
    }
}
