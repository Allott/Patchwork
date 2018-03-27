using System;
using System.Collections.Generic;
using System.Text;

namespace Patchwork
{
    public class Game
    {
        /*update ppt to consider pass > value of tile
         * 
         * reactive agent
         * 1. get opponent
         * 
         * 
         */




        Player player1;
        Player player2;

        List<Tile> tileList = new List<Tile>();
        List<int> specialPatches;
        List<int> timeButtons;

        int neutralToken = 0;
        Boolean player1Priority = true;
        char tieB = 'n';
        char sevenX = 'n';

        int tileLocation;//for buy action

        char[] delimiterChars = {' ','/',','};
        public Game()
        {
            tileList = new List<Tile>();
            specialPatches = new List<int>();
            timeButtons = new List<int>();
            specialPatches.Add(3);
            specialPatches.Add(9);
            specialPatches.Add(21);
            specialPatches.Add(27);
            specialPatches.Add(33);

            timeButtons.Add(1);
            timeButtons.Add(6);
            timeButtons.Add(12);
            timeButtons.Add(18);
            timeButtons.Add(24);
            timeButtons.Add(30);
            timeButtons.Add(36);
            timeButtons.Add(42);
            timeButtons.Add(48);


            //specialPatches.Add(50);//testing
        }

        public void setPlayers(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;
        }

        public void addTile(Tile t1)
        {
            tileList.Add(t1);
        }

        public void removeTileFromNeutral(int movement)
        {
            if (movement + neutralToken > tileList.Count)
            {
                removeTile((movement + neutralToken)- tileList.Count);
            }
            else
            {
                removeTile(neutralToken + movement);
            }
        }

        public void removeTile(int I)
        {
            tileList.RemoveAt(I);
        }

        public Player rungame()
        {
            Player winner = null;
            player1.Reset();
            player2.Reset();

            string action;
            while ((player1.GetTime() > 0)&&(player2.GetTime() > 0))//while is not over
            {
                if (player1.GetTime() > player2.GetTime())
                {
                    player1Priority = true;
                }//deturming current player
                else if (player1.GetTime() < player2.GetTime())
                {
                    player1Priority = false;
                }//if neither are true the person who went last takes a turn


                if (player1Priority)
                {
                    action = player1.Turn(this);
                    action = action.ToUpper();

                    if (action == "PASS")
                    {
                        Pass(player1, player2);
                    }
                    else if (action == "W")
                    {
                        neutralToken = move(1);
                    }
                    else if (action == "S")
                    {
                        neutralToken = move(-1);
                    }
                    else if (action == "R")
                    {
                        foreach(Tile t in tileList)
                        {
                            t.rotate(1);
                        }
                    }
                    else if (action == "F")
                    {
                        foreach (Tile t in tileList)
                        {
                            t.flip();
                        }
                    }
                    else
                    {
                        Buy(player1, action);
                    }
                }//there action
                else
                {
                    action = player2.Turn(this);
                    action = action.ToUpper();

                    if (action == "PASS")
                    {
                        Pass(player2, player1);
                    }
                    else if (action == "R")
                    {
                        foreach (Tile t in tileList)
                        {
                            t.rotate(1);
                        }
                    }
                    else if (action == "F")
                    {
                        foreach (Tile t in tileList)
                        {
                            t.flip();
                        }
                    }
                    else
                    {
                        Buy(player2, action);
                    }
                }
                List<int> toRemove = new List<int>();

                foreach (int i in specialPatches)//remove special patches
                {
                    if (player1Priority)
                    {
                        if (i > player1.GetTime())
                        {
                            SpecialPatch(player1);
                            toRemove.Add(i);
                        }
                    }
                    else
                    {
                        if (i > player2.GetTime())
                        {
                            SpecialPatch(player2);
                            toRemove.Add(i);
                        }
                    }
                }
                foreach (int i in toRemove)
                {
                    specialPatches.Remove(i);
                }


                if (sevenX == 'n')
                {
                    if (player1Priority)
                    {
                        if (CheckForSevenX(player1))
                        {
                            sevenX = '1';
                        }
                    }
                    else
                    {
                        if (CheckForSevenX(player2))
                        {
                            sevenX = '2';
                        }
                    }
                }



                if (tieB == 'n')
                {
                    if (player1.GetTime() <= 0)
                    {
                        tieB = '1';
                    }
                    else if (player2.GetTime() <= 0)
                    {
                        tieB = '2';
                    }
                }


            }

            
            int p1score = player1.GetGrid().returnScore() + player1.GetButtons();
            int p2score = player2.GetGrid().returnScore() + player2.GetButtons();

            if (sevenX == '1')
            {
                p1score += 7;
            }
            else if (sevenX == '2')
            {
                p2score += 7;
            }

            if (p1score > p2score)
            {
                winner = player1;
            }
            else if (p2score > p1score)
            {
                winner = player2;
            }
            else
            {
                //winner = new Player("Draw");
                if (tieB == '1')
                {
                    winner = player1;
                }
                else if (tieB == '2')
                {
                    winner = player2;
                }
                else
                {
                    winner = new Player("Draw");
                }
            }

            return winner;
        }



