using System;
using System.Collections.Generic;
using System.Threading;

class Samurai
{
    public string Name { get; set; }
    public string SamuraiClass { get; set; }
    public int Level { get; set; } = 1;
    public int Experience { get; set; } = 0;
    public int Health { get; set; } = 100;
    public int Energy { get; set; } = 100;
    public int Coins { get; set; } = 50;
    public string Weapon { get; set; } = "Wooden Sword";
    public string Armor { get; set; } = "Cloth Armor";
    public List<string> Inventory { get; set; } = new List<string>();
    
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Defense { get; set; }

    public Samurai(string name, string samuraiClass)
    {
        Name = name;
        SamuraiClass = samuraiClass;
        AssignClassAttributes();
    }

    private void AssignClassAttributes()
    {
        switch (SamuraiClass)
        {
            case "Ronin":
                Strength = 7;
                Agility = 5;
                Defense = 3;
                break;
            case "Bushido":
                Strength = 5;
                Agility = 7;
                Defense = 4;
                break;
            case "Shogun":
                Strength = 6;
                Agility = 4;
                Defense = 6;
                break;
            default:
                Strength = 5;
                Agility = 5;
                Defense = 5;
                break;
        }
    }

    public void Train()
    {
        if (Energy >= 10)
        {
            Energy -= 10;
            Strength += new Random().Next(1, 3);
            Agility += new Random().Next(1, 3);
            Defense += new Random().Next(1, 2);
            Console.WriteLine($"{Name} has trained! Strength: {Strength}, Agility: {Agility}, Defense: {Defense}");
        }
        else
        {
            Console.WriteLine("Not enough energy to train!");
        }
    }

    public void Rest()
    {
        Console.WriteLine($"{Name} is resting...");
        Thread.Sleep(2000);
        Energy = Math.Min(100, Energy + 30);
        Health = Math.Min(100, Health + 20);
        Console.WriteLine($"Energy restored to {Energy}, Health restored to {Health}");
    }

    public void Fight(string enemy, int enemyStrength, int enemyDefense)
    {
        if (Energy < 20)
        {
            Console.WriteLine("Not enough energy to fight!");
            return;
        }

        Energy -= 20;
        Console.WriteLine($"{Name} engages in battle with {enemy}!");
        int playerAttack = Strength + new Random().Next(0, 5) - enemyDefense;
        int enemyAttack = enemyStrength + new Random().Next(0, 5) - Defense;

        if (playerAttack > enemyAttack)
        {
            Console.WriteLine($"{Name} wins the fight!");
            Experience += 30;
            Coins += 20;
            LevelUp();
        }
        else
        {
            Console.WriteLine($"{Name} loses and takes damage!");
            Health -= 15;
        }
    }

    private void LevelUp()
    {
        if (Experience >= 100)
        {
            Level++;
            Experience = 0;
            Strength += 3;
            Agility += 3;
            Defense += 2;
            Health = 100;
            Energy = 100;
            Console.WriteLine($"{Name} has leveled up to Level {Level}!");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Samurai RPG!");
        Console.Write("Enter your samurai's name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Choose your class: Ronin, Bushido, Shogun");
        string samuraiClass = Console.ReadLine();

        Samurai player = new Samurai(name, samuraiClass);

        Dictionary<string, (int Strength, int Defense)> enemies = new Dictionary<string, (int, int)>
        {
            { "Bandit", (5, 2) },
            { "Ronin", (7, 3) },
            { "Warlord", (10, 5) }
        };

        while (true)
        {
            Console.WriteLine("\nActions: Train, Rest, Fight, Exit");
            Console.Write("What would you like to do? ");
            string action = Console.ReadLine().ToLower();

            switch (action)
            {
                case "train":
                    player.Train();
                    break;
                case "rest":
                    player.Rest();
                    break;
                case "fight":
                    var enemy = new List<string>(enemies.Keys)[new Random().Next(enemies.Count)];
                    player.Fight(enemy, enemies[enemy].Strength, enemies[enemy].Defense);
                    break;
                case "exit":
                    Console.WriteLine("Goodbye, warrior!");
                    return;
                default:
                    Console.WriteLine("Invalid action!");
                    break;
            }
        }
    }
}
