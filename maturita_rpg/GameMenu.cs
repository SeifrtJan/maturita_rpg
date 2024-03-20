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
            PrintInTheCenter(new List<string> { "PRESS ANY KEY TO PLAY" });
            Console.ReadKey(true);
            Console.Clear();
        }

        public void EndMenu()
        {
            Console.Clear();
            PrintInTheCenter(new List<string> { "YOWZA! You died :(" , " " , "press any key to play again"});
            Console.ReadKey(true);
            Console.Clear();
        }

        public void PauseMenu()
        {
            Console.Clear();
            PrintInTheCenter(new List<string>() { "GAME PAUSED", " ", "press esc to continue", "press delete to close the app" });

            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (true)
            {
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    break;
                }
                if (keyPressed.Key == ConsoleKey.Delete)
                {
                    Environment.Exit(0);
                }

                keyPressed = Console.ReadKey(true);
            }
            Console.Clear();
        }
        
        private void PrintInTheCenter(List<string> text)
        {
            foreach (var line in text)
            {
                Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, (Console.WindowHeight - text.Count) / 2 + text.IndexOf(line));
                Console.Write(line);
            }
        }
    }
}
