﻿namespace maturita_rpg
{
    partial class Game
    {
        private bool inventoryEscape;
        public int selectedItemIndex;

        public void PrintInventory()
        {
            EraseMapBox();
            List<string> allItemsNames = new List<string>();

            Console.SetCursorPosition(mapOffsetLeft, mapOffsetTop);
            Console.Write("----------INVENTORY-----------");
            foreach (Item item in player.inventory)
            {
                allItemsNames.Add(item.name);
            }

            //actual print
            for (int i = 0; i < allItemsNames.Count && i < mapBoxHeight - 1; i++)
            {
                Console.SetCursorPosition(mapOffsetLeft, mapOffsetTop + i + 1);

                //highlights selected item
                if (selectedItemIndex == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(">{0}<", allItemsNames[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White; 
                }
                else
                {
                    Console.Write(allItemsNames[i]);
                }
            }
        }

        //the logic of inventory
        public void InventoryTick() //private
        {
            if (!player.inventory.Any<Item>()) //inventory is open unless it has no items
                inventoryEscape = true;
            else
                InventoryNavigation();
        }

        //processes player input while in inventory
        private void InventoryNavigation() 
        {           
            Item selectedItem = player.inventory[selectedItemIndex];

            ConsoleKeyInfo keyPressed = Console.ReadKey(intercept: true);
            if (keyPressed.Key == ConsoleKey.E || keyPressed.Key == ConsoleKey.Escape) 
            {
                inventoryEscape = true; //closes inv
            }

            if ((keyPressed.Key == ConsoleKey.DownArrow || keyPressed.Key == ConsoleKey.D) && selectedItemIndex + 1 < player.inventory.Count)
            {
                selectedItemIndex++; //moves down in inv
                PrintInventory();
            }
            else if ((keyPressed.Key == ConsoleKey.UpArrow || keyPressed.Key == ConsoleKey.W) && selectedItemIndex > 0)
            {
                selectedItemIndex--; //move up in inv
                PrintInventory();
            }
            //equiping an item
            else if (keyPressed.Key == ConsoleKey.X)
            {
                selectedItem.Equip();
            }
            //prints info about selected item
            else if(keyPressed.Key == ConsoleKey.Enter)
            {
                selectedItem.PrintInfo();
            }

        }

        public void ErasePlayerInfoBox() // private
        {
            for (int y = 0; y < mapBoxHeight; y++)
            {
                Console.SetCursorPosition(0, mapOffsetTop + y);
                for (int x = 0; x < playerInfoBoxWidth; x++)
                {
                    Console.Write(" ");
                }
            }
        }

        //prints info about player into the left console segment
        public void PrintPlayerInfo()
        {
            ErasePlayerInfoBox();
            player.UpdateStats();
            Console.SetCursorPosition(0, mapOffsetTop);
            Console.WriteLine("PLAYER INFO");
            Console.WriteLine("HP: " + player.hp);
            Console.WriteLine("damage:{0}", player.totalDamage);
            Console.WriteLine("armor:{0}", player.armor);
            Console.WriteLine("------------------------");
            Console.WriteLine("INVENTORY [{0}]", player.inventory.Count);
            Console.WriteLine("equiped weapon:\n{0} ({1})", player.equipedWeapon.name, player.equipedWeapon.damage);
            Console.WriteLine("equiped armor:\n{0} ({1})", player.equipedArmor.name, player.equipedArmor.armor);
        }

    }
}
