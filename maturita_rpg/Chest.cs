namespace maturita_rpg
{
    internal class Chest : GameObject
    {
        public Item content;
        public Chest(int y, int x) : base(y, x)
        {
            charToPrint = 'C';
        }

        public override void TakeEffect(Game game)
        {          
            game.player.inventory.Add(content);
            content.game = game;
            game.WriteIntoActionText("You looted a chest and got " + content.name);

            game.currentMap.walls[y, x].isWall = true;
            game.PrintPlayerInfo();            
        }
    }
}
