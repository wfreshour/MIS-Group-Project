﻿// Will Freshour, Trevor Robinson, Nicholas Magana
// MIS 411
// This program is a RPG game where the user picks a class, is joined by a party, and fights their way through pirates,
// gains levels, and finds items throughout the game.



using System;
using System.IO;
using System.Reflection.Metadata;

namespace Group_Project
{
    public class Program
    {
        static void Main(string[] args)
        {

            bool PlayAgain = false;

            do
            {
                string input; // to hold users input
                string name; // name of the user
                int xp = 0; // amount of xp found in a chest
                int numRooms = 9; //holds number of rooms
                bool nextRoom = false; // tells whether or not user chose to enter next room
                ClassType userClass; // chosen class of the user
                List<Character> characters = new List<Character>(); // list of characters
                int topScore; // current highscore
                string topName = "";  // name of current high score holder
                List<Enemy> enemies = new List<Enemy>();
                List<int> loot = new List<int>(); // holds list of loot

                //get high score
                topScore = GetHighScores(ref topName);


                // greet user and present story
                Console.WriteLine("Welcome to Recapture!");
                Console.WriteLine("\nYou and your crew are traveling through space in your cargo freight ship when suddenly a large group of pirates\n" +
                    " trap you in a Grav Lock! They quickly board and overwhelm you and your crew and have locked you in your quarters.\n Luckily, the pirates do not" +
                    " know about the secret weapons locker you keep hidden away in the room. Arm yourselves\n and recapture your ship by clearing all the pirates!");

                Console.WriteLine("\nHIGH SCORE: {0} {1}", topName, topScore);

                // get users name
                Console.WriteLine("\nPlease enter your name: ");
                name = Console.ReadLine();

                // get users class
                Console.WriteLine("Below are the options for your classes:");
                DisplayClass();
                Console.WriteLine("Please enter the number for your choice of class (The classes you dont choose will be assigned to your crew)");
                userClass = (ClassType)ValidInput(Console.ReadLine(), 1, Enum.GetValues(typeof(ClassType)).Cast<ClassType>().Distinct().Count());
                Console.WriteLine($"You chose {userClass} as your class.");

                // create the users Character object
                characters.Add(new Character(name, userClass, true));
                Inventory inventory = new Inventory();

                // create remaining crew
                FillCrew(characters, userClass);

                //Prompt the user to continue
                Console.WriteLine("You are now ready to begin the journey, press any key to continue...");
                AnyKey();
                Console.Clear();

                //Load enemies from external file
                GenerateEnemies(enemies);

                // loops through each room
                for (int i = 0; i < numRooms; i++)
                {
                    nextRoom = false;
                    //Generate Room
                    Room r = new Room(i + 1);
                    
                    if (characters[0].isAlive) // if user is alive
                    {
                        loot = Combat(enemies, characters);                      //starts combat method
                        bool hasLooted = false;

                        do
                        {
                            if (characters[0].isAlive) // if user is alive
                            {
                                Console.Clear();
                                //Ask user what they would like to do
                                Console.WriteLine("You are now in the {0}. \nWhat would you like to do? (Loot Room, Check Stats, Enter next room, Check Inventory, Loot Pirates)", r.roomName);


                                bool isValid = true;

                                do
                                {
                                    input = Console.ReadLine();
                                    if (input == "Loot Room")
                                    {
                                        bool hasChest = r.Loot(ref xp);

                                        if (hasChest)
                                        {
                                            Console.WriteLine("\nA chest was found! It contained {0} xp", xp);
                                            foreach (Character c in characters)
                                            {
                                                c.AddXP(xp);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nNo loot was found.");

                                        }
                                        Console.WriteLine("Press Any Key to continue...");
                                        AnyKey();
                                    }
                                    else if (input == "Check Stats")
                                    {
                                        r.CheckStats(characters[0]);
                                        Console.WriteLine("Press Any Key to continue...");
                                        AnyKey();
                                    }
                                    else if (input == "Enter next room")
                                    {
                                        nextRoom = true;
                                        Console.Clear();
                                    }
                                    else if (input == "Check Inventory")
                                    {
                                        r.DisplayItems(inventory, characters[0], characters);
                                        Console.WriteLine("Press Any Key to continue...");
                                        AnyKey();
                                    }
                                    else if (input == "Loot Pirates")
                                    {
                                        LootPirates(loot, inventory, ref hasLooted);
                                    }
                                    else
                                    {
                                        isValid = false;
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Please enter one of the available options above.");
                                        Console.ResetColor();
                                    }

                                } while (!isValid);
                            }
                            else
                            {
                                nextRoom = true;
                            }
                        } while (!nextRoom); // stays in loop if they did not choose to exit the room

                    }
                }

                if (characters[0].isAlive) // congrats to user if they survived
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations! You cleared the ship of all pirates!");
                    Console.ResetColor();
                    Thread.Sleep(4000);
                }
                else // or tell them the game is over
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("GAME OVER");
                    Console.ResetColor();
                    Thread.Sleep(4000);
                }

                //update highscore if beaten
                if (characters[0].level > topScore)
                {
                    NewHighScore(name, characters[0].level);
                }

                // prompt user to play again
                Console.WriteLine("\nWould you like to play again?");
                PlayAgain = InputConfirm();
                Console.Clear();
            } while (PlayAgain); // stay in loop if they play again

