namespace maturita_rpg
{
    internal class Chest : GameObject
    {
        public Item content;
        public bool looted;
        public Chest(int y, int x) : base(y, x)
        {
            looted = false;
            charToPrint = 'C';
        }

        public override void TakeEffect(Game game)
        {
            if (!looted) //so that if the chest is next to a wall, you can't get infite loot
            {
                game.player.inventory.Add(content);
                content.game = game;
                game.WriteIntoActionText("You looted a chest and got " + content.name);

                looted = true;
                game.currentMap.walls[y, x].isWall = true; //chest tile can't be reentered
                game.PrintPlayerInfo();
            }
            else
                game.WriteIntoActionText("You have already looted this chest");
        }
    }
}
