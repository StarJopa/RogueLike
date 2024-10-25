using System;

namespace RoguelikeGame
{
    public class Player
    {
        public string Name { get; }
        public int Health { get; private set; }
        public int MaxHealth { get; }
        public Weapon Weapon { get; private set; }
        public int Score { get; private set; }
        public Aid Aid { get; private set; }

        public Player(string name, int maxHealth, Weapon weapon, Aid aid)
        {
            Name = name;
            Health = maxHealth;
            MaxHealth = maxHealth;
            Weapon = weapon;
            Score = 0;
            Aid = aid;
        }

        public void Attack(Enemy enemy)
        {
            int damage = Weapon != null && !Weapon.IsBroken ? Weapon.Use() : 1;
            Console.WriteLine($"{Name} атакует {enemy.Name} и наносит {damage} урона!");
            enemy.TakeDamage(damage);

            // Вывод информации о состоянии оружия после атаки
            Console.WriteLine(Weapon.CheckDurability());
        }

        public void Heal()
        {
            if (Aid != null)
            {
                if (Health < MaxHealth)
                {
                    Health += Aid.HealAmount;
                    if (Health > MaxHealth) Health = MaxHealth;
                    Console.WriteLine($"{Name} использует {Aid.Name} и восстанавливает {Aid.HealAmount} здоровья. Текущее здоровье: {Health}");
                    Aid = null;
                }
                else
                {
                    Console.WriteLine("Здоровье уже полное!");
                }
            }
            else
            {
                Console.WriteLine("У вас нет доступной аптечки!");
            }
        }

        public void PickupWeapon(Weapon newWeapon)
        {
            Weapon = newWeapon.Clone();
            Console.WriteLine($"{Name} подобрал новое оружие: {newWeapon.Name} с уроном {newWeapon.Damage} и прочностью {Weapon.Durability}");
        }

        public void PickupAid(Aid newAid)
        {
            Aid = newAid;
            Console.WriteLine($"{Name} получает {newAid.Name}, которая восстанавливает {newAid.HealAmount} hp.");
        }

        public void AddScore(int points)
        {
            Score += points;
            Console.WriteLine($"{Name} получает {points} очков. Текущий счёт: {Score}");
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
            Console.WriteLine($"{Name} получает {damage} урона. Текущее здоровье: {Health}");
        }

        public bool IsAlive => Health > 0;
    }
}
