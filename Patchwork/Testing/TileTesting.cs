using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patchwork;

namespace Testing
{
    [TestClass]
    public class TileTesting
    {

        //arrange act assert
        [TestMethod]
        public void ConstructorFour_ValidData_CreatesAndReturns()
        {
            int id = 1;
            int cost = 2;
            int time = 3;
            int buttons = 5;
            Tile t = new Tile(id, cost, time, buttons);

            int returnid = t.ReturnID();
            int returncost = t.ReturnCost();
            int returntime = t.ReturnTime();
            int returnbuttons = t.ReturnButtons();

            Assert.AreEqual(id, returnid, 0, "ConstructorFour_ValidData_CreatesAndReturns1");
            Assert.AreEqual(cost, returncost, 0, "ConstructorFour_ValidData_CreatesAndReturns2");
            Assert.AreEqual(time, returntime, 0, "ConstructorFour_ValidData_CreatesAndReturns3");
            Assert.AreEqual(buttons, returnbuttons, 0, "ConstructorFour_ValidData_CreatesAndReturns4");
        }

        [TestMethod]
        public void ConstructorFour_ValidData_CreatesCopysAndReturns()
        {
            int id = 1;
            int cost = 2;
            int time = 3;
            int buttons = 5;
            Tile t = new Tile(id, cost, time, buttons);
            Tile l = new Tile(t);

            int returnid = l.ReturnID();
            int returncost = l.ReturnCost();
            int returntime = l.ReturnTime();
            int returnbuttons = l.ReturnButtons();

            Assert.AreEqual(id, returnid, 0, "ConstructorFour_ValidData_CreatesCopysAndReturns1");
            Assert.AreEqual(cost, returncost, 0, "ConstructorFour_ValidData_CreatesCopysAndReturns2");
            Assert.AreEqual(time, returntime, 0, "ConstructorFour_ValidData_CreatesCopysAndReturns3");
            Assert.AreEqual(buttons, returnbuttons, 0, "ConstructorFour_ValidData_CreatesCopysAndReturns4");
        }

        [TestMethod]
        public void ConstructorFive_ValidData_CreatesAndRetuns()
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
            bool[,] returnshape = t.returnshape();

            Assert.AreEqual(shape, returnshape);
        }

        [TestMethod]
        public void ConstructorFive_ValidData_CreatesCopysAndReturns()
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
            Tile l = new Tile(t);
            bool[,] returnshape = l.returnshape();

            Assert.AreEqual(shape, returnshape);
        }

        [TestMethod]
        public void ChangeShape_ValidData_CreatesAndRetuns()
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

            bool[,] shape2 = {
                { false, false, false, false, false },
                { true, true, true, false, false },
                { true, false, false, false, false },
                { true, false, false, false, false },
                { false, false, false, false, false }};

            Tile t = new Tile(id, cost, time, buttons, shape);
            t.changeShape(shape2);
            bool[,] returnshape = t.returnshape();

            Assert.AreEqual(shape2, returnshape);
        }

        [TestMethod]
        public void RotateOne_ValidData_CreatesAndRetuns()
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

            bool[,] shape2 = {
                { false, false, false, false, false },
                { false, true, false, false, false },
                { false, true, false, false, false },
                { false, true, false, false, false },
                { false, true, true, false, false }};

            Tile t = new Tile(id, cost, time, buttons, shape);
            t.rotate(1);
            bool[,] returnshape = t.returnshape();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.AreEqual(shape2[i, j], returnshape[i, j]);
                }
            }
        }

        [TestMethod]
        public void RotateTwo_ValidData_CreatesAndRetuns()
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

            bool[,] shape2 = {
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, true },
                { false, true, true, true, true },
                { false, false, false, false, false }};

            Tile t = new Tile(id, cost, time, buttons, shape);
            t.rotate(2);
            bool[,] returnshape = t.returnshape();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.AreEqual(shape2[i, j], returnshape[i, j]);
                }
            }
        }

            [TestMethod]
        public void RotateThree_ValidData_CreatesAndRetuns()
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

            bool[,] shape2 = {
                { false, false, true, true, false },
                { false, false, false, true, false },
                { false, false, false, true, false },
                { false, false, false, true, false },
                { false, false, false, false, false }};

            Tile t = new Tile(id, cost, time, buttons, shape);
            t.rotate(3);
            bool[,] returnshape = t.returnshape();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.AreEqual(shape2[i, j], returnshape[i, j]);
                }
            }
        }

        [TestMethod]
        public void RotateZero_ValidData_CreatesAndRetuns()
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

            bool[,] shape2 = {
                { false, false, false, false, false },
                { true, true, true, true, false },
                { true, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }};

            Tile t = new Tile(id, cost, time, buttons, shape);
            t.rotate(0);
            bool[,] returnshape = t.returnshape();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.AreEqual(shape2[i, j], returnshape[i, j]);
                }
            }
        }

        [TestMethod]
        public void Flip_ValidData_CreatesAndRetuns()
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

            bool[,] shape2 = {
                { false, false, false, false, false },
                { false, true, true, true, true },
                { false, false, false, false, true },
                { false, false, false, false, false },
                { false, false, false, false, false }};

            Tile t = new Tile(id, cost, time, buttons, shape);
            t.flip();
            bool[,] returnshape = t.returnshape();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.AreEqual(shape2[i, j], returnshape[i, j]);
                }
            }
        }

    }
}
