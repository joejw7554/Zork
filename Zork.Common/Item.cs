namespace Zork.Common
{
    public class Item
    {
        public string Name { get; }

        public string LookDescription { get; }

        public string InventoryDescription { get; }

        public bool Weapon { get; }

        public int Damage { get; }

        public Item(string name, string lookDescription, string inventoryDescription, bool weapon, int damage)
        {
            Name = name;
            LookDescription = lookDescription;
            InventoryDescription = inventoryDescription;
            Weapon = weapon;
            Damage = damage;
        }

        public override string ToString() => Name;
    }
}