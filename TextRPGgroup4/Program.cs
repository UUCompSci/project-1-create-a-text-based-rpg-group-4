// Project 1: Create a Text-Based RPG
// by Jonathan Hudlow and William Venable



////////////////
// GAME SETUP //
////////////////

using System;
using static System.Console;
using System.Collections.Generic;
using System.ComponentModel;

class Program
{
    // These are the game state variables
    static int hitpoints = 120; // player's health
    static int maxHealth = 120; // maximum value player's health can be
    static int gold = 50; // player's gold
    static int attackPower = 5; // attack power of player's weapon
    static int defensePower = 5; // defense power of player's armor
    static List<string> inventory = new List<string>(); // players items

    static void Main(string[] args)
    {
        // These are the command line arguments to set initial state of the game
        try
        {
            if (args.Length > 0) gold = int.Parse(args[0]); // this is the first argument which is the starting gold
            if (args.Length > 1) inventory.Add(args[1]); // this is the second argument which is the starting item

            // If args[1] is an item from this program that's supposed to raise your attack/defense power, handle that
            switch (args[1])
            {
                case "Dark Candy": // do nothing with dark candy
                    break;

                case "Spookysword": // raises attack power by 10
                    attackPower += 10;
                    break;

                case "Twisted Sword": // raises attack power by 15
                    attackPower += 15;
                    break;

                case "Silver Card": // raises defense power by 5
                    defensePower += 5;
                    break;

                case "White Ribbon": // raises defense power by 10
                    defensePower += 10;
                    break;
            }
        }
        catch
        {
            WriteLine("Invalid command line arguments. Using defaults.");
        }

        // Setup starting item message based on if you started with one from the terminal
        string startingItem = "no items";
        if (args.Length > 1)
        {
            startingItem = "a" + inventory[0];
        }

        // Start of the game
        WriteLine("Welcome to the Dark World!");
        WriteLine("You've entered with {0} HP, {1} gold, and {2}.",
            hitpoints, gold, startingItem);



        ///////////////
        // MAIN LOOP //
        ///////////////

        bool playing = true; // this is the main game loop
        while (playing)

        { // These are the different scenes the player can go to. // the main menu options
            WriteLine("\nWhat would you like to do?");
            WriteLine("1. Talk to your friends.");
            WriteLine("2. Visit the Dojo."); 
            WriteLine("3. Visit the Shop.");
            WriteLine("4. Visit the Castle");
            WriteLine("5. Investigate outside of town.");
            WriteLine("6. View Status");
            WriteLine("7. Leave the Dark World.");

            try
            {
                switch (GetChoice())
                {
                    case 1: TalkToFriends(); break;
                    case 2: VisitDojo(); break;
                    case 3: VisitShop(); break;
                    case 4: Castle(); break;
                    case 5: Explore(); break;
                    case 6: ViewStatus(); break;
                    case 7: playing = false; break;
                    default: WriteLine("Invalid choice."); break;
                }
            }
            catch
            {
                WriteLine("Please enter a valid choice.");
            }

            if (hitpoints <= 0) // this checks if the player is dead
            {
                WriteLine("Your heart shatters...GAME OVER.");
                playing = false;
            }
        }
    }

    ///////////////////////////
    // CONVENIENCE FUNCTIONS //
    ///////////////////////////

    static int GetChoice() // get's player-input
    {
        return int.Parse(ReadLine());
    }

    static int SomebodyAttacks(int damageMin, int damageMax, string personOne, string personTwo) // easily handles an attack
    {
        // The numbers behind the attack
        Random rnd = new Random();
        int damage = rnd.Next(damageMin, damageMax);

        // The text behind the attack
        WriteLine($"{personOne} did {damage} damage to {personTwo}!");

        // Return the damage done
        return damage;
    }

    static void BuyItem(string item, int price)
    {
        if (gold >= price)
        {
            gold -= price;
            inventory.Add(item);
            WriteLine("SEAM: Thank you, traveller!");

            // handle special items
            switch (item)
            {
                case "Spookysword": // spookysword raises attack power by 10
                    attackPower += 10;
                    break;

                case "Silver Card": // silver card raises defense power by 5
                    defensePower += 5;
                    break;
            }
        }
        else
        {
            WriteLine("SEAM: Sorry, traveller, you don't possess enough gold for that");
        }
    }


