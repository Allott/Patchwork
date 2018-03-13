using System;
using System.Collections.Generic;
using System.Text;

namespace Patchwork
{
    /*
     ugly brute force agent
         
         fix the errors with seleting
         
         */


    class PlayerAgentBTalks : Player
    {
        public PlayerAgentBTalks(string nam) : base(nam)
        {
        }

        public override string Turn(Game current)
        {
            String returnString = "Pass";

            //Console.WriteLine("test");
            Tile t1 = new Tile(current.ReturnTile(0));
            Tile t2 = new Tile(current.ReturnTile(1));
            Tile t3 = new Tile(current.ReturnTile(2));


            Tile[] options = new Tile[] { t1, t2, t3 };


            for (int i = 0; i < 3; i++)
            {
                if (options[i].ReturnCost() < this.GetButtons())
                {
                    for (int r = 0; r < 4; r++)
                    {
                        for (int x = -1; x < 10; x++)
                        {
                            for (int y = -1; y < 10; y++)
                            {
                                if (this.GetGrid().tryTile(x, y, options[i]))
                                {
                                    returnString = i + "/" + y + "/" + x + "/R" + r; //consider flipping
                                    
                                    Console.Clear();

                                    Console.WriteLine(returnString);
                                    Console.WriteLine(r + "\r\n" + options[i].returnString());


                                    Console.WriteLine("Buttons:" + this.GetButtons() + "|Time:" + this.GetTime());
                                    Console.WriteLine(this.GetGrid().returnString());
                                    Console.WriteLine();
                                    Console.WriteLine(current.Purchases());
                                    Console.ReadLine();
                                    
                                    return returnString;

                                }
                            }
                        }
                        options[i].rotate(1);
                    }
                }
            }
            Console.WriteLine(returnString);
            return returnString;
        }

        public override string SpecialPatch(Game current)
        {
            String returnString = "";

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!this.GetGrid().tryPatch(i, j))
                    {
                        returnString = i + " " + j;
                        return returnString;
                    }
                }
            }

            return returnString;
        }
    }
}

