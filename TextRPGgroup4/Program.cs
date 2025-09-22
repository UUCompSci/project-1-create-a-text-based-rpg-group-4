using System;
using System.Collections.Generic;


// Will commented. 
class Program
{
    // These are the game state variables 
    static int hitpoints = 100; // player's health
    static int gold = 50; // player's gold
    static List<string> inventory = new List<string>(); // players items

    static void Main(string[] args)
    {
        // These are the command line arguments to set initial state of the game
        try
        {
            if (args.Length > 0) gold = int.Parse(args[0]); // this is the first argument which is the starting gold
            if (args.Length > 1) inventory.Add(args[1]); // this is the second argument which is the starting item
        }
        catch
        {
            Console.WriteLine("Invalid command line arguments. Using defaults.");
        }

        Console.WriteLine("Start Game!!!");
        Console.WriteLine("Starting with {0} HP, {1} gold, and items: {2}",
            hitpoints, gold, string.Join(", ", inventory));

        bool playing = true;// this is the main game loop
        while (playing)
        { // These are the different scenes the player can go to. // the main menu options
            Console.WriteLine("\nWhere would you like to go?");
            Console.WriteLine("1. Town");
            Console.WriteLine("2. Cave");
            Console.WriteLine("3. Road");
            Console.WriteLine("4. Castle");
            Console.WriteLine("5. Dungeon");
            Console.WriteLine("6. View Status");
            Console.WriteLine("7. Exit");

            try
            {
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1: Town(); break;
                    case 2: Cave(); break;
                    case 3: Road(); break;
                    case 4: Castle(); break;
                    case 5: Dungeon(); break;
                    case 6: ViewStatus(); break;
                    case 7: playing = false; break;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
            catch
            {
                Console.WriteLine("Please enter a valid number.");
            }

            if (hitpoints <= 0) // this checks if the player is dead
            {
                Console.WriteLine("You Died.");
                playing = false;
            }
        }
    }
    // this is the town scene
    static void Town()
    {
        Console.WriteLine("\nYou are in the town. The market is busy.");
        Console.WriteLine("1. Buy potion (-10 gold, +20 HP)");
        Console.WriteLine("2. Rest at inn (-5 gold, +10 HP)");
        Console.WriteLine("3. Return to main menu");

        try
        {
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    if (gold >= 10)
                    {
                        gold -= 10;
                        hitpoints += 20;
                        inventory.Add("Potion");
                        Console.WriteLine("You bought a potion.");
                    }
                    else Console.WriteLine("Get more gold.");
                    break;
                case 2:
                    if (gold >= 5)
                    {
                        gold -= 5;
                        hitpoints += 10;
                        Console.WriteLine("You rested at the inn.");
                    }
                    else Console.WriteLine("Get more gold.");
                    break;
                case 3: return; // this goes back to the main menu
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }
    // this is the cave scene
    static void Cave()
    {
        Console.WriteLine("\nYou venture into the dark cave.");
        Console.WriteLine("You encounter a monster!");

        Random rnd = new Random();
        int damage = rnd.Next(5, 20);
        hitpoints -= damage;
        Console.WriteLine("The monster attacks you for {0} damage!", damage);

        Console.WriteLine("1. Fight back");
        Console.WriteLine("2. Flee");

        try
        {
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                int loot = rnd.Next(5, 15); // the amount of gold from defeating the monster
                gold += loot;
                Console.WriteLine("You defeat the monster and gain {0} gold!", loot);
            }
            else Console.WriteLine("You flee.");
        }
        catch
        {
            Console.WriteLine("You hesitated and fled.");
        }
    }
    // this is the road scene 
    static void Road()
    {
        Console.WriteLine("\nYou walk along the road and find a chest.");
        Console.WriteLine("1. Open the chest");
        Console.WriteLine("2. Ignore and walk away");

        try
        {
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                string[] treasures = { "Sword", "Shield", "Gem", "Map", "Ring", "Amulet", "Helmet", "Boots" }; // these are the possible treasures
                Random rnd = new Random();
                string found = treasures[rnd.Next(treasures.Length)];
                inventory.Add(found);
                Console.WriteLine("You found a {0}!", found);
            }
            else Console.WriteLine("You continue on your journey.");
        }
        catch
        {
            Console.WriteLine("Invalid choice, you walk away.");
        }
    }
    // castle scene
    static void Castle()
    {
        Console.WriteLine("\nYou arrive at the castle gates.");
        Console.WriteLine("1. Speak with the guard");
        Console.WriteLine("2. Try to sneak inside");
        Console.WriteLine("3. Return to main menu");

        try
        {
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("The guard tells you rumors of treasure in the dungeon.");
                    break;
                case 2:
                    Random rnd = new Random();
                    if (rnd.Next(0, 2) == 0)
                    {
                        Console.WriteLine("You sneak inside and find 20 gold!");
                        gold += 20;
                    }
                    else
                    {
                        Console.WriteLine("You are caught and lose 10 HP.");
                        hitpoints -= 10;
                    }
                    break;
                case 3: return;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }
    // this is the dungeon scene
    static void Dungeon()
    {
        Console.WriteLine("\nYou enter the dark Dungeon.");
        Console.WriteLine("1. Explore deeper");
        Console.WriteLine("2. Search for valuables");
        Console.WriteLine("3. Return to main menu");

        try
        {
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("You fight a skeleton and lose 15 HP.");
                    hitpoints -= 15;
                    break;
                case 2:
                    Random rnd = new Random();
                    string[] junk = { "Old Tablet", "Broken Dagger", "Old Boot", "Rusty Key", "Torn Cloth", "Faded Map", "Steel Greatsword" };
                    string found = junk[rnd.Next(junk.Length)];
                    inventory.Add(found);
                    Console.WriteLine("You find a {0}!", found);
                    break;
                case 3: return;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    static void ViewStatus() // this views the player's current status
    {
        Console.WriteLine("\n--- Status ---");
        Console.WriteLine("Hitpoints: {0}", hitpoints);
        Console.WriteLine("Gold: {0}", gold);

        if (inventory.Count == 0) Console.WriteLine("Inventory: (empty)");
        else
        {
            Console.WriteLine("Inventory:");
            foreach (string item in inventory)
            {
                Console.WriteLine("- " + item);
            }
        }
    }
}