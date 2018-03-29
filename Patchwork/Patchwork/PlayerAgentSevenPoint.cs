using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patchwork
{
    class PlayerAgentSevenPoint : Player
    {
        public PlayerAgentSevenPoint(string nam) : base(nam)
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

            for (int i = 0; i < 3; i++)
            {
                if (options[i].ReturnCost() < this.GetButtons())
                {
                    for (int r = 0; r < 4; r++)
                    {
                        for (int x = -1; x < 7; x++)
                        {
                            for (int y = -1; y < 7; y++)
                            {
                                if (this.GetGrid().tryTile(x, y, options[i]))
                                {
                                    Grid g = new Grid(this.GetGrid());

                                    g.addTile(x, y, options[i]);//check x and y
                                    int worst = 99;

                                    for (int x2 = 0; x2 < 9; x2++)
                                    {
                                        for (int y2 = 0; y2 < 9; y2++)
                                        {
                                            if (!g.shape[x2, y2])
                                            {
                                                int currentGap = Gap(x2, y2, g);

                                                if (currentGap < worst)
                                                {
                                                    worst = currentGap;
                                                }
                                            }
                                        }
                                    }

                                    if (best < worst)
                                    {
                                        best = worst;
                                        returnString = i + "/" + y + "/" + x + "/R" + r;
                                    }

                                }
                            }
                        }
                        options[i].rotate(1);
                    }

                    t1 = new Tile(current.ReturnTile(0));//reset tile manipulations
                    t2 = new Tile(current.ReturnTile(1));
                    t3 = new Tile(current.ReturnTile(2));

                    for (int r = 0; r < 4; r++)
                    {
                        for (int x = 7; x < 10; x++)
                        {
                            for (int y = -1; y < 7; y++)
                            {
                                if (this.GetGrid().tryTile(x, y, options[i]))
                                {
                                    Grid g = new Grid(this.GetGrid());

                                    g.addTile(x, y, options[i]);//check x and y
                                    int worst = 99;

                                    for (int x2 = 0; x2 < 9; x2++)
                                    {
                                        for (int y2 = 0; y2 < 9; y2++)
                                        {
                                            if (!g.shape[x2, y2])
                                            {
                                                int currentGap = Gap(x2, y2, g);

                                                if (currentGap < worst)
                                                {
                                                    worst = currentGap;
                                                }
                                            }
                                        }
                                    }

                                    if (best < worst)
                                    {
                                        best = worst;
                                        returnString = i + "/" + y + "/" + x + "/R" + r;
                                    }

                                }
                            }
                        }
                        options[i].rotate(1);
                    }

                    t1 = new Tile(current.ReturnTile(0));//reset tile manipulations
                    t2 = new Tile(current.ReturnTile(1));
                    t3 = new Tile(current.ReturnTile(2));

                    for (int r = 0; r < 4; r++)
                    {
                        for (int x = -1; x < 10; x++)
                        {
                            for (int y = 7; y < 10; y++)
                            {
                                if (this.GetGrid().tryTile(x, y, options[i]))
                                {
                                    Grid g = new Grid(this.GetGrid());

                                    g.addTile(x, y, options[i]);//check x and y
                                    int worst = 99;

                                    for (int x2 = 0; x2 < 9; x2++)
                                    {
                                        for (int y2 = 0; y2 < 9; y2++)
                                        {
                                            if (!g.shape[x2, y2])
                                            {
                                                int currentGap = Gap(x2, y2, g);

                                                if (currentGap < worst)
                                                {
                                                    worst = currentGap;
                                                }
                                            }
                                        }
                                    }

                                    if (best < worst)
                                    {
                                        best = worst;
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

        public int Gap(int x,int y, Grid g)
        {

            int r = 0;


            try
            {
                if (!g.shape[x, y])
                {
                    r++;
                    g.shape[x, y] = true;//stop infinate

                    if (x != 0)
                    {r += Gap(x - 1, y, g);}

                    if (x != 8)
                    { r += Gap(x + 1, y, g); }

                    if (y != 0)
                    { r += Gap(x, y - 1, g); }

                    if (y != 8)
                    { r += Gap(x, y + 1, g); }
                }
            }
            catch
            { }
            return r;
        }

    }
}
