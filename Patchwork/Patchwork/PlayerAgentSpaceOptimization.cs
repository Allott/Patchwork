using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patchwork
{
    class PlayerAgentSpaceOptimization : Player
    {
        public PlayerAgentSpaceOptimization(string nam) : base(nam)
        {
        }

        public override string Turn(Game current)
        {
            String returnString = "Pass";

            Tile t1 = new Tile(current.ReturnTile(0));
            Tile t2 = new Tile(current.ReturnTile(1));
            Tile t3 = new Tile(current.ReturnTile(2));


            Tile[] options = new Tile[] { t1, t2, t3 };

            int best = 0;

            List<Tile> tiles = current.GetTileList();

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
                                    //returnString = toBePlaced + "/" + y + "/" + x + "/R" + r;


                                    Grid g = new Grid(this.GetGrid());

                                    g.addTile(x, y, options[i]);//check x and y
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
                                        returnString = i + "/" + y + "/" + x + "/R" + r;
                                    }

                                }
                            }
                        }
                        options[i].rotate(1);
                    }
                }
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
                            if (this.GetGrid().shape[i-1,j])
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
                            if (this.GetGrid().shape[i, j-1])
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

    }
}
