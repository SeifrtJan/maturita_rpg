namespace maturita_rpg
{
    abstract class GameObject
    {
        public int y;
        public int x;
        public char charToPrint;

        public GameObject(int y, int x)
        {
            this.y = y;
            this.x = x;
        }
        public abstract void TakeEffect(Game game);
    }
}
