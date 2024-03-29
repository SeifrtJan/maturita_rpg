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
            if (!looted)
            {
                game.player.inventory.Add(content);
                content.game = game;
                game.WriteIntoActionText("You looted a chest and got " + content.name);

                looted = true;
                game.currentMap.walls[y, x].isWall = true;
                game.PrintPlayerInfo();
            }
            else
                game.WriteIntoActionText("You have already looted this chest");
        }
    }
}
