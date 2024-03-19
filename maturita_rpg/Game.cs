namespace maturita_rpg
{
    internal partial class Game
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

        public Game()
        {
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
                    maps[maps.Count -1].walls[y, x] = new Coord(y, x, lines[y][x] == '#' || lines[y][x] == 'x');

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

            //tutorial
            tutorialMapDoors[0].enteredMap = mazeMap;
            tutorialMapDoors[0].twinDoor = mazeMapDoors[0];

            //maze
            mazeMapDoors[0].enteredMap = tutorialMap;
            mazeMapDoors[0].twinDoor = tutorialMapDoors[0];
            mazeMapDoors[1].enteredMap = corridorsMap;
            mazeMapDoors[1].twinDoor = corridorMapDoors[0];

            //corridors
            corridorMapDoors[0].enteredMap = mazeMap;
            corridorMapDoors[0].twinDoor = mazeMapDoors[1];

            
        }

        private void ConfigChests() 
        {
            Map tutorialMap = maps[0];
            Map mazeMap = maps[1];
            Map corridorsMap = maps[2];

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
            mazeMapChests[0].content = new Weapon("Stick", "Better than nothing!", 15);
            mazeMapChests[1].content = new HealingItem("Healing Soup", "Very yummy!", 35);
            mazeMapChests[2].content = new Weapon("Scotty's Knife", "Usually used for cooking", 99);
            mazeMapChests[3].content = new Armor("Leather Tunic", "At least it'll keep you warm.", 20);
            mazeMapChests[4].content = new HealingItem("Villager's Booze", "May cause health issues.", 40);


        }

        private void ConfigEnemies()
        {
            Map tutorialMap = maps[0];
            Map mazeMap = maps[1];
            Map corridorsMap = maps[2];

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
            mazeMapEnemies[1].enemy = new Enemy("Lil Scotty", 500, 100, 25, this);
            mazeMapEnemies[2].enemy = new Enemy("Angry Villager", 200, 23, 10, this);


        }
      

        public void PrintCurrentArea()
        {
            char[,] SymbolsToPrint = new char[mapBoxHeight, mapBoxWidth];

            // writes map into char 2D array
            for (int y = 0; y < mapBoxHeight; y++)
            {
                for (int x = 0; x < mapBoxWidth; x++)
                {
                    if (currentMap.walls[topLeftCornerShown.y + y, topLeftCornerShown.x + x].isWall == true)
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
                            SymbolsToPrint[y, x] = gameObject.charToPrint;
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
            this.HandleKeyInput();
            this.PrintCurrentArea();
            this.CheckForCollisions();
        }


        private void HandleKeyInput()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey(intercept: true); 
            
            //movement 2.0
            {
                Coord move = new Coord(0, 0, false);
                if (keyPressed.Key == ConsoleKey.UpArrow || keyPressed.Key == ConsoleKey.W)
                {
                    move.y--;
                }
                else if (keyPressed.Key == ConsoleKey.DownArrow || keyPressed.Key == ConsoleKey.S)
                {
                    move.y++;
                }
                else if (keyPressed.Key == ConsoleKey.LeftArrow || keyPressed.Key == ConsoleKey.A)
                {
                    move.x--;
                }
                else if (keyPressed.Key == ConsoleKey.RightArrow || keyPressed.Key == ConsoleKey.D)
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

                
                PrintBorders();
                PrintPlayerInfo();
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
            topLeftCornerShown = new Coord(0, 0, true); //sets initial view
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
        }

        private void StartConfig()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(playerInfoBoxWidth + mapBoxWidth + ActionTextBoxWidth + 3, mapBoxHeight + 10);
            Console.Title = "GAME";

            gameMenu.StartMenu();

            LoadMap("../../../\\maps\\tutorial.txt");
            LoadMap("../../../\\maps\\maze.txt");
            LoadMap("../../../\\maps\\corridors.txt");

            currentMap = maps[0];

            ConfigDoors();
            ConfigChests();
            ConfigEnemies();

            PrintBorders();
            PrintCurrentArea();
            PrintPlayerInfo();
        }

        public void GameLogic()
        {
            Initialize();
            StartConfig();
            while (!player.IsDead())
            {
                Tick();
            }
            gameMenu.EndMenu();
        }

    }  


}
