using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patchwork;


namespace Testing
{
    [TestClass]
    public class PlayerTesting
    {
        [TestMethod]
        public void GetName_Valid_CreatesAndReturns()
        {
            string name = "Dan";
            Player t = new Player(name);

            string returnname = t.GetName();

            Assert.AreEqual(name, returnname);

        }

        [TestMethod]
        public void GetTime_Valid_CreatesSetsAndReturns()
        {
            string name = "Dan";
            int time = 25;
            Player t = new Player(name);
            t.SetTime(time);

            int returntime = t.GetTime();

            Assert.AreEqual(time, returntime);

        }

        [TestMethod]
        public void SetButtons_Valid_CreatesSetsAndReturns()
        {
            string name = "Dan";
            int buttons = 25;
            Player t = new Player(name);
            t.SetButtons(buttons);

            int returnbuttons = t.GetButtons();

            Assert.AreEqual(buttons, returnbuttons);

        }

        [TestMethod]
        public void AddButtons_Valid_CreatesSetsAndReturns()
        {
            string name = "Dan";
            int buttons = 25;
            int addbuttons = 5;
            int total = buttons + addbuttons;
            Player t = new Player(name);
            t.SetButtons(buttons);
            t.AddButtons(addbuttons);

            int returnbuttons = t.GetButtons();

            Assert.AreEqual(total, returnbuttons);

        }

        public void SetButtonsgain_Valid_CreatesSetsAndReturns()
        {
            string name = "Dan";
            int buttons = 25;
            Player t = new Player(name);
            t.SetButtonGain(buttons);

            int returnbuttons = t.GetButtonGain();

            Assert.AreEqual(buttons, returnbuttons);

        }
    }
}
