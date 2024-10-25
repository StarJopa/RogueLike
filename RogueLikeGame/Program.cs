using System;
using System.Collections.Generic;

namespace RoguelikeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать, воин!\nНазови себя:");
            string playerName = Console.ReadLine();

            Random rnd = new Random();

            // Список доступного оружия
            List<Weapon> availableWeapons = new List<Weapon>
            {
                new Weapon("Фламберг", 20, 5),
                new Weapon("Экскалибур", 15, 7),
                new Weapon("Боевой топор", 25, 4),
                new Weapon("Длинный меч", 18, 6),
                new Weapon("Кинжал", 10, 10),
                new Weapon("Копье", 12, 8)
            };

            // Список доступных аптечек
            List<Aid> availableAids = new List<Aid>
            {
                new Aid("Малая аптечка", 10),
                new Aid("Средняя аптечка", 20),
                new Aid("Большая аптечка", 30)
            };

            // Генерация случайного оружия и аптечки для игрока
            Weapon playerWeapon = availableWeapons[rnd.Next(availableWeapons.Count)];
            Aid playerAid = availableAids[rnd.Next(availableAids.Count)];
            Player player = new Player(playerName, 100, playerWeapon, playerAid);

            Console.WriteLine($"\nВаше имя {playerName}!");
            Console.WriteLine($"Вам было дано оружие {playerWeapon.Name} ({playerWeapon.Damage} урона, {playerWeapon.Durability} прочности), а также {playerAid.Name} ({playerAid.HealAmount} hp).");
            Console.WriteLine($"У вас {player.Health} hp.");

            // Список доступных имен врагов
            List<string> enemyNames = new List<string> { "Варвар", "Гоблин", "Орк", "Тролль", "Скелет", "Зомби" };
            int round = 1;

            // Основной игровой цикл
            while (player.IsAlive)
            {
                Console.WriteLine($"\nРаунд {round}");

                // Генерация случайного оружия для врага
                Weapon enemyWeapon = availableWeapons[rnd.Next(availableWeapons.Count)];

                // Генерация случайного врага
                string enemyName = enemyNames[rnd.Next(enemyNames.Count)] + " " + round;
                int enemyHealth = rnd.Next(20, 60);
                Enemy enemy = new Enemy(enemyName, enemyHealth, enemyWeapon);

                Console.WriteLine($"{playerName} встречает врага {enemy.Name} ({enemy.Health}hp), у врага оружие {enemy.Weapon.Name} ({enemy.Weapon.Damage} урона)");

                while (enemy.IsAlive && player.IsAlive)
                {
                    Console.WriteLine("\nЧто вы будете делать?");
                    Console.WriteLine("1. Ударить\n2. Пропустить ход\n3. Использовать аптечку");

                    string action = Console.ReadLine();

                    if (action == "1")
                    {
                        player.Attack(enemy);
                    }
                    else if (action == "2")
                    {
                        Console.WriteLine($"{player.Name} пропускает ход.");
                    }
                    else if (action == "3")
                    {
                        player.Heal();
                    }
                    else
                    {
                        Console.WriteLine("Неверная команда, попробуйте снова.");
                        continue;
                    }

                    if (enemy.IsAlive)
                    {
                        int enemyDamage = enemy.Attack();
                        Console.WriteLine($"{enemy.Name} атакует и наносит {enemyDamage} урона!");
                        player.TakeDamage(enemyDamage);
                    }
                    else
                    {
                        Console.WriteLine($"{enemy.Name} повержен!");
                        player.AddScore(10);

                        // Подбор оружия, если текущее сломано
                        if (player.Weapon.IsBroken)
                        {
                            player.PickupWeapon(enemy.Weapon);
                        }

                        // Выпадение новой случайной аптечки после победы над врагом
                        Aid newAid = availableAids[rnd.Next(availableAids.Count)];
                        player.PickupAid(newAid);
                    }
                }

                if (!player.IsAlive)
                {
                    Console.WriteLine("\nИгра окончена. Вы погибли!");
                }
                else
                {
                    Console.WriteLine("\nВы победили врага и переходите к следующему раунду.");
                    round++;
                }
            }

            Console.WriteLine($"\nВаш итоговый счет: {player.Score} очков.");
        }
    }
}
