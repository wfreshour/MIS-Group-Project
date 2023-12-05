using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group_Project
{
    public class Room
    {
        public int roomNum; //holds the current room number
        public bool hasChest; // tells whether or not the current room has a chest
        public string roomName = string.Empty; //holds name of current room
        public bool hasLooted = false; // tells whether or not the user has already looted

        /// <summary>
        /// Generates the room
        /// </summary>
        /// <param name="rn">current room number</param>
        public Room(int rn)
        {
            roomNum = rn;

            Random rnd = new Random();
            int chestPercentage = rnd.Next(100);

            if (chestPercentage > 10) { hasChest = true; }
            else { hasChest = false; }

            if (roomNum == 1) { roomName = "Living Quarters"; }
            else if (roomNum == 2) { roomName = "Dining Hall"; }
            else if (roomNum == 3) { roomName = "Kitchen"; }
            else if (roomNum == 4) { roomName = "Cargo Hold"; }
            else if (roomNum == 5) { roomName = "Engine Room Hallway"; }
            else if (roomNum == 6) { roomName = "Engine Room 1"; }
            else if (roomNum == 7) { roomName = "Engine Room 2"; }
            else if (roomNum == 8) { roomName = "Control Room Hallway"; }
            else if (roomNum == 9) { roomName = "Control Room"; }
        }

        /// <summary>
        /// Loots the current room
        /// </summary>
        /// <param name="xp">xp amount in the chest</param>
        /// <returns>whether or not their was a chest</returns>
        public bool Loot(ref int xp)
        {
            if (hasChest == true && hasLooted == false)
            {
                Random rnd = new Random();
                xp = rnd.Next(5, 20);
                hasLooted = true;
                return true;
            }
            hasLooted = true;
            return false;
        }

        /// <summary>
        /// Allows user to check stats and spend upgrade points
        /// </summary>
        public void CheckStats(Character c)
        {
            Console.WriteLine("Your stats are:");
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("\tAccuracy: {0}", c.Accuracy);
                }
                else if (i == 1)
                {
                    Console.WriteLine("\tHealth: {0}", c.Health);
                }
                else if (i == 2)
                {
                    Console.WriteLine("\tSpeed: {0}", c.Speed);
                }
                else { Console.WriteLine("\tStealth: {0}", c.Stealth); }
            }
            if (c.upgradePoints > 0)
            {
                Console.WriteLine("You have {0} upgrade points, would you like to upgrade? (Y/N)", c.upgradePoints);
                bool input = InputConfirm();
                if (input) { c.UpgradeStats(); }
            }
            else
            {
                Console.WriteLine("You have no upgrade points available.");
            }
        }

        /// <summary>
        /// Loots the pirates that were killed
        /// </summary>
        /// <param name="pirates">List of pirates</param>
        public void LootPirates(List<Enemy> pirates, Inventory i)
        {
            foreach (var pirate in pirates)
            {
                // Add a random item from the pirate to the player's inventory
                int randomItem = 0;
                i.Add(randomItem);
            }
        }

        public void DisplayItems(Inventory inv, Character c, List<Character> characters)
        {
            Console.WriteLine("Inventory:");
            for (int i = 0; i < 5; i++)
            {
                if (i == 0) { Console.WriteLine("MedPack(s): {0}", inv.MedPack); }
                else if (i == 1) { Console.WriteLine("DMG Stim(s): {0}", inv.DMGStim); }
                else if (i == 2) { Console.WriteLine("BullsEye(s): {0}", inv.BullsEye); }
                else if (i == 3) { Console.WriteLine("Booster(s): {0}", inv.Booster); }
                else if (i == 4) { Console.WriteLine("ArmorPack(s): {0}", inv.Revive); }
            }

            Console.WriteLine("Would you like to use any itmes? (Y/N)");
            bool input = InputConfirm();

            if (input) { c.UseItem(inv, characters); }
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