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
        
        //called when player steps on the object's tile
        public abstract void TakeEffect(Game game);
    }
}
