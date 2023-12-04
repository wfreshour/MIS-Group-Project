using System;

namespace Group_Project
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input; // to hold users input
            string name; // name of the user
            int xp = 0; // amount of xp found in a chest
            int numRooms = 9; //holds number of rooms
            bool nextRoom = false; // tells whether or not user chose to enter next room
            ClassType userClass; // chosen class of the user
            List<Character> characters = new List<Character>();

            // greeet user and present story
            Console.WriteLine("Welcome to Recapture!");

            // get users name
            Console.WriteLine("Please enter your name: ");
            name = Console.ReadLine();

            // get users class
            Console.WriteLine("Below are the options for your classes:");
            DisplayClass();
            Console.WriteLine("Please enter the number for your choice of class (The classes you dont choose will be assigned to your crew)");
            userClass = (ClassType)ValidInput(Console.ReadLine(), 1, Enum.GetValues(typeof(ClassType)).Cast<ClassType>().Distinct().Count());

            // create the users Character object
            characters.Add(new Character(name, userClass, true));

            // create remaining crew
            FillCrew(characters, userClass);

            //Prompt the user to continue
            Console.WriteLine("You are now ready to begin the journey, press any key to continue...");
            AnyKey();

            //Load monsters from external file

            for (int i = 0; i < numRooms; i++)
            {
                //Generate Room
                Room r = new Room(i+1);

                //Start combat

                do
                {
                    //Ask user what they would like to do
                    Console.WriteLine("You are in the {0}, what would you like to do? (Loot, Check Stats, Enter next room)", r.roomName);
                    input = Console.ReadLine();

                    if (input == "Loot")
                    {
                        bool hasChest = r.Loot(ref xp);

                        if (hasChest)
                        {
                            Console.WriteLine("A chest was found! It contained {0} xp", xp);
                            foreach (Character c in characters)
                            {
                                c.AddXP(xp);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No loot was found.");
                        }
                    }
                    else if (input == "Check Stats")
                    {
                        r.CheckStats(characters[0]);
                    }
                    else if (input == "Enter next room")
                    {
                        nextRoom = true;
                    }
                } while (!nextRoom);
                nextRoom = false;
            }

        }

        /// <summary>
        /// Displays the options for the users class
        /// </summary>
        static void DisplayClass()
        {
            Console.WriteLine("\t\tHEALTH\tACCURACY   SPEED   STEALTH");
            for (int i = 0; i < Enum.GetValues(typeof(ClassType)).Cast<ClassType>().Distinct().Count(); i++)
            {
                ClassType ct = (ClassType)i;

                if (ct == ClassType.Pilot)
                {
                    Console.WriteLine("{0}. Pilot:\t2\t2\t   1\t   5",i+1);
                }
                else if (ct == ClassType.Engineer)
                {
                    Console.WriteLine("{0}. Engineer:\t2\t2\t   5\t   1",i+1);
                }
                else if (ct == ClassType.Heavy)
                {
                    Console.WriteLine("{0}. Heavy:\t5\t3\t   1\t   1", i + 1);
                }
                else
                {
                    Console.WriteLine("{0}. Marksman:\t1\t5\t   1\t   3", i + 1);
                }
            }
        }

        /// <summary>
        /// Takes a given input and trys to convert it to an integer. Keeps getting users input until a valid one is received
        /// </summary>
        /// <param name="input">input to test</param>
        /// <param name="lowerBound">gives lower bound to test against</param>
        /// <param name="upperBound">gives lower bound to test against</param>
        /// <returns>the valid input</returns>
        static int ValidInput(string input, int lowerBound, int upperBound)
        {
            bool isValid = false;
            while (!isValid)
            {
                try
                {
                    int i = int.Parse(input);
                    isValid = true;
                    if (i < lowerBound || i > upperBound)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter a number between {0} and {1}", lowerBound, upperBound);
                        Console.ResetColor();
                        input = Console.ReadLine();
                        isValid = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please type a numerical value, like 1, 2, 3, etc.");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    isValid = false;
                }
            }
            return int.Parse(input);
        }

        /// <summary>
        /// Fills the characters list with the rest of the crew
        /// </summary>
        /// <param name="characters">list of characters</param>
        /// <param name="userCT">the users class type</param>
        static void FillCrew(List<Character> characters, ClassType userCT)
        {
            for (int i = 0; i < Enum.GetValues(typeof(ClassType)).Cast<ClassType>().Distinct().Count(); i++)
            {
                ClassType ct = (ClassType)i;

                if (ct != userCT)
                {
                    if (ct == ClassType.Pilot)
                    {
                        characters.Add(new Character("Pilot", ClassType.Pilot, false));
                    }
                    else if (ct == ClassType.Engineer)
                    {
                        characters.Add(new Character("Engineer", ClassType.Engineer, false));
                    }
                    else if (ct == ClassType.Heavy)
                    {
                        characters.Add(new Character("Heavy", ClassType.Heavy, false));
                    }
                    else
                    {
                        characters.Add(new Character("Marksman", ClassType.Marksman, false));
                    }
                }
            }
        }

        /// <summary>
        /// waits for the user to press a key before returning to program
        /// </summary>
        static void AnyKey()
        {
            Console.ReadKey(true);
        }
    }
}