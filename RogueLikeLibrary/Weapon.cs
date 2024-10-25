using System;

namespace RoguelikeGame
{
    public class Weapon
    {
        public string Name { get; }
        public int Damage { get; }
        public int Durability { get; private set; }

        public Weapon(string name, int damage, int durability)
        {
            Name = name;
            Damage = damage;
            Durability = durability;
        }

        // Метод использования оружия с уменьшением прочности
        public int Use()
        {
            if (Durability > 0)
            {
                Durability--;
                return Damage;
            }
            else
            {
                Console.WriteLine("Ваше оружие сломано! Вы бьете кулаками.");
                return 1; // Урон кулаков
            }
        }

        // Метод проверки состояния оружия
        public string CheckDurability()
        {
            if (Durability <= 0)
                return $"{Name} сломано!";
            else if (Durability <= 2)
                return $"{Name} скоро сломается. Прочность: {Durability}";
            else
                return $"{Name} в хорошем состоянии. Прочность: {Durability}";
        }

        public bool IsBroken => Durability <= 0;

        // Клонирование оружия при подборе
        public Weapon Clone()
        {
            return new Weapon(Name, Damage, Durability);
        }
    }
}