            Console.WriteLine("Thank you for playing!");
        }

        /// <summary>
        /// Loots the Pirates that were killed
        /// </summary>
        /// <param name="loot">list holding their loot</param>
        /// <param name="inv">players inventory</param>
        /// <param name="hasLooted">tells whether or not the user already looted</param>
        static void LootPirates(List<int> loot, Inventory inv, ref bool hasLooted)
        {
            if (!hasLooted)
            {
                foreach (int i in loot)
                {
                    Thread.Sleep(1000);
                    if (i == 0)
                    {
                        Console.WriteLine("You found a medpack!");
                        inv.MedPack++;
                    }
                    else if (i == 1)
                    {
                        Console.WriteLine("You found a DMG Stim!");
                        inv.DMGStim++;
                    }
                    else if (i == 2)
                    {
                        Console.WriteLine("You found a BullsEye!");
                        inv.BullsEye++;
                    }
                    else if (i == 3)
                    {
                        Console.WriteLine("You found a Booster!");
                        inv.Booster++;
                    }
                    else if (i == 4)
                    {
                        Console.WriteLine("You found a ArmorPack!");
                        inv.Revive++;
                    }
                }
            }
            else
            {
                Console.WriteLine("You have already looted!");
            }
            hasLooted = true;
        }

        /// <summary>
        /// Displays the options for the users class
        /// </summary>
        static void DisplayClass()
        {
            Console.WriteLine("\t\tHEALTH\tACCURACY   SPEED   STEALTH");
            for (int i = 0; i < Enum.GetValues(typeof(ClassType)).Cast<ClassType>().Distinct().Count(); i++)
            {
                ClassType ct = (ClassType)i;

                if (ct == ClassType.Pilot)
                {
                    Console.WriteLine("{0}. Pilot:\t2\t2\t   1\t   5", i + 1);
                }
                else if (ct == ClassType.Engineer)
                {
                    Console.WriteLine("{0}. Engineer:\t2\t2\t   5\t   1", i + 1);
                }
                else if (ct == ClassType.Heavy)
                {
                    Console.WriteLine("{0}. Heavy:\t5\t3\t   1\t   1", i + 1);
                }
                else
                {
                    Console.WriteLine("{0}. Marksman:\t1\t5\t   1\t   3", i + 1);
                }
            }
        }

