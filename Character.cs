using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group_Project
{
    public class Character
    {
        public ClassType ClassType;
        public string name;
        public bool isUser;

        public Character(string name, ClassType ct, bool isUser)
        {
            this.isUser = isUser;
            this.name = name;
            this.ClassType = ct;
        }

        public void attack()
        {
            throw new System.NotImplementedException();
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