        public void Pass(Player currentPlayer, Player otherPlayer)
        {
            int origin = currentPlayer.GetTime();
            MovementButtons(currentPlayer.GetTime(), otherPlayer.GetTime() - 1, currentPlayer); // check this
            currentPlayer.SetTime(otherPlayer.GetTime() - 1);
            currentPlayer.AddButtons(origin - currentPlayer.GetTime());
        }

        public void SpecialPatch(Player currentPlayer)
        {
            string[] actionSteps;
            while (true)
            {
                actionSteps = currentPlayer.SpecialPatch(this).Split(delimiterChars);

                if ((Convert.ToInt32(actionSteps[0]) >= 9) || (Convert.ToInt32(actionSteps[0]) < 0))
                {
                    gameError("illegal move, trying to place tile X out of range");
                }
                else if ((Convert.ToInt32(actionSteps[1]) >= 9) || (Convert.ToInt32(actionSteps[1]) < 0))
                {
                    gameError("illegal move, trying to place tile X out of range");
                }
                else if (actionSteps.Length == 2)
                {
                    break;
                }
            }

            currentPlayer.GetGrid().addPatch(Convert.ToInt32(actionSteps[0]), Convert.ToInt32(actionSteps[1]));

        }

        public void Buy(Player currentPlayer, String action)
        {
            string[] actionSteps = action.Split(delimiterChars);
            
            int tileX = 0;
            int tileY = 0;
            Tile tileTransformed;
            bool valid = true;
            int moving = 0;

            try//get tile location
            {
                if ((Convert.ToInt32(actionSteps[0]) >= 3) || (Convert.ToInt32(actionSteps[0]) < 0))
                {
                    gameError("illegal move, trying to buy outside of range");
                    valid = false;
                }

                //tileLocation = Convert.ToInt32(actionSteps[0]) + neutralToken;
                moving = Convert.ToInt32(actionSteps[0]);
                tileLocation = move(moving);
            }
            catch (Exception ex)
            {
                gameError(Convert.ToString(ex));
            }

            if (tileList[tileLocation].ReturnCost() > currentPlayer.GetButtons())
            {
                gameError("illegal move, cant pay costs");
                valid = false;
            }

            try//get tile x
            {
                /*
                if ((Convert.ToInt32(actionSteps[1]) >= 9) || (Convert.ToInt32(actionSteps[1]) < 0))
                {
                    gameError("illegal move, trying to place tile X out of range");
                }
                */

                tileY = Convert.ToInt32(actionSteps[1]);
            }
            catch (Exception ex)
            {
                gameError(Convert.ToString(ex));
                valid = false;
            }

            try//get tile y
            {
                /*
                if ((Convert.ToInt32(actionSteps[2]) >= 9) || (Convert.ToInt32(actionSteps[2]) < 0))
                {
                    gameError("illegal move, trying to place tile Y out of range");
                }
                */

                tileX = Convert.ToInt32(actionSteps[2]);
            }
            catch (Exception ex)
            {
                gameError(Convert.ToString(ex));
                valid = false;
            }

            tileTransformed = new Tile(tileList[tileLocation]);

            try
            {
                for (int i = 3; i < actionSteps.Length; i++)
                {
                    switch (actionSteps[i])
                    {
                        case "R1":
                            tileTransformed.rotate(1);
                            //Console.WriteLine("r1");
                            break;
                        case "R2":
                            //Console.WriteLine("r2");
                            tileTransformed.rotate(2);
                            break;
                        case "R3":
                            //Console.WriteLine("r3");
                            tileTransformed.rotate(3);
                            break;
                        case "F":
                            tileTransformed.flip();
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                gameError(Convert.ToString(ex));
                valid = false;
            }

            if (valid)
            {//check try tile
                valid = currentPlayer.GetGrid().tryTile(tileX, tileY, tileTransformed);


                if (!valid)
                {
                    Console.WriteLine(currentPlayer.GetTime());
                    Console.WriteLine(action + "ahh\r\n" + tileTransformed.returnString() + currentPlayer.GetName());
                    Console.WriteLine(currentPlayer.GetGrid().returnString());
                    Console.ReadLine();
                }
                
            }

            if (valid)
            {
                currentPlayer.GetGrid().addTile(tileX, tileY, tileTransformed);
                currentPlayer.SetButtons(currentPlayer.GetButtons() - tileTransformed.ReturnCost());

                MovementButtons(currentPlayer.GetTime(), currentPlayer.GetTime() - tileTransformed.ReturnTime(), currentPlayer);// check this
                currentPlayer.SetTime(currentPlayer.GetTime() - tileTransformed.ReturnTime());
                currentPlayer.SetButtonGain(currentPlayer.GetButtonGain() + tileTransformed.ReturnButtons());
                //removeTileFromNeutral(tileLocation);
                //moveToken(tileLocation);
                removeTile(move(moving));
                neutralToken = move(moving);

            }
            

        }

        public void moveToken(int movement)//check this can wrap around to 0
        {
            if (movement + neutralToken >= tileList.Count)
            {
                neutralToken =(movement + neutralToken) - tileList.Count;
            }
            else
            {
                neutralToken += movement;
            }
        }

        public int move(int move)
        {
            if (move + neutralToken >= tileList.Count)
            {
                return move + neutralToken - tileList.Count;
            }
            else if (move + neutralToken < 0)
            {
                return tileList.Count - 1;
            }
            else
            {
                return move + neutralToken;
            }
        }

        public string Purchases()
        {
            string returned = "";
            for (int i = 0; i < 3; i++)
            {
                returned += "\r\n|ID: ";
                returned += tileList[move(i)].ReturnID();
                returned += "|Cost: ";
                returned += tileList[move(i)].ReturnCost();
                returned += "|Time: ";
                returned += tileList[move(i)].ReturnTime();
                returned += "|Buttons: ";
                returned += tileList[move(i)].ReturnButtons();
                returned += "\r\n";
                returned += tileList[move(i)].returnString();
            }

            
            

            return returned;
        }
        public void gameError(string ex)
        {
            Console.WriteLine("a game error has occured:");
            Console.WriteLine(ex);
            Console.WriteLine("press any key to continue");
            Console.ReadLine();
        }


        public Tile ReturnTile(int x)
        {
            Tile temp = tileList[move(x)];
            return temp;
        }

        public void MovementButtons(int start, int end , Player player)
        {
            int passedB = 0;

            for (int i = start + 1; i < end; i--)
            {
                if (timeButtons.Contains(i))
                {
                    passedB++;
                }
            }

            player.AddButtons(passedB * player.GetButtonGain());
        }

        public int ButtonsLeft(int time)
        {
            int r = 0;
            foreach (int i in timeButtons)
            {
                if (i > time)
                {
                    return r;
                }
                else
                {
                    r++;
                }
            }
            return r;
        }

        public List<Tile> GetTileList()
        {

            return tileList;
        }

        public bool CheckForSevenX(Player p)//i hate everythng about this
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    int counter = 0;

                    for (int x = 0; x < 7; x++)
                    {
                        for (int y = 0; y < 7; y++)
                        {
                            
                            if (p.GetGrid().shape[i+x,j+y])
                            {
                                counter++;
                                if (counter >= 49)
                                {

                                    Console.WriteLine(":O");
                                    Console.ReadLine();
                                    return true;
                                    
                                }
                            }
                            else
                            {
                                if ((i + x >= 2) && (i + x <= 6) && (j + y <= 6) && (j + y >= 2))//slight optimasation 
                                {
                                    return false;
                                }
                                goto Next;

                                
                            }
                        }
                    }

                Next:;
                }
                
            }




            return false;
        }

        public Player ReturnOpponent(string name)
        {
            if (player1.GetName() == name)
            {
                return player2;
            }
            return player1;
        }
    }
}
