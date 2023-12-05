using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Group_Project
{
    public class Enemy
    {
        public int health; //enemies health
        public string name; //enemies name
        public int speed; //enemies speed
        public int stealth; //enemies stealth
        public int accuracy; //enemies accuracy
        public int loot; //enemies loot

        /// <summary>
        /// Constructor for the enemy, randomly assigns it loot
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="speed"></param>
        /// <param name="stealth"></param>
        /// <param name="accuracy"></param>
        public Enemy(string name, int health, int speed, int stealth, int accuracy)
        {
            this.name = name;
            this.health = health;
            this.speed = speed;
            this.stealth = stealth;
            this.accuracy = accuracy;
            loot = new Random().Next(0, 4);
        }

        /// <summary>
        /// Allows the enemy to attack
        /// </summary>
        /// <param name="c">character they are attacking</param>
        public void EnemyAttack(Character c)
        {
            int successChance = 50 + (accuracy * 5);                    //uses accuracy stat to determine if they hit shot
            if (new Random().Next(successChance) <= successChance)
            {
                c.Health--;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pirate shot {0}! {1} has {2} health remaining\n", c.name, c.name, c.Health);              //pirate hit player message
                Console.ResetColor();
                

                if (c.Health == 0)
                {
                    c.isAlive = false;
                    Console.WriteLine("{0} has died\n", c.name);
                }
            }
            else { Console.WriteLine("Pirate Attack Missed\n"); }                   //pirate misses shot message

        }
    }
}






