using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patchwork;

namespace Testing
{
    [TestClass]
    public class GridTesting
    {
        [TestMethod]
        public void AddTile_Valid_Create()
        {
            int id = 1;
            int cost = 2;
            int time = 3;
            int buttons = 5;
            bool[,] shape = {
                { false, false, false, false, false },
                { true, true, true, true, false },
                { true, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }};
            Tile t = new Tile(id, cost, time, buttons, shape);
            Grid g = new Grid();

            try
            {
                g.addTile(0, 0, t);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AddTile_Invalid_Create()
        {
            int id = 1;
            int cost = 2;
            int time = 3;
            int buttons = 5;
            bool[,] shape = {
                { false, false, false, false, false },
                { true, true, true, true, false },
                { true, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }};
            Tile t = new Tile(id, cost, time, buttons, shape);
            Grid g = new Grid();

            try
            {
                g.addTile(0, 0, t);
                g.addTile(0, 0, t);
                Assert.Fail();
            }
            catch
            {

            }
        }
        [TestMethod]
        public void TryTile_Invalid_Create()
        {
            int id = 1;
            int cost = 2;
            int time = 3;
            int buttons = 5;
            bool[,] shape = {
                { false, false, false, false, false },
                { true, true, true, true, false },
                { true, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }};
            Tile t = new Tile(id, cost, time, buttons, shape);
            Grid g = new Grid();


            g.addTile(0, 0, t);
            Assert.IsFalse(g.tryTile(0, 0, t));
        }

        [TestMethod]
        public void TryTile_valid_Create()
        {
            int id = 1;
            int cost = 2;
            int time = 3;
            int buttons = 5;
            bool[,] shape = {
                { false, false, false, false, false },
                { true, true, true, true, false },
                { true, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }};
            Tile t = new Tile(id, cost, time, buttons, shape);
            Grid g = new Grid();

            Assert.IsTrue(g.tryTile(0, 0, t));
        }

        [TestMethod]
        public void Addpatch_Valid_Create()
        {
            Grid g = new Grid();

            try
            {
                g.addPatch(0, 0);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Addpatch_Invalid_Create()
        {
            Grid g = new Grid();

            try
            {
                g.addPatch(0, 0);
                g.addPatch(0, 0);
                Assert.Fail();
            }
            catch
            {

            }
        }
        [TestMethod]
        public void Trypatch_Invalid_Create()
        {
            Grid g = new Grid();
            g.addPatch(0, 0);
            Assert.IsTrue(g.tryPatch(0, 0));
        }

        [TestMethod]
        public void Trypatch_Valid_Create()
        {
            Grid g = new Grid();
            Assert.IsFalse(g.tryPatch(0, 0));
        }

        [TestMethod]

        public void Score_Valid_CreateAndReturns()
        {
            int id = 1;
            int cost = 2;
            int time = 3;
            int buttons = 5;
            bool[,] shape = {
                { false, false, false, false, false },
                { true, true, true, true, false },
                { true, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }};
            Tile t = new Tile(id, cost, time, buttons, shape);
            Grid g = new Grid();


            g.addTile(0, 0, t);
            Assert.AreEqual(-152, g.returnScore());

        }
    }
}
