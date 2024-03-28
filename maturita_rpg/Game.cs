namespace maturita_rpg
{
    partial class Game
    {
        public List<Map>? maps;
        public Map? currentMap;
        private Coord? topLeftCornerShown;
        public Player player;

        //vars for print
        public int mapBoxWidth;
        private int mapBoxHeight;
        public int mapOffsetLeft;
        public int mapOffsetTop;
        private int playerInfoBoxWidth;

        public GameMenu? gameMenu;

        public Random rng;

        private bool tutorialPlayed;
        public bool gameWon;

        public Game()
        {
            tutorialPlayed = false;
            Initialize();
        }

        private void LoadMap(string path)
        {
            string[] lines = File.ReadAllLines(path);
            maps.Add(new Map(new Coord[lines.Length, lines[0].Length]));

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    maps[maps.Count -1].walls[y, x] = new Coord(y, x, lines[y][x] == '#' || lines[y][x] == 'x', lines[y][x] == 'x');

                    //objects
                    switch (lines[y][x])
                    {
                        case '%':
                            maps[maps.Count - 1].gameObjects.Add(new Door(y, x));
                            break;

                        case 'C':
                            maps[maps.Count - 1].gameObjects.Add(new Chest(y, x));
                            break;

                        case '§':
                            maps[maps.Count - 1].gameObjects.Add(new EnemyObject(y, x));
                            break;

                    }

                }                
            }
        }

        private void ConfigDoors()
        {
            Map tutorialMap = maps[0];
            Map mazeMap = maps[1];
            Map corridorsMap = maps[2];
            Map roomsMap = maps[3];
            Map finalMap = maps[4];

            //TUTORIAL
            List<Door> tutorialMapDoors = new List<Door>();
            foreach (var gameObject in tutorialMap.gameObjects)
            {
                if (gameObject is Door)
                    tutorialMapDoors.Add(gameObject as Door);
            }

            //MAZE
            List<Door> mazeMapDoors = new List<Door>();
            foreach (var gameObject in mazeMap.gameObjects)
            {
                if (gameObject is Door)
                    mazeMapDoors.Add(gameObject as Door);
            }

            //CORRIDOR
            List<Door> corridorMapDoors = new List<Door>();
            foreach (var gameObject in corridorsMap.gameObjects)
            {
                if (gameObject is Door)
                    corridorMapDoors.Add(gameObject as Door);
            }

            //ROOMS
            List<Door> roomsMapDoors = new List<Door>();
            foreach (var gameObject in roomsMap.gameObjects)
            {
                if (gameObject is Door)
                    roomsMapDoors.Add(gameObject as Door);
            }

            //FINAL 
            List<Door> finalMapDoors = new List<Door>();
            foreach (var gameObject in finalMap.gameObjects)
            {
                if (gameObject is Door)
                    finalMapDoors.Add(gameObject as Door);
            }

            //tutorial
            tutorialMapDoors[0].enteredMap = mazeMap;
            tutorialMapDoors[0].twinDoor = mazeMapDoors[0];

            //maze
            mazeMap.walls[mazeMapDoors[0].y, mazeMapDoors[0].x].isWall = true;//so that you cant return to tutorial

            mazeMapDoors[0].enteredMap = tutorialMap;
            mazeMapDoors[0].twinDoor = tutorialMapDoors[0];
            mazeMapDoors[1].enteredMap = corridorsMap;
            mazeMapDoors[1].twinDoor = corridorMapDoors[0];

            //corridors
            corridorMapDoors[0].enteredMap = mazeMap;
            corridorMapDoors[0].twinDoor = mazeMapDoors[1];
            corridorMapDoors[1].enteredMap = roomsMap;
            corridorMapDoors[1].twinDoor = roomsMapDoors[4];
            corridorMapDoors[2].enteredMap = roomsMap;
            corridorMapDoors[2].twinDoor = roomsMapDoors[2];
            corridorMapDoors[3].enteredMap = finalMap;
            corridorMapDoors[3].twinDoor = finalMapDoors[0];
            corridorMapDoors[4].enteredMap = finalMap;
            corridorMapDoors[4].twinDoor = finalMapDoors[1];

            //rooms
            roomsMapDoors[0].enteredMap = corridorsMap;
            roomsMapDoors[0].twinDoor = corridorMapDoors[0];
            roomsMapDoors[1].enteredMap = corridorsMap;
            roomsMapDoors[1].twinDoor = corridorMapDoors[0];
            roomsMapDoors[2].enteredMap = corridorsMap;
            roomsMapDoors[2].twinDoor = corridorMapDoors[2];
            roomsMapDoors[3].enteredMap = corridorsMap;
            roomsMapDoors[3].twinDoor = corridorMapDoors[0];
            roomsMapDoors[4].enteredMap = corridorsMap;
            roomsMapDoors[4].twinDoor = corridorMapDoors[1];

            //final
            finalMapDoors[0].enteredMap = corridorsMap;
            finalMapDoors[0].twinDoor = corridorMapDoors[3];
            finalMapDoors[1].enteredMap = corridorsMap;
            finalMapDoors[1].twinDoor = corridorMapDoors[4];
            finalMapDoors[2].isEndOfGame = true;
        }

        private void ConfigChests() 
        {
            Map tutorialMap = maps[0];
            Map mazeMap = maps[1];
            Map corridorsMap = maps[2];
            Map roomsMap = maps[3];
            Map finalMap = maps[4];

            //TUTORIAL
            List<Chest> tutorialMapChests = new List<Chest>();
            foreach (var gameObject in tutorialMap.gameObjects)
            {
                if (gameObject is Chest)
                    tutorialMapChests.Add(gameObject as Chest);
            }
            tutorialMapChests[0].content = new Weapon("Weapon", "These make your attacks stronger.", 10);
            tutorialMapChests[1].content = new Armor("Armor", "These make you more resilient against attacks.", 2);
            tutorialMapChests[2].content = new HealingItem("Potion", "These regenerate your health", 20);

            //MAZE
            List<Chest> mazeMapChests = new List<Chest>();
            foreach (var gameObject in mazeMap.gameObjects)
            {
                if (gameObject is Chest)
                    mazeMapChests.Add(gameObject as Chest);
            }
            mazeMapChests[0].content = new Weapon("Stick", "Better than nothing!", 20);
            mazeMapChests[1].content = new HealingItem("Healing Soup", "Very yummy!", 35);
            mazeMapChests[2].content = new Weapon("Scotty's Knife", "Usually used for cooking", 199);
            mazeMapChests[3].content = new Armor("Leather Tunic", "At least it'll keep you warm.", 20);
            mazeMapChests[4].content = new HealingItem("Villager's Flask", "Drinking this may cause health issues.", 40);

            //CORRIDORS
            List<Chest> corridorsMapChests = new List<Chest>();
            foreach (var gameObject in corridorsMap.gameObjects)
            {
                if (gameObject is Chest)
                    corridorsMapChests.Add(gameObject as Chest);
            }
            corridorsMapChests[0].content = new HealingItem("Healing Potion", "Brewed in your local brewery!", 40);
            corridorsMapChests[1].content = new Weapon("Giants Boulder", "Crushes anything in its way.", 90);
            corridorsMapChests[2].content = new Armor("Giants Shirt", "Fits 8 players", 60);
            corridorsMapChests[3].content = new HealingItem("Apple", "An apple a day, keeps the doctor away!", 50);
            corridorsMapChests[4].content = new Weapon("Stick2", "Better luck next time.", 16);

            //ROOMS
            List<Chest> roomsMapChests = new List<Chest>();
            foreach (var gameObject in roomsMap.gameObjects)
            {
                if (gameObject is Chest)
                    roomsMapChests.Add(gameObject as Chest);
            }
            roomsMapChests[0].content = new Weapon("Baseball bat", "Can be deadly.", 35);
            roomsMapChests[1].content = new Weapon("Pencil", "Very sharp!", 80);
            roomsMapChests[2].content = new HealingItem("Medics Bag", "Everything you need to heal your wounds.", 100);
            roomsMapChests[3].content = new Armor("Chainmail Pants", "More comfy than you'd think!", 35);

            //FINAL
            List<Chest> finalMapChests = new List<Chest>();
            foreach (var gameObject in finalMap.gameObjects)
            {
                if (gameObject is Chest)
                    finalMapChests.Add(gameObject as Chest);
            }
            finalMapChests[0].content = new HealingItem("Cookie", "Baked by Little Scotty.", 50);
            finalMapChests[1].content = new HealingItem("Last Supper", "Might be the last thing you ever eat.", 80);            
        }

        private void ConfigEnemies()
        {
            Map tutorialMap = maps[0];
            Map mazeMap = maps[1];
            Map corridorsMap = maps[2];
            Map roomsMap = maps[3];
            Map finalMap = maps[4];

            //TUTORIAL
            List<EnemyObject> tutorialMapEnemies = new List<EnemyObject>();
            foreach (var gameObject in tutorialMap.gameObjects)
            {
                if (gameObject is EnemyObject)
                    tutorialMapEnemies.Add(gameObject as EnemyObject);
            }
            tutorialMapEnemies[0].enemy = new Enemy("Dummy", 100, 3, 0, this);

            //MAZE
            List<EnemyObject> mazeMapEnemies = new List<EnemyObject>();
            foreach (var gameObject in mazeMap.gameObjects)
            {
                if (gameObject is EnemyObject)
                    mazeMapEnemies.Add(gameObject as EnemyObject);
            }
            mazeMapEnemies[0].enemy = new Enemy("Bandit", 100, 20, 3, this);
            mazeMapEnemies[1].enemy = new Enemy("Lil Scotty", 400, 90, 25, this);
            mazeMapEnemies[2].enemy = new Enemy("Angry Villager", 200, 30, 10, this);

            //CORRIDORS
            List<EnemyObject> corridorsMapEnemies = new List<EnemyObject>();
            foreach (var gameObject in corridorsMap.gameObjects)
            {
                if (gameObject is EnemyObject)
                    corridorsMapEnemies.Add(gameObject as EnemyObject);
            }
            corridorsMapEnemies[0].enemy = new Enemy("Giant", 1000, 40, 10, this);
            corridorsMapEnemies[1].enemy = new Enemy("You shall not pass!", 600, 80, 60, this);
            corridorsMapEnemies[2].enemy = new Enemy("Spy", 111, 200, 0, this);

            //Rooms
            List<EnemyObject> roomsMapEnemies = new List<EnemyObject>();
            foreach (var gameObject in roomsMap.gameObjects)
            {
                if (gameObject is EnemyObject)
                    roomsMapEnemies.Add(gameObject as EnemyObject);
            }
            roomsMapEnemies[0].enemy = new Enemy("Guard", 167, 35, 10, this);
            roomsMapEnemies[1].enemy = new Enemy("Assassin", 70, 150, 0, this);
            roomsMapEnemies[2].enemy = new Enemy("Guard", 200, 30, 10, this);
            roomsMapEnemies[3].enemy = new Enemy("Guard", 400, 40, 30, this);

            //FINAL
            List<EnemyObject> finalMapEnemies = new List<EnemyObject>();
            foreach (var gameObject in finalMap.gameObjects)
            {
                if (gameObject is EnemyObject)
                    finalMapEnemies.Add(gameObject as EnemyObject);
            }
            finalMapEnemies[0].enemy = new Enemy("Dark Knight", 400, 100, 40, this);
            finalMapEnemies[1].enemy = new Enemy("Skeleton", 200, 80, 10, this);
            finalMapEnemies[2].enemy = new Enemy("Bosses PA", 200, 150, 20, this);
            finalMapEnemies[3].enemy = new Enemy("Ghost", 300, 70, 60, this);
            finalMapEnemies[4].enemy = new Enemy("Monke Frankie", 1500, 90, 40, this);
        }
      

        public void PrintCurrentArea()
        {
            char[,] SymbolsToPrint = new char[mapBoxHeight, mapBoxWidth];

            // writes map into char 2D array
            for (int y = 0; y < mapBoxHeight; y++)
            {
                for (int x = 0; x < mapBoxWidth; x++)
                {
                    if (currentMap.walls[topLeftCornerShown.y + y, topLeftCornerShown.x + x].isDeadSpace)
                    {
                        SymbolsToPrint[y, x] = ' ';
                    }
                    else if (currentMap.walls[topLeftCornerShown.y + y, topLeftCornerShown.x + x].isWall)
                    {
                        SymbolsToPrint[y, x] = '#';
                    }
                    else if (topLeftCornerShown.y + y == player.y && topLeftCornerShown.x + x == player.x)
                    {
                        SymbolsToPrint[y, x] = '@';
                    }                    
                    else
                    {
                        SymbolsToPrint[y, x] = '.';
                    }

                    foreach (GameObject gameObject in currentMap.gameObjects)
                    {
                        if (topLeftCornerShown.y + y == gameObject.y && topLeftCornerShown.x + x == gameObject.x)
                        {
                            SymbolsToPrint[y, x] = gameObject.charToPrint;
                        }
                    }
                }
            }

            // actual print
            for (int y = 0; y < mapBoxHeight; y++)
            {
                Console.SetCursorPosition(mapOffsetLeft, mapOffsetTop + y);
                for (int x = 0; x < mapBoxWidth; x++)
                {
                    Console.Write(SymbolsToPrint[y, x]);
                }                
            }
        }

        public void EraseMapBox()
        {
            for (int y = 0; y < mapBoxHeight; y++)
            {
                Console.SetCursorPosition(mapOffsetLeft, mapOffsetTop + y);
                for (int x = 0; x < mapBoxWidth; x++)
                {
                    Console.Write(" ");
                }
            }

            combatLineIndex = 0;
        }

        public void Tick()
        {            
            HandleKeyInput();
            PrintCurrentArea();
            CheckForCollisions();
        }


        private void HandleKeyInput()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey(intercept: true);
            switch (keyPressed.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.A:
                case ConsoleKey.S:
                case ConsoleKey.D:
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    //movement 2.0
                    {
                        Coord move = new Coord(0, 0, false, false);
                        if (keyPressed.Key == ConsoleKey.UpArrow || keyPressed.Key == ConsoleKey.W) //pohyb nahoru
                        {
                            move.y--;
                        }
                        else if (keyPressed.Key == ConsoleKey.DownArrow || keyPressed.Key == ConsoleKey.S) //pohyb dolů 
                        {
                            move.y++;
                        }
                        else if (keyPressed.Key == ConsoleKey.LeftArrow || keyPressed.Key == ConsoleKey.A) //pohyb vlevo
                        {
                            move.x--;
                        }
                        else if (keyPressed.Key == ConsoleKey.RightArrow || keyPressed.Key == ConsoleKey.D) //pohyb vpravo
                        {
                            move.x++;
                        }
                        if (currentMap.walls[player.y + move.y, player.x + move.x].isWall == false)
                        {
                            //moves the map and the player stays in the center
                            if (topLeftCornerShown.y + move.y >= 0 && topLeftCornerShown.y + move.y <= currentMap.walls.GetLength(0) - mapBoxHeight)
                            {
                                topLeftCornerShown.y = topLeftCornerShown.y + move.y;
                                player.y = player.y + move.y;
                            }
                            //moves the player (approaching end of the map)
                            else
                            {
                                player.y = player.y + move.y;
                            }
                            //moves the map and the player stays in the center
                            if (topLeftCornerShown.x + move.x >= 0 && topLeftCornerShown.x + move.x <= currentMap.walls.GetLength(1) - mapBoxWidth)
                            {
                                topLeftCornerShown.x = topLeftCornerShown.x + move.x;
                                player.x = player.x + move.x;
                            }
                            //moves the player
                            else
                            {
                                player.x = player.x + move.x;
                            }
                        }
                    }
                    break;

                default:
                    if (keyPressed.Key == ConsoleKey.E)
                    {
                        if (!player.inventory.Any<Item>())
                            WriteIntoActionText("inventory is empty");
                        else
                        {
                            PrintInventory();
                            while (inventoryEscape == false)
                            {
                                InventoryTick();
                            }
                            inventoryEscape = false;
                        }
                    }
                    else if (keyPressed.Key == ConsoleKey.Escape)
                    {
                        gameMenu.PauseMenu();

                        RefreshPrint();
                    }
                    else if (keyPressed.Key == ConsoleKey.F5)
                        RefreshPrint();
                    break;

            }        

            

        }

        public void PrintBorders()
        {
            Console.ForegroundColor = ConsoleColor.Red;            

            //bottom
            Console.SetCursorPosition(0, mapOffsetTop + mapBoxHeight);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("=");
            }
            //top
            Console.SetCursorPosition(0, mapOffsetTop - 1);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("_");
            }
            //right
            for (int i = 0; i < mapBoxHeight; i++)
            {
                Console.SetCursorPosition(mapOffsetLeft + mapBoxWidth, mapOffsetTop + i);
                Console.Write("||");
            }
            //left
            for (int i = 0; i < mapBoxHeight; i++)
            {
                Console.SetCursorPosition(mapOffsetLeft - 2, mapOffsetTop + i);
                Console.Write("||");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        private void CheckForCollisions()
        {            
            foreach (var gameObject in currentMap.gameObjects)
            {
                if (player.y == gameObject.y && player.x == gameObject.x)
                {
                    gameObject.TakeEffect(this);
                    break;
                }
            }
        }

        public void UpdateMapView()
        {
            //centers player
            //on the - side
            if (player.y - mapBoxHeight / 2 > 0)
            {
                topLeftCornerShown.y = player.y - mapBoxHeight / 2;
            }
            else
            {
                topLeftCornerShown.y = 0;
            }
            if (player.x - mapBoxWidth / 2 > 0)
            {
                topLeftCornerShown.x = player.x - mapBoxWidth / 2;
            }
            else
            {
                topLeftCornerShown.x = 0;
            }

            //out of bounds (on the + side)
            if (topLeftCornerShown.y + mapBoxHeight > currentMap.mapHeight)
            {
                topLeftCornerShown.y = currentMap.mapHeight - mapBoxHeight;
            }
            if (topLeftCornerShown.x + mapBoxWidth > currentMap.mapWidth)
            {
                topLeftCornerShown.x = currentMap.mapWidth - mapBoxWidth;
            }
        }
               
        private void Initialize() 
        {
            //map
            maps = new List<Map>();          
            topLeftCornerShown = new Coord(0, 0, true, true); //sets initial view
            mapOffsetLeft = 25;
            mapOffsetTop = 5;

            //print vars
            mapBoxWidth = 30;
            mapBoxHeight = 15;
            playerInfoBoxWidth = 24;
            ActionTextBoxWidth = 26;

            //player
            player = new Player(3, 25, "Player", 100, 10, 0, this); //sets initial player position

            //inventory
            inventoryEscape = false;
            selectedItemIndex = 0;

            //action text
            ActionText = new List<string>();
            actionLineIndex = 0;

            //combat text
            CombatText = new List<string>();
            combatLineIndex = 0;

            gameMenu = new GameMenu();

            rng = new Random();

            gameWon = false;
        }

        private void StartGame()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(playerInfoBoxWidth + mapBoxWidth + ActionTextBoxWidth + 3, mapBoxHeight + 10);
            Console.Title = "GAME";

            gameMenu.StartMenu();

            LoadMap("../../../\\maps\\tutorial.txt");
            LoadMap("../../../\\maps\\maze.txt");
            LoadMap("../../../\\maps\\corridors.txt");
            LoadMap("../../../\\maps\\rooms.txt");
            LoadMap("../../../\\maps\\final.txt");

            //player plays tutorial only once
            if (tutorialPlayed == false)
            {
                currentMap = maps[0];
                tutorialPlayed = true;

                gameMenu.TutorialMenu();
                WriteIntoActionText("This level is the TUTORIAL");
            }
            else
            {
                currentMap = maps[1];
                player.x = 5;
                player.y = 5;

            }

            ConfigDoors();
            ConfigChests();
            ConfigEnemies();

            RefreshPrint();
        }

        public void GameLogic()
        {
            Initialize();
            StartGame();
            while (!player.IsDead() && !gameWon)
            {
                Tick();
            }
            if (gameWon)
                gameMenu.WinMenu();
            else
                gameMenu.EndMenu();
        }

        public void RefreshPrint()
        {
            Console.Clear();
            PrintBorders();
            PrintCurrentArea();
            actionLineIndex = 0;
            PrintActionText();
            PrintPlayerInfo();
        }

    }  


}
