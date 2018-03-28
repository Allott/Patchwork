using System;
using System.Collections.Generic;
using System.Text;

namespace Patchwork
{

    class PlayerAgentFirstOptionSO : Player
    {
        public PlayerAgentFirstOptionSO(string nam) : base(nam)
        {
        }

        public override string Turn(Game current)
        {
            String returnString = "Pass";

            Tile t1 = new Tile(current.ReturnTile(0));
            Tile t2 = new Tile(current.ReturnTile(1));
            Tile t3 = new Tile(current.ReturnTile(2));


            Tile[] options = new Tile[] {t1, t2 , t3 };
            int toBePlaced = -1;

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
                                if (this.GetGrid().tryTile(x,y, options[i]))
                                {
                                    //returnString = i + "/" + y + "/" + x + "/R" + r; //consider flipping
                                    //return returnString;
                                    toBePlaced = i;
                                    goto Exit;
                                }
                            }
                        }
                        options[i].rotate(1);
                    }
                }
            }
        Exit:;


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
