using System;
using System.Collections.Generic;
using System.Text;


namespace Patchwork
{
    class PlayerHuman : Player
    {
        public PlayerHuman(string nam) : base(nam)
        {
        }

        public override string Turn(Game current)
        {
            String returnString = "Pass";

            Console.Clear();
            Console.WriteLine(this.GetName());
            Console.WriteLine();
            Console.WriteLine("Buttons:" + this.GetButtons() + "|Time:" + this.GetTime());
            Console.WriteLine(this.GetGrid().returnString());
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("pass = pass turn | w = move foward | s = move back | r = rotate all tiles | f = flip tiles");
            Console.WriteLine("buying = tilenumberinlist/x/y/modifiers");
            Console.WriteLine("modifiers: R1-3 and F");
            Console.WriteLine();
            Console.WriteLine(current.Purchases());


            returnString = Console.ReadLine();

            return returnString;
        }

        public override string SpecialPatch(Game current)
        {
            String returnString = "";

            Console.Clear();

            Console.WriteLine("Buttons:" + this.GetButtons() + "|Time:" + this.GetTime());
            Console.WriteLine(this.GetGrid().returnString());
            Console.WriteLine();
            Console.WriteLine("place special patch");


            returnString = Console.ReadLine();

            return returnString;
        }
    }
}