        /// <summary>
        /// Takes a given input and trys to convert it to an integer. Keeps getting users input until a valid one is received
        /// </summary>
        /// <param name="input">input to test</param>
        /// <param name="lowerBound">gives lower bound to test against</param>
        /// <param name="upperBound">gives lower bound to test against</param>
        /// <returns>the valid input</returns>
        static int ValidInput(string input, int lowerBound, int upperBound)
        {
            bool isValid = false;
            while (!isValid)
            {
                try
                {
                    int i = int.Parse(input);
                    isValid = true;
                    if (i < lowerBound || i > upperBound)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter a number between {0} and {1}", lowerBound, upperBound);
                        Console.ResetColor();
                        input = Console.ReadLine();
                        isValid = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please type a numerical value, like 1, 2, 3, etc.");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    isValid = false;
                }
            }
            return int.Parse(input) - 1;
        }

        /// <summary>
        /// Fills the characters list with the rest of the crew
        /// </summary>
        /// <param name="characters">list of characters</param>
        /// <param name="userCT">the users class type</param>
        static void FillCrew(List<Character> characters, ClassType userCT)
        {
            for (int i = 0; i < Enum.GetValues(typeof(ClassType)).Cast<ClassType>().Distinct().Count(); i++)
            {
                ClassType ct = (ClassType)i;

                if (ct != userCT)
                {
                    if (ct == ClassType.Pilot)
                    {
                        characters.Add(new Character("Pilot", ClassType.Pilot, false));
                    }
                    else if (ct == ClassType.Engineer)
                    {
                        characters.Add(new Character("Engineer", ClassType.Engineer, false));
                    }
                    else if (ct == ClassType.Heavy)
                    {
                        characters.Add(new Character("Heavy", ClassType.Heavy, false));
                    }
                    else
                    {
                        characters.Add(new Character("Marksman", ClassType.Marksman, false));
                    }
                }
            }
        }