    /////////////////////
    // SCENE FUNCTIONS //
    /////////////////////

    static void TalkToFriends() // Talking to your friends
    {
        bool isTalking = true;
        while (isTalking)
        {
            WriteLine("\nWho would you like to talk to?");
            WriteLine("1. Susie.");
            WriteLine("2. Ralsei.");
            WriteLine("3. Lancer.");
            WriteLine("4. Rouxls Kaard.");
            WriteLine("5. Return to Town Center.");

            try
            {
                switch (GetChoice())
                {
                    case 1: // talk to Susie
                        WriteLine("SUSIE: Heck yeah! Great to be back in the Dark World, ey, Kris?");
                        WriteLine("SUSIE: Though, maybe we should be working on that group project...");
                        WriteLine("SUSIE: Or, rather, YOU'LL work on the project, and I'll watch! HA!");
                        break;

                    case 2: // talk to Ralsei
                        WriteLine("RALSEI: Kris! Susie! I'm so glad you guys are here!");
                        WriteLine("RALSEI: There don't seem to be any looming Dark Fountains right now, but I'll be ready to help you guys if there is!");
                        WriteLine("RALSEI: In the mean time, I could bake you guys a cake! Go to the Castle, and I'll give it to you!");
                        break;

                    case 3: // talk to Lancer
                        WriteLine("LANCER: Susie! Blue person who I know the name of! Welcome back to my kingdom!");
                        WriteLine("RALSEI: Uh, Lancer, don't forget that I'M the Prince of this world!");
                        WriteLine("LANCER: Quiet, Toothpaste Boy!");
                        WriteLine();
                        WriteLine("Susie and Lancer proceed to laugh.");
                        WriteLine();
                        WriteLine("RALSEI: -_-");
                        break;

                    case 4: // "talk" to Rouxls Kaard
                        WriteLine("ROUXLS KAARD: Greetings, lowly worms! Have thoust come to take another cracketh at mineth puzzles?");
                        WriteLine("...");
                        WriteLine("You decided not to talk to Rouxls Kaard anymore.");
                        break;

                    default: // return to scene selection
                        isTalking = false;
                        break;
                }
            }
            catch
            {
                WriteLine("Please enter a valid choice.");
            }
        }
    }

    static void VisitDojo() // Challenge Clover at the Dojo!
    {
        WriteLine("\nYou enter the dojo...and Clover, the three headed, horned beast challenges you!");
        WriteLine();
        WriteLine("CLOVER:");
        WriteLine("Hiya friends! Let's have a friendly match~");
        WriteLine("We're going to PUMMEL YOU!");
        WriteLine("Uh, good luck have fun?");

        int cloverHealth = 150; // initialize Clover's health

        bool areFighting = true;
        while (areFighting)
        {
            WriteLine();
            WriteLine("1. Fight");
            WriteLine("2. Act");
            WriteLine("3. Item");
            WriteLine("4. Flee");

            try // MAIN FIGHT LOGIC
            {
                switch (GetChoice())
                {
                    case 1: // You fight!
                        cloverHealth -= SomebodyAttacks(25 + attackPower, 65 + attackPower, "KRIS", "CLOVER");

                        if (cloverHealth <= 0) // you won, but by killing them
                        {
                            WriteLine("CLOVER turns to dust...");
                            WriteLine("You won! Got 30 Gold and 1 Dark Candy!");

                            gold += 30;
                            inventory.Add("Dark Candy");

                            areFighting = false;
                            break;
                        }
                        else // the fight goes on
                        {
                            break;
                        }

                    case 2: // You act
                        Random randomAction = new Random();
                        int action = randomAction.Next(1, 4);

                        switch (action)
                        {
                            case 1:
                                WriteLine("You talk about CLOVER's birthday. It seems they would rather talk about something else.");
                                break;

                            case 2:
                                WriteLine("You talk about sports with CLOVER. They seem to kind of like that, but not enough to stop fighting.");
                                break;

                            case 3:
                                WriteLine("You talk about cute boys with CLOVER. They seem to love that!");
                                WriteLine("CLOVER doesn't want to fight anymore!");
                                WriteLine("You won! Got 130 Gold and 3 Dark Candy!");

                                gold += 130;
                                inventory.Add("Dark Candy");
                                inventory.Add("Dark Candy");
                                inventory.Add("Dark Candy");

                                areFighting = false;
                                break;

                            default:
                                WriteLine("You talk about the default option in switch statements. Wait, what?");
                                break;
                        }
                        break;

                    case 3: // use an item
                        if (inventory.Contains("Dark Candy"))
                        {
                            WriteLine("You used Dark Candy and healed 50 HP!");
                            hitpoints += 50;
                            inventory.Remove("Dark Candy");
                            break;
                        }
                        else
                        {
                            WriteLine("You have no proper usuable items!");
                            break;
                        }

                    default: // fled the Dojo
                        WriteLine("You fled the Dojo!");
                        areFighting = false;
                        break;
                }

                // unless you won or fled, Clover will attack you!
                // (Unlike attackPower increasing both min/max values for an attack, defensePower doesn't affect the min value so CLOVER at least does 10 HP)
                if (areFighting)
                {
                    WriteLine();
                    hitpoints -= SomebodyAttacks(20, 55 - defensePower, "CLOVER", "YOU");
                }

                if (hitpoints <= 0) // stop fight if you die
                {
                    areFighting = false;
                }
            }
            catch
            {
                WriteLine("Please enter a valid choice.");
            }
        }
    }

