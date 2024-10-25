using System;

namespace RoguelikeGame
{
    public class Enemy
    {
        public string Name { get; }
        public int Health { get; private set; }
        public int MaxHealth { get; }
        public Weapon Weapon { get; }

        public Enemy(string name, int health, Weapon weapon)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
            Weapon = weapon;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
            Console.WriteLine($"{Name} получает {damage} урона. Оставшееся здоровье: {Health}");
        }

        public int Attack()
        {
            return Weapon.Use();
        }

        public bool IsAlive => Health > 0;
    }
}
