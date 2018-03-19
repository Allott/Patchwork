using System;
using System.Collections.Generic;
using System.Text;

namespace Patchwork
{
    public class Player
    {
        int buttons = 5;//check this
        int buttonGain = 0;
        int timeLeft = 52;//check this
        string name;
        Grid playergrid = new Grid();
        public virtual string Turn(Game current)
        {
            String returnString = "pass";
            return returnString;
        }

        public Player(string nam)
        {
            name = nam;
        }
        public virtual string SpecialPatch(Game current)
        {
            String returnString = "";

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!this.GetGrid().tryPatch(i,j))
                    {
                        returnString = i + " " + j; //+
                    }
                }
            }

            return returnString;
        }

        public void Reset()
        {
            buttons = 5;//check this
            buttonGain = 0;
            timeLeft = 52;

            playergrid.reset();
        }

        public int GetTime()
        {
            return timeLeft;
        }
        public void SetTime(int newTime)
        {
            timeLeft = newTime;
        }

        public int GetButtons()
        {
            return buttons;
        }

        public void AddButtons(int addButtons)
        {
            buttons += addButtons;
        }
        public void SetButtons(int newButtons)
        {
            buttons = newButtons;
        }

        public int GetButtonGain()
        {
            return buttonGain;
        }
        public void SetButtonGain(int newButtonGain)
        {
            buttonGain = newButtonGain;
        }

        public Grid GetGrid()
        {
            return playergrid;
        }

        public string GetName()
        {
            return name;
        }
    }
}
