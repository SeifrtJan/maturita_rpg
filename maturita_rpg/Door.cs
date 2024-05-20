namespace maturita_rpg
{
    class Door : GameObject
    {
        public Door twinDoor;
        public Map enteredMap;
        public bool isEndOfGame;

        public Door(int y, int x) : base(y, x)
        {
            charToPrint = '%';
            isEndOfGame = false;
        }

        public override void TakeEffect(Game game)
        {
            if (!isEndOfGame)
            {
                game.currentMap = enteredMap; //changes the current map
                game.player.y = twinDoor.y; //changes player's location
                game.player.x = twinDoor.x;

                game.UpdateMapView();
                game.PrintCurrentArea();
            }
            else
            {
                game.gameWon = true;
            }

        }
    }
}
