using System;
using System.Collections.Generic;
using System.Text;

namespace Patchwork
{
    public class Grid
    {
        bool[,] shape = new bool[9, 9];

        public Grid()
        {
        }

        public Grid(Grid g)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    shape[i, j] = g.shape[i, j];
                }
            }



            //shape = g.shape;
        }

        public void reset()
        {
            shape = new bool[9, 9];
        }

        public void addTile(int x, int y, Tile t)
        {
            bool[,] newshape = shape;
            bool[,] tileadd = t.returnshape();
            bool error = false;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (tileadd[i,j])
                    {
                        try
                        {
                            if (!newshape[i + x, j + y]) //try catch stuff here probebly
                            {
                                newshape[i + x, j + y] = tileadd[i, j];
                            }
                            else
                            {
                                error = true;
                                break;
                                i = 11;
                                j = 11;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex + "/r/n");
                            error = true;
                            break;
                            i = 11;
                            j = 11;
                        }
                        
                    }
                }
            }

            if (error)
            {
                throw new System.ArgumentException("Error addtile");
            }
            else
            {
                shape = newshape;
            }
        }

        public bool tryTile(int x, int y, Tile t)
        {
            bool[,] tileadd = t.returnshape();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (tileadd[i,j])
                    {
                        if ((i + x > 8)||(i + x < 0))
                        {
                            return false;
                            //Console.WriteLine("grid check error 1");
                            //Console.ReadLine();
                        }

                        if ((j + y > 8) || (j + y < 0))
                        {
                            return false;
                            //Console.WriteLine("grid check error 2");
                            //Console.ReadLine();
                        }

                        if (shape[i+x,j+y])
                        {
                            //Console.WriteLine("grid check error 3");
                            //Console.ReadLine();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public void addPatch(int x, int y)
        {
            if (shape[x,y])
            {

            }
            else
            {
                shape[x, y] = true;
            }
        }

        public bool tryPatch(int x, int y)
        {
            return shape[x, y];
        }
        public string returnString()
        {
            string returend = "";
            returend += " 0 1 2 3 4 5 6 7 8 X\r\n";
            for (int i = 0; i < 9; i++)
            {
                returend += i;
                for (int j = 0; j < 9; j++)
                {
                    
                    if (shape[i, j])
                    {
                        returend += "# ";
                    }
                    else
                    {
                        returend += "_ ";
                    }

                }
                returend += "\r\n";
            }
            returend += "Y";

            return returend;
        }

        public int returnScore()
        {
            int score = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    if (shape[i, j])
                    {
                    }
                    else
                    {
                        score = score - 2;
                    }

                }
            }

            return score;
        }
    }
}
