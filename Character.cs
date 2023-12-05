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
        }

        public void AssignStats()
        {
            if (ct == ClassType.Pilot)
            {
                Accuracy = 2; Health = 2; Speed = 1; Stealth = 5;
            }
            else if (ct == ClassType.Heavy)
            {
                Accuracy = 3; Health = 5; Speed = 1; Stealth = 1;
            }
            else if (ct == ClassType.Marksman)
            {
                Accuracy = 5; Health = 1; Speed = 1; Stealth = 3;
            }
            else
            {
                 Accuracy = 2; Health = 2; Speed = 5; Stealth = 1;
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

        public void PlayerAttack(List<Enemy> enemies)
        {
            Console.WriteLine("Choose Which enemy to attack:"); //asks user which pirate to attack
            for (int i = 0; i < enemies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Pirate {i + 1} (Health: {enemies[i].Health})");      //lists remaining pirates
            }
            int choice = GetInput(1, enemies.Count);
            int successChance = 50 + Accuracy * 5;              //takes accuracy stat and detirmines if successfull hit occurs
            if (new Random().Next(1, 101) <= successChance)
                {
                    enemies[choice - 1].Health--;
                Console.WriteLine("You successfully hit the pirate! They have {0} health left", enemies[choice - 1].health);          //success message
                }
            else { Console.WriteLine("Your attack missed"); }         //fail message

        }

        public void death()
        {
            throw new System.NotImplementedException();
        }

        public void interact()
        {
            throw new System.NotImplementedException();
        }

        public void moveRoom()
        {
            throw new System.NotImplementedException();
        }
    }
}