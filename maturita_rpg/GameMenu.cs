namespace maturita_rpg
{
    internal class GameMenu
    {
        private List<string> controlsText;
        public GameMenu()
        {
            controlsText = new List<string>() //list of game controls
            {
                "CONTROLS:",
                "",
                "WASD or Arrows -> movement",
                "E -> open inventory",
                "Esc -> Pause menu",
                "F5 -> refresh print", 
                "",
                "Inventory:",
                "E or Esc -> close inventory",
                "W/S or Up/Down Arrows -> scroll inventory",
                "X -> equip/ use item",
                "Enter -> view item info",
                "",
                "Combat:",
                "1 -> normal attack",
                "2 -> attack with a chance for critical, costs 5 HP"
            };
        }

        public void StartMenu()
        {
            PrintInTheCenter(new List<string> {
                " _______   __    __  __    __   ______   ________   ______   __    __ ",
                "/       \\ /  |  /  |/  \\  /  | /      \\ /        | /      \\ /  \\  /  |",
                "$$$$$$$  |$$ |  $$ |$$  \\ $$ |/$$$$$$  |$$$$$$$$/ /$$$$$$  |$$  \\ $$ |",
                "$$ |  $$ |$$ |  $$ |$$$  \\$$ |$$ | _$$/ $$ |__    $$ |  $$ |$$$  \\$$ |",
                "$$ |  $$ |$$ |  $$ |$$$$  $$ |$$ |/    |$$    |   $$ |  $$ |$$$$  $$ |",
                "$$ |  $$ |$$ |  $$ |$$ $$ $$ |$$ |$$$$ |$$$$$/    $$ |  $$ |$$ $$ $$ |",
                "$$ |__$$ |$$ \\__$$ |$$ |$$$$ |$$ \\__$$ |$$ |_____ $$ \\__$$ |$$ |$$$$ |",
                "$$    $$/ $$    $$/ $$ | $$$ |$$    $$/ $$       |$$    $$/ $$ | $$$ |",
                "$$$$$$$/   $$$$$$/  $$/   $$/  $$$$$$/  $$$$$$$$/  $$$$$$/  $$/   $$/ ",
                "",
                "",
                "PRESS ANY KEY TO PLAY" });
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
            PrintInTheCenter(new List<string>() { "GAME PAUSED", " ", "Esc -> resume game", "H -> view controls","Del -> close the app" });

            //here the input is processed while the game is paused
            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (true)
            {
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    break; //resumes game
                }
                if (keyPressed.Key == ConsoleKey.Delete)
                {
                    Environment.Exit(0); //closes app
                }
                if (keyPressed.Key == ConsoleKey.H) //prints controls
                {
                    Console.Clear();
                    PrintInTheCenter(controlsText);
                    Console.ReadKey();
                    PauseMenu();
                    break;
                }
                keyPressed = Console.ReadKey(true);
            }
            Console.Clear();
        }

        public void TutorialMenu()
        {
            PrintInTheCenter(new List<string>()
            {
                "WELCOME TO THE GAME!",
                " ",
                "@ <- this is you",
                "C <- these are chests",
                "§ <- these are enemies",
                "% <- portals to other levels",
                "",
                "WASD or Arrows -> movement",
                "E -> open inventory, X -> equip/ use an item, Enter -> item info.",
                "Esc -> Pause Menu",
                "",
                "You can view the controls anytime in the pause menu.",
                "GOOD LUCK!"
            });
            Console.ReadKey();
            Console.Clear();
        }

        public void WinMenu()
        {
            Console.Clear();
            PrintInTheCenter(new List<string>
            {
                "You won!"
            });
            Console.ReadKey(true);
        }
        
        //prints text in the centre of the console
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
