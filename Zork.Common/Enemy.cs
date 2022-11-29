namespace Zork.Common
{
    public class Enemy
    {
        public string Name { get; }
        public string Description { get; }
        public int HitPoints { get; set; }


        public Enemy(string name, string description, int hitPoints)
        {
            Name = name;
            Description = description;
            HitPoints = hitPoints;
        }

    }
}
