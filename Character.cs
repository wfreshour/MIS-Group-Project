using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace Group_Project
{
    public class Character
    {
        public ClassType ct;
        public string name;
        public bool isUser;
        public int level;
        public int xp;
        public int upgradePoints;
        public int Accuracy;
        public int Health;
        public int Speed;
        public int Stealth;
        public bool isAlive;
        public int Damage;

        /// <summary>
        /// constructor for the character
        /// </summary>
        /// <param name="name">characters name</param>
        /// <param name="ct">characters class type</param>
        /// <param name="isUser">tells wheather or not the character is the users</param>
        public Character(string name, ClassType ct, bool isUser)
        {
            this.isUser = isUser;
            this.name = name;
            this.ct = ct;
            level = 0;
            xp = 0;
            upgradePoints = 0;
            AssignStats();
            isAlive = true;
            Damage = 1;
        }

        public void AssignStats()
        {
            if (ct == ClassType.Pilot)
            {
                Accuracy = 2; Health = 3; Speed = 2; Stealth = 5;
            }
            else if (ct == ClassType.Heavy)
            {
                Accuracy = 3; Health = 6; Speed = 2; Stealth = 1;
            }
            else if (ct == ClassType.Marksman)
            {
                Accuracy = 5; Health = 2; Speed = 2; Stealth = 3;
            }
            else
            {
                Accuracy = 3; Health = 3; Speed = 5; Stealth = 1;
            }
        }

        /// <summary>
        /// Adds xp to the character
        /// </summary>
        /// <param name="amount">amount of xp to add</param>
        public void AddXP(int amount)
        {
            xp += amount;
            if (xp > 20 && isUser == true)
            {
                level += 1;
                Console.WriteLine("LEVEL UP!! You are now level {0} and have earned an upgrade point!", level);
                upgradePoints += 1;
                xp = xp - 20;
            }
        }

        public void UpgradeStats()
        {
            Console.WriteLine("Which stat would you like to upgrade?");
            string input = Console.ReadLine();

            if (input == "Accuracy")
            {
                if (Accuracy < 10) { Accuracy += 1; upgradePoints -= 1; Console.WriteLine("Accuracy Increased!"); }
                else { Console.WriteLine("You already have 10 accuracy, please select another stat."); }
            }
            else if (input == "Health")
            {
                if (Health < 10) { Health += 1; upgradePoints -= 1; Console.WriteLine("Health Increased!"); }
                else { Console.WriteLine("You already have 10 health, please select another stat."); }
            }
            else if (input == "Speed")
            {
                if (Speed < 10) { Speed += 1; upgradePoints -= 1; Console.WriteLine("Speed Increased!"); }
                else { Console.WriteLine("You already have 10 speed, please select another stat."); }
            }
            else if (input == "Stealth")
            {
                if (Stealth < 10) { Stealth += 1; upgradePoints -= 1; Console.WriteLine("Stealth Increased!"); }
                else { Console.WriteLine("You already have 10 stealth, please select another stat."); }
            }
        }

        public bool PlayerAttack(Enemy e)
        {
            int successChance = 50 + (Accuracy * 5);              //takes accuracy stat and detirmines if successfull hit occurs
            if (new Random().Next(1, 101) <= successChance)
            {
                e.health -= Damage;
                if (e.health <= 0)
                {
                    Console.WriteLine("{0} has died!\n", e.name);
                    return true;
                }
                Console.WriteLine("{0} successfully hit the pirate! They have {1} health left\n", name, e.health);          //success message
            }
            else { Console.WriteLine("{0}'s attack missed\n", name); }         //fail message
            return false;

        }

        public void UseItem(Inventory i, List<Character> characters)
        {
            Console.WriteLine("Which Item would you like to use?");
            string input = Console.ReadLine();

            if (input == "MedPack")
            {
                if (i.MedPack > 0) { Health += 1; i.MedPack -= 1; Console.WriteLine("Health Increased!"); }
                else { Console.WriteLine("You do not have an available MedPack"); }
            }
            else if (input == "DMG Stim")
            {
                if (i.DMGStim > 0) { Damage += 1; i.DMGStim -= 1; Console.WriteLine("Damage Increased!"); }
                else { Console.WriteLine("You do not have a DMG Stim available"); }
            }
            else if (input == "BullsEye")
            {
                if (i.BullsEye > 0) { Accuracy += 1; i.BullsEye -= 1; Console.WriteLine("Accuracy Increased!"); }
                else { Console.WriteLine("You do not have a BullsEye available."); }
            }
            else if (input == "Booster")
            {
                if (i.Booster > 0) { Speed += 1; i.Booster -= 1; Console.WriteLine("Speed Increased!"); }
                else { Console.WriteLine("You do not have a Booster available."); }
            }
            else if (input == "Revive")
            {
                if (i.Revive > 0)
                {
                    bool hasRevived = false;
                    foreach (Character c in characters)
                    {
                        if (!c.isAlive && hasRevived == false)
                        {
                            c.isAlive = true;
                            hasRevived = true;
                            c.Health = 3;
                            Console.WriteLine("{0} has been revived! They now have 3 health!", c.name);
                            i.Revive -= 1;
                        }
                    }

                    if (!hasRevived)
                    {
                        Console.WriteLine("There is nobody to Revive.");
                    }
                }
                else { Console.WriteLine("You do not have a Revive available."); }
            }
        }
    }
}