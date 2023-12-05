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
            int successChance = 50 +( Accuracy * 5);              //takes accuracy stat and detirmines if successfull hit occurs
            if (new Random().Next(1, 101) <= successChance)
            {
                e.health-=1;
                if (e.health == 0)
                {
                    Console.WriteLine("{0} has died!", e.name);
                    return true;
                }
                Console.WriteLine("{0} successfully hit the pirate! They have {1} health left", name, e.health);          //success message
            }
            else { Console.WriteLine("{0}'s attack missed", name); }         //fail message
            return false;

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