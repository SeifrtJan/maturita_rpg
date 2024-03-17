using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace maturita_rpg
{
    internal partial class Game
    {
        private bool inventoryEscape;
        public int selectedItemIndex;

        public void PrintInventory()
        {
            EraseMapBox();
            List<string> allItemsNames = new List<string>();

            Console.SetCursorPosition(mapOffsetLeft, mapOffsetTop);
            Console.Write("--------INVENTORY--------");
            foreach (Item item in player.inventory)
            {
                allItemsNames.Add(item.name);
            }

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

        public void InventoryTick()
        {
            if (!player.inventory.Any<Item>())
                inventoryEscape = true;
            else
                InventoryNavigation();
        }

        private void InventoryNavigation()
        {           
            Item selectedItem = player.inventory[selectedItemIndex];

            ConsoleKeyInfo keyPressed = Console.ReadKey(intercept: true);
            if (keyPressed.Key == ConsoleKey.E || keyPressed.Key == ConsoleKey.Escape)
            {
                inventoryEscape = true;
            }

            if (keyPressed.Key == ConsoleKey.DownArrow && selectedItemIndex + 1 < this.player.inventory.Count)
            {
                selectedItemIndex++;
                PrintInventory();
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && selectedItemIndex > 0)
            {
                selectedItemIndex--;
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

        public void ErasePlayerInfoBox()
        {
            for (int y = 0; y < mapBoxHeight; y++)
            {
                Console.SetCursorPosition(0, mapOffsetTop + y);
                for (int x = 0; x < playerInfoBoxWidth; x++) // ta sirka je hodne makeshift.
                {
                    Console.Write(" ");
                }
            }
        }

        public void PrintPlayerInfo()
        {
            ErasePlayerInfoBox();
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
