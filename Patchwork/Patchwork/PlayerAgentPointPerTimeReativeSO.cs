﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patchwork
{
    class PlayerAgentPointPerTimeReactiveSO : Player
    {
        public PlayerAgentPointPerTimeReactiveSO(string nam) : base(nam)
        {
        }

        public PlayerAgentPointPerTimeReactiveSO(Player p) : base(p)
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
                }
            NextTile:;
            }

            PlayerAgentPointPerTimeReactiveSO oppC = new PlayerAgentPointPerTimeReactiveSO(current.ReturnOpponent(this.GetName()));//get opponent
            //use innerturn to get reactive values and ajust for this



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
                    float bestb = -80;
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

                            if (oppC.GetTime() < this.GetTime() - options[j].ReturnTime())
                            {
                                c += 1;
                            }

                            c -= oppC.OppChoice(current, j + 1);


                            if (c > bestb)
                            {
                                toBePlaced = j;
                                bestb = c;
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
            options = new Tile[] { t1, t2, t3 };






            float co = 0; //points for tile size
            foreach (bool t in options[toBePlaced].returnshape())
            {
                if (t)
                {
                    co += 2;
                }
            }

            if (oppC.GetTime() > this.GetTime() - options[toBePlaced].ReturnTime())//if opponent would get the next turn
            {
                if (1 - oppC.OppChoice(current, 0)//if passing is worth more than the tile considering opponents move
                    >
                    ((options[toBePlaced].ReturnButtons() * current.ButtonsLeft(this.GetTime()) - (options[toBePlaced].ReturnCost())+(co)) / options[toBePlaced].ReturnTime())
                    - oppC.OppChoice(current, toBePlaced + 1)
                    )
                {
                    return "Pass";
                }
            }
            else
            {
                if (1 >//if passing is worth more than the tile
                    ((options[toBePlaced].ReturnButtons() * current.ButtonsLeft(this.GetTime()) - (options[toBePlaced].ReturnCost())+(co)) / options[toBePlaced].ReturnTime())
                    )
                {
                    return "Pass";
                }
            }

















            //place that tile (more ugly brute force)


            int best = 0;

            List<Tile> tiles = current.GetTileList();

            for (int r = 0; r < 4; r++)
            {
                for (int x = -1; x < 10; x++)
                {
                    for (int y = -1; y < 10; y++)
                    {
                        if (this.GetGrid().tryTile(x, y, options[toBePlaced]))
                        {
                            //returnString = toBePlaced + "/" + y + "/" + x + "/R" + r;


                            Grid g = new Grid(this.GetGrid());

                            g.addTile(x, y, options[toBePlaced]);//check x and y
                            int bestL = 0;

                            foreach (Tile t in tiles)
                            {
                                if (CanBePlaced(t, g))
                                {
                                    bestL++;
                                }
                            }

                            if (best < bestL)
                            {
                                best = bestL;
                                returnString = toBePlaced + "/" + y + "/" + x + "/R" + r;
                            }

                        }
                    }
                }
                options[toBePlaced].rotate(1);
            }





            return returnString;
        }

        public override string SpecialPatch(Game current) //better version
        {
            String returnString = "";

            int best = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!this.GetGrid().tryPatch(i, j))
                    {
                        int total = 0;

                        if (i != 0)
                        {
                            if (this.GetGrid().shape[i - 1, j])
                            {
                                total++;
                            }
                        }
                        else
                        {
                            total++;
                        }

                        if (i != 8)
                        {
                            if (this.GetGrid().shape[i + 1, j])
                            {
                                total++;
                            }
                        }
                        else
                        {
                            total++;
                        }

                        if (j != 0)
                        {
                            if (this.GetGrid().shape[i, j - 1])
                            {
                                total++;
                            }
                        }
                        else
                        {
                            total++;
                        }

                        if (j != 8)
                        {
                            if (this.GetGrid().shape[i, j + 1])
                            {
                                total++;
                            }
                        }
                        else
                        {
                            total++;
                        }

                        if (total == 4)
                        {
                            return i + " " + j;
                        }

                        if (total > best)
                        {
                            returnString = i + " " + j;
                            total = best;
                        }


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

        public bool CanBePlaced(Tile t, Grid g)
        {
            Tile tile = new Tile(t);


            for (int r = 0; r < 4; r++)
            {
                for (int x = -1; x < 10; x++)
                {
                    for (int y = -1; y < 10; y++)
                    {
                        if (g.tryTile(x, y, tile))
                        {
                            return true;
                        }
                    }
                }
                tile.rotate(1);
            }
            return false;
        }

        public float OppChoice(Game current, int s)
        {

            Tile t1 = new Tile(current.ReturnTile(0 + s));
            Tile t2 = new Tile(current.ReturnTile(1 + s));
            Tile t3 = new Tile(current.ReturnTile(2 + s));


            Tile[] options = new Tile[] { t1, t2, t3 };
            bool[] canBePlaced = new bool[] { false, false, false };


            for (int i = 0; i < 3; i++) // ugly brute force check that a tile can be placed
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
                                    canBePlaced[i] = true;
                                    goto NextTile;
                                }
                            }
                        }
                        options[i].rotate(1);
                    }
                }
            NextTile:;
            }



            //determine what tile to place
            int numCanBePlaced = 0;

            foreach (bool b in canBePlaced)
            {
                if (b)
                {
                    numCanBePlaced++;
                }
            }

            float c = 0;
            float bestb = 0;

            switch (numCanBePlaced)
            {
                case 0:
                    return 1;
                case 1:
                case 2://if more than 1 choice then ppt
                case 3:
                    int j = 0;
                    bestb = 0;
                    c = 0;
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

                            if (c > bestb)
                            {
                                bestb = c;
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




            return bestb;
        }
    }
}