        /// <summary>
        /// Enters the user into combat
        /// </summary>
        /// <param name="enemies">List of possible enemies</param>
        /// <param name="characters">List of characters</param>
        /// <returns>List of the pirates loot</returns>
        static List<int> Combat(List<Enemy> enemies, List<Character> characters)           //starts combat
        {
            int numEnemies = new Random().Next(2, 5);    //decided how many enemies to spawn
            List<Enemy> currentEnemies = new List<Enemy>();
            List<int> loot = new List<int>();

            for (int i = 0; i < numEnemies; i++)          //creates enemy based on how many need to spawn
            {
                int enemyID = new Random().Next(0, 3);
                currentEnemies.Add(new Enemy(enemies[enemyID].name, enemies[enemyID].health, enemies[enemyID].speed, enemies[enemyID].stealth, enemies[enemyID].accuracy));
            }
            Console.WriteLine("You are faced against {0} pirates!\n", numEnemies);        //prints how many pirates user has to fight 

            Thread.Sleep(2500);

            while (currentEnemies.Count > 0 && characters[0].Health > 0)          //while there are pirates still alive and the player still has health
            {
                foreach (Character character in characters)
                {
                    if (character.isUser == true && currentEnemies.Count > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Choose Which enemy to attack:"); //asks user which pirate to attack
                        for (int i = 0; i < currentEnemies.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {currentEnemies[i].name} (Health: {currentEnemies[i].health})");      //lists remaining pirates
                        }
                        int choice = ValidInput(Console.ReadLine(), 0, currentEnemies.Count) + 1;
                        Thread.Sleep(1000);
                        Console.Clear();
                        if (currentEnemies[choice - 1].speed <= character.Speed)         //if the user is faster than a pirate they attack
                        {
                            bool death = character.PlayerAttack(currentEnemies[choice - 1]);
                            Thread.Sleep(1000);
                            if (death)
                            {
                                character.AddXP(15);
                                Thread.Sleep(1000);
                                loot.Add(currentEnemies[choice - 1].loot);
                                currentEnemies.RemoveAt(choice - 1);
                            }
                            else
                            {
                                currentEnemies[choice - 1].EnemyAttack(character);
                            }
                        }
                        else
                        {
                            currentEnemies[choice - 1].EnemyAttack(character);
                            Thread.Sleep(1000);
                            bool death = character.PlayerAttack(currentEnemies[choice - 1]);
                            if (death)
                            {
                                character.AddXP(15);
                                Thread.Sleep(1000);
                                loot.Add(currentEnemies[choice - 1].loot);
                                currentEnemies.RemoveAt(choice - 1);
                            }
                        }
                        if (characters[0].Health <= 0)                    // if player health is 0, death and exit
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("You were Killed");
                            Thread.Sleep(3000);
                            return loot;
                        }
                    }
                    else if (character.isAlive && currentEnemies.Count > 0)
                    {
                        int choice = new Random().Next(1, currentEnemies.Count());
                        if (currentEnemies[choice - 1].speed <= character.Speed)         //if the user is faster than a pirate they attack
                        {
                            bool death = character.PlayerAttack(currentEnemies[choice - 1]);
                            Thread.Sleep(1000);
                            if (death)
                            {
                                loot.Add(currentEnemies[choice - 1].loot);
                                currentEnemies.RemoveAt(choice - 1);
                            }
                            else
                            {
                                currentEnemies[choice - 1].EnemyAttack(character);
                            }
                        }
                        else
                        {
                            currentEnemies[choice - 1].EnemyAttack(character);
                            if (character.isAlive)
                            {
                                Thread.Sleep(1000);
                                bool death = character.PlayerAttack(currentEnemies[choice - 1]);
                                if (death)
                                {
                                    loot.Add(currentEnemies[choice - 1].loot);
                                    currentEnemies.RemoveAt(choice - 1);
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("All pirates defeated. The room is now safe. Press Any Key to contniue...");            //all pirates defeated message
            AnyKey();
            return loot;
        }

        /// <summary>
        /// waits for the user to press a key before returning to program
        /// </summary>
        static void AnyKey()
        {
            Console.ReadKey(true);
        }

        /// <summary>
        /// Gets the high score
        /// </summary>
        /// <param name="name">name of the person who got the high score</param>
        /// <returns>the high score</returns>
        static int GetHighScores(ref string name)
        {
            string path = "HighScores.txt";
            int score = 0;

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    name = sr.ReadLine();
                    score = int.Parse(sr.ReadLine());
                }
                sr.Close();
            }

            return score;
        }

        /// <summary>
        /// Changes the high score if it was passed
        /// </summary>
        /// <param name="name">name of user</param>
        /// <param name="score">the new high score</param>
        static void NewHighScore(string name, int score)
        {
            Console.WriteLine("NEW HIGH SCORE!!!");

            string path = "HighScores.txt";

            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine(name);
                streamWriter.WriteLine(score);
                streamWriter.Close();
            }
        }

        /// <summary>
        /// generates enemies from an external file
        /// </summary>
        /// <param name="enemies">list of enemies</param>
        static void GenerateEnemies(List<Enemy> enemies)
        {
            string path = "Pirates.txt";

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string n = sr.ReadLine();
                    int a = int.Parse(sr.ReadLine());
                    int h = int.Parse(sr.ReadLine());
                    int sp = int.Parse(sr.ReadLine());
                    int st = int.Parse(sr.ReadLine());
                    Enemy enemy = new Enemy(n, h, sp, st, a);
                    enemies.Add(enemy);
                }
                sr.Close();
            }
        }

        /// <summary>
        /// gets either a Y or N response from the user
        /// </summary>
        /// <returns>true if Y, false if N</returns>
        static bool InputConfirm()
        {
            bool isValid = false;
            bool response = false;

            while (!isValid)
            {
                ConsoleKey opt = Console.ReadKey().Key;
                Console.WriteLine();
                switch (opt)
                {
                    case ConsoleKey.Y:
                        isValid = true;
                        response = true;
                        break;
                    case ConsoleKey.N:
                        isValid = true;
                        response = false;
                        break;
                    default:
                        isValid = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please either press Y or N");
                        Console.ResetColor();
                        break;
                }
            }
            return response;
        }
    }
}
