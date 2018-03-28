using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patchwork
{
    class PlayerAgentPointPerTimeTalks : Player
    {
        public PlayerAgentPointPerTimeTalks(string nam) : base(nam)
        {
        }

        public override string Turn(Game current)
        {
            String returnString = "Pass";

            Tile t1 = new Tile(current.ReturnTile(0));
            Tile t2 = new Tile(current.ReturnTile(1));
            Tile t3 = new Tile(current.ReturnTile(2));


            Tile[] options = new Tile[] { t1, t2, t3 };
            bool[] canBePlaced = new bool[] { false, false, false };


            for (int i = 0; i < 3; i++) // ugly brute force check that a tile can be placed
            {
                if (options[i].ReturnCost() < this.GetButtons())
                {
                    for (int f = 0; f < 2; f++)
                    {
                        for (int r = 0; r < 4; r++)
                        {
                            for (int x = -1; x < 10; x++)
                            {
                                for (int y = -1; y < 10; y++)
                                {
                                    if (this.GetGrid().tryTile(x, y, options[i]))
                                    {
                                        canBePlaced[i] = true;
                                        goto NextTile;
                                    }
                                }
                            }
                            options[i].rotate(1);
                        }
                        options[i].flip();
                    }  
                }
            NextTile:;
            }



            //determine what tile to place
            int numCanBePlaced = 0;
            int toBePlaced = -1;

            foreach (bool b in canBePlaced)
            {
                if (b)
                {
                    numCanBePlaced++;
                }
            }

            switch (numCanBePlaced)
            {
                case 0:
                    return returnString;
                case 1:
                    int i = 0;
                    foreach (bool b in canBePlaced)
                    {
                        if (b)
                        {
                            toBePlaced = i;
                        }
                        i++;
                    }
                    break;
                    
                case 2://if more than 1 choice then ppt
                case 3:
                    int j = 0;
                    float best = 0;
                    float c;
                    foreach (bool b in canBePlaced)
                    {
                        if (b)
                        {
                            c = 0;
                            foreach (bool t in options[j].returnshape())//points for tile size
                            {
                                if (t)
                                {
                                    c += 2;
                                }
                            }
                            c += options[j].ReturnButtons() * current.ButtonsLeft(this.GetTime());//points for the buttons
                            c -= options[j].ReturnCost();//cost
                            c = c / options[j].ReturnTime();

                            if (c > best)
                            {
                                toBePlaced = j;
                                best = c;
                            }
                            
                        }
                        j++;
                    }
                    break;

                default:
                    Console.WriteLine("error");
                    Console.ReadLine();
                    break;
            }

            t1 = new Tile(current.ReturnTile(0));//reset tile manipulations
            t2 = new Tile(current.ReturnTile(1));
            t3 = new Tile(current.ReturnTile(2));
            //place that tile (more ugly brute force)

            for (int f = 0; f < 2; f++)
            {
                for (int r = 0; r < 4; r++)
                {
                    for (int x = -1; x < 10; x++)
                    {
                        for (int y = -1; y < 10; y++)
                        {
                            if (this.GetGrid().tryTile(x, y, options[toBePlaced]))
                            {
                                returnString = toBePlaced + "/" + y + "/" + x + "/R" + r;

                                if (f == 1)
                                {
                                    returnString += "/F";
                                }

                                Console.Clear();

                                Console.WriteLine(returnString);
                                Console.WriteLine(r + "\r\n" + options[toBePlaced].returnString());


                                Console.WriteLine("Buttons:" + this.GetButtons() + "|Time:" + this.GetTime());
                                Console.WriteLine(this.GetGrid().returnString());
                                Console.WriteLine();
                                Console.WriteLine(current.Purchases());
                                Console.ReadLine();

                                return returnString;
                            }
                        }
                    }
                    options[toBePlaced].rotate(1);
                }
                options[toBePlaced].flip();
            }




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

        public float GetTilePoints(Tile t, Game current)
        {
            float r = 0;

            bool[,] grid = t.returnshape();

            foreach (bool b in grid)
            {
                if (b)
                {
                    r = r + 2;
                }
            }

            r = r + t.ReturnButtons() * current.ButtonsLeft(this.GetTime());//fix this

            r = r - t.ReturnCost();
            
            r = r / t.ReturnTime();
            return r;
        }

    }
}
