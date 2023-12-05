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
        private int health;
        private string name;
        private int speed;
        private int stealth;
        private int accuracy;
        List<Enemy> pirates = new List<Enemy>();

        public void fileReader()
        {

        }
       
        

        public void death()
        {
            throw new System.NotImplementedException();
        }

        public void killPlayer()
        {
            throw new System.NotImplementedException();
        }

        public void EnemyAttack()
        {
            int successChance = 50 + Enemy.Accuracy * 5;                    //uses accuracy stat to determine if they hit shot
            if(new Random().Next(successChance) <= successChance)
            {
                userHealth--;
                Console.WriteLine("Pirate shot you! You have {0} health remaining",userHealth)              //pirate hit player message
            }
            else { Console.WriteLine("Pirate Attack Missed")}                   //pirate misses shot message

        }
    }
}






