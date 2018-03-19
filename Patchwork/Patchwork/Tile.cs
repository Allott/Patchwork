using System;
using System.Collections.Generic;
using System.Text;

namespace Patchwork
{
    public class Tile
    {
        int id;
        int cost;
        int time;
        int buttons;

        bool[,] shape = {};

        public int ReturnID()
        {
            return id;
        }
        public int ReturnCost()
        {
            return cost;
        }
        public int ReturnTime()
        {
            return time;
        }
        public int ReturnButtons()
        {
            return buttons;
        }
        public Tile(int iid, int icost, int itime, int ibuttons)
        {
            id = iid;
            cost = icost;
            time = itime;
            buttons = ibuttons;
        }

        public Tile(Tile t)
        {
            id = t.id;
            cost = t.cost;
            time = t.time;
            buttons = t.buttons;
            shape = t.shape;
        }

        public Tile(int iid, int icost, int itime, int ibuttons, bool[,] s)
        {
            id = iid;
            cost = icost;
            time = itime;
            buttons = ibuttons;
            changeShape(s);
        }

        public void changeShape(bool[,] s)
        {
            shape = s;
        }

        public bool[,] returnshape()
        {
            return shape;
        }

        public void rotate(int t)
        {
            

            for (int j = 0; j < t; j++)
            {
                bool[,] returnd = new bool[5, 5];
                for (int i = 0; i < 5; i++)
                {
                    returnd[4, i] = shape[i, 0];
                    returnd[3, i] = shape[i, 1];
                    returnd[2, i] = shape[i, 2];
                    returnd[1, i] = shape[i, 3];
                    returnd[0, i] = shape[i, 4];
                }
                shape = returnd;
            }

            
        }

        public void flip()
        {
            bool[,] returnd = new bool[5, 5];


            for (int i = 0; i < 5; i++)
            {
                returnd[i, 4] = shape[i, 0];
                returnd[i, 3] = shape[i, 1];
                returnd[i, 2] = shape[i, 2];
                returnd[i, 1] = shape[i, 3];
                returnd[i, 0] = shape[i, 4];
            }
            shape = returnd;
        }

        public string returnString()
        {
            string returend = "";

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    
                    if (shape[i, j])
                    {
                        returend += "#";
                    }
                    else
                    {
                        returend += "'";
                    }
                    
                }
                returend += "\r\n";
            }


            return returend;
        }
    }
}
