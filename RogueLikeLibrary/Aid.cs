namespace RoguelikeGame
{
    public class Aid
    {
        public string Name { get; }
        public int HealAmount { get; }

        public Aid(string name, int healAmount)
        {
            Name = name;
            HealAmount = healAmount;
        }
    }
}