    static void VisitShop() // Visiting the shop
    {
        WriteLine("SEAM: Welcome, travellers, to my humble shop. I am SEAM, prounounced SHAWM.");
        WriteLine("\nWhat would you like to buy?");

        bool browsingShop = true;
        while (browsingShop)
        {
            WriteLine();
            WriteLine("1. Dark Candy - 25 Gold.");
            WriteLine("2. Spookysword - Raises attack power by 10 - 100 Gold.");
            WriteLine("3. Silver Card - Raises defense power by 5 - 100 Gold.");
            WriteLine("4. Leave Shop - 0 Gold.");

            try
            {
                switch (GetChoice())
                {
                    case 1: // buy dark candy
                        BuyItem("Dark Candy", 25);
                        break;

                    case 2: // buy sword
                        BuyItem("Spookysword", 100);
                        break;

                    case 3: // buy armor
                        BuyItem("Silver Card", 100);
                        break;

                    default: // leave
                        browsingShop = false;
                        break;
                }
            }
            catch
            {
                WriteLine("Please enter a valid choice.");
            }
        }
    }

    static void Castle() // Castle scene
    {
        WriteLine("\nYou enter the Castle...");
        WriteLine("RALSEI: Welcome! You made it just in time - I baked a cake for you!");
        WriteLine("RALSEI: It's chocolate, your favorite!");
        WriteLine();
        WriteLine("You eat the Chocolate Cake...Your health is restored!");

        hitpoints = maxHealth;
    }

    static void Explore() // Exploring outside of town
    {
        WriteLine("\nYou explore the outside of town; the dusty cliffsides and around the grand door...");

        // get a random item
        Random randomItem = new Random();

        string[] possibleItems = new string[] { "Dark Candy", "Dark Candy", "Dark Candy", "Silver Card", "Silver Card", "Ball of Junk", "Twisted Sword", "White Ribbon", "White Ribbon" };
        string found = possibleItems[randomItem.Next(0, possibleItems.Length)]; // the random item
        inventory.Add(found); // add to inventory

        Write($"You found a {found}!"); // initial collection message

        switch (found) // determine effects of special items
        {
            case "Silver Card":
                WriteLine(" Defense increased by 5!");
                defensePower += 5;
                break;

            case "Twisted Sword":
                WriteLine(" Attack increased by 15!");
                attackPower += 15;
                break;

            case "White Ribbon":
                WriteLine(" Defense increased by 10!");
                defensePower += 10;
                break;

            default: // no special item
                WriteLine();
                break;
        }
    }

    static void ViewStatus() // This views the player's current status
    {
        WriteLine("\n--- Status ---");
        WriteLine($"Hitpoints: {hitpoints}");
        WriteLine($"Attack Power: {attackPower}");
        WriteLine($"Defense: {defensePower}");
        WriteLine($"Gold: {gold}");

        if (inventory.Count == 0)
        {
            WriteLine("Inventory: (empty)");
        }
        else
        {
            WriteLine("Inventory:");
            foreach (string item in inventory)
            {
                WriteLine("- " + item);
            }
        }
    }
}