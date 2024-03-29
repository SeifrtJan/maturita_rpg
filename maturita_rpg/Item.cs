namespace maturita_rpg
{
    abstract class Item
    {
        public string name;
        public string description;
        public Game? game;

        public Item(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public abstract void Equip();

        public abstract void PrintInfo();
    }
}
