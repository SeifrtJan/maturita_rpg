namespace maturita_rpg
{
    internal class Armor : Item
    {
        public int armor;

        public Armor(string name, string description, int armor) : base(name, description)
        {
            this.armor = armor;
        }

        public override void Equip()
        {
            game.player.equipedArmor = this;

            game.WriteIntoActionText("You equiped " + name + " as your armor.");
            game.player.UpdateStats();
            game.PrintPlayerInfo();
        }

        public override void PrintInfo()
        {
            game.WriteIntoActionText(name + ": " + description + " (" + armor + ").");
        }
    }
}
