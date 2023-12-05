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
        public int health;
        public string name;
        public int speed;
        public int stealth;
        public int accuracy;
        public int loot;
        

        public Enemy(string name, int health, int speed, int stealth, int accuracy)
        {
            this.name = name;
            this.health = health;
            this.speed = speed;
            this.stealth = stealth;
            this.accuracy = accuracy;
            loot = new Random().Next(0, 4);
        }

        public void death()
        {
            throw new System.NotImplementedException();
        }

        public void killPlayer()
        {
            throw new System.NotImplementedException();
        }

        public void EnemyAttack(Character c)
        {
            int successChance = 50 + (accuracy * 5);                    //uses accuracy stat to determine if they hit shot
            if(new Random().Next(successChance) <= successChance)
            {
                c.Health--;
                Console.WriteLine("Pirate shot {0}! {1} has {2} health remaining\n", c.name, c.name, c.Health);              //pirate hit player message
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






