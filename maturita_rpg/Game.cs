﻿namespace maturita_rpg
{
    internal partial class Game
    {
        public List<Map>? maps;
        public Map? currentMap;
        private Coord? topLeftCornerShown;
        public Player player;

        //vars for print
        private int mapBoxWidth;
        private int mapBoxHeight;
        private int mapOffsetLeft;
        private int mapOffsetTop;
        private int playerInfoBoxWidth;

        public GameMenu? gameMenu;

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

        
        private void ConfigDoors() //tyhle config metody jsou manualni pridelovani a je to docela prasarna
        {
            Map insideMap = maps[0];
            Map outsideMap = maps[1];
            Map basement = maps[2];

            //INSIDE MAP
            List<Door> insideMapDoors = new List<Door>();
            foreach (var gameObject in insideMap.gameObjects)
            {
                if (gameObject is Door)
                {
                    Door door = (Door)gameObject;
                    door.entredMap = outsideMap;
                    insideMapDoors.Add(door);
                }
            }
            insideMapDoors[1].entredMap = basement;

            //OUTSIDE MAP
            List<Door> outsideMapDoors = new List<Door>();
            foreach (var gameObject in outsideMap.gameObjects)
            {
                if (gameObject is Door)
                {
                    Door door = (Door)gameObject;
                    door.entredMap = insideMap;
                    outsideMapDoors.Add(door);
                }
                
            }

            //SKLEP
            List<Door> basementMapDoors = new List<Door>();
            foreach (var gameObject in basement.gameObjects)
            {
                if (gameObject is Door)
                {
                    Door door = (Door)gameObject;
                    door.entredMap = insideMap;
                    basementMapDoors.Add(door);
                }
            }
            basementMapDoors[0].twinDoor = insideMapDoors[1];

            //INSIDE MAP
            //leading outside
            insideMapDoors[0].twinDoor = outsideMapDoors[0];            
            insideMapDoors[2].twinDoor = outsideMapDoors[1];
            insideMapDoors[3].twinDoor = outsideMapDoors[2];
            //leading down
            insideMapDoors[1].twinDoor = basementMapDoors[0];

            //OUTSIDE MAP
            //leading inside
            outsideMapDoors[0].twinDoor = insideMapDoors[0];
            outsideMapDoors[1].twinDoor = insideMapDoors[2];
            outsideMapDoors[2].twinDoor = insideMapDoors[3];

            
        }

        private void ConfigChests() //tyhle config metody jsou manualni pridelovani a je to docela prasarna
        {
            Map insideMap = maps[0];
            List<Chest> insideMapChests = new List<Chest>();

            foreach (var gameObject in insideMap.gameObjects)
            {
                if (gameObject is Chest)
                {
                    insideMapChests.Add((Chest)gameObject);
                }                
            }

            insideMapChests[0].content = new Weapon("Sword1", "testovaci zbran", 200);
            insideMapChests[1].content = new Weapon("Sword2", "ahoj", 11);
            insideMapChests[2].content = new Armor("Armor1", "nic", 15);
            insideMapChests[3].content = new Armor("Armor2", "ajdskf", 100);
        }

        private void ConfigEnemies() //tyhle config metody jsou manualni pridelovani a je to docela prasarna
        {
            Map insideMap = maps[0];
            List<EnemyObject> insideMapEnemies = new List<EnemyObject>();

            foreach (var gameObject in insideMap.gameObjects)
            {
                if (gameObject is EnemyObject)
                {
                    insideMapEnemies.Add((EnemyObject)gameObject);
                }
            }

            insideMapEnemies[0].enemy = new Enemy("bandit", 400, 50, 20, this);
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
                if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    move.y--;
                }
                else if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    move.y++;
                }
                else if (keyPressed.Key == ConsoleKey.LeftArrow)
                {
                    move.x--;
                }
                else if (keyPressed.Key == ConsoleKey.RightArrow)
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

        private void PrintBorders()
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
            
            foreach (var @object in currentMap.gameObjects)
            {
                if (player.y == @object.y && player.x == @object.x)
                {
                    @object.TakeEffect(this);
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
            topLeftCornerShown = new Coord(10, 50, true); //sets initial view
            mapOffsetLeft = 25;
            mapOffsetTop = 5;

            //print vars
            mapBoxWidth = 30;
            mapBoxHeight = 15;
            playerInfoBoxWidth = 24;
            ActionTextBoxWidth = 26;

            //player
            player = new Player(20, 59, "johny", 100, 10, 0, this); //sets initial player position

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
        }

        private void StartConfig()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(playerInfoBoxWidth + mapBoxWidth + ActionTextBoxWidth + 3, mapBoxHeight + 10);
            Console.Title = "GAME";

            gameMenu.StartMenu();

            LoadMap("map.txt");
            LoadMap("outsideMap.txt");
            LoadMap("sklepMap.txt");
            LoadMap("maze.txt");
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