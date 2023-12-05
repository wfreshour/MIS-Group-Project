using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group_Project
{
    public class Inventory
    {
        public int MedPack;//Increases users health
        public int DMGStim;//increases users damage
        public int BullsEye;//Increases users accuracy
        public int Booster;//Increases users Speed
        public int Revive;//Allows user to revive a crew mate

        /// <summary>
        /// Constructor for inventory
        /// </summary>
        public Inventory()
        {
            MedPack = 0;
            DMGStim = 0;
            BullsEye = 0;
            Booster = 0;
            Revive = 0;
        }

        /// <summary>
        /// Adds one more of the given item
        /// </summary>
        /// <param name="item">item to increase</param>
        public void Add(int item)
        {
            if (item == 0) { MedPack++; }
            else if (item == 1) { DMGStim++; }
            else if (item == 2) { BullsEye++; }
            else if (item == 3) { Booster++; }
            else if (item == 4) { Revive++; }
        }
    }
}