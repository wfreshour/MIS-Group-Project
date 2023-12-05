using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group_Project
{
    public class Inventory
    {
        public int MedPack;
        public int DMGStim;
        public int BullsEye;
        public int Booster;
        public int Revive;

        public Inventory()
        {
            MedPack = 0;
            DMGStim = 0;
            BullsEye = 0;
            Booster = 0;
            Revive = 0;
        }

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